namespace RCPA.Proteomics.Quantification.Srm
{
  partial class MrmSelectFileForm
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
      this.multipleFiles = new RCPA.Gui.MultipleFileField();
      this.panel1 = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // multipleFiles
      // 
      this.multipleFiles.Dock = System.Windows.Forms.DockStyle.Fill;
      this.multipleFiles.FileArgument = null;
      this.multipleFiles.FileDescription = "MRM Files (selected file only)";
      this.multipleFiles.FileNames = new string[0];
      this.multipleFiles.Key = "File";
      this.multipleFiles.LoadButtonVisible = false;
      this.multipleFiles.Location = new System.Drawing.Point(0, 0);
      this.multipleFiles.Name = "multipleFiles";
      this.multipleFiles.SaveButtonVisible = false;
      this.multipleFiles.SelectedIndex = -1;
      this.multipleFiles.SelectedItem = null;
      this.multipleFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
      this.multipleFiles.Size = new System.Drawing.Size(809, 467);
      this.multipleFiles.TabIndex = 1;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btnCancel);
      this.panel1.Controls.Add(this.btnOk);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 467);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(809, 36);
      this.panel1.TabIndex = 2;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(731, 7);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.Location = new System.Drawing.Point(650, 7);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // MrmSelectFileForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(809, 503);
      this.Controls.Add(this.multipleFiles);
      this.Controls.Add(this.panel1);
      this.Name = "MrmSelectFileForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.TabText = "MRM File List";
      this.Text = "MRM File List";
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.MultipleFileField multipleFiles;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;

  }
}