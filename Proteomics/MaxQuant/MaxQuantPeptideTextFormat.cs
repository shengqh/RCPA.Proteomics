using RCPA.Converter;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Proteomics.MaxQuant
{
  public class MaxQuantPeptideTextFormat : MascotPeptideTextFormat
  {
    public void ResetBySpectra(List<IIdentifiedSpectrum> items)
    {
      var format = this.PeptideFormat;
      var converters = (format.Converter as CompositePropertyConverter<IIdentifiedSpectrum>).ItemList;

      converters.RemoveAll(m => m is MaxQuantItemListConverter<IIdentifiedSpectrum>);
      var probconv = new MaxQuantItemListRatioConverter<IIdentifiedSpectrum>();
      converters.Add(probconv);
      converters.AddRange(probconv.GetRelativeConverter(items));
    }

    protected override void DoAfterRead(List<IIdentifiedSpectrum> spectra)
    {
      base.DoAfterRead(spectra);


    }
  }
}
