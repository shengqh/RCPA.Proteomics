namespace RCPA.Tools.Raw
{
  partial class RawFileViewerUI
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
      this.btnPrev = new System.Windows.Forms.Button();
      this.btnFirst = new System.Windows.Forms.Button();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnLast = new System.Windows.Forms.Button();
      this.txtScan = new System.Windows.Forms.TextBox();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.zgcScan = new ZedGraph.ZedGraphControl();
      this.dgvPeak = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsPeak = new System.Windows.Forms.BindingSource();
      this.iPeakBindingSource = new System.Windows.Forms.BindingSource();
      this.pnlFile.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvPeak)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsPeak)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.iPeakBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(0, 582);
      this.pnlFile.Size = new System.Drawing.Size(980, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Size = new System.Drawing.Size(734, 21);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 562);
      this.lblProgress.Size = new System.Drawing.Size(980, 21);
      this.lblProgress.Visible = false;
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 583);
      this.progressBar.Size = new System.Drawing.Size(980, 21);
      this.progressBar.Visible = false;
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(538, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(453, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(368, 7);
      // 
      // btnPrev
      // 
      this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnPrev.Location = new System.Drawing.Point(40, 514);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(34, 23);
      this.btnPrev.TabIndex = 8;
      this.btnPrev.Text = "<";
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
      // 
      // btnFirst
      // 
      this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnFirst.Location = new System.Drawing.Point(1, 514);
      this.btnFirst.Name = "btnFirst";
      this.btnFirst.Size = new System.Drawing.Size(34, 23);
      this.btnFirst.TabIndex = 8;
      this.btnFirst.Text = "|<";
      this.btnFirst.UseVisualStyleBackColor = true;
      this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
      // 
      // btnNext
      // 
      this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnNext.Location = new System.Drawing.Point(185, 514);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(34, 23);
      this.btnNext.TabIndex = 8;
      this.btnNext.Text = ">";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // btnLast
      // 
      this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnLast.Location = new System.Drawing.Point(224, 514);
      this.btnLast.Name = "btnLast";
      this.btnLast.Size = new System.Drawing.Size(34, 23);
      this.btnLast.TabIndex = 8;
      this.btnLast.Text = ">|";
      this.btnLast.UseVisualStyleBackColor = true;
      this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
      // 
      // txtScan
      // 
      this.txtScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtScan.Location = new System.Drawing.Point(80, 516);
      this.txtScan.Name = "txtScan";
      this.txtScan.Size = new System.Drawing.Size(99, 21);
      this.txtScan.TabIndex = 9;
      this.txtScan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtScan_KeyUp);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 12);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.zgcScan);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.AutoScroll = true;
      this.splitContainer1.Panel2.Controls.Add(this.dgvPeak);
      this.splitContainer1.Size = new System.Drawing.Size(980, 496);
      this.splitContainer1.SplitterDistance = 665;
      this.splitContainer1.TabIndex = 10;
      // 
      // zgcScan
      // 
      this.zgcScan.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcScan.Location = new System.Drawing.Point(0, 0);
      this.zgcScan.Name = "zgcScan";
      this.zgcScan.ScrollGrace = 0D;
      this.zgcScan.ScrollMaxX = 0D;
      this.zgcScan.ScrollMaxY = 0D;
      this.zgcScan.ScrollMaxY2 = 0D;
      this.zgcScan.ScrollMinX = 0D;
      this.zgcScan.ScrollMinY = 0D;
      this.zgcScan.ScrollMinY2 = 0D;
      this.zgcScan.Size = new System.Drawing.Size(665, 496);
      this.zgcScan.TabIndex = 8;
      // 
      // dgvPeak
      // 
      this.dgvPeak.AutoGenerateColumns = false;
      this.dgvPeak.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvPeak.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
      this.dgvPeak.DataSource = this.bsPeak;
      this.dgvPeak.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvPeak.Location = new System.Drawing.Point(0, 0);
      this.dgvPeak.Name = "dgvPeak";
      this.dgvPeak.ReadOnly = true;
      this.dgvPeak.RowTemplate.Height = 23;
      this.dgvPeak.Size = new System.Drawing.Size(311, 496);
      this.dgvPeak.TabIndex = 0;
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DataPropertyName = "Mz";
      this.dataGridViewTextBoxColumn1.HeaderText = "Mz";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.DataPropertyName = "Intensity";
      this.dataGridViewTextBoxColumn2.HeaderText = "Intensity";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn3
      // 
      this.dataGridViewTextBoxColumn3.DataPropertyName = "Charge";
      this.dataGridViewTextBoxColumn3.HeaderText = "Charge";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 50;
      // 
      // bsPeak
      // 
      this.bsPeak.AllowNew = false;
      this.bsPeak.DataSource = typeof(RCPA.Proteomics.Spectrum.Peak);
      // 
      // iPeakBindingSource
      // 
      this.iPeakBindingSource.DataSource = typeof(RCPA.Proteomics.Spectrum.IPeak);
      // 
      // RawFileViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(980, 640);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.btnLast);
      this.Controls.Add(this.btnPrev);
      this.Controls.Add(this.btnFirst);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.txtScan);
      this.KeyPreview = true;
      this.Name = "RawFileViewerUI";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RawFileViewerUI_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RawFileViewerUI_KeyDown);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtScan, 0);
      this.Controls.SetChildIndex(this.btnNext, 0);
      this.Controls.SetChildIndex(this.btnFirst, 0);
      this.Controls.SetChildIndex(this.btnPrev, 0);
      this.Controls.SetChildIndex(this.btnLast, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.splitContainer1, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvPeak)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsPeak)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.iPeakBindingSource)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnPrev;
    private System.Windows.Forms.Button btnFirst;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnLast;
    private System.Windows.Forms.TextBox txtScan;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private ZedGraph.ZedGraphControl zgcScan;
    private System.Windows.Forms.DataGridView dgvPeak;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.BindingSource bsPeak;
    private System.Windows.Forms.BindingSource iPeakBindingSource;
  }
}
