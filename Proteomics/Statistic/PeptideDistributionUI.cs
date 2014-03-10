using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using RCPA.Gui.Command;
using RCPA.Proteomics.Distribution;

namespace RCPA.Proteomics.Statistic
{
  public partial class PeptideDistributionUI : AbstractDistributionUI
  {
    private static readonly string title = "Peptide Distribution Builder";

    private static readonly string version = "1.1.0";

    public PeptideDistributionUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override DistributionType GetDistributionType()
    {
      return DistributionType.Peptide;
    }

    protected override OpenFileArgument GetSourceFileArgument()
    {
      return new OpenFileArgument("peptides", "peptides");
    }

    protected override IFileProcessor GetFileProcessor()
    {
      DistributionOption option = GetDistributionOption();
      if (option.ModifiedPeptideOnly)
      {
        return new ModifiedPeptideDistributionCalculator(option.ModifiedPeptide, true);
      }
      else
      {
        return new PeptideDistributionCalculator(true);
      }
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Statistic;
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
        new PeptideDistributionUI().MyShow();
      }

      #endregion
    }
  }

}
