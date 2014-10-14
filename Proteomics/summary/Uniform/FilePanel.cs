using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class FilePanel : UserControl, IRcpaComponent
  {
    protected RcpaListViewMultipleFileField datFiles;

    public FilePanel()
    {
      InitializeComponent();
    }

    public void SetCategoryVisible(bool value)
    {
      if (value)
      {
        if (!lvDatFiles.Columns.Contains(chCategory))
        {
          lvDatFiles.Columns.Insert(2, chCategory);
        }
        btnCategory.Visible = true;
      }
      else
      {
        lvDatFiles.Columns.Remove(chCategory);
        btnCategory.Visible = false;
      }
    }

    public ListView FileView { get { return this.lvDatFiles; } }

    public void LoadFromDataset(IDatasetOptions options)
    {
      datFiles.ClearItems();
      datFiles.AddItems(options.PathNames.ToArray());
    }

    public void SaveToDataset(IDatasetOptions options)
    {
      options.PathNames = new List<string>(datFiles.GetAllItems());
    }

    public virtual void SaveDatasetList<T>(BuildSummaryOptions conf) where T : IDatasetOptions, new()
    {
      Dictionary<string, IDatasetOptions> dsmap = new Dictionary<string, IDatasetOptions>();
      foreach (ListViewItem item in this.lvDatFiles.Items)
      {
        if (item.Selected)
        {
          var key = string.Empty;
          if (item.SubItems.Count >= 3)
          {
            key = item.SubItems[2].Text;
          }

          if (!dsmap.ContainsKey(key))
          {
            var dsoptions = new T();
            dsoptions.Name = key;
            dsoptions.Parent = conf;

            dsmap[key] = dsoptions;
            conf.DatasetList.Add(dsoptions);
          }
          dsmap[key].PathNames.Add(item.SubItems[0].Text);
        }
      }
    }

    public virtual void LoadDatasetList<T>(BuildSummaryOptions options) where T : IDatasetOptions
    {
      this.lvDatFiles.Items.Clear();
      foreach (T dataset in options.DatasetList)
      {
        foreach (var file in dataset.PathNames)
        {
          ListViewItem item = this.lvDatFiles.Items.Add(file);
          item.SubItems.Add("");
          item.SubItems.Add(dataset.Name);
          item.Selected = true;
        }
      }
    }

    private void lvDatFiles_SizeChanged(object sender, EventArgs e)
    {
      this.lvDatFiles.Columns[0].Width = this.lvDatFiles.ClientSize.Width - this.lvDatFiles.Columns[1].Width;
    }

    public void ValidateComponent()
    {
      datFiles.ValidateComponent();
    }

    public void RemoveFromXml(System.Xml.Linq.XElement option)
    {
      datFiles.RemoveFromXml(option);
    }

    public void LoadFromXml(System.Xml.Linq.XElement option)
    {
      datFiles.LoadFromXml(option);
    }

    public void SaveToXml(System.Xml.Linq.XElement option)
    {
      datFiles.SaveToXml(option);
    }

    public string[] SelectFileNames
    {
      get
      {
        return datFiles.SelectFileNames;
      }
    }

    public string[] FileNames
    {
      get
      {
        return datFiles.FileNames;
      }
    }

    private void btnCategory_Click(object sender, EventArgs e)
    {
      if (this.lvDatFiles.SelectedIndices.Count == 0)
      {
        MessageBox.Show(this, "Select data files first!");
      }

      string oldClassification = this.lvDatFiles.SelectedItems[0].SubItems.Count >= 3 ? this.lvDatFiles.SelectedItems[0].SubItems[2].Text : "";

      var form = new InputTextForm(null, "Classification", "Input classification", "Classification", oldClassification, false);
      if (form.ShowDialog() == DialogResult.OK)
      {
        foreach (ListViewItem item in this.lvDatFiles.SelectedItems)
        {
          while (item.SubItems.Count < 3)
          {
            item.SubItems.Add("");
          }
          item.SubItems[2].Text = form.Value.Trim();
        }
      }
    }
  }
}
