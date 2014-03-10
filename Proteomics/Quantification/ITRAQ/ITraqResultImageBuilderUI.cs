using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Raw;
using RCPA.Utils;

namespace RCPA.Tools.Quantification
{
  public partial class ITraqResultImageBuilderUI : AbstractFileProcessorUI
  {
    private static readonly string title = "iTRAQ Result Image Builder";

    private static readonly string version = "1.0.0";

    private RcpaFileField rLocation;

    public ITraqResultImageBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("ITraqFile", new OpenFileArgument("ITraq", "itraq"));

      this.rLocation = new RcpaFileField(btnRLocation, txtRLocation, "RLocation", new OpenFileArgument("R execute", "exe"), false);
      this.AddComponent(this.rLocation);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void ShowReturnInfo(IEnumerable<string> returnInfo)
    {
      base.ShowReturnInfo(returnInfo);

      string picFileName = returnInfo.First();

      picBox.Load(picFileName);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new ITraqResultImageBuilder(rLocation.FullName);
    }

    public class Command : IToolSecondLevelCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Quantification;
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
        new ITraqResultImageBuilderUI().MyShow();
      }

      #endregion

      #region IToolSecondLevelCommand Members

      public string GetSecondLevelCommandItem()
      {
        return "ITRAQ";
      }

      #endregion
    }
  }
}
