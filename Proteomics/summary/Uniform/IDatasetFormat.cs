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

    bool Enabled { get; set; }

    void LoadFromDataset();

    void SaveToDataset();

    void ValidateComponents();
  }
}
