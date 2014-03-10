namespace RCPA.Proteomics.Snp
{
  partial class DatabaseSnpValidatorUI
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
      this.btnDatabase = new System.Windows.Forms.Button();
      this.txtDatabase = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(24, 24);
      this.btnOriginalFile.Size = new System.Drawing.Size(205, 21);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(751, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(22, 96);
      this.lblProgress.Size = new System.Drawing.Size(962, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(22, 120);
      this.progressBar.Size = new System.Drawing.Size(964, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(384, 155);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(554, 155);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(469, 155);
      // 
      // btnDatabase
      // 
      this.btnDatabase.Location = new System.Drawing.Point(24, 51);
      this.btnDatabase.Name = "btnDatabase";
      this.btnDatabase.Size = new System.Drawing.Size(205, 23);
      this.btnDatabase.TabIndex = 9;
      this.btnDatabase.Text = "button1";
      this.btnDatabase.UseVisualStyleBackColor = true;
      // 
      // txtDatabase
      // 
      this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDatabase.Location = new System.Drawing.Point(235, 53);
      this.txtDatabase.Name = "txtDatabase";
      this.txtDatabase.Size = new System.Drawing.Size(751, 21);
      this.txtDatabase.TabIndex = 10;
      // 
      // DatabaseSnpValidatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1012, 196);
      this.Controls.Add(this.txtDatabase);
      this.Controls.Add(this.btnDatabase);
      this.Name = "DatabaseSnpValidatorUI";
      this.TabText = "DatabaseSnpValidatorUI";
      this.Text = "DatabaseSnpValidatorUI";
      this.Controls.SetChildIndex(this.btnDatabase, 0);
      this.Controls.SetChildIndex(this.txtDatabase, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      
      
      
      
      
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnDatabase;
    private System.Windows.Forms.TextBox txtDatabase;
  }
}