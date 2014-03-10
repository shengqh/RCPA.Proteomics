using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using System.Xml.Linq;

namespace RCPA.Proteomics
{
  public partial class SiliconePolymerIonField : UserControl, IRcpaComponent
  {
    private List<SiliconePolymer> ionEntries = new List<SiliconePolymer>();

    public static string DefaultSiliconePolymers = "371,355,388,429,445,462,503,519,536,593";

    public SiliconePolymerIonField()
    {
      InitializeComponent();

      Required = true;

      ionEntries.Add(new SiliconePolymer(5, 371.101233));
      ionEntries.Add(new SiliconePolymer(6, 445.120024));
      ionEntries.Add(new SiliconePolymer(7, 519.138815));
      ionEntries.Add(new SiliconePolymer(8, 593.157607));
      ionEntries.Add(new SiliconePolymer(9, 667.176398));
      ionEntries.Add(new SiliconePolymer(10, 741.195190));

      bsPolymers.DataSource = ionEntries;
    }

    [Localizable(true)]
    [Category("SiliconePolymers"), DescriptionAttribute("Gets or sets required"), DefaultValue(true)]
    public bool Required { get; set; }

    [Localizable(true)]
    [Category("SiliconePolymers"), DescriptionAttribute("Gets or sets selected ion")]
    public string SelectedIon
    {
      get
      {
        var result = (from p in GetSelectedPloymers()
                let i = (int)p
                select i.ToString()).Merge(",");
        if (string.IsNullOrEmpty(result))
        {
          SelectedIon = DefaultSiliconePolymers;
          return DefaultSiliconePolymers;
        }
        else
        {
          return result;
        }
      }
      set
      {
        var parts = value.Split(',', ';', ' ', '\t');
        try
        {
          var ions = (from p in parts
                      select (int)double.Parse(p)).ToList();
          ions.ForEach(m => ionEntries.ForEach(n => n.SetChecked(m)));
        }
        catch (Exception)
        {
          MessageBox.Show("Format error! It should like 355,371,445");
        }
      }
    }

    public List<double> GetSelectedPloymers()
    {
      List<double> result = new List<double>();
      foreach (var ion in ionEntries)
      {
        result.AddRange(ion.GetSelectedPloymers());
      }
      return result;
    }

    #region IRcpaComponent Members

    public void ValidateComponent()
    {
      if (Required && ionEntries.All(m => m.GetSelectedPloymers().Count == 0))
      {
        throw new Exception("Must select silicone ploymer!");
      }
    }

    #endregion

    #region IOptionFile Members

    private string key = "SiliconePolymers";

    public void RemoveFromXml(System.Xml.Linq.XElement option)
    {
      var result = option.Descendants(key).ToArray();

      if (result.Count() > 0)
      {
        foreach (var item in result)
        {
          item.Remove();
        }
      }
    }

    public void LoadFromXml(XElement option)
    {
      var result =
        (from item in option.Descendants(key)
         select item).FirstOrDefault();

      if (null == result)
      {
        SelectedIon = DefaultSiliconePolymers;
      }
      else
      {
        SelectedIon = result.Value;
      }
    }

    public void SaveToXml(XElement option)
    {
      option.Add(new XElement(key, SelectedIon));
    }

    #endregion

    private void btnClearAll_Click(object sender, EventArgs e)
    {
      ionEntries.ForEach(m => m.SetCheckedAll(false));
    }

    private void btnSelectAll_Click(object sender, EventArgs e)
    {
      ionEntries.ForEach(m => m.SetCheckedAll(true));
    }

    private void btnDefault_Click(object sender, EventArgs e)
    {
      SelectedIon = DefaultSiliconePolymers;
    }
  }
}
