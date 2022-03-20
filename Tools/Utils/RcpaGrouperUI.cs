using MathNet.Numerics.Random;
using RCPA.Gui;
using RCPA.Gui.Command;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RCPA.Tools.Utils
{
  public partial class RcpaGrouperUI : AbstractUI
  {
    public static string title = "Rcpa Grouper";
    public static string version = "1.0.0";

    private RcpaIntegerField group;

    public RcpaGrouperUI()
    {
      InitializeComponent();
      this.Text = Constants.GetSQHTitle(title, version);

      group = new RcpaIntegerField(txtGroupCount, "group", "Group Count", 4, true);
      AddComponent(group);
      AddButton(btnCopy);

      string namesFile = new FileInfo(Application.ExecutablePath).DirectoryName + "\\names.txt";
      if (File.Exists(namesFile))
      {
        var names = File.ReadAllLines(namesFile);
        txtNames.Lines = names;
      }
      else
      {
        File.WriteAllLines(namesFile, txtNames.Lines);
      }
    }

    private Dictionary<int, List<string>> groups;
    protected override void DoRealGo()
    {
      int groupCount = group.Value;
      List<string> names = txtNames.Lines.ToList();

      double maxCount = ((double)names.Count) / groupCount;
      if (Math.Ceiling(maxCount) != maxCount)
      {
        maxCount = Math.Ceiling(maxCount) + 1;
      }

      groups = new Dictionary<int, List<string>>();
      for (int i = 0; i < groupCount; i++)
      {
        groups[i] = new List<string>();
      }

      List<string> shuffledNames = new List<string>();

      var random = new MersenneTwister();
      while (names.Count > 0)
      {
        int index = random.Next(names.Count);
        shuffledNames.Add(names[index]);
        names.RemoveAt(index);
      }

      int groupIndex = 0;
      while (shuffledNames.Count > 0)
      {
        groups[groupIndex].Add(shuffledNames[0]);
        shuffledNames.RemoveAt(0);
        groupIndex++;
        if (groupIndex >= groupCount)
        {
          groupIndex = 0;
        }
      }

      tvNames.Nodes.Clear();
      foreach (var key in groups)
      {
        var node = tvNames.Nodes.Add("Group_" + key.Key.ToString());
        foreach (var name in key.Value)
        {
          node.Nodes.Add(name);
        }
      }
      tvNames.ExpandAll();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Help;
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
        new RcpaGrouperUI().MyShow();
      }

      #endregion
    }

    private void btnCopy_Click(object sender, EventArgs e)
    {
      if (groups == null)
      {
        return;
      }

      StringBuilder sb = new StringBuilder();
      foreach (var key in groups)
      {
        sb.AppendLine("Group_" + key.Key.ToString());
        foreach (var name in key.Value)
        {
          sb.AppendLine("    " + name);
        }
      }

      Clipboard.SetText(sb.ToString());
    }
  }
}

