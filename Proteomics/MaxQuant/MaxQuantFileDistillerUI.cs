using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Seq;
using RCPA.Proteomics.Sequest;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantFileDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "MaxQuant File Distiller";

    public static readonly string version = "1.0.2";

    private RcpaFileField targetFile;

    [RcpaOptionAttribute("MatchRoles", RcpaOptionType.IXml)]
    public MaxQuantTagMatchRoles Roles { get; set; }

    private OpenFileArgument rolesOpenDlg = new OpenFileArgument("MaxQuant Tag Roles", "roles");

    private SaveFileArgument rolesSaveDlg = new SaveFileArgument("MaxQuant Tag Roles", "roles");

    public MaxQuantFileDistillerUI()
    {
      InitializeComponent();

      base.SetFileArgument("SourceFile", new OpenFileArgument("Source MaxQuant", "txt"));
      this.targetFile = new RcpaFileField(btnTarget, txtTarget, "TargetFile", new OpenFileArgument("Target MaxQuant", "txt"), true);
      AddComponent(targetFile);

      this.Roles = new MaxQuantTagMatchRoles();

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new MaxQuantFileDistiller(targetFile.FullName, this.Roles);
    }

    protected override void OnAfterLoadOption(EventArgs e)
    {
      base.OnAfterLoadOption(e);

      LoadRoles();
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
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
        new MaxQuantFileDistillerUI().MyShow();
      }

      #endregion
    }

    #endregion

    private void btnLoadSource_Click(object sender, EventArgs e)
    {
      if (File.Exists(originalFile.FullName))
      {
        LoadHeaderTag(originalFile.FullName, lbSource);
      }
    }

    private void LoadHeaderTag(string p, ListBox lbSource)
    {
      try
      {
        using (StreamReader sr = new StreamReader(p))
        {
          string line = sr.ReadLine();
          var parts = line.Split('\t');
          lbSource.BeginUpdate();
          try
          {
            lbSource.Items.Clear();
            lbSource.Items.AddRange(parts);
          }
          finally
          {
            lbSource.EndUpdate();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error when loading file " + p + "\n" + ex.Message);
      }
    }

    private void btnLoadTarget_Click(object sender, EventArgs e)
    {
      if (File.Exists(targetFile.FullName))
      {
        LoadHeaderTag(targetFile.FullName, lbTarget);
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (lbSource.SelectedItem == null || lbTarget.SelectedItem == null)
      {
        return;
      }

      var role = new MaxQuantTagMatchRole();
      role.SourceTag = lbSource.SelectedItem as string;
      role.TargetTag = lbTarget.SelectedItem as string;
      if (cbIsMultiple.Checked)
      {
        txtSplitChar.Text = txtSplitChar.Text.Trim();
        if (txtSplitChar.Text.Length == 0)
        {
          MessageBox.Show("Set split char first");
          return;
        }

        role.SplitChar = txtSplitChar.Text.Trim()[0];
      }
      lbRoles.Items.Add(role);
      Roles.Add(role);
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      if (lbRoles.SelectedItem != null)
      {
        Roles.Remove(lbRoles.SelectedItem as MaxQuantTagMatchRole);
        lbRoles.Items.Remove(lbRoles.SelectedItem);
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      var dlg = rolesSaveDlg.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.Roles.Save(dlg.FileName);
      }
    }

    private void LoadRoles()
    {
      lbRoles.Items.Clear();
      lbRoles.Items.AddRange(this.Roles.ToArray());
    }

    private void btnLoadRoles_Click(object sender, EventArgs e)
    {
      var dlg = rolesOpenDlg.GetFileDialog();
      if (dlg.ShowDialog() == DialogResult.OK)
      {
        this.Roles.Load(dlg.FileName);
        LoadRoles();
      }
    }
  }
}