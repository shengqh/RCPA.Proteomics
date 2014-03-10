namespace RCPA.Proteomics.Quantification.Srm
{
  partial class ExportCompoundForm
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
      this.gvFiles = new System.Windows.Forms.DataGridView();
      this.IsDecoy = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.cbViewValidOnly = new System.Windows.Forms.CheckBox();
      this.cbViewDecoy = new System.Windows.Forms.CheckBox();
      this.dlgSave = new System.Windows.Forms.SaveFileDialog();
      this.cbInvalidCompoundAsNA = new System.Windows.Forms.CheckBox();
      this.objectNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.precursurFormulaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lightMzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.heavyMzDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.enabledDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.compoundItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.compoundItemBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.gvFiles);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnSave);
      this.splitContainer1.Panel2.Controls.Add(this.btnClose);
      this.splitContainer1.Panel2.Controls.Add(this.cbInvalidCompoundAsNA);
      this.splitContainer1.Panel2.Controls.Add(this.cbViewValidOnly);
      this.splitContainer1.Panel2.Controls.Add(this.cbViewDecoy);
      this.splitContainer1.Size = new System.Drawing.Size(793, 357);
      this.splitContainer1.SplitterDistance = 314;
      this.splitContainer1.TabIndex = 0;
      // 
      // gvFiles
      // 
      this.gvFiles.AutoGenerateColumns = false;
      this.gvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.objectNameDataGridViewTextBoxColumn,
            this.precursurFormulaDataGridViewTextBoxColumn,
            this.lightMzDataGridViewTextBoxColumn,
            this.heavyMzDataGridViewTextBoxColumn,
            this.enabledDataGridViewCheckBoxColumn,
            this.IsDecoy});
      this.gvFiles.DataSource = this.compoundItemBindingSource;
      this.gvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvFiles.Location = new System.Drawing.Point(0, 0);
      this.gvFiles.MultiSelect = false;
      this.gvFiles.Name = "gvFiles";
      this.gvFiles.ReadOnly = true;
      this.gvFiles.RowTemplate.Height = 23;
      this.gvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.gvFiles.Size = new System.Drawing.Size(793, 314);
      this.gvFiles.TabIndex = 0;
      this.gvFiles.VirtualMode = true;
      this.gvFiles.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.gvCompounds_CellValueNeeded);
      // 
      // IsDecoy
      // 
      this.IsDecoy.DataPropertyName = "IsDecoy";
      this.IsDecoy.HeaderText = "Is Decoy";
      this.IsDecoy.Name = "IsDecoy";
      this.IsDecoy.ReadOnly = true;
      this.IsDecoy.Width = 60;
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(625, 8);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 3;
      this.btnSave.Text = "&Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(706, 8);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 23);
      this.btnClose.TabIndex = 2;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // cbViewValidOnly
      // 
      this.cbViewValidOnly.AutoSize = true;
      this.cbViewValidOnly.Location = new System.Drawing.Point(129, 11);
      this.cbViewValidOnly.Name = "cbViewValidOnly";
      this.cbViewValidOnly.Size = new System.Drawing.Size(114, 16);
      this.cbViewValidOnly.TabIndex = 1;
      this.cbViewValidOnly.Text = "View valid only";
      this.cbViewValidOnly.UseVisualStyleBackColor = true;
      this.cbViewValidOnly.CheckedChanged += new System.EventHandler(this.cbViewValidOnly_CheckedChanged);
      // 
      // cbViewDecoy
      // 
      this.cbViewDecoy.AutoSize = true;
      this.cbViewDecoy.Location = new System.Drawing.Point(22, 12);
      this.cbViewDecoy.Name = "cbViewDecoy";
      this.cbViewDecoy.Size = new System.Drawing.Size(84, 16);
      this.cbViewDecoy.TabIndex = 0;
      this.cbViewDecoy.Text = "View decoy";
      this.cbViewDecoy.UseVisualStyleBackColor = true;
      this.cbViewDecoy.CheckedChanged += new System.EventHandler(this.cbViewDecoy_CheckedChanged);
      // 
      // dlgSave
      // 
      this.dlgSave.DefaultExt = "csv";
      this.dlgSave.Filter = "Quantification result|*.csv|All files|*.*";
      this.dlgSave.Title = "Save compound quantification result to ...";
      // 
      // cbInvalidCompoundAsNA
      // 
      this.cbInvalidCompoundAsNA.AutoSize = true;
      this.cbInvalidCompoundAsNA.Location = new System.Drawing.Point(269, 11);
      this.cbInvalidCompoundAsNA.Name = "cbInvalidCompoundAsNA";
      this.cbInvalidCompoundAsNA.Size = new System.Drawing.Size(192, 16);
      this.cbInvalidCompoundAsNA.TabIndex = 1;
      this.cbInvalidCompoundAsNA.Text = "Show invalid compound As N/A";
      this.cbInvalidCompoundAsNA.UseVisualStyleBackColor = true;
      this.cbInvalidCompoundAsNA.CheckedChanged += new System.EventHandler(this.cbViewValidOnly_CheckedChanged);
      // 
      // objectNameDataGridViewTextBoxColumn
      // 
      this.objectNameDataGridViewTextBoxColumn.DataPropertyName = "ObjectName";
      this.objectNameDataGridViewTextBoxColumn.HeaderText = "Object Name";
      this.objectNameDataGridViewTextBoxColumn.Name = "objectNameDataGridViewTextBoxColumn";
      this.objectNameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // precursurFormulaDataGridViewTextBoxColumn
      // 
      this.precursurFormulaDataGridViewTextBoxColumn.DataPropertyName = "PrecursurFormula";
      this.precursurFormulaDataGridViewTextBoxColumn.HeaderText = "Compound Formula";
      this.precursurFormulaDataGridViewTextBoxColumn.Name = "precursurFormulaDataGridViewTextBoxColumn";
      this.precursurFormulaDataGridViewTextBoxColumn.ReadOnly = true;
      this.precursurFormulaDataGridViewTextBoxColumn.Width = 150;
      // 
      // lightMzDataGridViewTextBoxColumn
      // 
      this.lightMzDataGridViewTextBoxColumn.DataPropertyName = "LightMz";
      this.lightMzDataGridViewTextBoxColumn.HeaderText = "Light MZ";
      this.lightMzDataGridViewTextBoxColumn.Name = "lightMzDataGridViewTextBoxColumn";
      this.lightMzDataGridViewTextBoxColumn.ReadOnly = true;
      this.lightMzDataGridViewTextBoxColumn.Width = 80;
      // 
      // heavyMzDataGridViewTextBoxColumn
      // 
      this.heavyMzDataGridViewTextBoxColumn.DataPropertyName = "HeavyMz";
      this.heavyMzDataGridViewTextBoxColumn.HeaderText = "Heavy MZ";
      this.heavyMzDataGridViewTextBoxColumn.Name = "heavyMzDataGridViewTextBoxColumn";
      this.heavyMzDataGridViewTextBoxColumn.ReadOnly = true;
      this.heavyMzDataGridViewTextBoxColumn.Width = 80;
      // 
      // enabledDataGridViewCheckBoxColumn
      // 
      this.enabledDataGridViewCheckBoxColumn.DataPropertyName = "Enabled";
      this.enabledDataGridViewCheckBoxColumn.HeaderText = "Enabled";
      this.enabledDataGridViewCheckBoxColumn.Name = "enabledDataGridViewCheckBoxColumn";
      this.enabledDataGridViewCheckBoxColumn.ReadOnly = true;
      this.enabledDataGridViewCheckBoxColumn.Width = 60;
      // 
      // compoundItemBindingSource
      // 
      this.compoundItemBindingSource.DataSource = typeof(RCPA.Proteomics.Quantification.Srm.CompoundItem);
      // 
      // ExportCompoundForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(793, 357);
      this.Controls.Add(this.splitContainer1);
      this.Name = "ExportCompoundForm";
      this.TabText = "ExportCompoundForm";
      this.Text = "ExportCompoundForm";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.Panel2.PerformLayout();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvFiles)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.compoundItemBindingSource)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView gvFiles;
    private System.Windows.Forms.BindingSource compoundItemBindingSource;
    private System.Windows.Forms.DataGridViewTextBoxColumn objectNameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn precursurFormulaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn lightMzDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn heavyMzDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewCheckBoxColumn enabledDataGridViewCheckBoxColumn;
    private System.Windows.Forms.DataGridViewCheckBoxColumn IsDecoy;
    private System.Windows.Forms.CheckBox cbViewValidOnly;
    private System.Windows.Forms.CheckBox cbViewDecoy;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.SaveFileDialog dlgSave;
    private System.Windows.Forms.CheckBox cbInvalidCompoundAsNA;
  }
}