using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Format
{
  public partial class ProteinProphetToSummaryProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "ProteinProphet To BuildSummary";
    private static string version = "1.0.0";

    private RcpaDoubleField minProbability;

    public ProteinProphetToSummaryProcessorUI()
    {
      InitializeComponent();

      this.SetFileArgument("SourceFile", new OpenFileArgument("ProteinProphet Xml","xml"));
      this.minProbability = new RcpaDoubleField (txtMinProbability,"MinProbability","Min Probability",0.9,true);

      AddComponent(this.minProbability);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ProteinProphetToSummaryProcessor(minProbability.Value);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Format;
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
        new ProteinProphetToSummaryProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
