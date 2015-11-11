using RCPA.Gui.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCPA.Proteomics.Database
{
  public class ReversedDatabaseBuilderCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Database;
    }

    public string GetCaption()
    {
      return ReversedDatabaseBuilderUI.title;
    }

    public string GetVersion()
    {
      return ReversedDatabaseBuilderUI.version;
    }

    public void Run()
    {
      new ReversedDatabaseBuilderUI().MyShow();
    }

    #endregion
  }
}
