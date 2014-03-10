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
using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;
using RCPA.Utils;
using RCPA.Tools.Summary;

namespace RCPA.Tools.Modification
{
  public partial class IdentifiedResultMultipleLabellingSeparatorUI : AbstractMultipleLabellingSeparatorUI
  {
    public static readonly string title = "Separate Identification Result Based on Multiple Labelling";

    public static readonly string version = "1.0.1";

    public IdentifiedResultMultipleLabellingSeparatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("ProteinFile", new OpenFileArgument("Identified Protein", "noredundant"));
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedResultSeparatorBySpectrumFilter(GetFilterMap());
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Modification;
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
        new IdentifiedResultMultipleLabellingSeparatorUI().MyShow();
      }

      #endregion
    }
  }
}
