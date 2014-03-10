namespace RCPA.Tools.Mascot
{
  partial class TurboRaw2MzXmlProcessorUI
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
      this.btnTarget = new System.Windows.Forms.Button();
      this.txtTarget = new System.Windows.Forms.TextBox();
      this.cbCentroid = new System.Windows.Forms.CheckBox();
      this.cbFullMsOnly = new System.Windows.Forms.CheckBox();
      this.tcBatchMode.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage1.Location = new System.Drawing.Point(4, 21);
      this.tabPage1.Size = new System.Drawing.Size(823, 67);
      this.tabPage1.UseVisualStyleBackColor = false;
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage2.Location = new System.Drawing.Point(4, 21);
      this.tabPage2.Size = new System.Drawing.Size(823, 67);
      this.tabPage2.UseVisualStyleBackColor = false;
      // 
      // txtSingle
      // 
      this.txtSingle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSingle.Location = new System.Drawing.Point(250, 25);
      this.txtSingle.Size = new System.Drawing.Size(553, 21);
      // 
      // btnSingle
      // 
      this.btnSingle.Location = new System.Drawing.Point(16, 23);
      this.btnSingle.Size = new System.Drawing.Size(202, 21);
      // 
      // txtBatch
      // 
      this.txtBatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtBatch.Location = new System.Drawing.Point(250, 25);
      this.txtBatch.Size = new System.Drawing.Size(553, 21);
      // 
      // btnBatch
      // 
      this.btnBatch.Location = new System.Drawing.Point(16, 23);
      this.btnBatch.Size = new System.Drawing.Size(202, 21);
      // 
      // lblSecondProgress
      // 
      this.lblSecondProgress.Location = new System.Drawing.Point(32, 212);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(32, -1);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(270, 1);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(31, 259);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 236);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(326, 300);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(496, 300);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(411, 300);
      // 
      // btnTarget
      // 
      this.btnTarget.Location = new System.Drawing.Point(52, 131);
      this.btnTarget.Name = "btnTarget";
      this.btnTarget.Size = new System.Drawing.Size(201, 21);
      this.btnTarget.TabIndex = 2;
      this.btnTarget.Text = "button1";
      this.btnTarget.UseVisualStyleBackColor = true;
      // 
      // txtTarget
      // 
      this.txtTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTarget.Location = new System.Drawing.Point(285, 134);
      this.txtTarget.Name = "txtTarget";
      this.txtTarget.Size = new System.Drawing.Size(553, 21);
      this.txtTarget.TabIndex = 3;
      // 
      // cbCentroid
      // 
      this.cbCentroid.AutoSize = true;
      this.cbCentroid.Location = new System.Drawing.Point(423, 172);
      this.cbCentroid.Name = "cbCentroid";
      this.cbCentroid.Size = new System.Drawing.Size(72, 16);
      this.cbCentroid.TabIndex = 29;
      this.cbCentroid.Text = "Centroid";
      this.cbCentroid.UseVisualStyleBackColor = true;
      // 
      // cbFullMsOnly
      // 
      this.cbFullMsOnly.AutoSize = true;
      this.cbFullMsOnly.Location = new System.Drawing.Point(286, 172);
      this.cbFullMsOnly.Name = "cbFullMsOnly";
      this.cbFullMsOnly.Size = new System.Drawing.Size(96, 16);
      this.cbFullMsOnly.TabIndex = 28;
      this.cbFullMsOnly.Text = "Full MS only";
      this.cbFullMsOnly.UseVisualStyleBackColor = true;
      // 
      // TurboRaw2MzXmlProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(897, 341);
      this.Controls.Add(this.cbCentroid);
      this.Controls.Add(this.cbFullMsOnly);
      this.Controls.Add(this.txtTarget);
      this.Controls.Add(this.btnTarget);
      this.Name = "TurboRaw2MzXmlProcessorUI";
      this.Controls.SetChildIndex(this.tcBatchMode, 0);
      
      
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      
      
      this.Controls.SetChildIndex(this.btnTarget, 0);
      
      this.Controls.SetChildIndex(this.txtTarget, 0);
      this.Controls.SetChildIndex(this.lblSecondProgress, 0);
      this.Controls.SetChildIndex(this.cbFullMsOnly, 0);
      this.Controls.SetChildIndex(this.cbCentroid, 0);
      this.tcBatchMode.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnTarget;
    private System.Windows.Forms.TextBox txtTarget;
    private System.Windows.Forms.CheckBox cbCentroid;
    private System.Windows.Forms.CheckBox cbFullMsOnly;
  }
}