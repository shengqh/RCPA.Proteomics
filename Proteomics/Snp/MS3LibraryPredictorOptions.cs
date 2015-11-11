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

    public string LibraryFile { get; set; }

    public IList<string> RawFiles { get; set; }

    public string DatabaseFastaFile { get; set; }

    public string OutputFile { get; set; }

    public string OutputTableFile { get { return this.OutputFile + ".tsv"; } }

    public string MatchedFile { get; set; }

    public string TargetFastaFile { get; set; }

    public AbstractMS3LibraryPredictor GetPredictor()
    {
      return new MS3LibraryMS3FirstPredictor(this);
    }

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
            result[ai] = new List<TargetSAP>();
          }

          var deltaMass = aa[aj].MonoMass - aa[ai].MonoMass;
          if (Math.Abs(deltaMass) < MinimumAminoacidSubstitutionDeltaMass)
          {
            continue;
          }

          result[ai].Add(new TargetSAP() { Source = ai.ToString(), Target = aj.ToString(), DeltaMass = deltaMass });
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

      return ParsingErrors.Count == 0;
    }
  }
}
