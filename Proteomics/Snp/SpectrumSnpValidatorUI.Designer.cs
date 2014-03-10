namespace RCPA.Proteomics.Snp
{
  partial class SpectrumSnpValidatorUI
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.txtImageDirectory = new System.Windows.Forms.TextBox();
      this.txtPeptideFile = new System.Windows.Forms.TextBox();
      this.btnImageDirectory = new System.Windows.Forms.Button();
      this.btnPeptideFile = new System.Windows.Forms.Button();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.gvPeptides = new System.Windows.Forms.DataGridView();
      this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn31 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.gvcProtein = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsSpectrum = new System.Windows.Forms.BindingSource(this.components);
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.pbOriginal = new System.Windows.Forms.PictureBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.txtOriginalFilename = new System.Windows.Forms.TextBox();
      this.pbMutation = new System.Windows.Forms.PictureBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMutationFilename = new System.Windows.Forms.TextBox();
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblMutation = new System.Windows.Forms.Label();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvPeptides)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsSpectrum)).BeginInit();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).BeginInit();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMutation)).BeginInit();
      this.panel3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.txtImageDirectory);
      this.splitContainer1.Panel1.Controls.Add(this.txtPeptideFile);
      this.splitContainer1.Panel1.Controls.Add(this.btnImageDirectory);
      this.splitContainer1.Panel1.Controls.Add(this.btnPeptideFile);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1008, 769);
      this.splitContainer1.SplitterDistance = 72;
      this.splitContainer1.TabIndex = 1;
      // 
      // txtImageDirectory
      // 
      this.txtImageDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtImageDirectory.Location = new System.Drawing.Point(260, 41);
      this.txtImageDirectory.Name = "txtImageDirectory";
      this.txtImageDirectory.Size = new System.Drawing.Size(736, 21);
      this.txtImageDirectory.TabIndex = 3;
      // 
      // txtPeptideFile
      // 
      this.txtPeptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideFile.Location = new System.Drawing.Point(260, 14);
      this.txtPeptideFile.Name = "txtPeptideFile";
      this.txtPeptideFile.Size = new System.Drawing.Size(736, 21);
      this.txtPeptideFile.TabIndex = 2;
      // 
      // btnImageDirectory
      // 
      this.btnImageDirectory.Location = new System.Drawing.Point(12, 41);
      this.btnImageDirectory.Name = "btnImageDirectory";
      this.btnImageDirectory.Size = new System.Drawing.Size(242, 23);
      this.btnImageDirectory.TabIndex = 1;
      this.btnImageDirectory.Text = "button2";
      this.btnImageDirectory.UseVisualStyleBackColor = true;
      // 
      // btnPeptideFile
      // 
      this.btnPeptideFile.Location = new System.Drawing.Point(12, 12);
      this.btnPeptideFile.Name = "btnPeptideFile";
      this.btnPeptideFile.Size = new System.Drawing.Size(242, 23);
      this.btnPeptideFile.TabIndex = 0;
      this.btnPeptideFile.Text = "button1";
      this.btnPeptideFile.UseVisualStyleBackColor = true;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.gvPeptides);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      this.splitContainer2.Size = new System.Drawing.Size(1008, 693);
      this.splitContainer2.SplitterDistance = 242;
      this.splitContainer2.TabIndex = 0;
      // 
      // gvPeptides
      // 
      this.gvPeptides.AutoGenerateColumns = false;
      this.gvPeptides.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvPeptides.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.Index,
            this.dataGridViewTextBoxColumn31,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18,
            this.dataGridViewTextBoxColumn23,
            this.dataGridViewTextBoxColumn30,
            this.gvcProtein});
      this.gvPeptides.DataSource = this.bsSpectrum;
      this.gvPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvPeptides.Location = new System.Drawing.Point(0, 0);
      this.gvPeptides.MultiSelect = false;
      this.gvPeptides.Name = "gvPeptides";
      this.gvPeptides.RowTemplate.Height = 23;
      this.gvPeptides.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvPeptides.Size = new System.Drawing.Size(1008, 242);
      this.gvPeptides.TabIndex = 7;
      this.gvPeptides.VirtualMode = true;
      this.gvPeptides.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvPeptides_CellFormatting);
      this.gvPeptides.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvPeptides_CellValueNeeded);
      this.gvPeptides.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.gvPeptides_DataBindingComplete);
      this.gvPeptides.SelectionChanged += new System.EventHandler(this.gvPeptides_SelectionChanged);
      // 
      // Selected
      // 
      this.Selected.DataPropertyName = "Selected";
      this.Selected.HeaderText = "";
      this.Selected.Name = "Selected";
      this.Selected.Width = 20;
      // 
      // Index
      // 
      this.Index.HeaderText = "Index";
      this.Index.Name = "Index";
      this.Index.ReadOnly = true;
      this.Index.Width = 40;
      // 
      // dataGridViewTextBoxColumn31
      // 
      this.dataGridViewTextBoxColumn31.HeaderText = "Peptide";
      this.dataGridViewTextBoxColumn31.Name = "dataGridViewTextBoxColumn31";
      this.dataGridViewTextBoxColumn31.ReadOnly = true;
      this.dataGridViewTextBoxColumn31.Width = 200;
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.DataPropertyName = "Charge";
      this.dataGridViewTextBoxColumn4.HeaderText = "Charge";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.Width = 50;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.HeaderText = "FileScan";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 300;
      // 
      // dataGridViewTextBoxColumn5
      // 
      this.dataGridViewTextBoxColumn5.DataPropertyName = "ObservedMz";
      this.dataGridViewTextBoxColumn5.HeaderText = "ObservedMz";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn17
      // 
      this.dataGridViewTextBoxColumn17.DataPropertyName = "Rank";
      this.dataGridViewTextBoxColumn17.HeaderText = "Rank";
      this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
      this.dataGridViewTextBoxColumn17.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn18
      // 
      this.dataGridViewTextBoxColumn18.DataPropertyName = "Score";
      this.dataGridViewTextBoxColumn18.HeaderText = "Score";
      this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
      this.dataGridViewTextBoxColumn18.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn23
      // 
      this.dataGridViewTextBoxColumn23.DataPropertyName = "ExpectValue";
      this.dataGridViewTextBoxColumn23.HeaderText = "ExpectValue";
      this.dataGridViewTextBoxColumn23.Name = "dataGridViewTextBoxColumn23";
      this.dataGridViewTextBoxColumn23.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn30
      // 
      this.dataGridViewTextBoxColumn30.DataPropertyName = "Modifications";
      this.dataGridViewTextBoxColumn30.HeaderText = "Modifications";
      this.dataGridViewTextBoxColumn30.Name = "dataGridViewTextBoxColumn30";
      this.dataGridViewTextBoxColumn30.ReadOnly = true;
      // 
      // gvcProtein
      // 
      this.gvcProtein.HeaderText = "Protein";
      this.gvcProtein.Name = "gvcProtein";
      this.gvcProtein.ReadOnly = true;
      this.gvcProtein.Width = 400;
      // 
      // bsSpectrum
      // 
      this.bsSpectrum.DataSource = typeof(RCPA.Proteomics.Summary.IIdentifiedSpectrum);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.IsSplitterFixed = true;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.pbOriginal);
      this.splitContainer3.Panel1.Controls.Add(this.panel2);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.pbMutation);
      this.splitContainer3.Panel2.Controls.Add(this.panel3);
      this.splitContainer3.Size = new System.Drawing.Size(1008, 447);
      this.splitContainer3.SplitterDistance = 504;
      this.splitContainer3.TabIndex = 2;
      // 
      // pbOriginal
      // 
      this.pbOriginal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pbOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbOriginal.Location = new System.Drawing.Point(0, 32);
      this.pbOriginal.Name = "pbOriginal";
      this.pbOriginal.Size = new System.Drawing.Size(504, 415);
      this.pbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pbOriginal.TabIndex = 1;
      this.pbOriginal.TabStop = false;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.label1);
      this.panel2.Controls.Add(this.txtOriginalFilename);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(504, 32);
      this.panel2.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(13, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(89, 12);
      this.label1.TabIndex = 1;
      this.label1.Text = "Original file:";
      // 
      // txtOriginalFilename
      // 
      this.txtOriginalFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOriginalFilename.Location = new System.Drawing.Point(108, 6);
      this.txtOriginalFilename.Name = "txtOriginalFilename";
      this.txtOriginalFilename.Size = new System.Drawing.Size(367, 21);
      this.txtOriginalFilename.TabIndex = 0;
      // 
      // pbMutation
      // 
      this.pbMutation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pbMutation.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pbMutation.Location = new System.Drawing.Point(0, 32);
      this.pbMutation.Name = "pbMutation";
      this.pbMutation.Size = new System.Drawing.Size(500, 415);
      this.pbMutation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pbMutation.TabIndex = 1;
      this.pbMutation.TabStop = false;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.label2);
      this.panel3.Controls.Add(this.txtMutationFilename);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(500, 32);
      this.panel3.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(34, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(89, 12);
      this.label2.TabIndex = 1;
      this.label2.Text = "Mutation file:";
      // 
      // txtMutationFilename
      // 
      this.txtMutationFilename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMutationFilename.Location = new System.Drawing.Point(129, 6);
      this.txtMutationFilename.Name = "txtMutationFilename";
      this.txtMutationFilename.Size = new System.Drawing.Size(359, 21);
      this.txtMutationFilename.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.lblMutation);
      this.panel1.Controls.Add(this.btnLoad);
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 769);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1008, 64);
      this.panel1.TabIndex = 2;
      // 
      // lblMutation
      // 
      this.lblMutation.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblMutation.Location = new System.Drawing.Point(12, 8);
      this.lblMutation.Name = "lblMutation";
      this.lblMutation.Size = new System.Drawing.Size(803, 47);
      this.lblMutation.TabIndex = 9;
      this.lblMutation.Text = "MUTATION";
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(840, 19);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 6;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(921, 19);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 5;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // SpectrumSnpValidatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1008, 833);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel1);
      this.Name = "SpectrumSnpValidatorUI";
      this.TabText = "SpectrumSnpValidatorUI";
      this.Text = "SpectrumSnpValidatorUI";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpectrumSnpValidatorUI_FormClosing);
      this.Load += new System.EventHandler(this.SpectrumSnpValidatorUI_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvPeptides)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsSpectrum)).EndInit();
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).EndInit();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pbMutation)).EndInit();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox txtImageDirectory;
    private System.Windows.Forms.TextBox txtPeptideFile;
    private System.Windows.Forms.Button btnImageDirectory;
    private System.Windows.Forms.Button btnPeptideFile;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.DataGridView gvPeptides;
    private System.Windows.Forms.BindingSource bsSpectrum;
    private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
    private System.Windows.Forms.DataGridViewTextBoxColumn Index;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
    private System.Windows.Forms.DataGridViewTextBoxColumn gvcProtein;
    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.PictureBox pbOriginal;
    private System.Windows.Forms.PictureBox pbMutation;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtOriginalFilename;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMutationFilename;
    private System.Windows.Forms.Label lblMutation;
  }
}