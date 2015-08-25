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
      PrecursorPPMTolerance = 20;
      FragmentPPMTolerance = 50;
      MaxFragmentPeakCount = 10;
      MinimumMs3PrecursorMz = 200;
      MinimumMatchedMs3IonCount = 2;
      MinimumDeltaMass = 1.5;
    }

    public double PrecursorPPMTolerance { get; set; }

    public double FragmentPPMTolerance { get; set; }

    public double MinimumDeltaMass { get; set; }

    public int MaxFragmentPeakCount { get; set; }

    public string LibraryFile { get; set; }

    public string LibraryPeptideFile { get; set; }

    public string MatchedFile { get; set; }

    public IList<string> RawFiles { get; set; }

    public string TargetFastaFile { get; set; }

    public string DatabaseFastaFile { get; set; }

    public bool IgnoreDeamidatedMutation { get; set; }

    public bool IgnoreMultipleNucleotideMutation { get; set; }

    public string OutputFile { get; set; }

    private Dictionary<char, List<TargetSAP>> _allowedMassChangeMap = null;

    public Dictionary<char, List<TargetSAP>> AllowedMassChangeMap
    {
      get
      {
        if (_allowedMassChangeMap == null)
        {
          _allowedMassChangeMap = GetAllowedMassChange();
        }
        return _allowedMassChangeMap;
      }
      set
      {
        if (value == null)
        {
          _allowedMassChangeMap = GetAllowedMassChange();
        }
        else
        {
          _allowedMassChangeMap = value;
        }
      }
    }

    private Dictionary<char, List<TargetSAP>> GetAllowedMassChange()
    {
      var result = new Dictionary<char, List<TargetSAP>>();

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

          if (!result.ContainsKey(ai))
          {
            result[ai] = new List<TargetSAP>();
          }

          var deltaMass = aa[aj].MonoMass - aa[ai].MonoMass;
          if (Math.Abs(deltaMass) < MinimumDeltaMass)
          {
            continue;
          }

          result[ai].Add(new TargetSAP() { Source = ai, Target = aj, DeltaMass = deltaMass });
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

      if (!string.IsNullOrEmpty(LibraryPeptideFile) && !File.Exists(LibraryPeptideFile))
      {
        ParsingErrors.Add(string.Format("Library peptide file not exists : {0}", LibraryPeptideFile));
      }


      foreach (var rawFile in RawFiles)
      {
        if (!File.Exists(rawFile))
        {
          ParsingErrors.Add(string.Format("File not exists : {0}", rawFile));
        }
      }

      return ParsingErrors.Count == 0;
    }

    public double MinimumMs3PrecursorMz { get; set; }

    public int MinimumMatchedMs3IonCount { get; set; }
  }
}
