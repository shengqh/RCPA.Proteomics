using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary.Uniform
{
  public interface IDatasetFormat
  {
    string DatasetName { get; set; }

    bool DatasetEnabled { get; set; }

    void LoadFromDataset();

    void SaveToDataset(bool selectedOnly);

    void ValidateComponents();

    bool HasValidFile(bool selectedOnly);
  }
}
