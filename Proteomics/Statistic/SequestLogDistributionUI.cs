using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using System.IO;
using RCPA.Proteomics.Sequest;
using RCPA.Gui.Image;
using ZedGraph;

namespace RCPA.Proteomics.Statistic
{
  public partial class SequestLogDistributionUI : Form
  {
    public SequestLogDistributionUI()
    {
      InitializeComponent();
    }

    private void btnAddSubDirectories_Click(object sender, EventArgs e)
    {
      using (var form = new InputDirectoryForm("Input folder", "Folder", ""))
      {
        if (form.ShowDialog(this) == DialogResult.OK)
        {
          var files = new DirectoryInfo(form.Value).GetFiles("*.log");
          foreach (var file in files)
          {
            if (!lvFiles.Items.ContainsKey(file.FullName))
            {
              lvFiles.Items.Add(file.FullName);
            }
          }
        }
      }
    }

    private void lvDirectories_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (lvFiles.SelectedItems.Count > 0)
      {
        string log = lvFiles.SelectedItems[0].Text;

        SequestLogFormat reader = new SequestLogFormat();
        var counts = reader.ReadFromFile(log);

        ZedGraphicExtension.ClearData(zgc, false);

        GraphPane myPane = zgc.GraphPane;
        // Set the titles and axis labels
        myPane.Title.Text = "Sequest Node Distribution";
        myPane.XAxis.Title.Text = "Node";
        myPane.YAxis.Title.Text = "Count";

        var str =
          (from c in counts
           orderby c.Key
           select c.Key).ToArray();

        var y =
          (from c in counts
           orderby c.Key
           select (double)c.Value).ToArray();

        BarItem myCurve = myPane.AddBar("NodeCount", null, y, Color.Blue);

        // Draw the X tics between the labels instead of at the labels
        myPane.XAxis.MajorTic.IsBetweenLabels = true;

        // Set the XAxis labels
        myPane.XAxis.Scale.TextLabels = str;

        // Set the XAxis to Text type
        myPane.XAxis.Type = AxisType.Text;

        myPane.Legend.IsVisible = false;

        ZedGraphicExtension.UpdateGraph(zgc);
      }
    }
  }
}
