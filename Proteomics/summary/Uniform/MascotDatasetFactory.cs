using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class MascotDatasetFactory : IDatasetFactory
  {
    #region IDatasetFactory Members

    public string Name
    {
      get { return "MASCOT"; }
    }

    public IDatasetOptions CreateOptions()
    {
      return new MascotDatasetOptions();
    }

    #endregion
  }
}
