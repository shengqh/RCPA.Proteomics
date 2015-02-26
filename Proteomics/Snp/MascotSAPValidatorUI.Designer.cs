namespace RCPA.Proteomics.Snp
{
  partial class MascotSAPValidatorUI
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
      this.txtFastaFile = new System.Windows.Forms.TextBox();
      this.cbAccessNumberPattern = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.txtPattern = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnFastaFile = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.ignoreNTerm = new RCPA.Gui.RcpaCheckField();
      this.ignoreDeamidatedMutation = new RCPA.Gui.RcpaCheckField();
      this.ignoreMultipleNucleotideMutation = new RCPA.Gui.RcpaCheckField();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.txtDBFile = new System.Windows.Forms.TextBox();
      this.cbAnnotatedByDB = new RCPA.Gui.RcpaCheckField();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.btnPnovoPeptide = new System.Windows.Forms.Button();
      this.txtPnovoPeptide = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(35, 26);
      this.pnlFile.Size = new System.Drawing.Size(887, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(245, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(642, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(245, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 621);
      this.lblProgress.Size = new System.Drawing.Size(1039, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 644);
      this.progressBar.Size = new System.Drawing.Size(1039, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 667);
      this.pnlButton.Size = new System.Drawing.Size(1039, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(567, 9);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(482, 9);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(397, 9);
      // 
      // txtFastaFile
      // 
      this.txtFastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFastaFile.Location = new System.Drawing.Point(280, 57);
      this.txtFastaFile.Name = "txtFastaFile";
      this.txtFastaFile.Size = new System.Drawing.Size(718, 20);
      this.txtFastaFile.TabIndex = 12;
      // 
      // cbAccessNumberPattern
      // 
      this.cbAccessNumberPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbAccessNumberPattern.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbAccessNumberPattern.FormattingEnabled = true;
      this.cbAccessNumberPattern.Items.AddRange(new object[] {
            "(IPI\\d+)",
            "(gi:\\d+)",
            "(IPI\\d+|gi:\\d+)"});
      this.cbAccessNumberPattern.Location = new System.Drawing.Point(280, 117);
      this.cbAccessNumberPattern.Name = "cbAccessNumberPattern";
      this.cbAccessNumberPattern.Size = new System.Drawing.Size(718, 21);
      this.cbAccessNumberPattern.TabIndex = 26;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(156, 120);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(118, 13);
      this.label8.TabIndex = 25;
      this.label8.Text = "Access number format :";
      // 
      // txtPattern
      // 
      this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPattern.Location = new System.Drawing.Point(280, 145);
      this.txtPattern.Name = "txtPattern";
      this.txtPattern.Size = new System.Drawing.Size(718, 20);
      this.txtPattern.TabIndex = 31;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(184, 148);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(90, 13);
      this.label3.TabIndex = 30;
      this.label3.Text = "Mutation pattern :";
      // 
      // btnFastaFile
      // 
      this.btnFastaFile.Location = new System.Drawing.Point(35, 54);
      this.btnFastaFile.Name = "btnFastaFile";
      this.btnFastaFile.Size = new System.Drawing.Size(245, 25);
      this.btnFastaFile.TabIndex = 32;
      this.btnFastaFile.Text = "button1";
      this.btnFastaFile.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(923, 26);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 25);
      this.btnLoad.TabIndex = 34;
      this.btnLoad.Text = "&Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // ignoreNTerm
      // 
      this.ignoreNTerm.Key = "IgnoreNTermMutation";
      this.ignoreNTerm.Location = new System.Drawing.Point(281, 178);
      this.ignoreNTerm.Name = "ignoreNTerm";
      this.ignoreNTerm.PreCondition = null;
      this.ignoreNTerm.Size = new System.Drawing.Size(191, 23);
      this.ignoreNTerm.TabIndex = 37;
      this.ignoreNTerm.Text = "Ignore N Terminal Mutation";
      // 
      // ignoreDeamidatedMutation
      // 
      this.ignoreDeamidatedMutation.Key = "IgnoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.Location = new System.Drawing.Point(482, 178);
      this.ignoreDeamidatedMutation.Name = "ignoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.PreCondition = null;
      this.ignoreDeamidatedMutation.Size = new System.Drawing.Size(191, 23);
      this.ignoreDeamidatedMutation.TabIndex = 40;
      this.ignoreDeamidatedMutation.Text = "Ignore N->D, Q->E Mutation";
      // 
      // ignoreMultipleNucleotideMutation
      // 
      this.ignoreMultipleNucleotideMutation.Key = "KeepSingleNucleotideMutationOnly";
      this.ignoreMultipleNucleotideMutation.Location = new System.Drawing.Point(679, 179);
      this.ignoreMultipleNucleotideMutation.Name = "ignoreMultipleNucleotideMutation";
      this.ignoreMultipleNucleotideMutation.PreCondition = null;
      this.ignoreMultipleNucleotideMutation.Size = new System.Drawing.Size(260, 21);
      this.ignoreMultipleNucleotideMutation.TabIndex = 39;
      this.ignoreMultipleNucleotideMutation.Text = "Keep Single Nucleotide Mutation Only";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBrowse.Location = new System.Drawing.Point(923, 203);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 25);
      this.btnBrowse.TabIndex = 42;
      this.btnBrowse.Text = "button1";
      this.btnBrowse.UseVisualStyleBackColor = true;
      // 
      // txtDBFile
      // 
      this.txtDBFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDBFile.Location = new System.Drawing.Point(281, 203);
      this.txtDBFile.Name = "txtDBFile";
      this.txtDBFile.Size = new System.Drawing.Size(636, 20);
      this.txtDBFile.TabIndex = 41;
      // 
      // cbAnnotatedByDB
      // 
      this.cbAnnotatedByDB.Key = "DatabaseAnnotation";
      this.cbAnnotatedByDB.Location = new System.Drawing.Point(135, 203);
      this.cbAnnotatedByDB.Name = "cbAnnotatedByDB";
      this.cbAnnotatedByDB.PreCondition = null;
      this.cbAnnotatedByDB.Size = new System.Drawing.Size(145, 21);
      this.cbAnnotatedByDB.TabIndex = 43;
      this.cbAnnotatedByDB.Text = "Annotation by database :";
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Description";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(40, 234);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.+)_(\\d){1,2}";
      this.pnlClassification.Size = new System.Drawing.Size(961, 365);
      this.pnlClassification.TabIndex = 33;
      // 
      // btnPnovoPeptide
      // 
      this.btnPnovoPeptide.Location = new System.Drawing.Point(35, 84);
      this.btnPnovoPeptide.Name = "btnPnovoPeptide";
      this.btnPnovoPeptide.Size = new System.Drawing.Size(245, 25);
      this.btnPnovoPeptide.TabIndex = 45;
      this.btnPnovoPeptide.Text = "button1";
      this.btnPnovoPeptide.UseVisualStyleBackColor = true;
      // 
      // txtPnovoPeptide
      // 
      this.txtPnovoPeptide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPnovoPeptide.Location = new System.Drawing.Point(280, 87);
      this.txtPnovoPeptide.Name = "txtPnovoPeptide";
      this.txtPnovoPeptide.Size = new System.Drawing.Size(718, 20);
      this.txtPnovoPeptide.TabIndex = 44;
      // 
      // MascotSAPValidatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1039, 706);
      this.Controls.Add(this.btnPnovoPeptide);
      this.Controls.Add(this.txtPnovoPeptide);
      this.Controls.Add(this.cbAnnotatedByDB);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.txtDBFile);
      this.Controls.Add(this.ignoreDeamidatedMutation);
      this.Controls.Add(this.ignoreMultipleNucleotideMutation);
      this.Controls.Add(this.ignoreNTerm);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.btnFastaFile);
      this.Controls.Add(this.txtPattern);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cbAccessNumberPattern);
      this.Controls.Add(this.label8);
      this.Controls.Add(this.txtFastaFile);
      this.Name = "MascotSAPValidatorUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtFastaFile, 0);
      this.Controls.SetChildIndex(this.label8, 0);
      this.Controls.SetChildIndex(this.cbAccessNumberPattern, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.txtPattern, 0);
      this.Controls.SetChildIndex(this.btnFastaFile, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.ignoreNTerm, 0);
      this.Controls.SetChildIndex(this.ignoreMultipleNucleotideMutation, 0);
      this.Controls.SetChildIndex(this.ignoreDeamidatedMutation, 0);
      this.Controls.SetChildIndex(this.txtDBFile, 0);
      this.Controls.SetChildIndex(this.btnBrowse, 0);
      this.Controls.SetChildIndex(this.cbAnnotatedByDB, 0);
      this.Controls.SetChildIndex(this.txtPnovoPeptide, 0);
      this.Controls.SetChildIndex(this.btnPnovoPeptide, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtFastaFile;
    private System.Windows.Forms.ComboBox cbAccessNumberPattern;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtPattern;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnFastaFile;
    private ClassificationPanel pnlClassification;
    private System.Windows.Forms.Button btnLoad;
    private Gui.RcpaCheckField ignoreNTerm;
    private Gui.RcpaCheckField ignoreDeamidatedMutation;
    private Gui.RcpaCheckField ignoreMultipleNucleotideMutation;
    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox txtDBFile;
    private Gui.RcpaCheckField cbAnnotatedByDB;
    private System.Windows.Forms.Button btnPnovoPeptide;
    private System.Windows.Forms.TextBox txtPnovoPeptide;
  }
}
