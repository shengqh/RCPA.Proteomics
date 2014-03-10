namespace RCPA.Tools.Quantification
{
  partial class ITraqResultImageBuilderUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ITraqResultImageBuilderUI));
      this.btnRLocation = new System.Windows.Forms.Button();
      this.txtRLocation = new System.Windows.Forms.TextBox();
      this.picBox = new System.Windows.Forms.PictureBox();
      this.pnlFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(25, 13);
      this.pnlFile.Size = new System.Drawing.Size(1116, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(870, 20);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 510);
      this.lblProgress.Size = new System.Drawing.Size(1163, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 533);
      this.progressBar.Size = new System.Drawing.Size(1163, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(629, 9);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(544, 9);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(459, 9);
      // 
      // btnRLocation
      // 
      this.btnRLocation.Location = new System.Drawing.Point(25, 57);
      this.btnRLocation.Name = "btnRLocation";
      this.btnRLocation.Size = new System.Drawing.Size(166, 25);
      this.btnRLocation.TabIndex = 8;
      this.btnRLocation.Text = "button1";
      this.btnRLocation.UseVisualStyleBackColor = true;
      // 
      // txtRLocation
      // 
      this.txtRLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRLocation.Location = new System.Drawing.Point(213, 60);
      this.txtRLocation.Name = "txtRLocation";
      this.txtRLocation.Size = new System.Drawing.Size(928, 20);
      this.txtRLocation.TabIndex = 9;
      // 
      // picBox
      // 
      this.picBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.picBox.Image = ((System.Drawing.Image)(resources.GetObject("picBox.Image")));
      this.picBox.Location = new System.Drawing.Point(25, 102);
      this.picBox.Name = "picBox";
      this.picBox.Size = new System.Drawing.Size(1116, 371);
      this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picBox.TabIndex = 10;
      this.picBox.TabStop = false;
      // 
      // ITraqResultImageBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1163, 595);
      this.Controls.Add(this.picBox);
      this.Controls.Add(this.btnRLocation);
      this.Controls.Add(this.txtRLocation);
      this.Name = "ITraqResultImageBuilderUI";
      this.TabText = "ITraqFilePreviewUI";
      this.Text = "ITraqFilePreviewUI";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtRLocation, 0);
      this.Controls.SetChildIndex(this.btnRLocation, 0);
      this.Controls.SetChildIndex(this.picBox, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnRLocation;
    private System.Windows.Forms.TextBox txtRLocation;
    private System.Windows.Forms.PictureBox picBox;


  }
}