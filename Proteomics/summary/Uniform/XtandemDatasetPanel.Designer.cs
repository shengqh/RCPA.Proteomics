namespace RCPA.Proteomics.Summary.Uniform
{
  partial class XtandemDatasetPanel
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
      this.cbIgnoreAnticipatedCleavageSite = new System.Windows.Forms.CheckBox();
      this.groupBox2.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox2
      // 
      this.groupBox2.Text = "Select xml files you want to extract peptides (all files will be used)";
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Xml file";
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Source file";
      // 
      // btnMgfFiles
      // 
      this.btnMgfFiles.Text = "Find Source";
      this.btnMgfFiles.Click += new System.EventHandler(this.btnMgfFiles_Click);
      // 
      // btnRenameDat
      // 
      this.btnRenameDat.Text = "Rename Xml";
      this.btnRenameDat.Click += new System.EventHandler(this.btnRenameDat_Click);
      // 
      // splitContainer1
      // 
      // 
      // splitContainer2
      // 
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbIgnoreAnticipatedCleavageSite);
      this.groupBox1.Controls.SetChildIndex(this.cbIgnoreAnticipatedCleavageSite, 0);
      // 
      // cbIgnoreAnticipatedCleavageSite
      // 
      this.cbIgnoreAnticipatedCleavageSite.AutoSize = true;
      this.cbIgnoreAnticipatedCleavageSite.Location = new System.Drawing.Point(354, 70);
      this.cbIgnoreAnticipatedCleavageSite.Name = "cbIgnoreAnticipatedCleavageSite";
      this.cbIgnoreAnticipatedCleavageSite.Size = new System.Drawing.Size(216, 16);
      this.cbIgnoreAnticipatedCleavageSite.TabIndex = 65;
      this.cbIgnoreAnticipatedCleavageSite.Text = "Ignore anticipated cleavage site";
      this.cbIgnoreAnticipatedCleavageSite.UseVisualStyleBackColor = true;
      // 
      // XtandemDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.Name = "XtandemDatasetPanel";
      this.groupBox2.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox cbIgnoreAnticipatedCleavageSite;
  }
}
