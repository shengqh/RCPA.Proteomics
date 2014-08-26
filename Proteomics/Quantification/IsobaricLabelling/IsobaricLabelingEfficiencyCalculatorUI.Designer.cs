namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  partial class IsobaricLabelingEfficiencyCalculatorUI
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
      this.txtModifiedAminoacid = new RCPA.Gui.TextField();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 92);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 115);
      // 
      // txtModifiedAminoacid
      // 
      this.txtModifiedAminoacid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModifiedAminoacid.Caption = "Modified Amino Acid";
      this.txtModifiedAminoacid.CaptionWidth = 193;
      this.txtModifiedAminoacid.DefaultValue = "K";
      this.txtModifiedAminoacid.Description = "";
      this.txtModifiedAminoacid.Key = "ModifiedAminoAcid";
      this.txtModifiedAminoacid.Location = new System.Drawing.Point(49, 43);
      this.txtModifiedAminoacid.Name = "txtModifiedAminoacid";
      this.txtModifiedAminoacid.PreCondition = null;
      this.txtModifiedAminoacid.Size = new System.Drawing.Size(906, 23);
      this.txtModifiedAminoacid.TabIndex = 10;
      this.txtModifiedAminoacid.TextWidth = 694;
      // 
      // IsobaricLabelingEfficiencyCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(955, 177);
      this.Controls.Add(this.txtModifiedAminoacid);
      this.Name = "IsobaricLabelingEfficiencyCalculatorUI";
      this.TabText = "IsobaricLabelingEfficiencyCalculatorUI";
      this.Text = "IsobaricLabelingEfficiencyCalculatorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.txtModifiedAminoacid, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.TextField txtModifiedAminoacid;
  }
}