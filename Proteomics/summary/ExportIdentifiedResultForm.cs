using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCPA.Proteomics.Summary
{
  public partial class ExportIdentifiedResultForm : Form
  {
    public ExportIdentifiedResultForm()
    {
      InitializeComponent();
    }

    private List<object> GetObjectList<T>(IEnumerable<T> src)
    {
      return src.ToList().ConvertAll(m => (object)m).ToList();
    }

    public IEnumerable<string> ProteinColumns
    {
      get
      {
        return proteinColumns.Items.ConvertAll(m => m.ToString());
      }
      set
      {
        proteinColumns.Items = GetObjectList(value);
      }
    }

    public IEnumerable<string> PeptideColumns
    {
      get
      {
        return peptideColumns.Items.ConvertAll(m => m.ToString());
      }
      set
      {
        peptideColumns.Items = GetObjectList(value);
      }
    }

    public IEnumerable<string> SelectedProteinColumns
    {
      get
      {
        return proteinColumns.SelectedItems.ConvertAll(m => m.ToString());
      }
      set
      {
        proteinColumns.SelectedItems = GetObjectList(value);
      }
    }

    public IEnumerable<string> SelectedPeptideColumns
    {
      get
      {
        return peptideColumns.SelectedItems.ConvertAll(m => m.ToString());
      }
      set
      {
        peptideColumns.SelectedItems = GetObjectList(value);
      }
    }

    public IEnumerable<string> CheckedProteinColumns
    {
      get
      {
        return proteinColumns.CheckedItems.ConvertAll(m => m.ToString());
      }
      set
      {
        proteinColumns.CheckedItems = GetObjectList(value);
      }
    }

    public IEnumerable<string> CheckedPeptideColumns
    {
      get
      {
        return peptideColumns.CheckedItems.ConvertAll(m => m.ToString());
      }
      set
      {
        peptideColumns.CheckedItems = GetObjectList(value);
      }
    }
  }
}
