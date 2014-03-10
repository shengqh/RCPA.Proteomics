namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmExportForm
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
      this.cbTargetOnly = new System.Windows.Forms.CheckBox();
      this.cbValidOnly = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(295, 113);
      this.btnGo.Text = "&Ok";
      // 
      // btnClose
      // 
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(465, 113);
      this.btnClose.Text = "&Cancel";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(380, 113);
      this.btnCancel.Visible = false;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(12, 21);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(277, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(293, 23);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(529, 21);
      this.textBox1.TabIndex = 1;
      // 
      // cbTargetOnly
      // 
      this.cbTargetOnly.AutoSize = true;
      this.cbTargetOnly.Location = new System.Drawing.Point(293, 59);
      this.cbTargetOnly.Name = "cbTargetOnly";
      this.cbTargetOnly.Size = new System.Drawing.Size(168, 16);
      this.cbTargetOnly.TabIndex = 7;
      this.cbTargetOnly.Text = "Remove all decoy entries";
      this.cbTargetOnly.UseVisualStyleBackColor = true;
      // 
      // cbValidOnly
      // 
      this.cbValidOnly.AutoSize = true;
      this.cbValidOnly.Location = new System.Drawing.Point(293, 81);
      this.cbValidOnly.Name = "cbValidOnly";
      this.cbValidOnly.Size = new System.Drawing.Size(162, 16);
      this.cbValidOnly.TabIndex = 7;
      this.cbValidOnly.Text = "Keep valid entries only";
      this.cbValidOnly.UseVisualStyleBackColor = true;
      // 
      // SrmExportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(834, 154);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.cbTargetOnly);
      this.Controls.Add(this.cbValidOnly);
      this.Name = "SrmExportForm";
      this.TabText = "Export Compound Quantification Result";
      this.Text = "Export Compound Quantification Result";
      this.Controls.SetChildIndex(this.cbValidOnly, 0);
      this.Controls.SetChildIndex(this.cbTargetOnly, 0);
      this.Controls.SetChildIndex(this.button1, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      
      
      
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.CheckBox cbTargetOnly;
    private System.Windows.Forms.CheckBox cbValidOnly;
  }
}