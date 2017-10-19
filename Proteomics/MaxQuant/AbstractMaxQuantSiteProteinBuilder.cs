using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.MaxQuant
{
  public abstract class AbstractMaxQuantSiteProteinBuilder : AbstractThreadProcessor
  {
    protected MaxQuantSiteProteinBuilderOption _option;
    public AbstractMaxQuantSiteProteinBuilder(MaxQuantSiteProteinBuilderOption option)
    {
      _option = option;
    }

    protected void WriteAtom(StreamWriter sw, Aminoacid samAA, Aminoacid refAA, Atom[] atoms, string atomName, bool outputZero = true)
    {
      foreach (var atom in atoms)
      {
        if (samAA.Composition.ContainsKey(atom) || refAA.Composition.ContainsKey(atom))
        {
          sw.WriteLine("{0}\t{1}\t{2}", atomName, samAA.Composition.ContainsKey(atom) ? samAA.Composition[atom] : 0, refAA.Composition.ContainsKey(atom) ? refAA.Composition[atom] : 0);
          return;
        }
      }

      if (outputZero)
      {
        sw.WriteLine("{0}\t0\t0", atomName);
      }
    }

    private string ReplaceModificationStringToChar(string seq, Dictionary<string, MaxQuantModificationItem> modCharMap)
    {
      foreach (var key in modCharMap.Keys)
      {
        seq = seq.Replace(key, modCharMap[key].Symbol);
      }
      return seq;
    }

    private Regex modReg = new Regex(@"(\(.+?\))");

    private void ParseModification(Annotation msms, Dictionary<string, MaxQuantModificationItem> modCharMap, MaxQuantModificationList mods, out string modification, out string modifiedSequence)
    {
      var seq = msms.Annotations["Modified sequence"].ToString();
      seq = ReplaceModificationStringToChar(seq, modCharMap);
      seq = seq.Replace("_", "");

      Match m = modReg.Match(seq);
      if (m.Success)
      {
        var modMap = (from mo in msms.Annotations["Modifications"].ToString().Split(',')
                      let mof = Regex.Replace(mo, "^\\d+\\s+", "")
                      let shortName = "(" + mof.Substring(0, 2).ToLower() + ")"
                      select new { FullName = mof, ShortName = shortName }).ToDictionary(l => l.ShortName, l => l.FullName);

        while (m.Success)
        {
          string mod = m.Groups[1].Value;
          if (!modCharMap.ContainsKey(mod))
          {
            var fullName = modMap[mod];
            modCharMap[mod] = mods.Find(l => l.FullName.Equals(fullName));
            modCharMap[mod].Symbol = ModificationConsts.MODIFICATION_CHAR.Substring(modCharMap.Count + 1, 1);
          }
          m = m.NextMatch();
        }

        seq = ReplaceModificationStringToChar(seq, modCharMap);
      }

      modifiedSequence = seq;
      modification = msms.Annotations["Modifications"].ToString();
    }

    public override IEnumerable<string> Process()
    {
      var shortModMap = new Dictionary<string, MaxQuantModificationItem>();

      var peptideFile = _option.OutputFilePrefix + ".peptides";
      var noredundantFile = _option.OutputFilePrefix + ".noredundant";
      var iniFile = _option.OutputFilePrefix + ".ini";

      var spectra = ExtractPeptides(shortModMap, peptideFile);

      BuildResult(shortModMap, noredundantFile, iniFile, spectra);

      return new[] { peptideFile, noredundantFile, iniFile };
    }

    public void BuildResult(Dictionary<string, MaxQuantModificationItem> shortModMap, string noredundantFile, string iniFile, List<IIdentifiedSpectrum> spectra)
    {
      var proteins = new IdentifiedProteinBuilder().Build(spectra);
      var groups = new IdentifiedProteinGroupBuilder().Build(proteins);
      var ir = new IdentifiedResultBuilder(DefaultAccessNumberParser.GetInstance(), "").Build(groups);

      new MascotResultTextFormat().WriteToFile(noredundantFile, ir);

      Aminoacids samAminoacids, refAminoacids;

      InitializeAminoacids(out samAminoacids, out refAminoacids);

      using (var sw = new StreamWriter(iniFile))
      {
        for (var idx = 0; idx < samAminoacids.Count; idx++)
        {
          var samAA = samAminoacids[idx];
          var refAA = refAminoacids[idx];
          if (samAA.Visible)
          {
            sw.WriteLine("<{0}>\tSAM\tREF", samAA.OneName);
            WriteAtom(sw, samAA, refAA, new[] { Atom.C }, "C");
            WriteAtom(sw, samAA, refAA, new[] { Atom.H }, "H");
            WriteAtom(sw, samAA, refAA, new[] { Atom.O }, "O");
            WriteAtom(sw, samAA, refAA, new[] { Atom.N }, "N");
            WriteAtom(sw, samAA, refAA, new[] { Atom.S }, "S");
            WriteAtom(sw, samAA, refAA, new[] { Atom.P }, "P");
            WriteAtom(sw, samAA, refAA, new[] { Atom.N15, Atom.Nx }, "15N");
            WriteAtom(sw, samAA, refAA, new[] { Atom.H2, Atom.Hx }, "2H");
            WriteAtom(sw, samAA, refAA, new[] { Atom.C13, Atom.Cx }, "13C");
            WriteAtom(sw, samAA, refAA, new[] { Atom.O18, Atom.Ox }, "18O", false);
            sw.WriteLine();
          }
        }

        var terms = new[] {new MaxQuantModificationItem(){          Symbol = "NTERM",          Composition = new AtomComposition("H")        },
        new MaxQuantModificationItem()        {          Symbol = "CTERM",          Composition = new AtomComposition("OH")        }};
        var usedMods = (from v in shortModMap.Values
                        orderby v.Symbol
                        select v).ToList();
        usedMods.AddRange(terms);

        foreach (var mod in usedMods)
        {
          mod.WriteToSilacINI(sw);
        }

      }
    }

    public List<IIdentifiedSpectrum> ExtractPeptides(Dictionary<string, MaxQuantModificationItem> shortModMap, string peptideFile)
    {
      var mods = MaxQuantModificationList.ReadFromFile(_option.MaxQuantModificationXml);

      var sites = new AnnotationFormat().ReadFromFile(_option.MaxQuantSiteFile);
      var msmsIds = new HashSet<string>(from s in sites
                                        let msmsids = s.Annotations["MS/MS IDs"] as string
                                        let msmsIdList = msmsids.Split(';')
                                        from msmsId in msmsIdList
                                        select msmsId);
      var format = new AnnotationFormat();
      var msmsList = format.ReadFromFile(_option.MaxQuantMSMSFile);
      msmsList.RemoveAll(l => !msmsIds.Contains(l.Annotations["id"].ToString()));

      using (var sw = new StreamWriter(peptideFile))
      {
        sw.WriteLine("FileScan\tSequence\tMH+\tDiff(MH+)\tCharge\tScore\tReference\tModification\tRetentionTime");
        foreach (var msms in msmsList)
        {
          string modification;
          string modifiedSequence;

          ParseModification(msms, shortModMap, mods, out modification, out modifiedSequence);

          var mh = double.Parse(msms.Annotations["Mass"].ToString()) + Atom.H.MonoMass;
          var diffStr = msms.Annotations["Mass Error [ppm]"].ToString();
          var diffmh = diffStr.Equals("NaN") ? 0 : PrecursorUtils.ppm2mz(mh, double.Parse(diffStr));

          sw.WriteLine("{0},{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
            msms.Annotations["Raw file"],
            msms.Annotations["Scan number"],
            modifiedSequence,
            mh,
            diffmh,
            msms.Annotations["Charge"],
            msms.Annotations["Score"],
            msms.Annotations["Proteins"].ToString().Replace(";", "/"),
            modification,
            msms.Annotations["Retention time"]);
        }
      }

      return new MascotPeptideTextFormat().ReadFromFile(peptideFile);
    }

    protected abstract void InitializeAminoacids(out Aminoacids samAminoacids, out Aminoacids refAminoacids);
  }
}
