using System;
using System.IO;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using RCPA.Utils;

namespace RCPA.Proteomics.Distiller
{
  public partial class ExtractProteinOnlyProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extract Protein Only";

    public static readonly string version = "1.0.0";

    public ExtractProteinOnlyProcessorUI()
    {
      InitializeComponent();

      SetFileArgument("NoredundantFile", new OpenFileArgument("Noredundant", "noredundant"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ExtractProteinOnlyProcessor();
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
        new ExtractProteinOnlyProcessorUI().MyShow();
      }

      #endregion
    }
  }
}

