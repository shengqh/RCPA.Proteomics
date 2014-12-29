using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Summary;
using System.IO;

namespace RCPA.Proteomics.Quantification.SILAC
{
  public class SilacQuantificationSummaryOption : AbstractQuantificationSummaryOption
  {
    private SilacGetRatioIntensity func = new SilacGetRatioIntensity();

    private SilacQuantificationSummaryItemXmlFormat format = new SilacQuantificationSummaryItemXmlFormat();

    public SilacQuantificationSummaryOption()
    { }

    public override IProteinRatioCalculator GetProteinRatioCalculator()
    {
      return new SilacProteinRatioCalculator();
    }

    public override object ReadRatioFile(string file)
    {
      return format.ReadFromFile(file);
    }

    public override IQuantificationPeptideForm CreateForm()
    {
      return new SilacQuantificationSummaryItemForm();
    }

    public override IGetRatioIntensity Func
    {
      get { return func; }
    }
  }
}
