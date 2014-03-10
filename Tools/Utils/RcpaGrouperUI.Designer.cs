namespace RCPA.Tools.Utils
{
  partial class RcpaGrouperUI
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
      this.txtNames = new System.Windows.Forms.RichTextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.tvNames = new System.Windows.Forms.TreeView();
      this.label2 = new System.Windows.Forms.Label();
      this.txtGroupCount = new System.Windows.Forms.TextBox();
      this.btnCopy = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(324, 531);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(494, 531);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(409, 531);
      // 
      // txtNames
      // 
      this.txtNames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)));
      this.txtNames.Location = new System.Drawing.Point(12, 37);
      this.txtNames.Name = "txtNames";
      this.txtNames.Size = new System.Drawing.Size(346, 488);
      this.txtNames.TabIndex = 7;
      this.txtNames.Text = "‘¯·…\nÕı√Ù∑Â\n ¢»™ª¢\n÷ÏŒƒΩ‹\n¿Ó≥Ω\n¿Ó»Ÿœº\n∏ﬂœ»∏ª\n¬¿∞Æ∆Ω\n≥¬¥∫¿Ÿ\n¿Ó«‡»Û\nµÀŒƒæ˝\n¡ı—” ¢\nŒ‚≥¨≥¨\n’‘ ¿¡÷\nÃ∆º“‰¯\nÀ’÷«∂À\n∏ﬂÊ∫\nœƒ∑Ω”®\nπÀ≈‡√˜\n∫Ó¿ˆ∆º\n–ÏŒ°\n¿Ó" +
          "ºŒ√˙\nƒﬂ∫Á\n¥˜Ω›\nπÀ›º\n÷Ï∫ÈŒƒ\n¡ıƒ˛";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 12);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 12);
      this.label1.TabIndex = 8;
      this.label1.Text = "Names";
      // 
      // tvNames
      // 
      this.tvNames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tvNames.Location = new System.Drawing.Point(376, 37);
      this.tvNames.Name = "tvNames";
      this.tvNames.Size = new System.Drawing.Size(505, 488);
      this.tvNames.TabIndex = 9;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(374, 13);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 12);
      this.label2.TabIndex = 10;
      this.label2.Text = "Group count : ";
      // 
      // txtGroupCount
      // 
      this.txtGroupCount.Location = new System.Drawing.Point(469, 10);
      this.txtGroupCount.Name = "txtGroupCount";
      this.txtGroupCount.Size = new System.Drawing.Size(100, 21);
      this.txtGroupCount.TabIndex = 11;
      // 
      // btnCopy
      // 
      this.btnCopy.Location = new System.Drawing.Point(584, 531);
      this.btnCopy.Name = "btnCopy";
      this.btnCopy.Size = new System.Drawing.Size(156, 21);
      this.btnCopy.TabIndex = 12;
      this.btnCopy.Text = "Copy To Clipboard";
      this.btnCopy.UseVisualStyleBackColor = true;
      this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
      // 
      // RcpaGrouperUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(893, 572);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtNames);
      this.Controls.Add(this.tvNames);
      this.Controls.Add(this.btnCopy);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtGroupCount);
      this.Name = "RcpaGrouperUI";
      this.Controls.SetChildIndex(this.txtGroupCount, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.btnCopy, 0);
      this.Controls.SetChildIndex(this.tvNames, 0);
      this.Controls.SetChildIndex(this.txtNames, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      
      
      
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RichTextBox txtNames;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TreeView tvNames;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtGroupCount;
    private System.Windows.Forms.Button btnCopy;
  }
}
