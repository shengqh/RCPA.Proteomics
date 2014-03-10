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
using RCPA.Proteomics.Summary;
using RCPA.Gui.Command;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Spectrum;

namespace RCPA.Proteomics.Format
{
  public partial class ReplaceMascotGenericFormatProcessorUI : AbstractFileProcessorUI
  {
    private RcpaComboBox<ITitleParser> titleHeaderParser;

    private RcpaComboBox<ITitleParser> titlePeakParser;

    private RcpaComboBox<MascotTitle> titleFormat;

    private static string info = "This software will generate combined mgf file using the precursor m/z and precursor charge information from header mgf file and peak list information from peak file.";

    private static string title = "Replace Precursor Information Processor";

    private static string version = "1.0.0";

    public ReplaceMascotGenericFormatProcessorUI()
    {
      InitializeComponent();

      base.SetFileArgument("TargetFile", new SaveFileArgument("Target MGF", "mgf"));

      fileHeader.FileArgument = new OpenFileArgument("Precursor Info MGF","mgf");
      this.titleHeaderParser = new RcpaComboBox<ITitleParser>(cbHeaderFormat, "HeaderTitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titleHeaderParser);

      filePeak.FileArgument = new  OpenFileArgument("Peak Info MGF","mgf");
      this.titlePeakParser = new RcpaComboBox<ITitleParser>(cbPeakFormat, "PeakTitleFormat", TitleParserUtils.GetTitleParsers().ToArray(), 0);
      AddComponent(this.titlePeakParser);

      this.titleFormat = new RcpaComboBox<MascotTitle>(cbTitleFormat, "TitleFormat", MascotTitleFactory.Titles, 0);
      AddComponent(this.titleFormat);

      this.Text = Constants.GetSQHTitle(title, version);
      lblInfo.Text = info;
    }

    private CombineMascotGenericFormatOption GetFormatOption()
    {
      var result = new CombineMascotGenericFormatOption();

      result.HeaderFile = fileHeader.FullName;
      result.HeaderParser = titleHeaderParser.SelectedItem;
      result.PeakFile = filePeak.FullName;
      result.PeakParser = titlePeakParser.SelectedItem;
      result.TargetFile = base.GetOriginFile();
      result.Writer = titleFormat.SelectedItem.CreateWriter();

      return result;
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ReplaceMascotGenericFormatProcessor(GetFormatOption());
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
        new ReplaceMascotGenericFormatProcessorUI().MyShow();
      }

      #endregion
    }
  }
}
