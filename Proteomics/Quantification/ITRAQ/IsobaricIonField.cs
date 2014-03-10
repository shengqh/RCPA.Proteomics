using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;

namespace RCPA.Proteomics.Quantification.ITraq
{
  public partial class IsobaricIonField : UserControl, IRcpaComponent
  {
    private RcpaComponentList componentList = new RcpaComponentList();

    private Dictionary<int, CheckBox> indexMap = new Dictionary<int, CheckBox>();

    public IsobaricIonField()
    {
      InitializeComponent();

      Required = true;

      var plexTypes = EnumUtils.EnumToArray<IsobaricType>();
      var lst = (from ptype in plexTypes
                 let definition = ptype.GetDefinition()
                 from item in definition.Items
                 select item.Index).Distinct().ToList();
      lst.Sort();

      foreach (var index in lst)
      {
        var cb = new CheckBox();
        cb.Parent = this;
        this.Controls.Add(cb);
        cb.Text = index.ToString();
        cb.Name = "cb" + cb.Text;
        cb.AutoSize = true;
        cb.Dock = DockStyle.Left;
        cb.BringToFront();
        cb.TextAlign = ContentAlignment.MiddleLeft;
        AddCheckBox(cb, index);
      }
    }

    [Localizable(true)]
    [Category("ITraqIons"), DescriptionAttribute("Gets or sets the isobaric ion checked")]
    public string SelectedIons
    {
      get
      {
        return (from entry in indexMap
                where entry.Value.Checked
                orderby entry.Key
                select entry.Key.ToString()).Merge(',');
      }
      set
      {
        if (value == null)
        {
          value = string.Empty;
        }

        value = value.Trim();

        if (value.Length == 0)
        {
          foreach (RcpaCheckBox cb in componentList.Keys)
          {
            cb.Checked = false;
          }
          return;
        }

        var parts = value.Split(',', ';', ' ', '\t');
        if (value.Length != 0)
        {
          int index = 0;
          foreach (var part in parts)
          {
            if (!int.TryParse(part, out index))
            {
              MessageBox.Show("Error format of selected isobaric ions, input as \"113,114\"");
              return;
            }

            if (!indexMap.ContainsKey(index))
            {
              MessageBox.Show(string.Format("No mass index of {0}", index));
              return;
            }
          }

          foreach (RcpaCheckBox cb in componentList.Keys)
          {
            cb.Checked = false;
          }

          foreach (var part in parts)
          {
            indexMap[int.Parse(part)].Checked = true;
          }
        }
      }
    }

    [Localizable(true)]
    [Category("ITraqIons"), DescriptionAttribute("Gets or sets required"), DefaultValue(true)]
    public bool Required { get; set; }

    private void AddCheckBox(CheckBox cb, int index)
    {
      componentList[new RcpaCheckBox(cb, "I" + cb.Text, cb.Checked)] = true;
      indexMap[index] = cb;
    }

    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    public event EventHandler IonCheckedChanged;

    private void DoCheckedChanged(object sender, EventArgs e)
    {
      if (IonCheckedChanged != null)
      {
        IonCheckedChanged(sender, e);
      }
    }

    #region IRcpaComponent Members

    public void ValidateComponent()
    {
      if (Required && GetFuncs().Count == 0)
      {
        throw new Exception("Must select ion types!");
      }
    }

    #endregion

    #region IOptionFile Members

    public void RemoveFromXml(System.Xml.Linq.XElement option)
    {
      componentList.RemoveFromXml(option);
    }

    public void LoadFromXml(System.Xml.Linq.XElement option)
    {
      componentList.LoadFromXml(option);
    }

    public void SaveToXml(System.Xml.Linq.XElement option)
    {
      componentList.SaveToXml(option);
    }

    #endregion

    public List<IsobaricIndex> GetFuncs()
    {
      var result = new List<IsobaricIndex>();

      foreach (var index in indexMap.Keys)
      {
        var cb = indexMap[index];
        if (cb.Checked)
        {
          result.Add(new IsobaricIndex(index));
        }
      }

      return result;
    }
  }
}
