namespace RCPA.Proteomics.Statistic
{
  partial class MassOffsetCalculatorUI
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
      this.siliconePolymerIons = new RCPA.Proteomics.SiliconePolymerIonField();
      this.label1 = new System.Windows.Forms.Label();
      this.txtInitPPM = new System.Windows.Forms.TextBox();
      this.txtPrecursorPPM = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtRtWindow = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(34, 12);
      this.pnlFile.Size = new System.Drawing.Size(987, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(741, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 292);
      this.lblProgress.Size = new System.Drawing.Size(1040, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 313);
      this.progressBar.Size = new System.Drawing.Size(1040, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(568, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(483, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(398, 7);
      // 
      // siliconePolymerIons
      // 
      this.siliconePolymerIons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.siliconePolymerIons.Location = new System.Drawing.Point(34, 80);
      this.siliconePolymerIons.Name = "siliconePolymerIons";
      this.siliconePolymerIons.SelectedIon = "355,371,388,429,445,462,503,519,536,593";
      this.siliconePolymerIons.Size = new System.Drawing.Size(987, 197);
      this.siliconePolymerIons.TabIndex = 9;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(146, 56);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 12);
      this.label1.TabIndex = 10;
      this.label1.Text = "Max shift ppm";
      // 
      // txtInitPPM
      // 
      this.txtInitPPM.Location = new System.Drawing.Point(235, 53);
      this.txtInitPPM.Name = "txtInitPPM";
      this.txtInitPPM.Size = new System.Drawing.Size(100, 21);
      this.txtInitPPM.TabIndex = 11;
      // 
      // txtPrecursorPPM
      // 
      this.txtPrecursorPPM.Location = new System.Drawing.Point(515, 53);
      this.txtPrecursorPPM.Name = "txtPrecursorPPM";
      this.txtPrecursorPPM.Size = new System.Drawing.Size(100, 21);
      this.txtPrecursorPPM.TabIndex = 13;
      this.txtPrecursorPPM.Visible = false;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(402, 56);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 12);
      this.label2.TabIndex = 12;
      this.label2.Text = "Max precursor ppm";
      this.label2.Visible = false;
      // 
      // txtRtWindow
      // 
      this.txtRtWindow.Location = new System.Drawing.Point(859, 53);
      this.txtRtWindow.Name = "txtRtWindow";
      this.txtRtWindow.Size = new System.Drawing.Size(100, 21);
      this.txtRtWindow.TabIndex = 15;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(686, 56);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(167, 12);
      this.label3.TabIndex = 14;
      this.label3.Text = "Smoothing window size (min)";
      // 
      // MassOffsetCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1040, 370);
      this.Controls.Add(this.txtRtWindow);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtPrecursorPPM);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.siliconePolymerIons);
      this.Controls.Add(this.txtInitPPM);
      this.Controls.Add(this.label1);
      this.Name = "MassOffsetCalculatorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtInitPPM, 0);
      this.Controls.SetChildIndex(this.siliconePolymerIons, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPrecursorPPM, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.txtRtWindow, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SiliconePolymerIonField siliconePolymerIons;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtInitPPM;
    private System.Windows.Forms.TextBox txtPrecursorPPM;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtRtWindow;
    private System.Windows.Forms.Label label3;
  }
}
