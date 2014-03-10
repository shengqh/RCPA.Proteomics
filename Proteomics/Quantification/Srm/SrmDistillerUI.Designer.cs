namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmDistillerUI
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
      this.outsGroup = new System.Windows.Forms.GroupBox();
      this.btnAddTag = new System.Windows.Forms.Button();
      this.btnAddDirectory = new System.Windows.Forms.Button();
      this.btnAddSubdirectory = new System.Windows.Forms.Button();
      this.lvDirectories = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnAddFiles = new System.Windows.Forms.Button();
      this.btnRemove = new System.Windows.Forms.Button();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.cbRefinePeakPicking = new System.Windows.Forms.CheckBox();
      this.cbRatioByArea = new System.Windows.Forms.CheckBox();
      this.txtBaselinePercentage = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.cbBaseLineExtraction = new System.Windows.Forms.CheckBox();
      this.cbSmooth = new System.Windows.Forms.CheckBox();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.txtHighestPeakPercentage = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.rbHighestPeak = new System.Windows.Forms.RadioButton();
      this.rbBaseline = new System.Windows.Forms.RadioButton();
      this.txtSignalToNoise = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.btnNewSample = new System.Windows.Forms.Button();
      this.btnNew = new System.Windows.Forms.Button();
      this.btnEdit = new System.Windows.Forms.Button();
      this.txtPrecursorTolerance = new System.Windows.Forms.TextBox();
      this.rbClusterByRealData = new System.Windows.Forms.RadioButton();
      this.txtRetentionTimeTolerance = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cbFormat = new System.Windows.Forms.ComboBox();
      this.cbDecoyPattern = new System.Windows.Forms.CheckBox();
      this.txtMaxPrecursorDistance = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.txtMzTolerance = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtDecoyPattern = new System.Windows.Forms.TextBox();
      this.txtRtToleranceBetweenRealAndPredefined = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtPredefinedFile = new System.Windows.Forms.TextBox();
      this.btnPredefined = new System.Windows.Forms.Button();
      this.rbClusterByPredefine = new System.Windows.Forms.RadioButton();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.txtMinEnabledScan = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.txtValidateCorrel = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtValidateSignalToNoise = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMinValidTransactionPair = new System.Windows.Forms.TextBox();
      this.label8 = new System.Windows.Forms.Label();
      this.dlgSave = new System.Windows.Forms.SaveFileDialog();
      this.pnlFile.SuspendLayout();
      this.outsGroup.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      this.tabPage1.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.pnlFile.Location = new System.Drawing.Point(0, 453);
      this.pnlFile.Size = new System.Drawing.Size(1062, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(816, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 704);
      this.lblProgress.Size = new System.Drawing.Size(1062, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 725);
      this.progressBar.Size = new System.Drawing.Size(1062, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(579, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(494, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(409, 7);
      // 
      // outsGroup
      // 
      this.outsGroup.Controls.Add(this.btnAddTag);
      this.outsGroup.Controls.Add(this.btnAddDirectory);
      this.outsGroup.Controls.Add(this.btnAddSubdirectory);
      this.outsGroup.Controls.Add(this.lvDirectories);
      this.outsGroup.Controls.Add(this.btnSave);
      this.outsGroup.Controls.Add(this.btnLoad);
      this.outsGroup.Controls.Add(this.btnAddFiles);
      this.outsGroup.Controls.Add(this.btnRemove);
      this.outsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
      this.outsGroup.Location = new System.Drawing.Point(0, 0);
      this.outsGroup.Name = "outsGroup";
      this.outsGroup.Size = new System.Drawing.Size(1062, 453);
      this.outsGroup.TabIndex = 51;
      this.outsGroup.TabStop = false;
      this.outsGroup.Text = "Select mzXml/mzData/Thermo raw files or Agilent .d directories (only selected ite" +
    "ms will be processed)";
      // 
      // btnAddTag
      // 
      this.btnAddTag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddTag.Location = new System.Drawing.Point(921, 200);
      this.btnAddTag.Name = "btnAddTag";
      this.btnAddTag.Size = new System.Drawing.Size(135, 23);
      this.btnAddTag.TabIndex = 18;
      this.btnAddTag.Text = "Set as same group";
      this.btnAddTag.UseVisualStyleBackColor = true;
      this.btnAddTag.Click += new System.EventHandler(this.btnAddTag_Click);
      // 
      // btnAddDirectory
      // 
      this.btnAddDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddDirectory.Location = new System.Drawing.Point(921, 55);
      this.btnAddDirectory.Name = "btnAddDirectory";
      this.btnAddDirectory.Size = new System.Drawing.Size(135, 23);
      this.btnAddDirectory.TabIndex = 16;
      this.btnAddDirectory.Text = "Add directory";
      this.btnAddDirectory.UseVisualStyleBackColor = true;
      // 
      // btnAddSubdirectory
      // 
      this.btnAddSubdirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddSubdirectory.Location = new System.Drawing.Point(921, 84);
      this.btnAddSubdirectory.Name = "btnAddSubdirectory";
      this.btnAddSubdirectory.Size = new System.Drawing.Size(135, 23);
      this.btnAddSubdirectory.TabIndex = 17;
      this.btnAddSubdirectory.Text = "Add sub directories";
      this.btnAddSubdirectory.UseVisualStyleBackColor = true;
      // 
      // lvDirectories
      // 
      this.lvDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lvDirectories.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
      this.lvDirectories.FullRowSelect = true;
      this.lvDirectories.HideSelection = false;
      this.lvDirectories.Location = new System.Drawing.Point(6, 26);
      this.lvDirectories.Name = "lvDirectories";
      this.lvDirectories.Size = new System.Drawing.Size(909, 421);
      this.lvDirectories.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvDirectories.TabIndex = 15;
      this.lvDirectories.UseCompatibleStateImageBehavior = false;
      this.lvDirectories.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Files or Directories";
      this.columnHeader1.Width = 670;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "Group Name";
      this.columnHeader2.Width = 121;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(921, 171);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(135, 23);
      this.btnSave.TabIndex = 14;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(921, 142);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(135, 23);
      this.btnLoad.TabIndex = 13;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      // 
      // btnAddFiles
      // 
      this.btnAddFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddFiles.Location = new System.Drawing.Point(921, 26);
      this.btnAddFiles.Name = "btnAddFiles";
      this.btnAddFiles.Size = new System.Drawing.Size(135, 23);
      this.btnAddFiles.TabIndex = 11;
      this.btnAddFiles.Text = "Add files";
      this.btnAddFiles.UseVisualStyleBackColor = true;
      // 
      // btnRemove
      // 
      this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemove.Location = new System.Drawing.Point(921, 113);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(135, 23);
      this.btnRemove.TabIndex = 12;
      this.btnRemove.Text = "Remove";
      this.btnRemove.UseVisualStyleBackColor = true;
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage3);
      this.tabControl1.Controls.Add(this.tabPage4);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.tabControl1.Location = new System.Drawing.Point(0, 475);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1062, 229);
      this.tabControl1.TabIndex = 54;
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage2.Controls.Add(this.cbRefinePeakPicking);
      this.tabPage2.Controls.Add(this.cbRatioByArea);
      this.tabPage2.Controls.Add(this.txtBaselinePercentage);
      this.tabPage2.Controls.Add(this.label9);
      this.tabPage2.Controls.Add(this.cbBaseLineExtraction);
      this.tabPage2.Controls.Add(this.cbSmooth);
      this.tabPage2.Location = new System.Drawing.Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(1054, 203);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Global parameters";
      // 
      // cbRefinePeakPicking
      // 
      this.cbRefinePeakPicking.AutoSize = true;
      this.cbRefinePeakPicking.Location = new System.Drawing.Point(18, 83);
      this.cbRefinePeakPicking.Name = "cbRefinePeakPicking";
      this.cbRefinePeakPicking.Size = new System.Drawing.Size(300, 16);
      this.cbRefinePeakPicking.TabIndex = 61;
      this.cbRefinePeakPicking.Text = "Refine peak picking by linear regression model";
      this.cbRefinePeakPicking.UseVisualStyleBackColor = true;
      // 
      // cbRatioByArea
      // 
      this.cbRatioByArea.AutoSize = true;
      this.cbRatioByArea.Location = new System.Drawing.Point(18, 61);
      this.cbRatioByArea.Name = "cbRatioByArea";
      this.cbRatioByArea.Size = new System.Drawing.Size(162, 16);
      this.cbRatioByArea.TabIndex = 60;
      this.cbRatioByArea.Text = "Calculate ratio by area";
      this.cbRatioByArea.UseVisualStyleBackColor = true;
      // 
      // txtBaselinePercentage
      // 
      this.txtBaselinePercentage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtBaselinePercentage.Location = new System.Drawing.Point(231, 104);
      this.txtBaselinePercentage.Name = "txtBaselinePercentage";
      this.txtBaselinePercentage.Size = new System.Drawing.Size(100, 21);
      this.txtBaselinePercentage.TabIndex = 59;
      this.txtBaselinePercentage.Text = "5";
      this.txtBaselinePercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(17, 107);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(317, 12);
      this.label9.TabIndex = 58;
      this.label9.Text = "Calculate baseline from the lowest        % of peaks";
      // 
      // cbBaseLineExtraction
      // 
      this.cbBaseLineExtraction.AutoSize = true;
      this.cbBaseLineExtraction.Location = new System.Drawing.Point(18, 39);
      this.cbBaseLineExtraction.Name = "cbBaseLineExtraction";
      this.cbBaseLineExtraction.Size = new System.Drawing.Size(252, 16);
      this.cbBaseLineExtraction.TabIndex = 55;
      this.cbBaseLineExtraction.Text = "Deduct base line for ratio calculation";
      this.cbBaseLineExtraction.UseVisualStyleBackColor = true;
      // 
      // cbSmooth
      // 
      this.cbSmooth.AutoSize = true;
      this.cbSmooth.Checked = true;
      this.cbSmooth.CheckState = System.Windows.Forms.CheckState.Checked;
      this.cbSmooth.Location = new System.Drawing.Point(18, 16);
      this.cbSmooth.Name = "cbSmooth";
      this.cbSmooth.Size = new System.Drawing.Size(324, 16);
      this.cbSmooth.TabIndex = 54;
      this.cbSmooth.Text = "Smooth peak using Savitzky Golay 5 Point algorithm";
      this.cbSmooth.UseVisualStyleBackColor = true;
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage1.Controls.Add(this.txtHighestPeakPercentage);
      this.tabPage1.Controls.Add(this.label10);
      this.tabPage1.Controls.Add(this.rbHighestPeak);
      this.tabPage1.Controls.Add(this.rbBaseline);
      this.tabPage1.Controls.Add(this.txtSignalToNoise);
      this.tabPage1.Controls.Add(this.label2);
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(1054, 203);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Peak picking";
      // 
      // txtHighestPeakPercentage
      // 
      this.txtHighestPeakPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtHighestPeakPercentage.Location = new System.Drawing.Point(281, 117);
      this.txtHighestPeakPercentage.Name = "txtHighestPeakPercentage";
      this.txtHighestPeakPercentage.Size = new System.Drawing.Size(34, 21);
      this.txtHighestPeakPercentage.TabIndex = 60;
      this.txtHighestPeakPercentage.Text = "5";
      this.txtHighestPeakPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(35, 120);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(389, 12);
      this.label10.TabIndex = 59;
      this.label10.Text = "Select peaks whose intensity larger than       % of highest peak";
      // 
      // rbHighestPeak
      // 
      this.rbHighestPeak.AutoSize = true;
      this.rbHighestPeak.Location = new System.Drawing.Point(19, 93);
      this.rbHighestPeak.Name = "rbHighestPeak";
      this.rbHighestPeak.Size = new System.Drawing.Size(113, 16);
      this.rbHighestPeak.TabIndex = 58;
      this.rbHighestPeak.Text = "By highest peak";
      this.rbHighestPeak.UseVisualStyleBackColor = true;
      this.rbHighestPeak.Click += new System.EventHandler(this.rbHighestPeak_Click);
      // 
      // rbBaseline
      // 
      this.rbBaseline.AutoSize = true;
      this.rbBaseline.Checked = true;
      this.rbBaseline.Location = new System.Drawing.Point(19, 17);
      this.rbBaseline.Name = "rbBaseline";
      this.rbBaseline.Size = new System.Drawing.Size(137, 16);
      this.rbBaseline.TabIndex = 55;
      this.rbBaseline.TabStop = true;
      this.rbBaseline.Text = "By baseline (noise)";
      this.rbBaseline.UseVisualStyleBackColor = true;
      this.rbBaseline.Click += new System.EventHandler(this.rbBaseline_Click);
      // 
      // txtSignalToNoise
      // 
      this.txtSignalToNoise.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSignalToNoise.Location = new System.Drawing.Point(286, 40);
      this.txtSignalToNoise.Name = "txtSignalToNoise";
      this.txtSignalToNoise.Size = new System.Drawing.Size(48, 21);
      this.txtSignalToNoise.TabIndex = 48;
      this.txtSignalToNoise.Text = "3";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(35, 43);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(245, 12);
      this.label2.TabIndex = 47;
      this.label2.Text = "Mininum signal to noise for peak picking";
      // 
      // tabPage3
      // 
      this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage3.Controls.Add(this.btnNewSample);
      this.tabPage3.Controls.Add(this.btnNew);
      this.tabPage3.Controls.Add(this.btnEdit);
      this.tabPage3.Controls.Add(this.txtPrecursorTolerance);
      this.tabPage3.Controls.Add(this.rbClusterByRealData);
      this.tabPage3.Controls.Add(this.txtRetentionTimeTolerance);
      this.tabPage3.Controls.Add(this.label7);
      this.tabPage3.Controls.Add(this.label1);
      this.tabPage3.Controls.Add(this.cbFormat);
      this.tabPage3.Controls.Add(this.cbDecoyPattern);
      this.tabPage3.Controls.Add(this.txtMaxPrecursorDistance);
      this.tabPage3.Controls.Add(this.label11);
      this.tabPage3.Controls.Add(this.txtMzTolerance);
      this.tabPage3.Controls.Add(this.label3);
      this.tabPage3.Controls.Add(this.txtDecoyPattern);
      this.tabPage3.Controls.Add(this.txtRtToleranceBetweenRealAndPredefined);
      this.tabPage3.Controls.Add(this.label6);
      this.tabPage3.Controls.Add(this.txtPredefinedFile);
      this.tabPage3.Controls.Add(this.btnPredefined);
      this.tabPage3.Controls.Add(this.rbClusterByPredefine);
      this.tabPage3.Location = new System.Drawing.Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(1054, 203);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = "Transition grouping";
      // 
      // btnNewSample
      // 
      this.btnNewSample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNewSample.Location = new System.Drawing.Point(904, 42);
      this.btnNewSample.Name = "btnNewSample";
      this.btnNewSample.Size = new System.Drawing.Size(80, 23);
      this.btnNewSample.TabIndex = 79;
      this.btnNewSample.Text = "New Sample";
      this.btnNewSample.UseVisualStyleBackColor = true;
      this.btnNewSample.Click += new System.EventHandler(this.btnNewSample_Click);
      // 
      // btnNew
      // 
      this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnNew.Location = new System.Drawing.Point(821, 42);
      this.btnNew.Name = "btnNew";
      this.btnNew.Size = new System.Drawing.Size(77, 23);
      this.btnNew.TabIndex = 78;
      this.btnNew.Text = "New Format";
      this.btnNew.UseVisualStyleBackColor = true;
      this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
      // 
      // btnEdit
      // 
      this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEdit.Location = new System.Drawing.Point(725, 42);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(90, 23);
      this.btnEdit.TabIndex = 78;
      this.btnEdit.Text = "Edit Format";
      this.btnEdit.UseVisualStyleBackColor = true;
      this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
      // 
      // txtPrecursorTolerance
      // 
      this.txtPrecursorTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPrecursorTolerance.Location = new System.Drawing.Point(330, 172);
      this.txtPrecursorTolerance.Name = "txtPrecursorTolerance";
      this.txtPrecursorTolerance.Size = new System.Drawing.Size(654, 21);
      this.txtPrecursorTolerance.TabIndex = 77;
      this.txtPrecursorTolerance.Text = "6,7,8,10";
      // 
      // rbClusterByRealData
      // 
      this.rbClusterByRealData.AutoSize = true;
      this.rbClusterByRealData.Location = new System.Drawing.Point(21, 125);
      this.rbClusterByRealData.Name = "rbClusterByRealData";
      this.rbClusterByRealData.Size = new System.Drawing.Size(149, 16);
      this.rbClusterByRealData.TabIndex = 76;
      this.rbClusterByRealData.Text = "Grouping by real data";
      this.rbClusterByRealData.UseVisualStyleBackColor = true;
      // 
      // txtRetentionTimeTolerance
      // 
      this.txtRetentionTimeTolerance.Location = new System.Drawing.Point(451, 148);
      this.txtRetentionTimeTolerance.Name = "txtRetentionTimeTolerance";
      this.txtRetentionTimeTolerance.Size = new System.Drawing.Size(54, 21);
      this.txtRetentionTimeTolerance.TabIndex = 75;
      this.txtRetentionTimeTolerance.Text = "1.0";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(37, 151);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(413, 12);
      this.label7.TabIndex = 74;
      this.label7.Text = "Retention time tolerance for product ions from same peptide (second)";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(37, 175);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(287, 12);
      this.label1.TabIndex = 73;
      this.label1.Text = "Distance between light and heavy precursor mass";
      // 
      // cbFormat
      // 
      this.cbFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFormat.FormattingEnabled = true;
      this.cbFormat.Location = new System.Drawing.Point(375, 44);
      this.cbFormat.Name = "cbFormat";
      this.cbFormat.Size = new System.Drawing.Size(344, 20);
      this.cbFormat.TabIndex = 72;
      // 
      // cbDecoyPattern
      // 
      this.cbDecoyPattern.AutoSize = true;
      this.cbDecoyPattern.Location = new System.Drawing.Point(558, 99);
      this.cbDecoyPattern.Name = "cbDecoyPattern";
      this.cbDecoyPattern.Size = new System.Drawing.Size(174, 16);
      this.cbDecoyPattern.TabIndex = 71;
      this.cbDecoyPattern.Text = "Object name decoy pattern";
      this.cbDecoyPattern.UseVisualStyleBackColor = true;
      // 
      // txtMaxPrecursorDistance
      // 
      this.txtMaxPrecursorDistance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMaxPrecursorDistance.Location = new System.Drawing.Point(655, 19);
      this.txtMaxPrecursorDistance.Name = "txtMaxPrecursorDistance";
      this.txtMaxPrecursorDistance.Size = new System.Drawing.Size(54, 21);
      this.txtMaxPrecursorDistance.TabIndex = 70;
      this.txtMaxPrecursorDistance.Text = "20";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(386, 22);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(263, 12);
      this.label11.TabIndex = 69;
      this.label11.Text = "Max light/heavy precursor m/z distance (da)";
      // 
      // txtMzTolerance
      // 
      this.txtMzTolerance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMzTolerance.Location = new System.Drawing.Point(270, 19);
      this.txtMzTolerance.Name = "txtMzTolerance";
      this.txtMzTolerance.Size = new System.Drawing.Size(54, 21);
      this.txtMzTolerance.TabIndex = 68;
      this.txtMzTolerance.Text = "0.1";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(19, 22);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(245, 12);
      this.label3.TabIndex = 67;
      this.label3.Text = "Precursor/Product ion m/z tolerance (da)";
      // 
      // txtDecoyPattern
      // 
      this.txtDecoyPattern.Location = new System.Drawing.Point(738, 97);
      this.txtDecoyPattern.Name = "txtDecoyPattern";
      this.txtDecoyPattern.Size = new System.Drawing.Size(162, 21);
      this.txtDecoyPattern.TabIndex = 65;
      this.txtDecoyPattern.Text = "DECOY";
      // 
      // txtRtToleranceBetweenRealAndPredefined
      // 
      this.txtRtToleranceBetweenRealAndPredefined.Location = new System.Drawing.Point(482, 97);
      this.txtRtToleranceBetweenRealAndPredefined.Name = "txtRtToleranceBetweenRealAndPredefined";
      this.txtRtToleranceBetweenRealAndPredefined.Size = new System.Drawing.Size(54, 21);
      this.txtRtToleranceBetweenRealAndPredefined.TabIndex = 65;
      this.txtRtToleranceBetweenRealAndPredefined.Text = "2.0";
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(37, 100);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(443, 12);
      this.label6.TabIndex = 64;
      this.label6.Text = "Retention time tolerance between real and predefined product ion (second)";
      // 
      // txtPredefinedFile
      // 
      this.txtPredefinedFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPredefinedFile.Location = new System.Drawing.Point(281, 69);
      this.txtPredefinedFile.Name = "txtPredefinedFile";
      this.txtPredefinedFile.Size = new System.Drawing.Size(703, 21);
      this.txtPredefinedFile.TabIndex = 62;
      // 
      // btnPredefined
      // 
      this.btnPredefined.Location = new System.Drawing.Point(37, 68);
      this.btnPredefined.Name = "btnPredefined";
      this.btnPredefined.Size = new System.Drawing.Size(238, 23);
      this.btnPredefined.TabIndex = 61;
      this.btnPredefined.Text = "button1";
      this.btnPredefined.UseVisualStyleBackColor = true;
      // 
      // rbClusterByPredefine
      // 
      this.rbClusterByPredefine.AutoSize = true;
      this.rbClusterByPredefine.Checked = true;
      this.rbClusterByPredefine.Location = new System.Drawing.Point(21, 46);
      this.rbClusterByPredefine.Name = "rbClusterByPredefine";
      this.rbClusterByPredefine.Size = new System.Drawing.Size(353, 16);
      this.rbClusterByPredefine.TabIndex = 60;
      this.rbClusterByPredefine.TabStop = true;
      this.rbClusterByPredefine.Text = "Grouping by pre-defined SRM transaction file, format = ";
      this.rbClusterByPredefine.UseVisualStyleBackColor = true;
      // 
      // tabPage4
      // 
      this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
      this.tabPage4.Controls.Add(this.txtMinEnabledScan);
      this.tabPage4.Controls.Add(this.label12);
      this.tabPage4.Controls.Add(this.txtValidateCorrel);
      this.tabPage4.Controls.Add(this.label5);
      this.tabPage4.Controls.Add(this.txtValidateSignalToNoise);
      this.tabPage4.Controls.Add(this.label4);
      this.tabPage4.Controls.Add(this.txtMinValidTransactionPair);
      this.tabPage4.Controls.Add(this.label8);
      this.tabPage4.Location = new System.Drawing.Point(4, 22);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(1054, 203);
      this.tabPage4.TabIndex = 3;
      this.tabPage4.Text = "Filter";
      // 
      // txtMinEnabledScan
      // 
      this.txtMinEnabledScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinEnabledScan.Location = new System.Drawing.Point(521, 64);
      this.txtMinEnabledScan.Name = "txtMinEnabledScan";
      this.txtMinEnabledScan.Size = new System.Drawing.Size(53, 21);
      this.txtMinEnabledScan.TabIndex = 55;
      this.txtMinEnabledScan.Text = "5";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(354, 67);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(161, 12);
      this.label12.TabIndex = 54;
      this.label12.Text = "minimum valid scan count =";
      // 
      // txtValidateCorrel
      // 
      this.txtValidateCorrel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtValidateCorrel.Location = new System.Drawing.Point(521, 39);
      this.txtValidateCorrel.Name = "txtValidateCorrel";
      this.txtValidateCorrel.Size = new System.Drawing.Size(53, 21);
      this.txtValidateCorrel.TabIndex = 55;
      this.txtValidateCorrel.Text = "0.5";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(247, 42);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(269, 12);
      this.label5.TabIndex = 54;
      this.label5.Text = "linear regression correlation coefficient >=";
      // 
      // txtValidateSignalToNoise
      // 
      this.txtValidateSignalToNoise.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtValidateSignalToNoise.Location = new System.Drawing.Point(521, 14);
      this.txtValidateSignalToNoise.Name = "txtValidateSignalToNoise";
      this.txtValidateSignalToNoise.Size = new System.Drawing.Size(53, 21);
      this.txtValidateSignalToNoise.TabIndex = 52;
      this.txtValidateSignalToNoise.Text = "3";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(19, 17);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(497, 12);
      this.label4.TabIndex = 51;
      this.label4.Text = "At transition level, mininum signal to noise for both light and heavy transition " +
    "=";
      // 
      // txtMinValidTransactionPair
      // 
      this.txtMinValidTransactionPair.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinValidTransactionPair.Location = new System.Drawing.Point(334, 123);
      this.txtMinValidTransactionPair.Name = "txtMinValidTransactionPair";
      this.txtMinValidTransactionPair.Size = new System.Drawing.Size(53, 21);
      this.txtMinValidTransactionPair.TabIndex = 49;
      this.txtMinValidTransactionPair.Text = "2";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(19, 128);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(317, 12);
      this.label8.TabIndex = 48;
      this.label8.Text = "At component level, mininum valid transition pair = ";
      // 
      // SrmDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1062, 782);
      this.Controls.Add(this.outsGroup);
      this.Controls.Add(this.tabControl1);
      this.KeyPreview = true;
      this.Name = "SrmDistillerUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.tabControl1, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.outsGroup, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.outsGroup.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.tabPage4.ResumeLayout(false);
      this.tabPage4.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox outsGroup;
    private System.Windows.Forms.ListView lvDirectories;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnAddFiles;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.RadioButton rbBaseline;
    private System.Windows.Forms.TextBox txtSignalToNoise;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TextBox txtHighestPeakPercentage;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.RadioButton rbHighestPeak;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.CheckBox cbSmooth;
    private System.Windows.Forms.CheckBox cbBaseLineExtraction;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TextBox txtRtToleranceBetweenRealAndPredefined;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtPredefinedFile;
    private System.Windows.Forms.Button btnPredefined;
    private System.Windows.Forms.RadioButton rbClusterByPredefine;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.TextBox txtValidateCorrel;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtValidateSignalToNoise;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMinValidTransactionPair;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.TextBox txtMzTolerance;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnAddDirectory;
    private System.Windows.Forms.Button btnAddSubdirectory;
    private System.Windows.Forms.Button btnAddTag;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.TextBox txtMaxPrecursorDistance;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.CheckBox cbDecoyPattern;
    private System.Windows.Forms.TextBox txtDecoyPattern;
    private System.Windows.Forms.ComboBox cbFormat;
    private System.Windows.Forms.TextBox txtMinEnabledScan;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.TextBox txtBaselinePercentage;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.CheckBox cbRatioByArea;
    private System.Windows.Forms.TextBox txtPrecursorTolerance;
    private System.Windows.Forms.RadioButton rbClusterByRealData;
    private System.Windows.Forms.TextBox txtRetentionTimeTolerance;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnEdit;
    private System.Windows.Forms.Button btnNew;
    private System.Windows.Forms.Button btnNewSample;
    private System.Windows.Forms.SaveFileDialog dlgSave;
    private System.Windows.Forms.CheckBox cbRefinePeakPicking;


  }
}
