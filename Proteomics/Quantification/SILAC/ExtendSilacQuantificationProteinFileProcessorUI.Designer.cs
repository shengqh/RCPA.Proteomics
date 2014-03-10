namespace RCPA.Proteomics.Quantification.SILAC
{
  partial class ExtendSilacQuantificationProteinFileProcessorUI
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
      this.btnRawDirectory = new System.Windows.Forms.Button();
      this.txtRawDirectory = new System.Windows.Forms.TextBox();
      this.txtSilacFile = new System.Windows.Forms.TextBox();
      this.txtPrecursorTolerance = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnSilacFile = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.cbSearchEngine = new System.Windows.Forms.ComboBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMinPepRegCorrelation = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtProfileLength = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.btnLoad = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.datasetClassification = new RCPA.Proteomics.ClassificationPanel();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.rawPairClassification = new RCPA.Proteomics.ClassificationPanel();
      this.btnLoadParam = new System.Windows.Forms.Button();
      this.btnSaveParam = new System.Windows.Forms.Button();
      this.pnlFile.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(35, 18);
      this.pnlFile.Size = new System.Drawing.Size(676, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(244, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(432, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(244, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 546);
      this.lblProgress.Size = new System.Drawing.Size(828, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 567);
      this.progressBar.Size = new System.Drawing.Size(828, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(462, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(377, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(292, 7);
      // 
      // btnRawDirectory
      // 
      this.btnRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnRawDirectory.Location = new System.Drawing.Point(34, 302);
      this.btnRawDirectory.Name = "btnRawDirectory";
      this.btnRawDirectory.Size = new System.Drawing.Size(243, 21);
      this.btnRawDirectory.TabIndex = 7;
      this.btnRawDirectory.Text = "Raw Directory";
      this.btnRawDirectory.UseVisualStyleBackColor = true;
      // 
      // txtRawDirectory
      // 
      this.txtRawDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawDirectory.Location = new System.Drawing.Point(284, 303);
      this.txtRawDirectory.Name = "txtRawDirectory";
      this.txtRawDirectory.Size = new System.Drawing.Size(509, 21);
      this.txtRawDirectory.TabIndex = 8;
      // 
      // txtSilacFile
      // 
      this.txtSilacFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSilacFile.Location = new System.Drawing.Point(284, 358);
      this.txtSilacFile.Name = "txtSilacFile";
      this.txtSilacFile.Size = new System.Drawing.Size(509, 21);
      this.txtSilacFile.TabIndex = 10;
      // 
      // txtPrecursorTolerance
      // 
      this.txtPrecursorTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPrecursorTolerance.Location = new System.Drawing.Point(284, 387);
      this.txtPrecursorTolerance.Name = "txtPrecursorTolerance";
      this.txtPrecursorTolerance.Size = new System.Drawing.Size(509, 21);
      this.txtPrecursorTolerance.TabIndex = 12;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(134, 390);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(143, 12);
      this.label2.TabIndex = 11;
      this.label2.Text = "Precursor PPM Tolerance";
      // 
      // btnSilacFile
      // 
      this.btnSilacFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSilacFile.Location = new System.Drawing.Point(34, 357);
      this.btnSilacFile.Name = "btnSilacFile";
      this.btnSilacFile.Size = new System.Drawing.Size(243, 21);
      this.btnSilacFile.TabIndex = 7;
      this.btnSilacFile.Text = "Silac File";
      this.btnSilacFile.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(194, 417);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(83, 12);
      this.label1.TabIndex = 13;
      this.label1.Text = "Search Engine";
      // 
      // cbSearchEngine
      // 
      this.cbSearchEngine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbSearchEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbSearchEngine.FormattingEnabled = true;
      this.cbSearchEngine.Location = new System.Drawing.Point(284, 414);
      this.cbSearchEngine.Name = "cbSearchEngine";
      this.cbSearchEngine.Size = new System.Drawing.Size(509, 20);
      this.cbSearchEngine.TabIndex = 14;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(284, 440);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(509, 21);
      this.textBox1.TabIndex = 16;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(158, 443);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(119, 12);
      this.label3.TabIndex = 15;
      this.label3.Text = "Ignore modification";
      // 
      // comboBox1
      // 
      this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new System.Drawing.Point(284, 330);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(509, 20);
      this.comboBox1.TabIndex = 18;
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(212, 333);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(65, 12);
      this.label4.TabIndex = 17;
      this.label4.Text = "Raw Format";
      // 
      // txtMinPepRegCorrelation
      // 
      this.txtMinPepRegCorrelation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinPepRegCorrelation.Location = new System.Drawing.Point(284, 467);
      this.txtMinPepRegCorrelation.Name = "txtMinPepRegCorrelation";
      this.txtMinPepRegCorrelation.Size = new System.Drawing.Size(509, 21);
      this.txtMinPepRegCorrelation.TabIndex = 20;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(45, 470);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(233, 12);
      this.label5.TabIndex = 19;
      this.label5.Text = "Minimum peptide regression correlation";
      // 
      // txtProfileLength
      // 
      this.txtProfileLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtProfileLength.Location = new System.Drawing.Point(284, 494);
      this.txtProfileLength.Name = "txtProfileLength";
      this.txtProfileLength.Size = new System.Drawing.Size(509, 21);
      this.txtProfileLength.TabIndex = 22;
      this.txtProfileLength.Text = "3";
      // 
      // label6
      // 
      this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(188, 497);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(89, 12);
      this.label6.TabIndex = 21;
      this.label6.Text = "Profile length";
      // 
      // btnLoad
      // 
      this.btnLoad.Location = new System.Drawing.Point(718, 17);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 23;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Location = new System.Drawing.Point(35, 47);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(758, 250);
      this.tabControl1.TabIndex = 24;
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage1.Controls.Add(this.datasetClassification);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(750, 224);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Data sets";
      // 
      // datasetClassification
      // 
      this.datasetClassification.BackColor = System.Drawing.SystemColors.Control;
      this.datasetClassification.Description = "The protein ratio will be calculated from the peptide ratios in same dataset.";
      this.datasetClassification.Dock = System.Windows.Forms.DockStyle.Fill;
      this.datasetClassification.GetName = null;
      this.datasetClassification.Location = new System.Drawing.Point(3, 3);
      this.datasetClassification.Name = "datasetClassification";
      this.datasetClassification.Pattern = "(.*)";
      this.datasetClassification.Size = new System.Drawing.Size(744, 218);
      this.datasetClassification.TabIndex = 0;
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage2.Controls.Add(this.rawPairClassification);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(750, 224);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Raw pairs";
      // 
      // rawPairClassification
      // 
      this.rawPairClassification.BackColor = System.Drawing.SystemColors.Control;
      this.rawPairClassification.Description = "The quantification process will be extended to other raw files based on the raw p" +
    "airs";
      this.rawPairClassification.Dock = System.Windows.Forms.DockStyle.Fill;
      this.rawPairClassification.GetName = null;
      this.rawPairClassification.Location = new System.Drawing.Point(3, 3);
      this.rawPairClassification.Name = "rawPairClassification";
      this.rawPairClassification.Pattern = "(.*)";
      this.rawPairClassification.Size = new System.Drawing.Size(744, 218);
      this.rawPairClassification.TabIndex = 0;
      // 
      // btnLoadParam
      // 
      this.btnLoadParam.Location = new System.Drawing.Point(552, 583);
      this.btnLoadParam.Name = "btnLoadParam";
      this.btnLoadParam.Size = new System.Drawing.Size(75, 21);
      this.btnLoadParam.TabIndex = 25;
      this.btnLoadParam.Text = "&Load Param";
      this.btnLoadParam.UseVisualStyleBackColor = true;
      this.btnLoadParam.Click += new System.EventHandler(this.btnLoadParam_Click);
      // 
      // btnSaveParam
      // 
      this.btnSaveParam.Location = new System.Drawing.Point(636, 583);
      this.btnSaveParam.Name = "btnSaveParam";
      this.btnSaveParam.Size = new System.Drawing.Size(75, 21);
      this.btnSaveParam.TabIndex = 26;
      this.btnSaveParam.Text = "&Save Param";
      this.btnSaveParam.UseVisualStyleBackColor = true;
      this.btnSaveParam.Click += new System.EventHandler(this.btnSaveParam_Click);
      // 
      // ExtendSilacQuantificationProteinFileProcessorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(828, 624);
      this.Controls.Add(this.btnSaveParam);
      this.Controls.Add(this.btnLoadParam);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.txtProfileLength);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txtMinPepRegCorrelation);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cbSearchEngine);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtPrecursorTolerance);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnSilacFile);
      this.Controls.Add(this.btnRawDirectory);
      this.Controls.Add(this.txtRawDirectory);
      this.Controls.Add(this.txtSilacFile);
      this.Name = "ExtendSilacQuantificationProteinFileProcessorUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtSilacFile, 0);
      this.Controls.SetChildIndex(this.txtRawDirectory, 0);
      this.Controls.SetChildIndex(this.btnRawDirectory, 0);
      this.Controls.SetChildIndex(this.btnSilacFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPrecursorTolerance, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbSearchEngine, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.comboBox1, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.txtMinPepRegCorrelation, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.txtProfileLength, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.Controls.SetChildIndex(this.btnLoadParam, 0);
      this.Controls.SetChildIndex(this.btnSaveParam, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnRawDirectory;
    private System.Windows.Forms.TextBox txtRawDirectory;
    private System.Windows.Forms.TextBox txtSilacFile;
    private System.Windows.Forms.TextBox txtPrecursorTolerance;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnSilacFile;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbSearchEngine;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMinPepRegCorrelation;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtProfileLength;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private ClassificationPanel datasetClassification;
    private System.Windows.Forms.TabPage tabPage2;
    private ClassificationPanel rawPairClassification;
    private System.Windows.Forms.Button btnLoadParam;
    private System.Windows.Forms.Button btnSaveParam;
  }
}
