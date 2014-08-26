namespace RCPA.Tools.Quantification
{
  partial class ITraqPeptideStatisticBuilderUI
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
      this.btnRLocation = new System.Windows.Forms.Button();
      this.txtRLocation = new System.Windows.Forms.TextBox();
      this.itraqIons = new RCPA.Proteomics.Quantification.ITraq.IsobaricIonField();
      this.txtValidProbability = new System.Windows.Forms.TextBox();
      this.lblValidation = new System.Windows.Forms.Label();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(25, 13);
      this.pnlFile.Size = new System.Drawing.Size(972, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(226, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(746, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(226, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 191);
      this.lblProgress.Size = new System.Drawing.Size(1019, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 214);
      this.progressBar.Size = new System.Drawing.Size(1019, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(557, 9);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(472, 9);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(387, 9);
      // 
      // btnRLocation
      // 
      this.btnRLocation.Location = new System.Drawing.Point(25, 57);
      this.btnRLocation.Name = "btnRLocation";
      this.btnRLocation.Size = new System.Drawing.Size(226, 25);
      this.btnRLocation.TabIndex = 8;
      this.btnRLocation.Text = "button1";
      this.btnRLocation.UseVisualStyleBackColor = true;
      // 
      // txtRLocation
      // 
      this.txtRLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtRLocation.Location = new System.Drawing.Point(257, 60);
      this.txtRLocation.Name = "txtRLocation";
      this.txtRLocation.Size = new System.Drawing.Size(740, 20);
      this.txtRLocation.TabIndex = 9;
      // 
      // itraqIons
      // 
      this.itraqIons.Location = new System.Drawing.Point(140, 90);
      this.itraqIons.Name = "itraqIons";
      this.itraqIons.Required = false;
      this.itraqIons.SelectedIons = "";
      this.itraqIons.Size = new System.Drawing.Size(857, 30);
      this.itraqIons.TabIndex = 13;
      // 
      // txtValidProbability
      // 
      this.txtValidProbability.Location = new System.Drawing.Point(539, 127);
      this.txtValidProbability.Name = "txtValidProbability";
      this.txtValidProbability.Size = new System.Drawing.Size(100, 20);
      this.txtValidProbability.TabIndex = 15;
      // 
      // lblValidation
      // 
      this.lblValidation.AutoSize = true;
      this.lblValidation.Location = new System.Drawing.Point(253, 131);
      this.lblValidation.Name = "lblValidation";
      this.lblValidation.Size = new System.Drawing.Size(217, 13);
      this.lblValidation.TabIndex = 14;
      this.lblValidation.Text = "Filter peptide by iTRAQ invalid probability <= ";
      // 
      // ITraqPeptideStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1019, 276);
      this.Controls.Add(this.txtValidProbability);
      this.Controls.Add(this.lblValidation);
      this.Controls.Add(this.btnRLocation);
      this.Controls.Add(this.txtRLocation);
      this.Controls.Add(this.itraqIons);
      this.Name = "ITraqPeptideStatisticBuilderUI";
      this.TabText = "";
      this.Text = "Identified Peptide iTRAQ Statistic Builder";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.itraqIons, 0);
      this.Controls.SetChildIndex(this.txtRLocation, 0);
      this.Controls.SetChildIndex(this.btnRLocation, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.lblValidation, 0);
      this.Controls.SetChildIndex(this.txtValidProbability, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnRLocation;
    private System.Windows.Forms.TextBox txtRLocation;
    private Proteomics.Quantification.ITraq.IsobaricIonField itraqIons;
    private System.Windows.Forms.TextBox txtValidProbability;
    private System.Windows.Forms.Label lblValidation;


  }
}