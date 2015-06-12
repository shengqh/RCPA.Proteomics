namespace RCPA.Proteomics.Snp
{
  partial class SpectrumScanValidatorUI
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
      this.txtRawFile = new System.Windows.Forms.TextBox();
      this.txtMatchFile = new System.Windows.Forms.TextBox();
      this.btnRawFile = new System.Windows.Forms.Button();
      this.btnMatchFile = new System.Windows.Forms.Button();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.zgcMS2 = new ZedGraph.ZedGraphControl();
      this.splitter1 = new System.Windows.Forms.Splitter();
      this.gvPeptides = new System.Windows.Forms.DataGridView();
      this.ms3Source = new System.Windows.Forms.BindingSource(this.components);
      this.zgcMS3 = new ZedGraph.ZedGraphControl();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.categoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.sequence1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.scan1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.sequence2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.scan2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.minMzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.precursorMatchedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.mS3MatchedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvPeptides)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ms3Source)).BeginInit();
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
      this.splitContainer1.Panel1.Controls.Add(this.txtRawFile);
      this.splitContainer1.Panel1.Controls.Add(this.txtMatchFile);
      this.splitContainer1.Panel1.Controls.Add(this.btnRawFile);
      this.splitContainer1.Panel1.Controls.Add(this.btnMatchFile);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(1623, 833);
      this.splitContainer1.SplitterDistance = 72;
      this.splitContainer1.TabIndex = 1;
      // 
      // txtRawFile
      // 
      this.txtRawFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRawFile.Location = new System.Drawing.Point(260, 44);
      this.txtRawFile.Name = "txtRawFile";
      this.txtRawFile.Size = new System.Drawing.Size(1351, 20);
      this.txtRawFile.TabIndex = 3;
      // 
      // txtMatchFile
      // 
      this.txtMatchFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMatchFile.Location = new System.Drawing.Point(260, 15);
      this.txtMatchFile.Name = "txtMatchFile";
      this.txtMatchFile.Size = new System.Drawing.Size(1351, 20);
      this.txtMatchFile.TabIndex = 2;
      // 
      // btnRawFile
      // 
      this.btnRawFile.Location = new System.Drawing.Point(12, 44);
      this.btnRawFile.Name = "btnRawFile";
      this.btnRawFile.Size = new System.Drawing.Size(242, 25);
      this.btnRawFile.TabIndex = 1;
      this.btnRawFile.Text = "button2";
      this.btnRawFile.UseVisualStyleBackColor = true;
      // 
      // btnMatchFile
      // 
      this.btnMatchFile.Location = new System.Drawing.Point(12, 13);
      this.btnMatchFile.Name = "btnMatchFile";
      this.btnMatchFile.Size = new System.Drawing.Size(242, 25);
      this.btnMatchFile.TabIndex = 0;
      this.btnMatchFile.Text = "button1";
      this.btnMatchFile.UseVisualStyleBackColor = true;
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
      this.splitContainer2.Panel1.Controls.Add(this.zgcMS2);
      this.splitContainer2.Panel1.Controls.Add(this.splitter1);
      this.splitContainer2.Panel1.Controls.Add(this.gvPeptides);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.zgcMS3);
      this.splitContainer2.Size = new System.Drawing.Size(1623, 757);
      this.splitContainer2.SplitterDistance = 242;
      this.splitContainer2.TabIndex = 0;
      // 
      // zgcMS2
      // 
      this.zgcMS2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcMS2.Location = new System.Drawing.Point(1048, 0);
      this.zgcMS2.Name = "zgcMS2";
      this.zgcMS2.ScrollGrace = 0D;
      this.zgcMS2.ScrollMaxX = 0D;
      this.zgcMS2.ScrollMaxY = 0D;
      this.zgcMS2.ScrollMaxY2 = 0D;
      this.zgcMS2.ScrollMinX = 0D;
      this.zgcMS2.ScrollMinY = 0D;
      this.zgcMS2.ScrollMinY2 = 0D;
      this.zgcMS2.Size = new System.Drawing.Size(575, 242);
      this.zgcMS2.TabIndex = 9;
      // 
      // splitter1
      // 
      this.splitter1.Location = new System.Drawing.Point(1045, 0);
      this.splitter1.Name = "splitter1";
      this.splitter1.Size = new System.Drawing.Size(3, 242);
      this.splitter1.TabIndex = 8;
      this.splitter1.TabStop = false;
      // 
      // gvPeptides
      // 
      this.gvPeptides.AutoGenerateColumns = false;
      this.gvPeptides.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvPeptides.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.categoryDataGridViewTextBoxColumn,
            this.sequence1DataGridViewTextBoxColumn,
            this.scan1DataGridViewTextBoxColumn,
            this.sequence2DataGridViewTextBoxColumn,
            this.scan2DataGridViewTextBoxColumn,
            this.minMzDataGridViewTextBoxColumn,
            this.precursorMatchedDataGridViewTextBoxColumn,
            this.mS3MatchedDataGridViewTextBoxColumn});
      this.gvPeptides.DataSource = this.ms3Source;
      this.gvPeptides.Dock = System.Windows.Forms.DockStyle.Left;
      this.gvPeptides.Location = new System.Drawing.Point(0, 0);
      this.gvPeptides.MultiSelect = false;
      this.gvPeptides.Name = "gvPeptides";
      this.gvPeptides.ReadOnly = true;
      this.gvPeptides.RowTemplate.Height = 23;
      this.gvPeptides.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvPeptides.Size = new System.Drawing.Size(1045, 242);
      this.gvPeptides.TabIndex = 7;
      this.gvPeptides.VirtualMode = true;
      this.gvPeptides.SelectionChanged += new System.EventHandler(this.gvPeptides_SelectionChanged);
      // 
      // ms3Source
      // 
      this.ms3Source.DataSource = typeof(RCPA.Proteomics.Snp.MS3MatchItem);
      // 
      // zgcMS3
      // 
      this.zgcMS3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcMS3.Location = new System.Drawing.Point(0, 0);
      this.zgcMS3.Name = "zgcMS3";
      this.zgcMS3.ScrollGrace = 0D;
      this.zgcMS3.ScrollMaxX = 0D;
      this.zgcMS3.ScrollMaxY = 0D;
      this.zgcMS3.ScrollMaxY2 = 0D;
      this.zgcMS3.ScrollMinX = 0D;
      this.zgcMS3.ScrollMinY = 0D;
      this.zgcMS3.ScrollMinY2 = 0D;
      this.zgcMS3.Size = new System.Drawing.Size(1623, 511);
      this.zgcMS3.TabIndex = 10;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnLoad);
      this.panel1.Controls.Add(this.btnClose);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 833);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1623, 69);
      this.panel1.TabIndex = 2;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(1455, 21);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 25);
      this.btnLoad.TabIndex = 6;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(1536, 21);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 25);
      this.btnClose.TabIndex = 5;
      this.btnClose.Text = "Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // categoryDataGridViewTextBoxColumn
      // 
      this.categoryDataGridViewTextBoxColumn.DataPropertyName = "Category";
      this.categoryDataGridViewTextBoxColumn.HeaderText = "Category";
      this.categoryDataGridViewTextBoxColumn.Name = "categoryDataGridViewTextBoxColumn";
      this.categoryDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // sequence1DataGridViewTextBoxColumn
      // 
      this.sequence1DataGridViewTextBoxColumn.DataPropertyName = "Sequence1";
      this.sequence1DataGridViewTextBoxColumn.HeaderText = "Sequence1";
      this.sequence1DataGridViewTextBoxColumn.Name = "sequence1DataGridViewTextBoxColumn";
      this.sequence1DataGridViewTextBoxColumn.ReadOnly = true;
      this.sequence1DataGridViewTextBoxColumn.Width = 200;
      // 
      // scan1DataGridViewTextBoxColumn
      // 
      this.scan1DataGridViewTextBoxColumn.DataPropertyName = "Scan1";
      this.scan1DataGridViewTextBoxColumn.HeaderText = "Scan1";
      this.scan1DataGridViewTextBoxColumn.Name = "scan1DataGridViewTextBoxColumn";
      this.scan1DataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // sequence2DataGridViewTextBoxColumn
      // 
      this.sequence2DataGridViewTextBoxColumn.DataPropertyName = "Sequence2";
      this.sequence2DataGridViewTextBoxColumn.HeaderText = "Sequence2";
      this.sequence2DataGridViewTextBoxColumn.Name = "sequence2DataGridViewTextBoxColumn";
      this.sequence2DataGridViewTextBoxColumn.ReadOnly = true;
      this.sequence2DataGridViewTextBoxColumn.Width = 200;
      // 
      // scan2DataGridViewTextBoxColumn
      // 
      this.scan2DataGridViewTextBoxColumn.DataPropertyName = "Scan2";
      this.scan2DataGridViewTextBoxColumn.HeaderText = "Scan2";
      this.scan2DataGridViewTextBoxColumn.Name = "scan2DataGridViewTextBoxColumn";
      this.scan2DataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // minMzDataGridViewTextBoxColumn
      // 
      this.minMzDataGridViewTextBoxColumn.DataPropertyName = "MinMz";
      this.minMzDataGridViewTextBoxColumn.HeaderText = "MinMz";
      this.minMzDataGridViewTextBoxColumn.Name = "minMzDataGridViewTextBoxColumn";
      this.minMzDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // precursorMatchedDataGridViewTextBoxColumn
      // 
      this.precursorMatchedDataGridViewTextBoxColumn.DataPropertyName = "PrecursorMatched";
      this.precursorMatchedDataGridViewTextBoxColumn.HeaderText = "PrecursorMatched";
      this.precursorMatchedDataGridViewTextBoxColumn.Name = "precursorMatchedDataGridViewTextBoxColumn";
      this.precursorMatchedDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // mS3MatchedDataGridViewTextBoxColumn
      // 
      this.mS3MatchedDataGridViewTextBoxColumn.DataPropertyName = "MS3Matched";
      this.mS3MatchedDataGridViewTextBoxColumn.HeaderText = "MS3Matched";
      this.mS3MatchedDataGridViewTextBoxColumn.Name = "mS3MatchedDataGridViewTextBoxColumn";
      this.mS3MatchedDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // SpectrumScanValidatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1623, 902);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.panel1);
      this.Name = "SpectrumScanValidatorUI";
      this.TabText = "SpectrumSnpValidatorUI";
      this.Text = "SpectrumSnpValidatorUI";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpectrumSnpValidatorUI_FormClosing);
      this.Load += new System.EventHandler(this.SpectrumSnpValidatorUI_Load);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvPeptides)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ms3Source)).EndInit();
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TextBox txtRawFile;
    private System.Windows.Forms.TextBox txtMatchFile;
    private System.Windows.Forms.Button btnRawFile;
    private System.Windows.Forms.Button btnMatchFile;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.DataGridView gvPeptides;
    private System.Windows.Forms.BindingSource ms3Source;
    private ZedGraph.ZedGraphControl zgcMS2;
    private System.Windows.Forms.Splitter splitter1;
    private ZedGraph.ZedGraphControl zgcMS3;
    private System.Windows.Forms.DataGridViewTextBoxColumn categoryDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn sequence1DataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn scan1DataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn sequence2DataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn scan2DataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn minMzDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn precursorMatchedDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn mS3MatchedDataGridViewTextBoxColumn;
  }
}