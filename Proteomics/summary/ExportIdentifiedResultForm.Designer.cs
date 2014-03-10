namespace RCPA.Proteomics.Summary
{
  partial class ExportIdentifiedResultForm
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.proteinColumns = new RCPA.RcpaListBox();
      this.peptideColumns = new RCPA.RcpaListBox();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Panel2.Controls.Add(this.btnOk);
      this.splitContainer1.Size = new System.Drawing.Size(904, 482);
      this.splitContainer1.SplitterDistance = 372;
      this.splitContainer1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.proteinColumns);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.peptideColumns);
      this.splitContainer2.Size = new System.Drawing.Size(904, 372);
      this.splitContainer2.SplitterDistance = 448;
      this.splitContainer2.TabIndex = 0;
      // 
      // proteinColumns
      // 
      this.proteinColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.proteinColumns.Location = new System.Drawing.Point(0, 0);
      this.proteinColumns.Name = "proteinColumns";
      this.proteinColumns.Size = new System.Drawing.Size(448, 372);
      this.proteinColumns.TabIndex = 0;
      this.proteinColumns.Title = "Protein Columns";
      // 
      // peptideColumns
      // 
      this.peptideColumns.Dock = System.Windows.Forms.DockStyle.Fill;
      this.peptideColumns.Location = new System.Drawing.Point(0, 0);
      this.peptideColumns.Name = "peptideColumns";
      this.peptideColumns.Size = new System.Drawing.Size(452, 372);
      this.peptideColumns.TabIndex = 0;
      this.peptideColumns.Title = "Peptide Columns";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(475, 71);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(357, 71);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // ExportIdentifiedResultForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(904, 482);
      this.Controls.Add(this.splitContainer1);
      this.Name = "ExportIdentifiedResultForm";
      this.Text = "ExportIdentifiedResultForm";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private RcpaListBox proteinColumns;
    private RcpaListBox peptideColumns;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;

  }
}