using RCPA.Gui.Command;
using RCPA.Proteomics.PFind;
using RCPA.Proteomics.Summary.Uniform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCPA.Tools.Summary
{
  public partial class pFindSummaryBuilder2UI : UniformBuildSummaryUI
  {
    public new static readonly string title = "BuildSummary - pFind Summary Builder";

    public pFindSummaryBuilder2UI()
    {
      InitializeComponent();
      
      pnlAdd.Visible = false;
      pnlAdd.Width = 0;

      SetOneEngineMode();

      DoAddDatasetOption(new PFindDatasetOptions());

      this.Text = Constants.GetSQHTitle(title, UniformBuildSummaryUI.version);
    }

    #region Nested type: Command

    public new class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Summary;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return UniformBuildSummaryUI.version;
      }

      public void Run()
      {
        new pFindSummaryBuilder2UI().MyShow();
      }

      #endregion
    }

    #endregion
  }
}
