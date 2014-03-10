using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui.FileArgument;
using RCPA.Gui.Command;
using RCPA.Seq;
using RCPA.Gui;

namespace RCPA.Tools.Database
{
  public partial class FastaDatabaseDistillerUI : AbstractFileProcessorUI
  {
    public static string title = "Fasta Database Distiller";
    public static string version = "1.0.0";

    private RcpaTextField dbName;
    private RcpaTextField dbPattern;

    public FastaDatabaseDistillerUI()
    {
      InitializeComponent();

      SetFileArgument("Database", new OpenFileArgument("Source Database (*.fasta)", "fasta"));

      dbName = new RcpaTextField(txtName, "DbName", "Database Name", "HUMAN", true);
      AddComponent(dbName);

      dbPattern = new RcpaTextField(txtPattern, "DbPattern", "Entry Name Pattern", "_HUMAN", true);
      AddComponent(dbPattern);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new FastaDatabaseDistiller(dbName.Text, dbPattern.Text);
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
        new FastaDatabaseDistillerUI().MyShow();
      }

      #endregion
    }
  }
}
