using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using MathNet.Numerics.Statistics;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public abstract class AbstractITraqPeptideStatisticBuilder : AbstractThreadFileProcessor
  {
    private string rawFileName;

    protected MascotPeptideTextFormat format = new MascotPeptideTextFormat ();

    public AbstractITraqPeptideStatisticBuilder(string rawFileName)
    {
      this.rawFileName = rawFileName;
    }

    protected List<IIdentifiedSpectrum> GetSpectra(string fileName)
    {
      Progress.SetMessage("Reading peptides ...");
      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(fileName);

      Progress.SetMessage("Reading itraq ...");
      IsobaricResult itraq = ITraqResultFileFormatFactory.GetXmlFormat().ReadFromFile(rawFileName);

      Progress.SetMessage("Matching peptide and itraq ...");

      ITraqItemUtils.MatchPeptideWithItraq(itraq, spectra);

      return spectra;
    }
  }
}
