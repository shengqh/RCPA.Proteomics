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
  public partial class ValidatedPeptideDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Validated Peptide Distiller";

    public static readonly string version = "1.0.0";

    private RcpaDirectoryField imageDir;

    public ValidatedPeptideDistillerUI()
    {
      InitializeComponent();

      base.SetFileArgument("Peptides", new OpenFileArgument("Peptides", "peptides"));

      imageDir = new RcpaDirectoryField(btnImage, txtImageDirectory, "ImageDir", "Spectrum Image", true);
      AddComponent(imageDir);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ValidatedPeptideDistiller(imageDir.FullName);
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
        new ValidatedPeptideDistillerUI().MyShow();
      }

      #endregion
    }
  }
}
