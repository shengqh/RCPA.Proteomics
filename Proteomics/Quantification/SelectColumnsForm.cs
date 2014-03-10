using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification
{
  public partial class SelectColumnsForm : Form
  {
    public SelectColumnsForm()
    {
      InitializeComponent();
    }

    public void InitializeProteins(List<string> allItems, List<string> checkedItems, List<string> oldItems)
    {
      cbProteins.Initialize(allItems, checkedItems, oldItems);
    }

    public void InitializePeptides(List<string> allItems, List<string> checkedItems, List<string> oldItems)
    {
      cbPeptides.Initialize(allItems, checkedItems, oldItems);
    }

    public List<Tuple<string,bool>> GetProteinColumns()
    {
      return cbProteins.GetItems();
    }

    public List<string> GetCheckedProteinColumns()
    {
      return cbProteins.GetCheckedItems();
    }

    public List<Tuple<string, bool>> GetPeptideColumns()
    {
      return cbPeptides.GetItems();
    }

    public List<string> GetCheckedPeptideColumns()
    {
      return cbPeptides.GetCheckedItems();
    }

    public int ScoreDecimal
    {
      get
      {
        return decimalScore.Value;
      }
      set
      {
        decimalScore.Value = value;
      }
    }

    public int DiffDecimal
    {
      get
      {
        return decimalDiff.Value;
      }
      set
      {
        decimalDiff.Value = value;
      }
    }
  }
}
