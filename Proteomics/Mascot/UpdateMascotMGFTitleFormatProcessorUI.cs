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

namespace RCPA.Proteomics.Mascot
{
  public partial class UpdateMascotMGFTitleFormatProcessorUI : AbstractFileProcessorUI
  {
    private static string title = "Update Mascot MGF Title Format Processor";
    private static string version = "1.0.1";

    private RcpaComboBox<ITitleParser> titleParsers;

    public UpdateMascotMGFTitleFormatProcessorUI()
    {
      InitializeComponent();

      this.SetDirectoryArgument("TargetDir", "Target MGF");

      this.datFiles.FileArgument = new OpenFileArgument("Mascot MGF", new string[]{"mgf","msm"});
      AddComponent(new RcpaMultipleFileComponent(this.datFiles.GetItemInfos(),"DatFiles","Mgf Files", false, true));

      this.titleParsers = new RcpaComboBox<ITitleParser>(cbTitleFormat, "TitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titleParsers);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new UpdateMascotMGFTitleFormatProcessor(titleParsers.SelectedItem, datFiles.FileNames);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Mascot;
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
        new UpdateMascotMGFTitleFormatProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
