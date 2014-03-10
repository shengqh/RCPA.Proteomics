using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui.FileArgument;
using RCPA.Gui;
using RCPA.Gui.Command;

namespace RCPA.Proteomics.Isotopic
{
  public partial class GenerateIsotopicFileUI : AbstractProcessorFileUI
  {
    public static readonly string title = "Generate Isotopic File";

    public static readonly string version = "1.0.0";

    private RcpaTextField url;
    public GenerateIsotopicFileUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      url = new RcpaTextField(txtUrl, "Url", "Url", "http://physics.nist.gov/cgi-bin/Compositions/stand_alone.pl", true);
      AddComponent(url);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new GenerateIsotopicFile(this.url.Text);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new GenerateIsotopicFileUI().MyShow();
      }

      #endregion
    }
  }
}
