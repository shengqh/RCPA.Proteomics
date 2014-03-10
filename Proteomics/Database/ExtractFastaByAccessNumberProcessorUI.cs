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
using RCPA.Seq;

namespace RCPA.Proteomics.Database
{
  public partial class ExtractFastaByAccessNumberProcessorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Extract Fasta By Access Number";

    public static readonly string version = "1.0.1";

    private RcpaCheckBox replaceName;

    private RcpaFileField fastaFile;

    private RcpaComboBox<IAccessNumberParser> parsers;

    public ExtractFastaByAccessNumberProcessorUI()
    {
      InitializeComponent();

      SetFileArgument("AccessNumberFile", new OpenFileArgument("Access Number", "txt"));

      fastaFile = new RcpaFileField(btnFastaFile, textBox1, "FastaFile", new OpenFileArgument("Database", "fasta"), true);
      AddComponent(fastaFile);

      replaceName = new RcpaCheckBox(cbReplaceName, "ReplaceName", false);
      AddComponent(replaceName);

      parsers = new RcpaComboBox<IAccessNumberParser>(cbAccessNumberParser, "AccessNumberParser", AccessNumberParserFactory.GetParsers().ToArray(), 0);
      AddComponent(parsers);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ExtractFastaByAccessNumberProcessor(parsers.SelectedItem, fastaFile.FullName, replaceName.Checked);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Database;
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
        new ExtractFastaByAccessNumberProcessorUI().MyShow();
      }

      #endregion
    }
  }
}

