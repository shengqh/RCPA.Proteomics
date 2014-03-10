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
using RCPA.Proteomics.Modification;

namespace RCPA.Tools.Distiller
{
  public partial class LabelledResultDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Labeled Identification Result Distiller";

    public static readonly string version = "1.0.1";

    private RcpaTextField aminoAcids;

    private RcpaComboBox<LabelPosition> labelPosition;

    public LabelledResultDistillerUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      SetFileArgument("ProteinFile", new OpenFileArgument("Protein", "noredundant"));

      aminoAcids = new RcpaTextField(textBox1, "AminoAcids", "Labelled amino acids", "K", true);
      AddComponent(aminoAcids);

      labelPosition = new RcpaComboBox<LabelPosition>(comboBox1, "Position", new LabelPosition[] { LabelPosition.ALL, LabelPosition.CTERM, LabelPosition.NTERM }, 0);
      AddComponent(labelPosition);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new LabelledResultDistiller(aminoAcids.Text, labelPosition.SelectedItem);
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
        new LabelledResultDistillerUI().MyShow();
      }

      #endregion
    }
  }
}
