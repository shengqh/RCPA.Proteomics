using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Utils;
using System;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public class ITraqUniquePeptideStatisticBuilder : AbstractITraqProteinStatisticBuilder
  {
    private string fastaFile;
    private IAccessNumberParser parser;
    public ITraqUniquePeptideStatisticBuilder(ITraqProteinStatisticOption option, bool isSiteLevel, string fastaFile, IAccessNumberParser parser)
      : base(option)
    {
      this.isSiteLevel = isSiteLevel;
      this.fastaFile = fastaFile;
      this.parser = parser;
    }

    private MascotPeptideTextFormat format;
    private bool isSiteLevel;

    protected override IIdentifiedResult GetIdentifiedResult(string fileName)
    {
      format = new MascotPeptideTextFormat();
      var spectra = format.ReadFromFile(fileName);
      IIdentifiedResult result;
      if (isSiteLevel)
      {
        result = IdentifiedSpectrumUtils.BuildGroupByPeptide(spectra);
      }
      else
      {
        result = IdentifiedSpectrumUtils.BuildGroupByUniquePeptide(spectra);
      }

      var map = SequenceUtils.ReadAccessNumberReferenceMap(new FastaFormat(), this.fastaFile, this.parser);

      foreach (var group in result)
      {
        var proteins = group[0].Description.Split('/');
        group[0].Description = (from p in proteins
                                let ac = parser.GetValue(p)
                                select map[ac]).ToList().Merge(" ! ");
      }
      return result;
    }

    protected override string GetResultFileName(string fileName, string refNames)
    {
      return fileName + "." + refNames + ".unique.itraq";
    }

    protected override MascotResultTextFormat GetFormat(IIdentifiedResult ir)
    {
      var result = new MascotResultTextFormat();
      result.PeptideFormat = format.PeptideFormat;
      result.InitializeByResult(ir);
      return result;
    }
  }
}
