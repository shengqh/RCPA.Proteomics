namespace RCPA.Proteomics.Mascot
{
  partial class MascotFilePanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // btnFindDatabase
      // 
      this.btnFindDatabase.Click += new System.EventHandler(this.btnFindDatabase_Click);
      // 
      // btnAutoRename
      // 
      this.btnAutoRename.Click += new System.EventHandler(this.btnAutoRename_Click);
      // 
      // btnSouceFiles
      // 
      this.btnSouceFiles.Click += new System.EventHandler(this.btnSouceFiles_Click);
      // 
      // MascotFilePanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "MascotFilePanel";
      this.ResumeLayout(false);

    }

    #endregion
  }
}
