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
using RCPA;

namespace TICDistiller
{
  public partial class TICDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Total Ion Count Distiller";

    public static readonly string version = "1.0.0";

    public TICDistillerUI()
    {
      InitializeComponent();

      SetDirectoryArgument("RawDirectory", "Thermo Raw");

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new TICDirectoryDistiller();
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
        new TICDistillerUI().MyShow();
      }

      #endregion
    }
  }
}

