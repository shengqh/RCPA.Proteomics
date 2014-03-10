namespace RCPA.Tools.Annotation
{
  partial class SequestSpectrumImageBuilderUI
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
      this.btnDtaFile = new System.Windows.Forms.Button();
      this.txtDtaFile = new System.Windows.Forms.TextBox();
      this.zgcPeaks = new ZedGraph.ZedGraphControl();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(324, 463);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(494, 463);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(409, 463);
      // 
      // btnDtaFile
      // 
      this.btnDtaFile.Location = new System.Drawing.Point(12, 15);
      this.btnDtaFile.Name = "btnDtaFile";
      this.btnDtaFile.Size = new System.Drawing.Size(220, 23);
      this.btnDtaFile.TabIndex = 7;
      this.btnDtaFile.Text = "button1";
      this.btnDtaFile.UseVisualStyleBackColor = true;
      // 
      // txtDtaFile
      // 
      this.txtDtaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDtaFile.Location = new System.Drawing.Point(238, 15);
      this.txtDtaFile.Name = "txtDtaFile";
      this.txtDtaFile.Size = new System.Drawing.Size(631, 21);
      this.txtDtaFile.TabIndex = 8;
      // 
      // zgcPeaks
      // 
      this.zgcPeaks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.zgcPeaks.Location = new System.Drawing.Point(12, 54);
      this.zgcPeaks.Name = "zgcPeaks";
      this.zgcPeaks.ScrollGrace = 0;
      this.zgcPeaks.ScrollMaxX = 0;
      this.zgcPeaks.ScrollMaxY = 0;
      this.zgcPeaks.ScrollMaxY2 = 0;
      this.zgcPeaks.ScrollMinX = 0;
      this.zgcPeaks.ScrollMinY = 0;
      this.zgcPeaks.ScrollMinY2 = 0;
      this.zgcPeaks.Size = new System.Drawing.Size(857, 391);
      this.zgcPeaks.TabIndex = 9;
      // 
      // SequestSpectrumImageBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(893, 504);
      this.Controls.Add(this.zgcPeaks);
      this.Controls.Add(this.txtDtaFile);
      this.Controls.Add(this.btnDtaFile);
      this.Name = "SequestSpectrumImageBuilderUI";
      this.Text = "SequestSpectrumImageBuilderUI";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Controls.SetChildIndex(this.btnDtaFile, 0);
      this.Controls.SetChildIndex(this.txtDtaFile, 0);
      this.Controls.SetChildIndex(this.zgcPeaks, 0);
      
      
      
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnDtaFile;
    private System.Windows.Forms.TextBox txtDtaFile;
    private ZedGraph.ZedGraphControl zgcPeaks;
  }
}