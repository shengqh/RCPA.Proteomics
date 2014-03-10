namespace RCPA.Tools.Quantification
{
  partial class CensusResultFileMerger2UI
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
      this.lbResultFiles = new System.Windows.Forms.ListBox();
      this.btnAdd = new System.Windows.Forms.Button();
      this.btnRemove = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(0, 384);
      this.pnlFile.Size = new System.Drawing.Size(903, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(264, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(639, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(264, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 406);
      this.lblProgress.Size = new System.Drawing.Size(903, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 427);
      this.progressBar.Size = new System.Drawing.Size(903, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(499, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(414, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(329, 7);
      // 
      // lbResultFiles
      // 
      this.lbResultFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbResultFiles.FormattingEnabled = true;
      this.lbResultFiles.ItemHeight = 12;
      this.lbResultFiles.Location = new System.Drawing.Point(35, 91);
      this.lbResultFiles.Name = "lbResultFiles";
      this.lbResultFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.lbResultFiles.Size = new System.Drawing.Size(751, 256);
      this.lbResultFiles.Sorted = true;
      this.lbResultFiles.TabIndex = 7;
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Location = new System.Drawing.Point(792, 297);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 8;
      this.btnAdd.Text = "&Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      // 
      // btnRemove
      // 
      this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemove.Location = new System.Drawing.Point(793, 326);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(75, 23);
      this.btnRemove.TabIndex = 9;
      this.btnRemove.Text = "&Remove";
      this.btnRemove.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(32, 70);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(341, 12);
      this.label1.TabIndex = 10;
      this.label1.Text = "Census Result Files (only selected files will be merged)";
      // 
      // CensusResultFileMerger2UI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(903, 484);
      this.Controls.Add(this.lbResultFiles);
      this.Controls.Add(this.btnRemove);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.label1);
      this.Name = "CensusResultFileMerger2UI";
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.btnAdd, 0);
      this.Controls.SetChildIndex(this.btnRemove, 0);
      this.Controls.SetChildIndex(this.lbResultFiles, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListBox lbResultFiles;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Label label1;
  }
}
