namespace RCPA.Proteomics.Quantification
{
  partial class QuantificationExportForm
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
      this.panel2 = new System.Windows.Forms.Panel();
      this.cbProteinName = new System.Windows.Forms.CheckBox();
      this.cbExportScan = new System.Windows.Forms.CheckBox();
      this.cbSingleFile = new System.Windows.Forms.CheckBox();
      this.txtProteinNamePattern = new System.Windows.Forms.TextBox();
      this.panel3 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.lbScans = new RCPA.Gui.RcpaSelectList();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.cbProteinName);
      this.panel2.Controls.Add(this.cbExportScan);
      this.panel2.Controls.Add(this.cbSingleFile);
      this.panel2.Controls.Add(this.txtProteinNamePattern);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(876, 111);
      this.panel2.TabIndex = 5;
      // 
      // cbProteinName
      // 
      this.cbProteinName.AutoSize = true;
      this.cbProteinName.Location = new System.Drawing.Point(17, 26);
      this.cbProteinName.Name = "cbProteinName";
      this.cbProteinName.Size = new System.Drawing.Size(112, 17);
      this.cbProteinName.TabIndex = 4;
      this.cbProteinName.Text = "Filter protein name";
      this.cbProteinName.UseVisualStyleBackColor = true;
      // 
      // cbExportScan
      // 
      this.cbExportScan.AutoSize = true;
      this.cbExportScan.Location = new System.Drawing.Point(17, 72);
      this.cbExportScan.Name = "cbExportScan";
      this.cbExportScan.Size = new System.Drawing.Size(142, 17);
      this.cbExportScan.TabIndex = 5;
      this.cbExportScan.Text = "Export scan information?";
      this.cbExportScan.UseVisualStyleBackColor = true;
      // 
      // cbSingleFile
      // 
      this.cbSingleFile.AutoSize = true;
      this.cbSingleFile.Location = new System.Drawing.Point(17, 49);
      this.cbSingleFile.Name = "cbSingleFile";
      this.cbSingleFile.Size = new System.Drawing.Size(115, 17);
      this.cbSingleFile.TabIndex = 6;
      this.cbSingleFile.Text = "Save to single file?";
      this.cbSingleFile.UseVisualStyleBackColor = true;
      // 
      // txtProteinNamePattern
      // 
      this.txtProteinNamePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtProteinNamePattern.Location = new System.Drawing.Point(161, 24);
      this.txtProteinNamePattern.Name = "txtProteinNamePattern";
      this.txtProteinNamePattern.Size = new System.Drawing.Size(699, 20);
      this.txtProteinNamePattern.TabIndex = 3;
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.btnCancel);
      this.panel3.Controls.Add(this.btnOk);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel3.Location = new System.Drawing.Point(0, 557);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(876, 37);
      this.panel3.TabIndex = 5;
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(448, 6);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 25);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Location = new System.Drawing.Point(350, 6);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 25);
      this.btnOk.TabIndex = 5;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // lbScans
      // 
      this.lbScans.Description = "Select/sort scan headers";
      this.lbScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbScans.LoadButtonVisible = false;
      this.lbScans.Location = new System.Drawing.Point(0, 111);
      this.lbScans.Name = "lbScans";
      this.lbScans.SaveButtonVisible = false;
      this.lbScans.Size = new System.Drawing.Size(876, 446);
      this.lbScans.TabIndex = 6;
      // 
      // QuantificationExportForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(876, 594);
      this.Controls.Add(this.lbScans);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.panel3);
      this.Name = "QuantificationExportForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Export Options";
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.CheckBox cbProteinName;
    private System.Windows.Forms.CheckBox cbExportScan;
    private System.Windows.Forms.CheckBox cbSingleFile;
    private System.Windows.Forms.TextBox txtProteinNamePattern;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private Gui.RcpaSelectList lbScans;

  }
}