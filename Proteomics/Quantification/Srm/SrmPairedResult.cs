using System.Collections.Generic;
using System.IO;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmPairedResult : List<SrmPairedPeptideItem>
  {
    public string FileName { get; set; }

    public bool Modified { get; set; }

    public string PureFileName
    {
      get
      {
        if (string.IsNullOrEmpty(FileName))
        {
          return string.Empty;
        }
        return FileUtils.ChangeExtension(new FileInfo(FileName).Name, "");
      }
    }

    public SrmPairedResult()
    {
      Options = new SrmOptions();
      Modified = false;
    }

    public SrmOptions Options { get; set; }

    public void CalculateTransitionRatio()
    {
      this.ForEach(m => m.CalculateTransactionRatio(this.Options));
    }

    public void CalculatePeptideRatio()
    {
      this.ForEach(m => m.CalculatePeptideRatio());
    }

    public void CalculateRatio()
    {
      this.ForEach(m => m.CalculateTransactionRatio(this.Options));
      this.ForEach(m => m.CalculatePeptideRatio());
    }

    public void PeakPicking()
    {
      var peakPicking = Options.GetPeakPickingMethod();

      this.ForEach(m => m.PeakPicking(peakPicking, this.Options));
    }

    public void Update(SrmPairedPeptideItem pep)
    {
      var filter = Options.GetFilter();

      pep.CalculateTransactionRatio(Options);

      pep.ProductIonPairs.ForEach(m => m.Enabled = filter.Accept(m));

      pep.CheckEnabled(Options.OutlierEvalue, Options.MinValidTransitionPair);

      pep.CalculatePeptideRatio();
    }

    public void AssignDecoy()
    {
      if (this.Options.HasDecoy)
      {
        var decoyFilter = this.Options.GetDecoyFilter();
        this.ForEach(m => m.ProductIonPairs.ForEach(n =>
        {
          n.IsDecoy = decoyFilter.Accept(n);
        }));
      }
    }
  }
}
