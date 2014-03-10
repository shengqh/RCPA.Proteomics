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

namespace RCPA.Tools.Modification
{
  public partial class IdentifiedResultModificationSeparatorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Separate Identification Result Based on Modification";

    public static readonly string version = "1.0.0";

    private RcpaTextField modifiedAminoacids;

    public IdentifiedResultModificationSeparatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("ProteinFile", new OpenFileArgument("Identified Protein", "noredundant"));

      this.modifiedAminoacids = new RcpaTextField(txtModifiedAminoacids, "ModifiedAminoacids", "Modified Aminoacids", "STY", true);

      this.AddComponent(this.modifiedAminoacids);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new IdentifiedResultModificationSeparator(modifiedAminoacids.Text);
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
        new IdentifiedResultModificationSeparatorUI().MyShow();
      }

      #endregion
    }
  }
}
