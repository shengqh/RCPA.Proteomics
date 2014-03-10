namespace RCPA.Tools.Raw
{
  partial class AgilentFileViewerUI
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
      this.zgcScan = new ZedGraph.ZedGraphControl();
      this.btnPrev = new System.Windows.Forms.Button();
      this.btnFirst = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnLast = new System.Windows.Forms.Button();
      this.txtScan = new System.Windows.Forms.TextBox();
      this.txtMinPeakIntensity = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtTopPeaks = new System.Windows.Forms.TextBox();
      this.txtPeaks = new System.Windows.Forms.RichTextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
      this.pnlFile.Location = new System.Drawing.Point(0, 0);
      this.pnlFile.Size = new System.Drawing.Size(980, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(734, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 562);
      this.lblProgress.Size = new System.Drawing.Size(980, 21);
      this.lblProgress.Visible = false;
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 583);
      this.progressBar.Size = new System.Drawing.Size(980, 21);
      this.progressBar.Visible = false;
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(538, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(453, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(368, 7);
      // 
      // zgcScan
      // 
      this.zgcScan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.zgcScan.Location = new System.Drawing.Point(31, 39);
      this.zgcScan.Name = "zgcScan";
      this.zgcScan.ScrollGrace = 0D;
      this.zgcScan.ScrollMaxX = 0D;
      this.zgcScan.ScrollMaxY = 0D;
      this.zgcScan.ScrollMaxY2 = 0D;
      this.zgcScan.ScrollMinX = 0D;
      this.zgcScan.ScrollMinY = 0D;
      this.zgcScan.ScrollMinY2 = 0D;
      this.zgcScan.Size = new System.Drawing.Size(711, 484);
      this.zgcScan.TabIndex = 7;
      // 
      // btnPrev
      // 
      this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnPrev.Location = new System.Drawing.Point(72, 533);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(34, 23);
      this.btnPrev.TabIndex = 8;
      this.btnPrev.Text = "<";
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
      // 
      // btnFirst
      // 
      this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnFirst.Location = new System.Drawing.Point(33, 533);
      this.btnFirst.Name = "btnFirst";
      this.btnFirst.Size = new System.Drawing.Size(34, 23);
      this.btnFirst.TabIndex = 8;
      this.btnFirst.Text = "|<";
      this.btnFirst.UseVisualStyleBackColor = true;
      this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
      // 
      // btnNext
      // 
      this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnNext.Location = new System.Drawing.Point(217, 533);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(34, 23);
      this.btnNext.TabIndex = 8;
      this.btnNext.Text = ">";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnLast
      // 
      this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnLast.Location = new System.Drawing.Point(256, 533);
      this.btnLast.Name = "btnLast";
      this.btnLast.Size = new System.Drawing.Size(34, 23);
      this.btnLast.TabIndex = 8;
      this.btnLast.Text = ">|";
      this.btnLast.UseVisualStyleBackColor = true;
      this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
      // 
      // txtScan
      // 
      this.txtScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtScan.Location = new System.Drawing.Point(112, 535);
      this.txtScan.Name = "txtScan";
      this.txtScan.Size = new System.Drawing.Size(99, 21);
      this.txtScan.TabIndex = 9;
      this.txtScan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtScan_KeyUp);
      // 
      // txtMinPeakIntensity
      // 
      this.txtMinPeakIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtMinPeakIntensity.Location = new System.Drawing.Point(453, 535);
      this.txtMinPeakIntensity.Name = "txtMinPeakIntensity";
      this.txtMinPeakIntensity.Size = new System.Drawing.Size(100, 21);
      this.txtMinPeakIntensity.TabIndex = 10;
      this.txtMinPeakIntensity.Text = "1";
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(321, 539);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(131, 12);
      this.label1.TabIndex = 11;
      this.label1.Text = "Minmum Peak Intensity";
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(586, 539);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 12);
      this.label2.TabIndex = 14;
      this.label2.Text = "Display Top Peaks";
      // 
      // txtTopPeaks
      // 
      this.txtTopPeaks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtTopPeaks.Location = new System.Drawing.Point(699, 535);
      this.txtTopPeaks.Name = "txtTopPeaks";
      this.txtTopPeaks.Size = new System.Drawing.Size(100, 21);
      this.txtTopPeaks.TabIndex = 13;
      this.txtTopPeaks.Text = "100";
      // 
      // txtPeaks
      // 
      this.txtPeaks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeaks.DetectUrls = false;
      this.txtPeaks.Location = new System.Drawing.Point(755, 39);
      this.txtPeaks.Name = "txtPeaks";
      this.txtPeaks.ReadOnly = true;
      this.txtPeaks.Size = new System.Drawing.Size(190, 484);
      this.txtPeaks.TabIndex = 15;
      this.txtPeaks.Text = "";
      this.txtPeaks.WordWrap = false;
      // 
      // AgilentFileViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(980, 640);
      this.Controls.Add(this.txtPeaks);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtTopPeaks);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.zgcScan);
      this.Controls.Add(this.btnPrev);
      this.Controls.Add(this.btnFirst);
      this.Controls.Add(this.txtMinPeakIntensity);
      this.Controls.Add(this.btnLast);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.txtScan);
      this.KeyPreview = true;
      this.Name = "AgilentFileViewerUI";
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RawFileViewerUI_KeyDown);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtScan, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.btnLast, 0);
      this.Controls.SetChildIndex(this.txtMinPeakIntensity, 0);
      this.Controls.SetChildIndex(this.btnFirst, 0);
      this.Controls.SetChildIndex(this.btnPrev, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.zgcScan, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtTopPeaks, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPeaks, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ZedGraph.ZedGraphControl zgcScan;
    private System.Windows.Forms.Button btnPrev;
    private System.Windows.Forms.Button btnFirst;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnLast;
    private System.Windows.Forms.TextBox txtScan;
    private System.Windows.Forms.TextBox txtMinPeakIntensity;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTopPeaks;
    private System.Windows.Forms.RichTextBox txtPeaks;
  }
}
