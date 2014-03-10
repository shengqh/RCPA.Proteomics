using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IDatasetFactory
  {
    string Name { get; }

    IDatasetOptions CreateOptions();
  }
}
