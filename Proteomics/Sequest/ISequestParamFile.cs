using System;

namespace RCPA.Proteomics.Sequest
{
  public interface ISequestParamFile
  {
    SequestParam ReadFromFile(String paramFilename);
  }
}