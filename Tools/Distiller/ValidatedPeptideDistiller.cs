using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RCPA.Proteomics.Summary;
using RCPA.Proteomics.Sequest;
using MathNet.Numerics;
using RCPA.Utils;

namespace RCPA.Tools.Distiller
{
  public class ValidatedPeptideDistiller : AbstractThreadFileProcessor
  {
    private string imageDirectory;

    public ValidatedPeptideDistiller(string imageDirectory)
    {
      this.imageDirectory = imageDirectory;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      SequestPeptideTextFormat format = new SequestPeptideTextFormat();

      string[] files = Directory.GetFiles(imageDirectory, "*.jpg");

      var dtaFiles = new HashSet<string>(files.ToList().ConvertAll(m => FileUtils.ChangeExtension(new FileInfo(m).Name, "").ToLower()));

      List<IIdentifiedSpectrum> spectra = format.ReadFromFile(fileName);
      var seqs = new HashSet<string>(
        from spectrum in spectra
        where dtaFiles.Contains(spectrum.Query.FileScan.LongFileName.ToLower())
        select spectrum.Peptide.Sequence);

      var validated =
        from spectrum in spectra
        where seqs.Contains(spectrum.Peptide.Sequence)
        select spectrum;

      string resultFilename = fileName + ".validated";

      format.WriteToFile(resultFilename, validated.ToList());

      return new[] { resultFilename };
    }
  }
}
