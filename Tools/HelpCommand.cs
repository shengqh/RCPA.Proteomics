using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui.Command;

namespace RCPA.Tools
{
  public class HelpCommand:IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Help;
    }

    public string GetCaption()
    {
      return "Homepage";
    }

    public string GetVersion()
    {
      return "";
    }

    public void Run()
    {
      System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo("http://www.proteomics.ac.cn/software/proteomicstools/index.htm");
      System.Diagnostics.Process Pro = System.Diagnostics.Process.Start(Info);
    }

    #endregion
  }
}
