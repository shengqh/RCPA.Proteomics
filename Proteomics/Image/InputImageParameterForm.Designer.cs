namespace RCPA.Proteomics.Image
{
  partial class InputImageParameterForm
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
      this.mgfFiles = new RCPA.Gui.MultipleFileField();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.btnClose = new System.Windows.Forms.ToolStripButton();
      this.btnOk = new System.Windows.Forms.ToolStripButton();
      this.peptideFile = new RCPA.Gui.FileField();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // mgfFiles
      // 
      this.mgfFiles.FileArgument = null;
      this.mgfFiles.FileDescription = "Mascot Generic Format Files or Raw Files";
      this.mgfFiles.FileNames = new string[0];
      this.mgfFiles.Key = "File";
      this.mgfFiles.Location = new System.Drawing.Point(103, 120);
      this.mgfFiles.Name = "mgfFiles";
      this.mgfFiles.SelectedIndex = -1;
      this.mgfFiles.SelectedItem = null;
      this.mgfFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.mgfFiles.Size = new System.Drawing.Size(812, 191);
      this.mgfFiles.TabIndex = 0;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClose,
            this.btnOk});
      this.toolStrip1.Location = new System.Drawing.Point(0, 374);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.toolStrip1.Size = new System.Drawing.Size(1076, 39);
      this.toolStrip1.TabIndex = 1;
      this.toolStrip1.Text = "toolStrip1";
      // 
      // btnClose
      // 
      this.btnClose.Image = global::RCPA.Properties.Resources.cancel;
      this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(76, 36);
      this.btnClose.Text = "Close";
      this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      // 
      // btnOk
      // 
      this.btnOk.Image = global::RCPA.Properties.Resources.ok;
      this.btnOk.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(61, 36);
      this.btnOk.Text = "Ok";
      this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
      // 
      // peptideFile
      // 
      this.peptideFile.Dock = System.Windows.Forms.DockStyle.Top;
      this.peptideFile.FullName = "";
      this.peptideFile.Key = "Key";
      this.peptideFile.Location = new System.Drawing.Point(0, 0);
      this.peptideFile.Name = "peptideFile";
      this.peptideFile.WidthOpenButton = 300;
      this.peptideFile.Size = new System.Drawing.Size(1076, 21);
      this.peptideFile.TabIndex = 2;
      // 
      // InputImageParameterForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1076, 413);
      this.Controls.Add(this.peptideFile);
      this.Controls.Add(this.toolStrip1);
      this.Controls.Add(this.mgfFiles);
      this.Name = "InputImageParameterForm";
      this.Text = "InputImageParameterForm";
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Gui.MultipleFileField mgfFiles;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton btnClose;
    private System.Windows.Forms.ToolStripButton btnOk;
    private Gui.FileField peptideFile;
  }
}