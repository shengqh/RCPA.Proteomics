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
      this.txtIsobaricXmlFile = new System.Windows.Forms.TextBox();
      this.cbNormalize = new System.Windows.Forms.CheckBox();
      this.cbModifiedOnly = new System.Windows.Forms.CheckBox();
      this.txtModifiedCharacter = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(25, 24);
      this.pnlFile.Size = new System.Drawing.Size(1024, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(300, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(724, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(300, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 140);
      this.lblProgress.Size = new System.Drawing.Size(1080, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 163);
      this.progressBar.Size = new System.Drawing.Size(1080, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 186);
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
      this.btnDesignFile.Location = new System.Drawing.Point(25, 57);
      this.btnDesignFile.Name = "btnDesignFile";
      this.btnDesignFile.Size = new System.Drawing.Size(300, 25);
      this.btnDesignFile.TabIndex = 8;
      this.btnDesignFile.Text = "btnDesignFile";
      this.btnDesignFile.UseVisualStyleBackColor = true;
      // 
      // txtIsobaricXmlFile
      // 
      this.txtIsobaricXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIsobaricXmlFile.Location = new System.Drawing.Point(325, 60);
      this.txtIsobaricXmlFile.Name = "txtIsobaricXmlFile";
      this.txtIsobaricXmlFile.Size = new System.Drawing.Size(724, 20);
      this.txtIsobaricXmlFile.TabIndex = 9;
      // 
      // cbNormalize
      // 
      this.cbNormalize.AutoSize = true;
      this.cbNormalize.Location = new System.Drawing.Point(325, 92);
      this.cbNormalize.Name = "cbNormalize";
      this.cbNormalize.Size = new System.Drawing.Size(234, 17);
      this.cbNormalize.TabIndex = 15;
      this.cbNormalize.Text = "Normalize channels by cyclic loess algorithm";
      this.cbNormalize.UseVisualStyleBackColor = true;
      // 
      // cbModifiedOnly
      // 
      this.cbModifiedOnly.AutoSize = true;
      this.cbModifiedOnly.Location = new System.Drawing.Point(591, 92);
      this.cbModifiedOnly.Name = "cbModifiedOnly";
      this.cbModifiedOnly.Size = new System.Drawing.Size(270, 17);
      this.cbModifiedOnly.TabIndex = 19;
      this.cbModifiedOnly.Text = "Quantify peptides with modification characters only :";
      this.cbModifiedOnly.UseVisualStyleBackColor = true;
      this.cbModifiedOnly.Visible = false;
      // 
      // txtModifiedCharacter
      // 
      this.txtModifiedCharacter.Location = new System.Drawing.Point(858, 90);
      this.txtModifiedCharacter.Name = "txtModifiedCharacter";
      this.txtModifiedCharacter.Size = new System.Drawing.Size(100, 20);
      this.txtModifiedCharacter.TabIndex = 20;
      this.txtModifiedCharacter.Visible = false;
      // 
      // IsobaricPeptideStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1080, 225);
      this.Controls.Add(this.txtModifiedCharacter);
      this.Controls.Add(this.cbModifiedOnly);
      this.Controls.Add(this.btnDesignFile);
      this.Controls.Add(this.txtIsobaricXmlFile);
      this.Controls.Add(this.cbNormalize);
      this.Name = "IsobaricPeptideStatisticBuilderUI";
      this.TabText = "";
      this.Text = "Identified Protein iTRAQ Statistic Builder";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.cbNormalize, 0);
      this.Controls.SetChildIndex(this.txtIsobaricXmlFile, 0);
      this.Controls.SetChildIndex(this.btnDesignFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.cbModifiedOnly, 0);
      this.Controls.SetChildIndex(this.txtModifiedCharacter, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected System.Windows.Forms.Button btnDesignFile;
    protected System.Windows.Forms.TextBox txtIsobaricXmlFile;
    protected System.Windows.Forms.CheckBox cbNormalize;
    protected System.Windows.Forms.CheckBox cbModifiedOnly;
    protected System.Windows.Forms.TextBox txtModifiedCharacter;


  }
}