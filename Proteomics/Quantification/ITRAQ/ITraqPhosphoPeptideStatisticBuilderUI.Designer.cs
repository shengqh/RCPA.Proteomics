namespace RCPA.Tools.Quantification
{
  partial class ITraqPhosphoPeptideStatisticBuilderUI
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
      this.btnRLocation = new System.Windows.Forms.Button();
      this.txtRLocation = new System.Windows.Forms.TextBox();
      this.txtModifications = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtNormal = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtModifiedAminoacids = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(25, 13);
      this.pnlFile.Size = new System.Drawing.Size(846, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(226, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(620, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(226, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 210);
      this.lblProgress.Size = new System.Drawing.Size(893, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 233);
      this.progressBar.Size = new System.Drawing.Size(893, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(494, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(409, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(324, 8);
      // 
      // btnRLocation
      // 
      this.btnRLocation.Location = new System.Drawing.Point(25, 53);
      this.btnRLocation.Name = "btnRLocation";
      this.btnRLocation.Size = new System.Drawing.Size(226, 25);
      this.btnRLocation.TabIndex = 8;
      this.btnRLocation.Text = "button1";
      this.btnRLocation.UseVisualStyleBackColor = true;
      // 
      // txtRLocation
      // 
      this.txtRLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRLocation.Location = new System.Drawing.Point(257, 55);
      this.txtRLocation.Name = "txtRLocation";
      this.txtRLocation.Size = new System.Drawing.Size(614, 20);
      this.txtRLocation.TabIndex = 9;
      // 
      // txtModifications
      // 
      this.txtModifications.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModifications.Location = new System.Drawing.Point(257, 114);
      this.txtModifications.Name = "txtModifications";
      this.txtModifications.Size = new System.Drawing.Size(614, 20);
      this.txtModifications.TabIndex = 10;
      this.txtModifications.Text = "116,117";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(102, 117);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(120, 13);
      this.label1.TabIndex = 11;
      this.label1.Text = "Input Modification Index";
      // 
      // txtNormal
      // 
      this.txtNormal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtNormal.Location = new System.Drawing.Point(257, 85);
      this.txtNormal.Name = "txtNormal";
      this.txtNormal.Size = new System.Drawing.Size(614, 20);
      this.txtNormal.TabIndex = 12;
      this.txtNormal.Text = "114,115";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(138, 88);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(96, 13);
      this.label2.TabIndex = 13;
      this.label2.Text = "Input Normal Index";
      // 
      // txtModifiedAminoacids
      // 
      this.txtModifiedAminoacids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModifiedAminoacids.Location = new System.Drawing.Point(257, 143);
      this.txtModifiedAminoacids.Name = "txtModifiedAminoacids";
      this.txtModifiedAminoacids.Size = new System.Drawing.Size(614, 20);
      this.txtModifiedAminoacids.TabIndex = 14;
      this.txtModifiedAminoacids.Text = "STY";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(96, 146);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(131, 13);
      this.label3.TabIndex = 15;
      this.label3.Text = "Input Modified Aminoacids";
      // 
      // ITraqPhosphoPeptideStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(893, 295);
      this.Controls.Add(this.txtModifiedAminoacids);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtNormal);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtModifications);
      this.Controls.Add(this.btnRLocation);
      this.Controls.Add(this.txtRLocation);
      this.Controls.Add(this.label1);
      this.Name = "ITraqPhosphoPeptideStatisticBuilderUI";
      this.TabText = "ITraqFilePreviewUI";
      this.Text = "ITraqFilePreviewUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.txtRLocation, 0);
      this.Controls.SetChildIndex(this.btnRLocation, 0);
      this.Controls.SetChildIndex(this.txtModifications, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtNormal, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.txtModifiedAminoacids, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnRLocation;
    private System.Windows.Forms.TextBox txtRLocation;
    private System.Windows.Forms.TextBox txtModifications;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtNormal;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtModifiedAminoacids;
    private System.Windows.Forms.Label label3;


  }
}