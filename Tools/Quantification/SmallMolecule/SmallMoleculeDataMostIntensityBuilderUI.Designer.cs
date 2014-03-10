namespace RCPA.Tools.Quantification.SmallMolecule
{
  partial class SmallMoleculeDataMostIntensityBuilderUI
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
      this.label1 = new System.Windows.Forms.Label();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(250, 23);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(287, 25);
      this.txtOriginalFile.Size = new System.Drawing.Size(694, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 100);
      this.lblProgress.Size = new System.Drawing.Size(949, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 123);
      this.progressBar.Size = new System.Drawing.Size(946, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(386, 151);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(556, 151);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(471, 151);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(168, 57);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(113, 12);
      this.label1.TabIndex = 9;
      this.label1.Text = "Input name pattern";
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(287, 54);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(694, 21);
      this.textBox1.TabIndex = 10;
      // 
      // SmallMoleculeDataMostIntensityBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1016, 192);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label1);
      this.Name = "SmallMoleculeDataMostIntensityBuilderUI";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      
      
      
      
      
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox1;

  }
}
