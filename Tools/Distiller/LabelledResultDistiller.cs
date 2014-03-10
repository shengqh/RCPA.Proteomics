using System;
using System.Collections.Generic;
using System.Text;
using RCPA;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.Utils;
using RCPA.Proteomics.Modification;
using System.IO;

namespace RCPA.Tools.Distiller
{
  /// <summary>
  /// 根据给定氨基酸以及标记位点，将鉴定结果中全部标记或者全部非标记结果提取出来。
  /// </summary>
  public class LabelledResultDistiller : AbstractThreadFileProcessor
  {
    private string aminoAcids;

    private ILabelValidator validator;

    public LabelledResultDistiller(string aminoAcids, LabelPosition position)
    {
      this.aminoAcids = aminoAcids;

      this.validator = new LabelValidator(aminoAcids, position);
    }

    public override IEnumerable<string> Process(string fileName)
    {
      MascotResultTextFormat format = new MascotResultTextFormat();

      IIdentifiedResult ir = format.ReadFromFile(fileName);

      List<IIdentifiedSpectrum> spectra = ir.GetSpectra();

      spectra.ForEach(m =>
      {
        for (int i = m.Peptides.Count - 1; i >= 0; i--)
        {
          IIdentifiedPeptide peptide = m.Peptides[i];

          string seq = PeptideUtils.GetMatchedSequence(peptide.Sequence);

          if (!validator.Validate(seq))
          {
            m.RemovePeptideAt(i);
            peptide.Spectrum = null;
          }
        }
      });

      ir.Filter(m =>
      {
        return m.Spectrum != null;
      });

      string result = fileName + ".Labeled";

      format.WriteToFile(result, ir);

      return new[] { result };
    }
  }
}
