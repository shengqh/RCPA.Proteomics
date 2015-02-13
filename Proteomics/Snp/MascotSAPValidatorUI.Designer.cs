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
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(40, 24);
      this.pnlFile.Size = new System.Drawing.Size(882, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(245, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(637, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(245, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 574);
      this.lblProgress.Size = new System.Drawing.Size(1039, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 595);
      this.progressBar.Size = new System.Drawing.Size(1039, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(567, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(482, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(397, 7);
      // 
      // txtFastaFile
      // 
      this.txtFastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtFastaFile.Location = new System.Drawing.Point(286, 53);
      this.txtFastaFile.Name = "txtFastaFile";
      this.txtFastaFile.Size = new System.Drawing.Size(712, 21);
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
      this.cbAccessNumberPattern.Location = new System.Drawing.Point(286, 108);
      this.cbAccessNumberPattern.Name = "cbAccessNumberPattern";
      this.cbAccessNumberPattern.Size = new System.Drawing.Size(712, 20);
      this.cbAccessNumberPattern.TabIndex = 26;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(143, 111);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(137, 12);
      this.label8.TabIndex = 25;
      this.label8.Text = "Access number format :";
      // 
      // txtPattern
      // 
      this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPattern.Location = new System.Drawing.Point(286, 134);
      this.txtPattern.Name = "txtPattern";
      this.txtPattern.Size = new System.Drawing.Size(712, 21);
      this.txtPattern.TabIndex = 31;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(167, 137);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(113, 12);
      this.label3.TabIndex = 30;
      this.label3.Text = "Mutation pattern :";
      // 
      // btnFastaFile
      // 
      this.btnFastaFile.Location = new System.Drawing.Point(35, 53);
      this.btnFastaFile.Name = "btnFastaFile";
      this.btnFastaFile.Size = new System.Drawing.Size(245, 23);
      this.btnFastaFile.TabIndex = 32;
      this.btnFastaFile.Text = "button1";
      this.btnFastaFile.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(923, 24);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 34;
      this.btnLoad.Text = "&Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // ignoreNTerm
      // 
      this.ignoreNTerm.Key = "IgnoreNTermMutation";
      this.ignoreNTerm.Location = new System.Drawing.Point(286, 163);
      this.ignoreNTerm.Name = "ignoreNTerm";
      this.ignoreNTerm.PreCondition = null;
      this.ignoreNTerm.Size = new System.Drawing.Size(191, 21);
      this.ignoreNTerm.TabIndex = 37;
      this.ignoreNTerm.Text = "Ignore N Terminal Mutation";
      // 
      // ignoreDeamidatedMutation
      // 
      this.ignoreDeamidatedMutation.Key = "IgnoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.Location = new System.Drawing.Point(482, 164);
      this.ignoreDeamidatedMutation.Name = "ignoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.PreCondition = null;
      this.ignoreDeamidatedMutation.Size = new System.Drawing.Size(191, 21);
      this.ignoreDeamidatedMutation.TabIndex = 40;
      this.ignoreDeamidatedMutation.Text = "Ignore N->D, Q->E Mutation";
      // 
      // ignoreMultipleNucleotideMutation
      // 
      this.ignoreMultipleNucleotideMutation.Key = "KeepSingleNucleotideMutationOnly";
      this.ignoreMultipleNucleotideMutation.Location = new System.Drawing.Point(679, 165);
      this.ignoreMultipleNucleotideMutation.Name = "ignoreMultipleNucleotideMutation";
      this.ignoreMultipleNucleotideMutation.PreCondition = null;
      this.ignoreMultipleNucleotideMutation.Size = new System.Drawing.Size(260, 19);
      this.ignoreMultipleNucleotideMutation.TabIndex = 39;
      this.ignoreMultipleNucleotideMutation.Text = "Keep Single Nucleotide Mutation Only";
      // 
      // btnBrowse
      // 
      this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBrowse.Location = new System.Drawing.Point(923, 187);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 42;
      this.btnBrowse.Text = "button1";
      this.btnBrowse.UseVisualStyleBackColor = true;
      // 
      // txtDBFile
      // 
      this.txtDBFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDBFile.Location = new System.Drawing.Point(286, 187);
      this.txtDBFile.Name = "txtDBFile";
      this.txtDBFile.Size = new System.Drawing.Size(631, 21);
      this.txtDBFile.TabIndex = 41;
      // 
      // cbAnnotatedByDB
      // 
      this.cbAnnotatedByDB.Key = "DatabaseAnnotation";
      this.cbAnnotatedByDB.Location = new System.Drawing.Point(115, 187);
      this.cbAnnotatedByDB.Name = "cbAnnotatedByDB";
      this.cbAnnotatedByDB.PreCondition = null;
      this.cbAnnotatedByDB.Size = new System.Drawing.Size(165, 19);
      this.cbAnnotatedByDB.TabIndex = 43;
      this.cbAnnotatedByDB.Text = "Annotation by database";
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Description";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(40, 216);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.+)_(\\d){1,2}";
      this.pnlClassification.Size = new System.Drawing.Size(961, 337);
      this.pnlClassification.TabIndex = 33;
      // 
      // btnPnovoPeptide
      // 
      this.btnPnovoPeptide.Location = new System.Drawing.Point(35, 80);
      this.btnPnovoPeptide.Name = "btnPnovoPeptide";
      this.btnPnovoPeptide.Size = new System.Drawing.Size(245, 23);
      this.btnPnovoPeptide.TabIndex = 45;
      this.btnPnovoPeptide.Text = "button1";
      this.btnPnovoPeptide.UseVisualStyleBackColor = true;
      // 
      // txtPnovoPeptide
      // 
      this.txtPnovoPeptide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPnovoPeptide.Location = new System.Drawing.Point(286, 80);
      this.txtPnovoPeptide.Name = "txtPnovoPeptide";
      this.txtPnovoPeptide.Size = new System.Drawing.Size(712, 21);
      this.txtPnovoPeptide.TabIndex = 44;
      // 
      // MascotSnpValidatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1039, 652);
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
      this.Name = "MascotSnpValidatorUI";
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
