using RCPA.Format;
using RCPA.Gui.Command;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RCPA.Proteomics.Quantification.Srm
{
  public partial class SrmTransitionDefinitionForm : Form
  {
    private List<FileDefinitionItem> headers;

    private TextFileDefinition items;

    private List<string> propertyNames;

    public SrmTransitionDefinitionForm()
    {
      InitializeComponent();

      items = new TextFileDefinition();

      headers = new List<FileDefinitionItem>();

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "ObjectName",
        ValueType = "string",
        Required = false
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "PrecursorFormula",
        ValueType = "string",
        Required = true
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "PrecursorMZ",
        ValueType = "double",
        Format = "{0:0.0000}",
        Required = true
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "PrecursorCharge",
        ValueType = "integer",
        Required = false
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "ProductIon",
        ValueType = "double",
        Format = "{0:0.0000}",
        Required = true
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "ExpectCenterRetentionTime",
        ValueType = "double",
        Format = "{0:0.00}",
        Required = false
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "IsHeavy",
        ValueType = "boolean",
        Required = false
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "IsLight",
        ValueType = "boolean",
        Required = false
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "IonType",
        ValueType = "string",
        Required = false
      });

      headers.Add(new FileDefinitionItem()
      {
        PropertyName = "IonIndex",
        ValueType = "integer",
        Required = false
      });

      propertyNames = (from h in headers
                       select h.PropertyName).ToList();
      propertyNames.Insert(0, string.Empty);

      colPropertyName.Items.AddRange(propertyNames.ToArray());
    }

    private void btnInit_Click(object sender, EventArgs e)
    {
      if (dlgOpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        NewFromData(dlgOpenFile.FileName);
      }
    }

    public void NewFromData(string fileName)
    {
      using (StreamReader sr = new StreamReader(fileName))
      {
        var line = sr.ReadLine();
        if (line == null || line.Trim() == string.Empty)
        {
          MessageBox.Show(this, "No header in file " + fileName, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
          return;
        }

        gvItems.DataSource = null;

        string[] parts;
        char delimiter;
        if (line.Contains('\t'))
        {
          delimiter = '\t';
        }
        else
        {
          delimiter = ',';
        }
        parts = line.Split(delimiter);

        items.Clear();
        items.Delimiter = delimiter;
        items.Description = Path.GetFileNameWithoutExtension(fileName);
        foreach (var part in parts)
        {
          items.Add(new FileDefinitionItem()
          {
            AnnotationName = part
          });
        }

        UpdateDataSource();

        lastFile = string.Empty;
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return "Setup";
      }

      public string GetCaption()
      {
        return "Srm Transition Format";
      }

      public string GetVersion()
      {
        return "1.0.1";
      }

      public void Run()
      {
        new SrmTransitionDefinitionForm().ShowDialog();
      }

      #endregion
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }

    private string lastFile;
    private void btnLoad_Click(object sender, EventArgs e)
    {
      dlgOpenFormatFile.InitialDirectory = FileUtils.GetTemplateDir().FullName;

      if (dlgOpenFormatFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        var fileName = dlgOpenFormatFile.FileName;

        ReadFormatFile(fileName);
      }
    }

    public void ReadFormatFile(string fileName)
    {
      gvItems.DataSource = null;
      items.ReadFromFile(fileName);

      UpdateDataSource();

      lastFile = fileName;
    }

    private void UpdateDataSource()
    {
      gvItems.DataSource = items;

      txtDelimiter.Text = items.DelimiterString;
      txtDescription.Text = items.Description;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < items.Count; i++)
      {
        if (!string.IsNullOrEmpty(items[i].PropertyName))
        {
          if (!propertyNames.Contains(items[i].PropertyName))
          {
            MessageBox.Show("Property " + items[i].PropertyName + " is illegal!");
            return;
          }

          for (int j = i + 1; j < items.Count; j++)
          {
            if (items[i].PropertyName.Equals(items[j].PropertyName))
            {
              MessageBox.Show("Property " + items[i].PropertyName + " are multi-defined!");
              return;
            }
          }
        }
      }

      dlgSaveFormatFile.InitialDirectory = FileUtils.GetTemplateDir().FullName;

      if (File.Exists(lastFile) && !File.Exists(dlgSaveFormatFile.FileName))
      {
        dlgSaveFormatFile.FileName = lastFile;
      }

      if (dlgSaveFormatFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        var map = headers.ToDictionary(m => m.PropertyName);
        items.ForEach(m =>
        {
          if (!string.IsNullOrEmpty(m.PropertyName))
          {
            var item = map[m.PropertyName];
            m.ValueType = item.ValueType;
            m.Required = item.Required;
            m.Format = item.Format;
          }
        });

        items.DelimiterString = txtDelimiter.Text;
        items.Description = txtDescription.Text;

        items.WriteToFile(dlgSaveFormatFile.FileName);

        SrmFormatFactory.Refresh();
      }
    }
  }
}
