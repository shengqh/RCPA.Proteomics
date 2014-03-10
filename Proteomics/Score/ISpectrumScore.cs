using RCPA.Proteomics.Spectrum;
using System.Collections.Generic;
namespace RCPA.Proteomics.Score
{
  public interface ISpectrumScore
  {
    double Calculate<T>(List<T> pkl) where T : IPeak;
  }
}