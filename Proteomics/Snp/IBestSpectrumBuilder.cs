using System.Collections.Generic;

namespace RCPA.Proteomics.Snp
{
  public interface IBestSpectrumBuilder
  {
    MS3Item Build(List<MS3Item> pkls);
  }
}
