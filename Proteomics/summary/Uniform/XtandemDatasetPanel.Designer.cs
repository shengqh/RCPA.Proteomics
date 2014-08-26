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
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox2
      // 
      this.groupBox2.Size = new System.Drawing.Size(1016, 263);
      this.groupBox2.Text = "Select xml files you want to extract peptides (all files will be used)";
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Xml file";
      this.columnHeader1.Width = 582;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Source file";
      // 
      // btnRenameDat
      // 
      this.btnAutoRename.Text = "Rename Xml";
      this.btnAutoRename.Click += new System.EventHandler(this.btnRenameDat_Click);
      // 
      // btnMgfFiles
      // 
      this.btnMgfFiles.Click += new System.EventHandler(this.btnMgfFiles_Click);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Size = new System.Drawing.Size(1016, 293);
      this.splitContainer3.SplitterDistance = 263;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(8, 6);
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Location = new System.Drawing.Point(106, 2);
      // 
      // splitContainer1
      // 
      // 
      // splitContainer2
      // 
      this.splitContainer2.Size = new System.Drawing.Size(1016, 392);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.cbIgnoreAnticipatedCleavageSite);
      this.groupBox1.Size = new System.Drawing.Size(1016, 95);
      this.groupBox1.Controls.SetChildIndex(this.cbIgnoreAnticipatedCleavageSite, 0);
      // 
      // cbIgnoreAnticipatedCleavageSite
      // 
      this.cbIgnoreAnticipatedCleavageSite.AutoSize = true;
      this.cbIgnoreAnticipatedCleavageSite.Location = new System.Drawing.Point(354, 76);
      this.cbIgnoreAnticipatedCleavageSite.Name = "cbIgnoreAnticipatedCleavageSite";
      this.cbIgnoreAnticipatedCleavageSite.Size = new System.Drawing.Size(177, 17);
      this.cbIgnoreAnticipatedCleavageSite.TabIndex = 65;
      this.cbIgnoreAnticipatedCleavageSite.Text = "Ignore anticipated cleavage site";
      this.cbIgnoreAnticipatedCleavageSite.UseVisualStyleBackColor = true;
      // 
      // XtandemDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Name = "XtandemDatasetPanel";
      this.groupBox2.ResumeLayout(false);
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.CheckBox cbIgnoreAnticipatedCleavageSite;
  }
}
