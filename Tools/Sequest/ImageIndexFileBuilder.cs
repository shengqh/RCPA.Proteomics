using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.IO;

namespace RCPA.Tools.Sequest
{
  public class ImageIndexFileBuilder : AbstractThreadFileProcessor
  {
    private string imageDirectory;

    private string relativeDirectory;

    public ImageIndexFileBuilder() { }

    public ImageIndexFileBuilder(string imageDirectory, string relativeDirectory)
    {
      this.imageDirectory = imageDirectory;
      this.relativeDirectory = relativeDirectory;
    }

    public override IEnumerable<string> Process(string peptideFilename)
    {
      SequestPeptideTextFormat format = new SequestPeptideTextFormat();
      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(peptideFilename);

      string indexFilename = FileUtils.ChangeExtension(peptideFilename, ".images.html");

      Dictionary<string, List<IIdentifiedSpectrum>> peptideMap = new Dictionary<string, List<IIdentifiedSpectrum>>();
      foreach (IIdentifiedSpectrum spectrum in spectra)
      {
        string pureSeq = spectrum.Peptide.PureSequence;
        if (!peptideMap.ContainsKey(pureSeq))
        {
          peptideMap[pureSeq] = new List<IIdentifiedSpectrum>();
        }
        peptideMap[pureSeq].Add(spectrum);
      }

      List<string> pureSeqs = new List<string>(peptideMap.Keys);
      pureSeqs.Sort();

      using (StreamWriter sw = new StreamWriter(indexFilename))
      {
        sw.WriteLine("<html>");
        foreach (string pureSeq in pureSeqs)
        {
          List<IIdentifiedSpectrum> curSpectra = peptideMap[pureSeq];

          string seqFilename = imageDirectory + pureSeq + ".html";
          using (StreamWriter swSeq = new StreamWriter(seqFilename))
          {
            swSeq.WriteLine("<html>");
            foreach (IIdentifiedSpectrum spectrum in curSpectra)
            {
              spectrum.Query.FileScan.Extension = "jpg";
              string imageFilename = spectrum.Query.FileScan.LongFileName;

              swSeq.WriteLine(format.PeptideFormat.GetString(spectrum) + "<br>");
              swSeq.WriteLine("<img src=\"{0}\"><br>", imageFilename);
            }
            swSeq.WriteLine("</html>");
          }

          sw.WriteLine("<a href=\"{0}\"  target=\"_blank\">{1}</a><br>", relativeDirectory + pureSeq + ".html", pureSeq);
        }
        sw.WriteLine("</html>");
      }

      return new[] { indexFilename };
    }
  }
}
