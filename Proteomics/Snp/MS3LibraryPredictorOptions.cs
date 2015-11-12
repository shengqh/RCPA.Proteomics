using RCPA.Commandline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Snp
{
  public class MS3LibraryPredictorOptions : AbstractOptions
  {
    public MS3LibraryPredictorOptions()
    {
      MinimumMS3PrecursorMz = 200;
      MS2PrecursorPPMTolerance = 20;
      MS3PrecursorPPMTolerance = 20;
      MS3FragmentIonPPMTolerance = 50;
      MaximumFragmentPeakCount = 10;
      MinimumMatchedMS3SpectrumCount = 1;
      MinimumMatchedMS3IonCount = 2;
      MinimumAminoacidSubstitutionDeltaMass = 1.5;
      AllowTerminalLoss = false;
      AllowTerminalExtension = false;
      MatchMS3First = false;
    }

    public double MinimumMS3PrecursorMz { get; set; }

    public double MS2PrecursorPPMTolerance { get; set; }

    public double MS3PrecursorPPMTolerance { get; set; }

    public double MS3FragmentIonPPMTolerance { get; set; }

    public int MaximumFragmentPeakCount { get; set; }

    public int MinimumMatchedMS3SpectrumCount { get; set; }

    public int MinimumMatchedMS3IonCount { get; set; }

    public bool IgnoreDeamidatedMutation { get; set; }

    public bool IsSingleNucleotideMutationOnly { get; set; }

    public bool AllowTerminalLoss { get; set; }

    public bool AllowTerminalExtension { get; set; }

    public bool MatchMS3First { get; set; }

    public double MinimumAminoacidSubstitutionDeltaMass { get; set; }

    public IList<string> RawFiles { get; set; }

    public string LibraryFile { get; set; }

    public string PeptidesFile { get; set; }

    public string DatabaseFastaFile { get; set; }

    public string OutputFile { get; set; }

    public string OutputTableFile { get { return this.OutputFile + ".tsv"; } }

    public string MatchedFile { get; set; }

    public string TargetFastaFile { get; set; }

    public AbstractMS3LibraryPredictor GetPredictor()
    {
      return new MS3LibraryMS3FirstPredictor(this);
    }

    private List<TargetVariant> _extensionDeltaMassList = null;

    public List<TargetVariant> ExtensionDeltaMassList
    {
      get
      {
        if (_extensionDeltaMassList == null)
        {
          _extensionDeltaMassList = GetExtensionDeltaMass();
        }
        return _extensionDeltaMassList;
      }
      set
      {
        if (value == null)
        {
          _extensionDeltaMassList = GetExtensionDeltaMass();
        }
        else
        {
          _extensionDeltaMassList = value;
        }
      }
    }

    private List<TargetVariant> GetExtensionDeltaMass()
    {
      var result = new List<TargetVariant>();

      var aa = new Aminoacids();
      var validAA = (from a in aa.GetVisibleAminoacids() where a != 'I' select a).ToArray();
      foreach (var ai in validAA)
      {
        result.Add(new TargetVariant()
        {
          Source = string.Empty,
          Target = new HashSet<string>(new[] { ai.ToString() }),
          DeltaMass = aa[ai].MonoMass
        });

        foreach (var aj in validAA)
        {
          result.Add(new TargetVariant()
          {
            Source = string.Empty,
            Target = new HashSet<string>(new[] { ai.ToString() + aj.ToString() }),
            DeltaMass = aa[ai].MonoMass + aa[aj].MonoMass
          });

          foreach (var ak in validAA)
          {
            result.Add(new TargetVariant()
            {
              Source = string.Empty,
              Target = new HashSet<string>(new[] { ai.ToString() + aj.ToString() + ak.ToString() }),
              DeltaMass = aa[ai].MonoMass + aa[aj].MonoMass + aa[ak].MonoMass
            });
          }
        }
      }

      var grp = result.GroupBy(m => m.DeltaMass).ToList().ConvertAll(l => l.ToList());
      result.Clear();

      foreach (var g in grp)
      {
        var tv = g.First();
        for (int i = 1; i < g.Count; i++)
        {
          tv.Target.UnionWith(g[i].Target);
        }
        result.Add(tv);
      }

      return result;
    }

    private Dictionary<char, List<TargetVariant>> _substitutionDeltaMassMap = null;

    public Dictionary<char, List<TargetVariant>> SubstitutionDeltaMassMap
    {
      get
      {
        if (_substitutionDeltaMassMap == null)
        {
          _substitutionDeltaMassMap = GetSubstitutionDeltaMass();
        }
        return _substitutionDeltaMassMap;
      }
      set
      {
        if (value == null)
        {
          _substitutionDeltaMassMap = GetSubstitutionDeltaMass();
        }
        else
        {
          _substitutionDeltaMassMap = value;
        }
      }
    }

    private Dictionary<char, List<TargetVariant>> GetSubstitutionDeltaMass()
    {
      var result = new Dictionary<char, List<TargetVariant>>();

      var aa = new Aminoacids();
      var validAA = aa.GetVisibleAminoacids();
      foreach (var ai in validAA)
      {
        foreach (var aj in validAA)
        {
          if (ai == aj)
          {
            continue;
          }

          if (this.IsSingleNucleotideMutationOnly && !MutationUtils.IsSingleNucleotideMutation(ai, aj))
          {
            continue;
          }

          if (this.IgnoreDeamidatedMutation && MutationUtils.IsDeamidatedMutation(ai, aj))
          {
            continue;
          }

          if (!result.ContainsKey(ai))
          {
            result[ai] = new List<TargetVariant>();
          }

          var deltaMass = aa[aj].MonoMass - aa[ai].MonoMass;
          if (Math.Abs(deltaMass) < MinimumAminoacidSubstitutionDeltaMass)
          {
            continue;
          }

          result[ai].Add(new TargetVariant()
          {
            Source = ai.ToString(),
            Target = new HashSet<string>(new[] { aj.ToString() }),
            DeltaMass = deltaMass,
            TargetType = VariantType.SingleAminoacidPolymorphism
          });
        }
      }

      foreach (var v in result.Values)
      {
        v.Sort((m1, m2) => m1.DeltaMass.CompareTo(m2.DeltaMass));
      }
      return result;
    }

    public override bool PrepareOptions()
    {
      if (!File.Exists(LibraryFile))
      {
        ParsingErrors.Add(string.Format("Library file not exists : {0}", LibraryFile));
      }

      foreach (var rawFile in RawFiles)
      {
        if (!File.Exists(rawFile))
        {
          ParsingErrors.Add(string.Format("File not exists : {0}", rawFile));
        }
      }

      if (!string.IsNullOrWhiteSpace(PeptidesFile) && !File.Exists(PeptidesFile))
      {
        ParsingErrors.Add(string.Format("Excluding peptides file not exists : {0}", PeptidesFile));
      }

      return ParsingErrors.Count == 0;
    }
  }
}
