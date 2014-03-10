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

namespace RCPA.Proteomics.Summary.Uniform
{
  public partial class TitleDatasetPanel : DatasetPanelBase
  {
    private ITitleParser[] parsers;

    protected RcpaComboBox<ITitleParser> titleParsers;

    private AbstractTitleDatasetOptions TitleOption { get { return Option as AbstractTitleDatasetOptions; } }

    public TitleDatasetPanel()
    {
      InitializeComponent();

      parsers = TitleParserUtils.GetTitleParsers().ToArray();
      this.titleParsers = new RcpaComboBox<ITitleParser>(this.cbTitleFormat, "TitleFormat", parsers, parsers.Length - 1);
      AddComponent(this.titleParsers);
    }

    public override void LoadFromDataset()
    {
      base.LoadFromDataset();

      SetTitleParserFromDataset();
    }

    private void SetTitleParserFromDataset()
    {
      foreach (var parser in titleParsers.Items)
      {
        if (parser.FormatName.Equals(TitleOption.TitleParserName))
        {
          titleParsers.SelectedItem = parser;
          return;
        }
      }

      foreach (var parser in titleParsers.Items)
      {
        if (parser.FormatName.Equals(DefaultTitleParser.FORMAT_NAME))
        {
          titleParsers.SelectedItem = parser;
          return;
        }
      }
    }

    public override void SaveToDataset()
    {
      base.SaveToDataset();

      TitleOption.TitleParserName = titleParsers.SelectedItem.FormatName;
    }

    protected virtual string GetTitleSample()
    {
      return null;
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
      var sample = GetTitleSample();
      if (!string.IsNullOrEmpty(sample))
      {
        var guess = TitleParserUtils.GuessTitleParser(sample, parsers);
        if (guess != null)
        {
          titleParsers.SelectedItem = guess;
        }
        else
        {
          MessageBox.Show(this, MyConvert.Format("Cannot find corresponding title parser for {0}", guess), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
  }
}
