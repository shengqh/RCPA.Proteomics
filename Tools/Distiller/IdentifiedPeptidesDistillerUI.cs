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

namespace RCPA.Tools.Distiller
{
  public partial class IdentifiedPeptidesDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extract Peptides from Protein File";

    public static readonly string version = "1.0.0";

    public IdentifiedPeptidesDistillerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("ProteinFile", new OpenFileArgument("Identified Protein", "noredundant"));
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedPeptidesDistiller();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Distiller;
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
        new IdentifiedPeptidesDistillerUI().MyShow();
      }

      #endregion
    }
  }
}
