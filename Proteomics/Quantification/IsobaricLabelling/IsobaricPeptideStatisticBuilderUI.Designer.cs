namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  partial class IsobaricPeptideStatisticBuilderUI
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
      this.btnDesignFile = new System.Windows.Forms.Button();
      this.txtDesignFile = new System.Windows.Forms.TextBox();
      this.cbNormalize = new System.Windows.Forms.CheckBox();
      this.txtModifiedAminoacids = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbQuantifyMode = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.btnPeptideFile = new System.Windows.Forms.Button();
      this.txtPeptideFile = new System.Windows.Forms.TextBox();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 161);
      this.lblProgress.Size = new System.Drawing.Size(1080, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 184);
      this.progressBar.Size = new System.Drawing.Size(1080, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 207);
      this.pnlButton.Size = new System.Drawing.Size(1080, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(588, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(503, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(418, 8);
      // 
      // btnDesignFile
      // 
      this.btnDesignFile.Location = new System.Drawing.Point(25, 51);
      this.btnDesignFile.Name = "btnDesignFile";
      this.btnDesignFile.Size = new System.Drawing.Size(300, 25);
      this.btnDesignFile.TabIndex = 8;
      this.btnDesignFile.Text = "btnDesignFile";
      this.btnDesignFile.UseVisualStyleBackColor = true;
      // 
      // txtDesignFile
      // 
      this.txtDesignFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDesignFile.Location = new System.Drawing.Point(325, 54);
      this.txtDesignFile.Name = "txtDesignFile";
      this.txtDesignFile.Size = new System.Drawing.Size(724, 20);
      this.txtDesignFile.TabIndex = 9;
      // 
      // cbNormalize
      // 
      this.cbNormalize.AutoSize = true;
      this.cbNormalize.Location = new System.Drawing.Point(325, 86);
      this.cbNormalize.Name = "cbNormalize";
      this.cbNormalize.Size = new System.Drawing.Size(204, 17);
      this.cbNormalize.TabIndex = 15;
      this.cbNormalize.Text = "Normalize channels by loess algorithm";
      this.cbNormalize.UseVisualStyleBackColor = true;
      // 
      // txtModifiedAminoacids
      // 
      this.txtModifiedAminoacids.Location = new System.Drawing.Point(700, 110);
      this.txtModifiedAminoacids.Name = "txtModifiedAminoacids";
      this.txtModifiedAminoacids.Size = new System.Drawing.Size(100, 20);
      this.txtModifiedAminoacids.TabIndex = 20;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(323, 113);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 13);
      this.label1.TabIndex = 22;
      this.label1.Text = "Quantify peptides: ";
      // 
      // cbQuantifyMode
      // 
      this.cbQuantifyMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbQuantifyMode.FormattingEnabled = true;
      this.cbQuantifyMode.Location = new System.Drawing.Point(418, 109);
      this.cbQuantifyMode.Name = "cbQuantifyMode";
      this.cbQuantifyMode.Size = new System.Drawing.Size(160, 21);
      this.cbQuantifyMode.TabIndex = 23;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(580, 113);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(114, 13);
      this.label2.TabIndex = 24;
      this.label2.Text = ", modified amino acids:";
      // 
      // btnPeptideFile
      // 
      this.btnPeptideFile.Location = new System.Drawing.Point(25, 22);
      this.btnPeptideFile.Name = "btnPeptideFile";
      this.btnPeptideFile.Size = new System.Drawing.Size(300, 25);
      this.btnPeptideFile.TabIndex = 25;
      this.btnPeptideFile.Text = "btnPeptideFile";
      this.btnPeptideFile.UseVisualStyleBackColor = true;
      // 
      // txtPeptideFile
      // 
      this.txtPeptideFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPeptideFile.Location = new System.Drawing.Point(325, 25);
      this.txtPeptideFile.Name = "txtPeptideFile";
      this.txtPeptideFile.Size = new System.Drawing.Size(724, 20);
      this.txtPeptideFile.TabIndex = 26;
      // 
      // IsobaricPeptideStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1080, 246);
      this.Controls.Add(this.btnPeptideFile);
      this.Controls.Add(this.txtPeptideFile);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbQuantifyMode);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtModifiedAminoacids);
      this.Controls.Add(this.btnDesignFile);
      this.Controls.Add(this.txtDesignFile);
      this.Controls.Add(this.cbNormalize);
      this.Name = "IsobaricPeptideStatisticBuilderUI";
      this.TabText = "";
      this.Text = "Isobaric Labeling Peptide Statistic Builder";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.cbNormalize, 0);
      this.Controls.SetChildIndex(this.txtDesignFile, 0);
      this.Controls.SetChildIndex(this.btnDesignFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtModifiedAminoacids, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbQuantifyMode, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtPeptideFile, 0);
      this.Controls.SetChildIndex(this.btnPeptideFile, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected System.Windows.Forms.Button btnDesignFile;
    protected System.Windows.Forms.TextBox txtDesignFile;
    protected System.Windows.Forms.CheckBox cbNormalize;
    protected System.Windows.Forms.TextBox txtModifiedAminoacids;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbQuantifyMode;
    private System.Windows.Forms.Label label2;
    protected System.Windows.Forms.Button btnPeptideFile;
    protected System.Windows.Forms.TextBox txtPeptideFile;


  }
}