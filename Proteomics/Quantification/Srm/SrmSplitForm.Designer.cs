namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmSplitForm
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
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.btnLeftToRight = new System.Windows.Forms.Button();
      this.btnRightToLeft = new System.Windows.Forms.Button();
      this.gvLeft = new System.Windows.Forms.DataGridView();
      this.gvRight = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsProductIon = new System.Windows.Forms.BindingSource(this.components);
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.splitContainer3.Panel1.SuspendLayout();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.panel1.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvLeft)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvRight)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsProductIon)).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel1
      // 
      this.splitContainer3.Panel1.Controls.Add(this.splitContainer1);
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.panel1);
      this.splitContainer3.Size = new System.Drawing.Size(639, 355);
      this.splitContainer3.SplitterDistance = 320;
      this.splitContainer3.TabIndex = 2;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnOk);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(639, 31);
      this.panel1.TabIndex = 2;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(556, 5);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(475, 5);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.AutoScroll = true;
      this.splitContainer1.Panel1.Controls.Add(this.gvLeft);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
      this.splitContainer1.Size = new System.Drawing.Size(639, 320);
      this.splitContainer1.SplitterDistance = 293;
      this.splitContainer1.SplitterWidth = 1;
      this.splitContainer1.TabIndex = 1;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.IsSplitterFixed = true;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.btnLeftToRight);
      this.splitContainer2.Panel1.Controls.Add(this.btnRightToLeft);
      this.splitContainer2.Panel1.Resize += new System.EventHandler(this.splitContainer2_Panel1_Resize);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.gvRight);
      this.splitContainer2.Size = new System.Drawing.Size(345, 320);
      this.splitContainer2.SplitterDistance = 45;
      this.splitContainer2.SplitterWidth = 1;
      this.splitContainer2.TabIndex = 0;
      // 
      // btnLeftToRight
      // 
      this.btnLeftToRight.Location = new System.Drawing.Point(3, 165);
      this.btnLeftToRight.Name = "btnLeftToRight";
      this.btnLeftToRight.Size = new System.Drawing.Size(39, 23);
      this.btnLeftToRight.TabIndex = 1;
      this.btnLeftToRight.Text = "=>";
      this.btnLeftToRight.UseVisualStyleBackColor = true;
      this.btnLeftToRight.Click += new System.EventHandler(this.btnLeftToRight_Click);
      // 
      // btnRightToLeft
      // 
      this.btnRightToLeft.Location = new System.Drawing.Point(3, 136);
      this.btnRightToLeft.Name = "btnRightToLeft";
      this.btnRightToLeft.Size = new System.Drawing.Size(39, 23);
      this.btnRightToLeft.TabIndex = 0;
      this.btnRightToLeft.Text = "<=";
      this.btnRightToLeft.UseVisualStyleBackColor = true;
      this.btnRightToLeft.Click += new System.EventHandler(this.btnRightToLeft_Click);
      // 
      // gvLeft
      // 
      this.gvLeft.AutoGenerateColumns = false;
      this.gvLeft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvLeft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
      this.gvLeft.DataSource = this.bsProductIon;
      this.gvLeft.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvLeft.Location = new System.Drawing.Point(0, 0);
      this.gvLeft.Name = "gvLeft";
      this.gvLeft.ReadOnly = true;
      this.gvLeft.RowTemplate.Height = 23;
      this.gvLeft.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvLeft.Size = new System.Drawing.Size(293, 320);
      this.gvLeft.TabIndex = 0;
      // 
      // gvRight
      // 
      this.gvRight.AutoGenerateColumns = false;
      this.gvRight.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvRight.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
      this.gvRight.DataSource = this.bsProductIon;
      this.gvRight.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvRight.Location = new System.Drawing.Point(0, 0);
      this.gvRight.Name = "gvRight";
      this.gvRight.ReadOnly = true;
      this.gvRight.RowTemplate.Height = 23;
      this.gvRight.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvRight.Size = new System.Drawing.Size(299, 320);
      this.gvRight.TabIndex = 1;
      // 
      // dataGridViewTextBoxColumn15
      // 
      this.dataGridViewTextBoxColumn15.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn15.HeaderText = "Light";
      this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
      this.dataGridViewTextBoxColumn15.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn16
      // 
      this.dataGridViewTextBoxColumn16.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn16.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
      this.dataGridViewTextBoxColumn16.ReadOnly = true;
      // 
      // bsProductIon
      // 
      this.bsProductIon.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.SrmPairedProductIon);
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.DataPropertyName = "Light";
      this.dataGridViewTextBoxColumn1.HeaderText = "Light";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.DataPropertyName = "Heavy";
      this.dataGridViewTextBoxColumn2.HeaderText = "Heavy";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      // 
      // MRMSplitForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(639, 355);
      this.Controls.Add(this.splitContainer3);
      this.Name = "MRMSplitForm";
      this.Text = "MRMSplitForm";
      this.splitContainer3.Panel1.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      this.splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvLeft)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.gvRight)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.bsProductIon)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer3;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Button btnLeftToRight;
    private System.Windows.Forms.Button btnRightToLeft;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.DataGridView gvLeft;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
    private System.Windows.Forms.BindingSource bsProductIon;
    private System.Windows.Forms.DataGridView gvRight;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

  }
}