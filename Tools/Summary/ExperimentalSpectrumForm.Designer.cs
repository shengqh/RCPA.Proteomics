namespace RCPA.Tools.Summary
{
  partial class ExperimentalSpectrumForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.zgcExperimentalScans = new ZedGraph.ZedGraphControl();
      this.SuspendLayout();
      // 
      // zgcExperimentalScans
      // 
      this.zgcExperimentalScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcExperimentalScans.Location = new System.Drawing.Point(0, 0);
      this.zgcExperimentalScans.Name = "zgcExperimentalScans";
      this.zgcExperimentalScans.ScrollGrace = 0;
      this.zgcExperimentalScans.ScrollMaxX = 0;
      this.zgcExperimentalScans.ScrollMaxY = 0;
      this.zgcExperimentalScans.ScrollMaxY2 = 0;
      this.zgcExperimentalScans.ScrollMinX = 0;
      this.zgcExperimentalScans.ScrollMinY = 0;
      this.zgcExperimentalScans.ScrollMinY2 = 0;
      this.zgcExperimentalScans.Size = new System.Drawing.Size(501, 266);
      this.zgcExperimentalScans.TabIndex = 15;
      // 
      // ExperimentalSpectrumForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(501, 266);
      this.ControlBox = false;
      this.Controls.Add(this.zgcExperimentalScans);
      this.Name = "ExperimentalSpectrumForm";
      this.Text = "ExperimentalSpectrumForm";
      this.ResumeLayout(false);

    }

    #endregion

    private ZedGraph.ZedGraphControl zgcExperimentalScans;
  }
}