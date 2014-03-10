using System.Collections.Generic;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.PeptideProphet
{
  public class PepXmlInfo<T, U>
    where T : IIdentifiedSpectrum
    where U : IPeptideProphetItem<T>
  {
    private readonly List<U> peptideProphetItems = new List<U>();
    public string SearchDatabase { get; set; }

    public List<U> PeptideProphetItems
    {
      get { return this.peptideProphetItems; }
    }
  }

  public class SequestPepXmlInfo : PepXmlInfo<IdentifiedSpectrum, SequestPeptideProphetItem>
  {
  }


  public class MascotPepXmlInfo : PepXmlInfo<IdentifiedSpectrum, MascotPeptideProphetItem>
  {
  }
}