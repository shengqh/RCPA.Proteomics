using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using System.IO;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class ExportCompoundForm : ComponentUI
  {
    private RcpaCheckBox exportDecoy;
    private RcpaCheckBox exportEnabled;
    private RcpaCheckBox exportInvalidCompoundAsNA;

    public ExportCompoundForm()
    {
      InitializeComponent();
      FirstProductIonColumn = gvFiles.Columns.Count;

      exportDecoy = new RcpaCheckBox(cbViewDecoy, "ViewDecoy", false);
      AddComponent(exportDecoy);
      exportEnabled = new RcpaCheckBox(cbViewValidOnly, "ViewVaidOnly", false);
      AddComponent(exportEnabled);
      exportInvalidCompoundAsNA = new RcpaCheckBox(cbInvalidCompoundAsNA, "ViewInvalidCompoundAsNA", true);
      AddComponent(exportInvalidCompoundAsNA);

      LoadOption();
    }

    protected int FirstProductIonColumn { get; set; }

    private List<CompoundItem> allItems = null;

    private List<CompoundItem> currentItems = null;

    private List<string> filenames = null;

    public void InitializeByFileItems(List<CompoundItem> items)
    {
      filenames = (from f in items
                   from fi in f.FileItems
                   where fi != null
                   from fii in fi.Items
                   where fii != null
                   select fii.PairedResult.PureFileName).Distinct().ToList();
      filenames.Sort();

      filenames.ForEach(m =>
      {
        gvFiles.Columns.Add(new DataGridViewTextBoxColumn()
        {
          HeaderText = m + "_Ratio",
          Width = 100
        });
        gvFiles.Columns.Add(new DataGridViewTextBoxColumn()
        {
          HeaderText = m + "_SD",
          Width = 100
        });
        gvFiles.Columns.Add(new DataGridViewTextBoxColumn()
        {
          HeaderText = m + "_ValidTransCount",
          Width = 100
        });
      });

      allItems = items;

      RefreshCompound();
    }

    private void RefreshCompound()
    {
      if (allItems != null)
      {
        gvFiles.DataSource = null;
        currentItems = allItems.ToList();

        CompoundItemFilter filter = GetInvalidFilter();

        currentItems.RemoveAll(m => filter(m));

        gvFiles.DataSource = currentItems;
      }
    }

    private CompoundItemFilter GetInvalidFilter()
    {
      SrmViewOption option = new SrmViewOption()
      {
        ViewDecoy = cbViewDecoy.Checked,
        ViewValidOnly = cbViewValidOnly.Checked
      };

      return option.GetRejectCompoundFilter();
    }

    private void gvCompounds_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
      if (e.RowIndex >= currentItems.Count)
      {
        e.Value = string.Empty;
        return;
      }

      var compound = currentItems[e.RowIndex];
      if (e.ColumnIndex < FirstProductIonColumn)
      {
        if (e.ColumnIndex == 0)
        {
          e.Value = compound.ObjectName;
        }
        else if (e.ColumnIndex == 1)
        {
          e.Value = compound.PrecursurFormula;
        }
        else if (e.ColumnIndex == 2)
        {
          e.Value = compound.LightMz;
        }
        else if (e.ColumnIndex == 3)
        {
          e.Value = compound.HeavyMz;
        }
        else if (e.ColumnIndex == 4)
        {
          e.Value = compound.Enabled;
        }
        else if (e.ColumnIndex == 5)
        {
          e.Value = compound.IsDecoy;
        }
      }
      else
      {
        var itemindex = (int)(Math.Truncate((e.ColumnIndex - FirstProductIonColumn) / 3.0));
        var colindex = (e.ColumnIndex - FirstProductIonColumn) % 3;
        var fileitem =
          (from f in compound.FileItems
           where f != null && f.Items[itemindex] != null
           select f.Items[itemindex]).FirstOrDefault();

        if (fileitem == null)
        {
          e.Value = string.Empty;
        }
        else
        {
          var pep = fileitem.PairedPeptide;
          if (IsPeptideInvalid(pep))
          {
            if (colindex == 0)
            {
              e.Value = "#N/A";
            }
            else
            {
              e.Value = "0";
            }
          }
          else if (colindex == 0)
          {
            e.Value = MyConvert.Format("{0:0.0000}", pep.Ratio);
          }
          else if (colindex == 1)
          {
            if (double.IsNaN(pep.SD) || double.IsInfinity(pep.SD))
            {
              e.Value = "0";
            }
            else
            {
              e.Value = MyConvert.Format("{0:0.0000}", pep.SD);
            }
          }
          else
          {
            e.Value = pep.ProductIonPairs.Count(m => m.Enabled);
          }
        }
      }
    }

    private bool IsPeptideInvalid(SrmPairedPeptideItem pep)
    {
      return pep == null || pep.Ratio == -1 || (exportInvalidCompoundAsNA.Checked && !pep.Enabled);
    }

    private void cbViewDecoy_CheckedChanged(object sender, EventArgs e)
    {
      RefreshCompound();
    }

    private void cbViewValidOnly_CheckedChanged(object sender, EventArgs e)
    {
      RefreshCompound();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (dlgSave.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        ExportCompound(dlgSave.FileName);
      }
    }

    private void ExportCompound(string targetFile)
    {
      try
      {
        using (StreamWriter sw = new StreamWriter(targetFile))
        {
          sw.Write("ObjectName,CompoundFormula,LightMz,HeavyMz,Enabled,IsDecoy");
          filenames.ForEach(m => sw.Write(",{0}_Ratio,{0}_SD,{0}_ValidTransCount", m));
          sw.WriteLine();

          foreach (var item in currentItems)
          {
            sw.Write("{0},{1},{2:0.0000},{3:0.0000},{4},{5}", item.ObjectName, item.PrecursurFormula, item.LightMz, item.HeavyMz, item.Enabled, item.IsDecoy);
            foreach (var file in filenames)
            {
              var pep = (from t in item.TransitionItems
                         from m in t.FileItems
                         where m.PairedResult.PureFileName.Equals(file)
                         select m.PairedPeptide).FirstOrDefault();
              if (IsPeptideInvalid(pep))
              {
                sw.Write(",#N/A,0,0");
              }
              else
              {
                var transCount = pep.ProductIonPairs.Count(m => m.Enabled);
                if (double.IsNaN(pep.SD) || double.IsInfinity(pep.SD))
                {
                  sw.Write(",{0:0.0000},0,{1}", pep.Ratio, transCount);
                }
                else
                {
                  sw.Write(",{0:0.0000},{1:0.0000},{2}", pep.Ratio, pep.SD, transCount);
                }
              }
            }
            sw.WriteLine();
          }
        }
        MessageBox.Show("Compound quantification result has been saved to " + targetFile);
        Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Save compound quantification result failed : " + ex.Message);
      }
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
