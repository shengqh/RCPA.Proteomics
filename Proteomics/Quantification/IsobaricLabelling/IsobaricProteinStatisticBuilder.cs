using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.R;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public class IsobaricProteinStatisticBuilder : AbstractThreadProcessor
  {
    private IsobaricProteinStatisticBuilderOptions options;

    public IsobaricProteinStatisticBuilder(IsobaricProteinStatisticBuilderOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var design = new IsobaricLabelingExperimentalDesign();
      design.LoadFromFile(options.ExpermentalDesignFile);

      string resultFileName = GetResultFilePrefix(options.ProteinFileName, design.GetReferenceNames(""));

      string paramFileName = Path.ChangeExtension(resultFileName, ".param");
      options.SaveToFile(paramFileName);

      Progress.SetMessage("Reading proteins...");

      IIdentifiedResult ir = new MascotResultTextFormat().ReadFromFile(options.ProteinFileName);

      var proteinpeptidefile = string.Format("{0}.pro_pep.tsv", resultFileName);
      using (var sw = new StreamWriter(proteinpeptidefile))
      {
        sw.WriteLine("Index\tPeptide\tProteins\tDescription\tPepCount\tUniquePepCount");
        foreach (var g in ir)
        {
          var peps = g.GetPeptides();
          var seqs = (from p in peps
                      select p.Peptide.PureSequence).Distinct().OrderBy(m => m).ToArray();
          var proname = (from p in g select p.Name).Merge(" ! ");
          var description = (from p in g select p.Description).Merge(" ! ");
          foreach (var seq in seqs)
          {
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
              g.Index,
              seq,
              proname,
              description,
              g[0].PeptideCount,
              g[0].UniquePeptideCount);
          }
        }
      }

      Progress.SetMessage("Quantifing proteins...");

      var qoptions = new RTemplateProcessorOptions();
      qoptions.InputFile = options.QuanPeptideFileName;
      qoptions.OutputFile = resultFileName + ".quan." + options.PeptideToProteinMethod + ".tsv";

      qoptions.RTemplate = string.Format("{0}/ProteinQuantification.r", FileUtils.GetTemplateDir(), options.PeptideToProteinMethod);
      qoptions.Parameters.Add(string.Format("proteinfile<-\"{0}\"", proteinpeptidefile.Replace("\\", "/")));
      qoptions.Parameters.Add(string.Format("method<-\"{0}\"", options.PeptideToProteinMethod));
      qoptions.Parameters.Add("pvalue<-0.01");
      qoptions.Parameters.Add("minFinalCount<-3");

      new RTemplateProcessor(qoptions).Process();

      Progress.SetMessage("Finished.");

      return new[] { qoptions.OutputFile };
    }

    protected virtual string GetResultFilePrefix(string fileName, string refNames)
    {
      return fileName + "." + refNames;
    }
  }
}
