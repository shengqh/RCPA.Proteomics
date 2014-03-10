namespace RCPA.Proteomics.Quantification.Srm
{
  partial class SrmTransitionDefinitionForm
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
      this.gvItems = new System.Windows.Forms.DataGridView();
      this.colAnnotationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colPropertyName = new System.Windows.Forms.DataGridViewComboBoxColumn();
      this.fileDefinitionItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.panel1 = new System.Windows.Forms.Panel();
      this.txtDescription = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDelimiter = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnInit = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnClose = new System.Windows.Forms.Button();
      this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
      this.dlgOpenFormatFile = new System.Windows.Forms.OpenFileDialog();
      this.dlgSaveFormatFile = new System.Windows.Forms.SaveFileDialog();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.gvItems)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.fileDefinitionItemBindingSource)).BeginInit();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.IsSplitterFixed = true;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.gvItems);
      this.splitContainer1.Panel1.Controls.Add(this.panel1);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.AutoScroll = true;
      this.splitContainer1.Panel2.Controls.Add(this.btnInit);
      this.splitContainer1.Panel2.Controls.Add(this.btnLoad);
      this.splitContainer1.Panel2.Controls.Add(this.btnSave);
      this.splitContainer1.Panel2.Controls.Add(this.btnClose);
      this.splitContainer1.Size = new System.Drawing.Size(723, 504);
      this.splitContainer1.SplitterDistance = 452;
      this.splitContainer1.TabIndex = 0;
      // 
      // gvItems
      // 
      this.gvItems.AutoGenerateColumns = false;
      this.gvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.gvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAnnotationName,
            this.colPropertyName});
      this.gvItems.DataSource = this.fileDefinitionItemBindingSource;
      this.gvItems.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gvItems.Location = new System.Drawing.Point(0, 47);
      this.gvItems.Name = "gvItems";
      this.gvItems.RowTemplate.Height = 23;
      this.gvItems.Size = new System.Drawing.Size(723, 405);
      this.gvItems.TabIndex = 0;
      // 
      // colAnnotationName
      // 
      this.colAnnotationName.DataPropertyName = "AnnotationName";
      this.colAnnotationName.HeaderText = "AnnotationName";
      this.colAnnotationName.Name = "colAnnotationName";
      this.colAnnotationName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colAnnotationName.Width = 250;
      // 
      // colPropertyName
      // 
      this.colPropertyName.DataPropertyName = "PropertyName";
      this.colPropertyName.HeaderText = "PropertyName";
      this.colPropertyName.Name = "colPropertyName";
      this.colPropertyName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.colPropertyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      this.colPropertyName.Width = 250;
      // 
      // fileDefinitionItemBindingSource
      // 
      this.fileDefinitionItemBindingSource.DataSource = typeof(RCPA.Format.FileDefinitionItem);
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtDescription);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.txtDelimiter);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(723, 47);
      this.panel1.TabIndex = 1;
      // 
      // txtDescription
      // 
      this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDescription.Location = new System.Drawing.Point(273, 11);
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new System.Drawing.Size(438, 20);
      this.txtDescription.TabIndex = 3;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(193, 15);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(60, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Description";
      // 
      // txtDelimiter
      // 
      this.txtDelimiter.Location = new System.Drawing.Point(75, 10);
      this.txtDelimiter.Name = "txtDelimiter";
      this.txtDelimiter.Size = new System.Drawing.Size(100, 20);
      this.txtDelimiter.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(47, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Delimiter";
      // 
      // btnInit
      // 
      this.btnInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnInit.Location = new System.Drawing.Point(293, 14);
      this.btnInit.Name = "btnInit";
      this.btnInit.Size = new System.Drawing.Size(121, 25);
      this.btnInit.TabIndex = 0;
      this.btnInit.Text = "&New from data...";
      this.btnInit.UseVisualStyleBackColor = true;
      this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(420, 14);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(108, 25);
      this.btnLoad.TabIndex = 0;
      this.btnLoad.Text = "&Load format...";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(534, 14);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(105, 25);
      this.btnSave.TabIndex = 0;
      this.btnSave.Text = "&Save format...";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(645, 14);
      this.btnClose.Name = "btnClose";
      this.btnClose.Size = new System.Drawing.Size(75, 25);
      this.btnClose.TabIndex = 0;
      this.btnClose.Text = "&Close";
      this.btnClose.UseVisualStyleBackColor = true;
      this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
      // 
      // dlgOpenFile
      // 
      this.dlgOpenFile.DefaultExt = "csv";
      this.dlgOpenFile.Filter = "Transition Files|*.csv;*.txt|All Files|*.*";
      this.dlgOpenFile.Title = "Open transition file";
      // 
      // dlgOpenFormatFile
      // 
      this.dlgOpenFormatFile.DefaultExt = "srmformat";
      this.dlgOpenFormatFile.Filter = "Transition format file|*.srmformat|All files|*.*";
      this.dlgOpenFormatFile.Title = "Open transition format file";
      // 
      // dlgSaveFormatFile
      // 
      this.dlgSaveFormatFile.DefaultExt = "srmformat";
      this.dlgSaveFormatFile.Filter = "Transition format file|*.srmformat|All files|*.*";
      this.dlgSaveFormatFile.Title = "Save transition format file";
      // 
      // SrmTransitionDefinitionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(723, 504);
      this.Controls.Add(this.splitContainer1);
      this.Name = "SrmTransitionDefinitionForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "SrmTransitionDefinitionForm";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.gvItems)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.fileDefinitionItemBindingSource)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.DataGridView gvItems;
    private System.Windows.Forms.BindingSource fileDefinitionItemBindingSource;
    private System.Windows.Forms.Button btnInit;
    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    private System.Windows.Forms.DataGridViewTextBoxColumn colAnnotationName;
    private System.Windows.Forms.DataGridViewComboBoxColumn colPropertyName;
    private System.Windows.Forms.OpenFileDialog dlgOpenFormatFile;
    private System.Windows.Forms.SaveFileDialog dlgSaveFormatFile;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtDelimiter;
    private System.Windows.Forms.Label label1;
  }
}