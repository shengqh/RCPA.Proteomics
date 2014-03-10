namespace RCPA.Proteomics.Snp
{
  partial class PNovoSnpValidatorUI
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
      this.components = new System.ComponentModel.Container();
      this.pNovoFiles = new RCPA.Gui.MultipleFileField();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.fastaFile = new RCPA.Gui.FileField();
      this.databaseFile = new RCPA.Gui.FileField();
      this.ignoreDeamidatedMutation = new RCPA.Gui.RcpaCheckField();
      this.label2 = new System.Windows.Forms.Label();
      this.ignoreMultipleNucleotideMutation = new RCPA.Gui.RcpaCheckField();
      this.ignoreNTerm = new RCPA.Gui.RcpaCheckField();
      this.txtThreadCount = new System.Windows.Forms.TextBox();
      this.lblThread = new System.Windows.Forms.Label();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cbProtease = new System.Windows.Forms.ComboBox();
      this.cbAccessNumberPattern = new System.Windows.Forms.ComboBox();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      this.pnlFile.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 573);
      this.pnlFile.Size = new System.Drawing.Size(1215, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(250, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(965, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(250, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 595);
      this.lblProgress.Size = new System.Drawing.Size(1215, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 616);
      this.progressBar.Size = new System.Drawing.Size(1215, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(655, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(570, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(485, 7);
      // 
      // pNovoFiles
      // 
      this.pNovoFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pNovoFiles.FileArgument = null;
      this.pNovoFiles.FileDescription = "pNovo Result Files";
      this.pNovoFiles.FileNames = new string[0];
      this.pNovoFiles.Key = "pNovoFiles";
      this.pNovoFiles.Location = new System.Drawing.Point(0, 0);
      this.pNovoFiles.Name = "pNovoFiles";
      this.pNovoFiles.SelectedIndex = -1;
      this.pNovoFiles.SelectedItem = null;
      this.pNovoFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.pNovoFiles.Size = new System.Drawing.Size(1215, 396);
      this.pNovoFiles.TabIndex = 13;
      this.pNovoFiles.ValidateSelectedItemOnly = true;
      // 
      // toolTip1
      // 
      this.toolTip1.ShowAlways = true;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.fastaFile);
      this.panel1.Controls.Add(this.databaseFile);
      this.panel1.Controls.Add(this.ignoreDeamidatedMutation);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.ignoreMultipleNucleotideMutation);
      this.panel1.Controls.Add(this.ignoreNTerm);
      this.panel1.Controls.Add(this.txtThreadCount);
      this.panel1.Controls.Add(this.lblThread);
      this.panel1.Controls.Add(this.txtMinScore);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.cbProtease);
      this.panel1.Controls.Add(this.cbAccessNumberPattern);
      this.panel1.Controls.Add(this.label8);
      this.panel1.Controls.Add(this.label7);
      this.panel1.Controls.Add(this.cbTitleFormat);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 396);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1215, 177);
      this.panel1.TabIndex = 41;
      // 
      // fastaFile
      // 
      this.fastaFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.fastaFile.FullName = "";
      this.fastaFile.Key = "FastaFile";
      this.fastaFile.Location = new System.Drawing.Point(3, 96);
      this.fastaFile.Name = "fastaFile";
      this.fastaFile.OpenButtonText = "Browse Fasta File ...";
      this.fastaFile.WidthOpenButton = 250;
      this.fastaFile.PreCondition = null;
      this.fastaFile.Size = new System.Drawing.Size(1201, 21);
      this.fastaFile.TabIndex = 57;
      // 
      // databaseFile
      // 
      this.databaseFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.databaseFile.FullName = "";
      this.databaseFile.Key = "DatabaseFile";
      this.databaseFile.Location = new System.Drawing.Point(2, 123);
      this.databaseFile.Name = "databaseFile";
      this.databaseFile.OpenButtonText = "Browse Target File ...";
      this.databaseFile.WidthOpenButton = 250;
      this.databaseFile.PreCondition = null;
      this.databaseFile.Size = new System.Drawing.Size(1201, 21);
      this.databaseFile.TabIndex = 56;
      // 
      // ignoreDeamidatedMutation
      // 
      this.ignoreDeamidatedMutation.Key = "IgnoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.Location = new System.Drawing.Point(447, 44);
      this.ignoreDeamidatedMutation.Name = "ignoreDeamidatedMutation";
      this.ignoreDeamidatedMutation.PreCondition = null;
      this.ignoreDeamidatedMutation.Size = new System.Drawing.Size(191, 21);
      this.ignoreDeamidatedMutation.TabIndex = 54;
      this.ignoreDeamidatedMutation.Text = "Ignore N->D, Q->E Mutation";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(137, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(107, 12);
      this.label2.TabIndex = 53;
      this.label2.Text = "Mutation filter :";
      // 
      // ignoreMultipleNucleotideMutation
      // 
      this.ignoreMultipleNucleotideMutation.Key = "KeepSingleNucleotideMutationOnly";
      this.ignoreMultipleNucleotideMutation.Location = new System.Drawing.Point(644, 45);
      this.ignoreMultipleNucleotideMutation.Name = "ignoreMultipleNucleotideMutation";
      this.ignoreMultipleNucleotideMutation.PreCondition = null;
      this.ignoreMultipleNucleotideMutation.Size = new System.Drawing.Size(260, 19);
      this.ignoreMultipleNucleotideMutation.TabIndex = 52;
      this.ignoreMultipleNucleotideMutation.Text = "Keep Single Nucleotide Mutation Only";
      // 
      // ignoreNTerm
      // 
      this.ignoreNTerm.Key = "RemoveNTerm";
      this.ignoreNTerm.Location = new System.Drawing.Point(250, 44);
      this.ignoreNTerm.Name = "ignoreNTerm";
      this.ignoreNTerm.PreCondition = null;
      this.ignoreNTerm.Size = new System.Drawing.Size(191, 21);
      this.ignoreNTerm.TabIndex = 51;
      this.ignoreNTerm.Text = "Remove N Terminal Mutation";
      // 
      // txtThreadCount
      // 
      this.txtThreadCount.Location = new System.Drawing.Point(747, 19);
      this.txtThreadCount.Name = "txtThreadCount";
      this.txtThreadCount.Size = new System.Drawing.Size(67, 21);
      this.txtThreadCount.TabIndex = 50;
      // 
      // lblThread
      // 
      this.lblThread.AutoSize = true;
      this.lblThread.Location = new System.Drawing.Point(652, 22);
      this.lblThread.Name = "lblThread";
      this.lblThread.Size = new System.Drawing.Size(89, 12);
      this.lblThread.TabIndex = 49;
      this.lblThread.Text = "Thread count :";
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(250, 17);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(75, 21);
      this.txtMinScore.TabIndex = 48;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(155, 21);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(89, 12);
      this.label3.TabIndex = 47;
      this.label3.Text = "Minmum score :";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(357, 22);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(65, 12);
      this.label1.TabIndex = 46;
      this.label1.Text = "Protease :";
      // 
      // cbProtease
      // 
      this.cbProtease.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbProtease.FormattingEnabled = true;
      this.cbProtease.Location = new System.Drawing.Point(428, 18);
      this.cbProtease.Name = "cbProtease";
      this.cbProtease.Size = new System.Drawing.Size(188, 20);
      this.cbProtease.TabIndex = 45;
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
      this.cbAccessNumberPattern.Location = new System.Drawing.Point(250, 150);
      this.cbAccessNumberPattern.Name = "cbAccessNumberPattern";
      this.cbAccessNumberPattern.Size = new System.Drawing.Size(953, 20);
      this.cbAccessNumberPattern.TabIndex = 44;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(113, 153);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(137, 12);
      this.label8.TabIndex = 43;
      this.label8.Text = "Access number format :";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(161, 73);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(89, 12);
      this.label7.TabIndex = 42;
      this.label7.Text = "Title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(250, 70);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(953, 20);
      this.cbTitleFormat.TabIndex = 41;
      // 
      // PNovoSnpValidatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1215, 673);
      this.Controls.Add(this.pNovoFiles);
      this.Controls.Add(this.panel1);
      this.Name = "PNovoSnpValidatorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.panel1, 0);
      this.Controls.SetChildIndex(this.pNovoFiles, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.MultipleFileField pNovoFiles;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Panel panel1;
    private Gui.RcpaCheckField ignoreDeamidatedMutation;
    private System.Windows.Forms.Label label2;
    private Gui.RcpaCheckField ignoreMultipleNucleotideMutation;
    private Gui.RcpaCheckField ignoreNTerm;
    private System.Windows.Forms.TextBox txtThreadCount;
    private System.Windows.Forms.Label lblThread;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbProtease;
    private System.Windows.Forms.ComboBox cbAccessNumberPattern;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.ComboBox cbTitleFormat;
    private Gui.FileField fastaFile;
    private Gui.FileField databaseFile;
  }
}
