namespace RCPA.Proteomics.Summary.Uniform
{
  partial class ExpectValueDatasetPanel
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
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.btnFindDatabase = new System.Windows.Forms.Button();
      this.btnAutoRename = new System.Windows.Forms.Button();
      this.btnMgfFiles = new System.Windows.Forms.Button();
      this.pnlDataButton.SuspendLayout();
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
      // pnlDataButton
      // 
      this.pnlDataButton.Controls.Add(this.btnMgfFiles);
      this.pnlDataButton.Controls.Add(this.btnAutoRename);
      this.pnlDataButton.Controls.Add(this.btnFindDatabase);
      this.pnlDataButton.Controls.SetChildIndex(this.btnAddFiles, 0);
      this.pnlDataButton.Controls.SetChildIndex(this.btnRemoveFiles, 0);
      this.pnlDataButton.Controls.SetChildIndex(this.btnLoad, 0);
      this.pnlDataButton.Controls.SetChildIndex(this.btnSave, 0);
      this.pnlDataButton.Controls.SetChildIndex(this.btnFindDatabase, 0);
      this.pnlDataButton.Controls.SetChildIndex(this.btnAutoRename, 0);
      this.pnlDataButton.Controls.SetChildIndex(this.btnMgfFiles, 0);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Width = 582;
      // 
      // splitContainer3
      // 
      // 
      // splitContainer2
      // 
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.txtMaxEvalue);
      this.groupBox1.Controls.Add(this.cbFilterByEvalue);
      this.groupBox1.Controls.SetChildIndex(this.cbFilterByEvalue, 0);
      this.groupBox1.Controls.SetChildIndex(this.txtMaxEvalue, 0);
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(180, 73);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 20);
      this.txtMaxEvalue.TabIndex = 51;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(9, 75);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(139, 17);
      this.cbFilterByEvalue.TabIndex = 50;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // btnFindDatabase
      // 
      this.btnFindDatabase.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnFindDatabase.Location = new System.Drawing.Point(0, 92);
      this.btnFindDatabase.Name = "btnFindDatabase";
      this.btnFindDatabase.Size = new System.Drawing.Size(125, 23);
      this.btnFindDatabase.TabIndex = 25;
      this.btnFindDatabase.Text = "Find Database";
      this.btnFindDatabase.UseVisualStyleBackColor = true;
      // 
      // btnAutoRename
      // 
      this.btnAutoRename.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnAutoRename.Location = new System.Drawing.Point(0, 115);
      this.btnAutoRename.Name = "btnAutoRename";
      this.btnAutoRename.Size = new System.Drawing.Size(125, 23);
      this.btnAutoRename.TabIndex = 24;
      this.btnAutoRename.Text = "Auto Rename";
      this.btnAutoRename.UseVisualStyleBackColor = true;
      // 
      // btnMgfFiles
      // 
      this.btnMgfFiles.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnMgfFiles.Location = new System.Drawing.Point(0, 138);
      this.btnMgfFiles.Name = "btnMgfFiles";
      this.btnMgfFiles.Size = new System.Drawing.Size(125, 23);
      this.btnMgfFiles.TabIndex = 23;
      this.btnMgfFiles.Text = "Find Source";
      this.btnMgfFiles.UseVisualStyleBackColor = true;
      // 
      // ExpectValueDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "ExpectValueDatasetPanel";
      this.pnlDataButton.ResumeLayout(false);
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

    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    protected System.Windows.Forms.Button btnFindDatabase;
    protected System.Windows.Forms.Button btnAutoRename;
    protected System.Windows.Forms.Button btnMgfFiles;
  }
}
