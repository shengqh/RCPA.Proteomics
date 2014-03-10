namespace RCPA.Proteomics.Quantification.Labelfree
{
  partial class ProteinLabelFreeQuantificationBuilderUI
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
      this.btnLoad = new System.Windows.Forms.Button();
      this.cbFilterType = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.cbAccessNumberParser = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(45, 32);
      this.pnlFile.Size = new System.Drawing.Size(877, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(192, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(685, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(192, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 487);
      this.lblProgress.Size = new System.Drawing.Size(1040, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 508);
      this.progressBar.Size = new System.Drawing.Size(1040, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(568, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(483, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(398, 7);
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(928, 31);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 23);
      this.btnLoad.TabIndex = 9;
      this.btnLoad.Text = "&Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // cbFilterType
      // 
      this.cbFilterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFilterType.FormattingEnabled = true;
      this.cbFilterType.Location = new System.Drawing.Point(243, 60);
      this.cbFilterType.Name = "cbFilterType";
      this.cbFilterType.Size = new System.Drawing.Size(679, 20);
      this.cbFilterType.TabIndex = 16;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(106, 63);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(131, 12);
      this.label3.TabIndex = 15;
      this.label3.Text = "Quantification method";
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Description";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(45, 127);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.+)_(\\d){1,2}";
      this.pnlClassification.Size = new System.Drawing.Size(958, 320);
      this.pnlClassification.TabIndex = 21;
      // 
      // cbAccessNumberParser
      // 
      this.cbAccessNumberParser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbAccessNumberParser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbAccessNumberParser.FormattingEnabled = true;
      this.cbAccessNumberParser.Location = new System.Drawing.Point(243, 86);
      this.cbAccessNumberParser.Name = "cbAccessNumberParser";
      this.cbAccessNumberParser.Size = new System.Drawing.Size(679, 20);
      this.cbAccessNumberParser.TabIndex = 23;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(58, 89);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(179, 12);
      this.label1.TabIndex = 22;
      this.label1.Text = "Protein access number pattern";
      // 
      // ProteinLabelFreeQuantificationBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1040, 565);
      this.Controls.Add(this.cbAccessNumberParser);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.cbFilterType);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnLoad);
      this.Name = "ProteinLabelFreeQuantificationBuilderUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbFilterType, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbAccessNumberParser, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnLoad;
    private System.Windows.Forms.ComboBox cbFilterType;
    private System.Windows.Forms.Label label3;
    private RCPA.Proteomics.ClassificationPanel pnlClassification;
    private System.Windows.Forms.ComboBox cbAccessNumberParser;
    private System.Windows.Forms.Label label1;
  }
}
