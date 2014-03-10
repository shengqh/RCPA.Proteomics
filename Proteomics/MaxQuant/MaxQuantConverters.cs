using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantReverseConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Reverse"; }
    }

    public override string GetProperty(T t)
    {
      if (t.FromDecoy)
      {
        return "+";
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      t.FromDecoy = "+".Equals(value);
    }
  }

  public class MaxQuantContaminantConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Contaminant"; }
    }

    public override string GetProperty(T t)
    {
      if (t.IsContaminant)
      {
        return "+";
      }

      return "";
    }

    public override void SetProperty(T t, string value)
    {
      t.IsContaminant = "+".Equals(value);
    }
  }

  public class MaxQuantBestLocalizationRawFileConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Best Localization Raw File"; }
    }

    public override string GetProperty(T t)
    {
      return t.Query.FileScan.Experimental;
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.FileScan.Experimental = value;
    }
  }

  public class MaxQuantBestLocalizationScanNumberConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Best Localization Scan Number"; }
    }

    public override string GetProperty(T t)
    {
      return t.Query.FileScan.FirstScan.ToString();
    }

    public override void SetProperty(T t, string value)
    {
      t.Query.FileScan.FirstScan = Convert.ToInt32(value);
      t.Query.FileScan.LastScan = t.Query.FileScan.FirstScan;
    }
  }

  public class MaxQuantModifiedSequenceConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Modified Sequence"; }
    }

    public override string GetProperty(T t)
    {
      return t.Peptide.Sequence;
    }

    public override void SetProperty(T t, string value)
    {
      if (t.Peptide == null)
      {
        t.AddPeptide(new IdentifiedPeptide(t));
      }
      t.Peptide.Sequence = value;
    }
  }

  public class MaxQuantPureSequenceConverter<T> : AbstractPropertyConverter<T> where T : IIdentifiedSpectrum
  {
    public override string Name
    {
      get { return "Sequence Window"; }
    }

    public override string GetProperty(T t)
    {
      return t.Peptide.PureSequence;
    }

    public override void SetProperty(T t, string value)
    { }
  }
}
