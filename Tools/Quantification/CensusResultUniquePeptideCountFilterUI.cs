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
using RCPA.Tools.Quantification;

namespace RCPA.Tools.Sequest
{
  public partial class CensusResultUniquePeptideCountFilterUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Get Census Protein Whose Unique Peptide Count Larger Than/Equals X";

    public static readonly string version = "1.0.0";

    private RcpaIntegerField uniqueCount;

    public CensusResultUniquePeptideCountFilterUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("CensusResult", new OpenFileArgument("Census Result", "txt"));

      this.uniqueCount = new RcpaIntegerField(txtUniquePeptide, "UniqueCount", "Minimum Unique Peptide Count", 2, true);

      this.AddComponent(this.uniqueCount);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new CensusResultUniquePeptideCountFilter(uniqueCount.Value);
    }
  }

  public class CensusResultUniquePeptideCountFilterCommand : IToolSecondLevelCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Quantification;
    }

    public string GetCaption()
    {
      return CensusResultUniquePeptideCountFilterUI.title;
    }

    public string GetVersion()
    {
      return CensusResultUniquePeptideCountFilterUI.version;
    }

    public void Run()
    {
      new CensusResultUniquePeptideCountFilterUI().MyShow();
    }

    #endregion

    #region IToolSecondLevelCommand Members

    public string GetSecondLevelCommandItem()
    {
      return "Census";
    }

    #endregion
  }

}
