namespace RCPA.Tools.Quantification.SmallMolecule
{
  partial class SmallMoleculeSignificantPeakFinderUI
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
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Location = new System.Drawing.Point(31, 93);
      this.btnOriginalFile.Size = new System.Drawing.Size(250, 23);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(287, 94);
      this.txtOriginalFile.Size = new System.Drawing.Size(694, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(32, 129);
      this.lblProgress.Size = new System.Drawing.Size(949, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(35, 152);
      this.progressBar.Size = new System.Drawing.Size(946, 21);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(386, 180);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(556, 180);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(471, 180);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(31, 35);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(250, 24);
      this.button1.TabIndex = 9;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(287, 37);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(694, 21);
      this.textBox1.TabIndex = 10;
      // 
      // textBox2
      // 
      this.textBox2.Location = new System.Drawing.Point(287, 67);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(694, 21);
      this.textBox2.TabIndex = 12;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(31, 65);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(250, 23);
      this.button2.TabIndex = 11;
      this.button2.Text = "button2";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // SmallMoleculeSignificantPeakFinderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1016, 221);
      this.Controls.Add(this.textBox2);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button1);
      this.Name = "SmallMoleculeSignificantPeakFinderUI";
      this.Controls.SetChildIndex(this.button1, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      
      
      
      
      
      this.Controls.SetChildIndex(this.button2, 0);
      this.Controls.SetChildIndex(this.textBox2, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button button2;

  }
}
