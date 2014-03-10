using System;
using System.Collections.Generic;
using System.Text;

namespace RCPA.Proteomics.Summary
{
  public interface IIdentificationFactory
  {
    IIdentifiedSpectrum AllocateSpectrum();

    IIdentifiedProtein AllocateProtein();

    IIdentifiedProteinGroup AllocateProteinGroup();

    IIdentifiedResult AllocateResult();
  }
}
