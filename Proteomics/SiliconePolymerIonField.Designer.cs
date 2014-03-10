namespace RCPA.Proteomics
{
  partial class SiliconePolymerIonField
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.siliconePolymerDataGridView = new System.Windows.Forms.DataGridView();
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.panel2 = new System.Windows.Forms.Panel();
      this.btnSelectAll = new System.Windows.Forms.Button();
      this.btnClearAll = new System.Windows.Forms.Button();
      this.btnDefault = new System.Windows.Forms.Button();
      this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.bsPolymers = new System.Windows.Forms.BindingSource(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.siliconePolymerDataGridView)).BeginInit();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.bsPolymers)).BeginInit();
      this.SuspendLayout();
      // 
      // siliconePolymerDataGridView
      // 
      this.siliconePolymerDataGridView.AutoGenerateColumns = false;
      this.siliconePolymerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.siliconePolymerDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn4});
      this.siliconePolymerDataGridView.DataSource = this.bsPolymers;
      this.siliconePolymerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.siliconePolymerDataGridView.Location = new System.Drawing.Point(0, 23);
      this.siliconePolymerDataGridView.Name = "siliconePolymerDataGridView";
      this.siliconePolymerDataGridView.RowTemplate.Height = 23;
      this.siliconePolymerDataGridView.Size = new System.Drawing.Size(522, 326);
      this.siliconePolymerDataGridView.TabIndex = 14;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(609, 23);
      this.panel1.TabIndex = 15;
      // 
      // label1
      // 
      this.label1.Dock = System.Windows.Forms.DockStyle.Left;
      this.label1.Location = new System.Drawing.Point(0, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(155, 23);
      this.label1.TabIndex = 14;
      this.label1.Text = "Select silico polymers";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.btnDefault);
      this.panel2.Controls.Add(this.btnClearAll);
      this.panel2.Controls.Add(this.btnSelectAll);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
      this.panel2.Location = new System.Drawing.Point(522, 23);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(87, 326);
      this.panel2.TabIndex = 16;
      // 
      // btnSelectAll
      // 
      this.btnSelectAll.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSelectAll.Location = new System.Drawing.Point(0, 0);
      this.btnSelectAll.Name = "btnSelectAll";
      this.btnSelectAll.Size = new System.Drawing.Size(87, 23);
      this.btnSelectAll.TabIndex = 18;
      this.btnSelectAll.Text = "Select All";
      this.btnSelectAll.UseVisualStyleBackColor = true;
      this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
      // 
      // btnClearAll
      // 
      this.btnClearAll.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnClearAll.Location = new System.Drawing.Point(0, 23);
      this.btnClearAll.Name = "btnClearAll";
      this.btnClearAll.Size = new System.Drawing.Size(87, 23);
      this.btnClearAll.TabIndex = 19;
      this.btnClearAll.Text = "Clear All";
      this.btnClearAll.UseVisualStyleBackColor = true;
      this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
      // 
      // btnDefault
      // 
      this.btnDefault.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnDefault.Location = new System.Drawing.Point(0, 46);
      this.btnDefault.Name = "btnDefault";
      this.btnDefault.Size = new System.Drawing.Size(87, 23);
      this.btnDefault.TabIndex = 20;
      this.btnDefault.Text = "Set Default";
      this.btnDefault.UseVisualStyleBackColor = true;
      this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
      // 
      // dataGridViewTextBoxColumn1
      // 
      this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn1.DataPropertyName = "PolymerNumber";
      this.dataGridViewTextBoxColumn1.HeaderText = "N";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.Width = 36;
      // 
      // dataGridViewCheckBoxColumn1
      // 
      this.dataGridViewCheckBoxColumn1.DataPropertyName = "PrecursorMinusCH4Checked";
      this.dataGridViewCheckBoxColumn1.HeaderText = "";
      this.dataGridViewCheckBoxColumn1.MinimumWidth = 20;
      this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
      this.dataGridViewCheckBoxColumn1.Width = 20;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.DataPropertyName = "PrecursorMinusCH4";
      this.dataGridViewTextBoxColumn2.HeaderText = "MH-CH4";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.Width = 150;
      // 
      // dataGridViewCheckBoxColumn2
      // 
      this.dataGridViewCheckBoxColumn2.DataPropertyName = "PrecursorChecked";
      this.dataGridViewCheckBoxColumn2.HeaderText = "";
      this.dataGridViewCheckBoxColumn2.MinimumWidth = 20;
      this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
      this.dataGridViewCheckBoxColumn2.Width = 20;
      // 
      // dataGridViewTextBoxColumn3
      // 
      this.dataGridViewTextBoxColumn3.DataPropertyName = "Precursor";
      this.dataGridViewTextBoxColumn3.HeaderText = "MH";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.Width = 150;
      // 
      // dataGridViewCheckBoxColumn3
      // 
      this.dataGridViewCheckBoxColumn3.DataPropertyName = "PrecursorNH3Checked";
      this.dataGridViewCheckBoxColumn3.HeaderText = "";
      this.dataGridViewCheckBoxColumn3.MinimumWidth = 20;
      this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
      this.dataGridViewCheckBoxColumn3.Width = 20;
      // 
      // dataGridViewTextBoxColumn4
      // 
      this.dataGridViewTextBoxColumn4.DataPropertyName = "PrecursorNH3";
      this.dataGridViewTextBoxColumn4.HeaderText = "MH+MH3";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.Width = 150;
      // 
      // bsPolymers
      // 
      this.bsPolymers.DataSource = typeof(RCPA.Proteomics.SiliconePolymer);
      // 
      // SiliconePolymerIonField
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.siliconePolymerDataGridView);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel1);
      this.Name = "SiliconePolymerIonField";
      this.Size = new System.Drawing.Size(609, 349);
      ((System.ComponentModel.ISupportInitialize)(this.siliconePolymerDataGridView)).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.bsPolymers)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.BindingSource bsPolymers;
    private System.Windows.Forms.DataGridView siliconePolymerDataGridView;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Button btnDefault;
    private System.Windows.Forms.Button btnClearAll;
    private System.Windows.Forms.Button btnSelectAll;

  }
}
