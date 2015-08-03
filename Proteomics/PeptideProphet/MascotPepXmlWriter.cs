using System;
using System.Collections.Generic;
using System.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.PeptideProphet
{
  public class MascotPepXmlWriter : AbstractPepXmlWriter
  {
    public MascotPepXmlWriter(PepXmlWriterParameters parameters) : base(parameters) { }

    protected override void WriteScore(StreamWriter sw, IIdentifiedSpectrum sph)
    {
      sw.WriteLine("          <search_score name=\"ionscore\" value=\"{0:0.##}\"/>", sph.Score);
      if (sph.Query.MatchCount > 0)
      {
        sw.WriteLine("          <search_score name=\"identityscore\" value=\"{0:0.##}\"/>", 10 * Math.Log10(sph.Query.MatchCount));
      }

      sw.WriteLine("          <search_score name=\"star\" value=\"0\"/>");

      object homologyScore;
      if (sph.Annotations.TryGetValue(MascotDatSpectrumParser.HomologyScoreKey, out homologyScore))
      {
        sw.WriteLine("          <search_score name=\"homologyscore\" value=\"{0:0.##}\"/>", homologyScore);
      }
      sw.WriteLine("          <search_score name=\"expect\" value=\"{0}\"/>", sph.ExpectValue);
    }
  }
}