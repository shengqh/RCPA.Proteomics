using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;
using RCPA.Proteomics.Summary.Processor;

namespace RCPA.Tools.Distiller
{
  public class SimpleProteinGroupBuilder : AbstractThreadFileProcessor
  {
    public SimpleProteinGroupBuilder()
      : this(new string[] { "SWISS-PROT", "|sp|", "REFSEQ_NP", "|ref|NP" })
    { }

    private CompositeProcessor<IIdentifiedProteinGroup> processor;

    public SimpleProteinGroupBuilder(string[] perferReferenceRegexString)
    {
      processor = new CompositeProcessor<IIdentifiedProteinGroup>();
      processor.Add(new ProteinNameProcessor(perferReferenceRegexString));
      processor.Add(new ProteinLengthProcessor(false));
    }

    public override IEnumerable<string> Process(string fileName)
    {
      MascotResultTextFormat format = new MascotResultTextFormat();

      IIdentifiedResult ir = format.ReadFromFile(fileName);

      foreach (IIdentifiedProteinGroup group in ir)
      {
        processor.Process(group);
      }

      string resultFileName = fileName + ".Unduplicated";

      format.WriteToFile(resultFileName, ir);

      return new[] { resultFileName };
    }
  }
}
