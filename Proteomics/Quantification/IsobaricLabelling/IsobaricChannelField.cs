using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public partial class IsobaricChannelField : UserControl, IRcpaComponent
  {
    private RcpaComponentList componentList = new RcpaComponentList();

    private List<CheckBox> channelList = new List<CheckBox>();

    public IsobaricChannelField()
    {
      InitializeComponent();
      Required = true;
    }

    public string Description
    {
      get
      {
        return label1.Text;
      }
      set
      {
        label1.Text = value;
      }
    }

    private IsobaricType _plexType;

    public IsobaricType PlexType
    {
      get
      {
        return _plexType;
      }
      set
      {
        if (_plexType != value)
        {
          //remove old controls
          ClearPreviousIsobaricType();

          foreach (var channel in value.Channels)
          {
            var cb = new CheckBox();
            cb.Parent = this;
            this.Controls.Add(cb);
            cb.Text = channel.Name;
            cb.Name = "cb" + cb.Text;
            cb.AutoSize = true;
            cb.Dock = DockStyle.Left;
            cb.BringToFront();
            cb.TextAlign = ContentAlignment.MiddleLeft;
            cb.Checked = Checked;

            componentList[new RcpaCheckBox(cb, cb.Text, cb.Checked)] = true;
            channelList.Add(cb);
          }

          _plexType = value;
        }
      }
    }

    private void ClearPreviousIsobaricType()
    {
      var lst = new List<Control>();
      foreach (Control comp in this.Controls)
      {
        if (comp is CheckBox)
        {
          lst.Add(comp);
        }
      }
      lst.ForEach(comp => this.Controls.Remove(comp));
      componentList.Clear();
      channelList.Clear();
    }

    [Localizable(true)]
    [Category("IsobaraicIons"), DescriptionAttribute("Gets or sets the isobaric ion checked")]
    public string SelectedIons
    {
      get
      {
        return (from entry in channelList
                where entry.Checked
                select entry.Name).Merge(',');
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
          foreach (var part in parts)
          {
            if (!channelList.Any(m => m.Name.Equals(part)))
            {
              MessageBox.Show(string.Format("No isobaric channel of {0}", part));
              return;
            }
          }

          foreach (RcpaCheckBox cb in componentList.Keys)
          {
            cb.Checked = false;
          }

          foreach (var part in parts)
          {
            channelList.Find(m => m.Name.Equals(part)).Checked = true;
          }
        }
      }
    }

    [Localizable(true)]
    [Category("IsobaricIons"), DescriptionAttribute("Gets or sets required"), DefaultValue(true)]
    public bool Checked { get; set; }

    [Localizable(true)]
    [Category("IsobaricIons"), DescriptionAttribute("Gets or sets required"), DefaultValue(true)]
    public bool Required { get; set; }

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
        throw new Exception("At least one isobaric channel need to be selected!");
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

      foreach (var channel in channelList)
      {
        if (channel.Checked)
        {
          result.Add(new IsobaricIndex(channel.Text, _plexType.Channels.FindIndex(l => l.Name.Equals(channel.Text))));
        }
      }

      return result;
    }
  }
}
