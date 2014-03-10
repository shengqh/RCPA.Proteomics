namespace RCPA.Proteomics.Quantification.ITraq
{
  partial class ITraqOpenFileDialog
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
      this.proteinFile = new RCPA.Gui.FileField();
      this.itraqFile = new RCPA.Gui.FileField();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(436, 95);
      this.btnGo.Text = "&Ok";
      // 
      // btnClose
      // 
      this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnClose.Location = new System.Drawing.Point(606, 95);
      this.btnClose.Text = "&Cancel";
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(521, 95);
      this.btnCancel.Visible = false;
      // 
      // proteinFile
      // 
      this.proteinFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.proteinFile.FullName = "";
      this.proteinFile.Key = "ProteinFile";
      this.proteinFile.Location = new System.Drawing.Point(12, 21);
      this.proteinFile.Name = "proteinFile";
      this.proteinFile.WidthOpenButton = 300;
      this.proteinFile.Required = false;
      this.proteinFile.Size = new System.Drawing.Size(1093, 21);
      this.proteinFile.TabIndex = 0;
      // 
      // itraqFile
      // 
      this.itraqFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.itraqFile.FullName = "";
      this.itraqFile.Key = "ITraqFile";
      this.itraqFile.Location = new System.Drawing.Point(12, 48);
      this.itraqFile.Name = "itraqFile";
      this.itraqFile.WidthOpenButton = 300;
      this.itraqFile.Required = false;
      this.itraqFile.Size = new System.Drawing.Size(1093, 21);
      this.itraqFile.TabIndex = 1;
      // 
      // ITraqOpenFileDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1117, 136);
      this.Controls.Add(this.itraqFile);
      this.Controls.Add(this.proteinFile);
      this.Name = "ITraqOpenFileDialog";
      this.TabText = "ITraqOpenFileDialog";
      this.Text = "Open iTRAQ Files";
      this.Controls.SetChildIndex(this.proteinFile, 0);
      this.Controls.SetChildIndex(this.itraqFile, 0);
      this.ResumeLayout(false);
    }

    #endregion

    private Gui.FileField proteinFile;
    private Gui.FileField itraqFile;
  }
}