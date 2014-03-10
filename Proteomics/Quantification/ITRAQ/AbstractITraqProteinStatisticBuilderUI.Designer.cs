namespace RCPA.Proteomics.Quantification.ITraq
{
  partial class AbstractITraqProteinStatisticBuilderUI
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
      this.txtValidProbability = new System.Windows.Forms.TextBox();
      this.btnLoad = new System.Windows.Forms.Button();
      this.cbNormalize = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbRatioCalculator = new System.Windows.Forms.ComboBox();
      this.cbFilterPeptide = new System.Windows.Forms.CheckBox();
      this.cbModifiedOnly = new System.Windows.Forms.CheckBox();
      this.itraqIons = new RCPA.Proteomics.Quantification.ITraq.IsobaricIonField();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.txtModifiedCharacter = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(25, 24);
      this.pnlFile.Size = new System.Drawing.Size(942, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(226, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(716, 20);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(226, 24);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 538);
      this.lblProgress.Size = new System.Drawing.Size(1080, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 561);
      this.progressBar.Size = new System.Drawing.Size(1080, 23);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(588, 9);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(503, 9);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(418, 9);
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
      this.txtRLocation.Location = new System.Drawing.Point(251, 60);
      this.txtRLocation.Name = "txtRLocation";
      this.txtRLocation.Size = new System.Drawing.Size(807, 20);
      this.txtRLocation.TabIndex = 9;
      // 
      // txtValidProbability
      // 
      this.txtValidProbability.Location = new System.Drawing.Point(541, 123);
      this.txtValidProbability.Name = "txtValidProbability";
      this.txtValidProbability.Size = new System.Drawing.Size(100, 20);
      this.txtValidProbability.TabIndex = 12;
      // 
      // btnLoad
      // 
      this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoad.Location = new System.Drawing.Point(981, 24);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(75, 25);
      this.btnLoad.TabIndex = 14;
      this.btnLoad.Text = "&Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // cbNormalize
      // 
      this.cbNormalize.AutoSize = true;
      this.cbNormalize.Location = new System.Drawing.Point(257, 154);
      this.cbNormalize.Name = "cbNormalize";
      this.cbNormalize.Size = new System.Drawing.Size(297, 17);
      this.cbNormalize.TabIndex = 15;
      this.cbNormalize.Text = "Normalize median value of ratio(channel/reference) to 1.0";
      this.cbNormalize.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(249, 208);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(196, 13);
      this.label1.TabIndex = 16;
      this.label1.Text = "Calculate ratio from peptide to protein by";
      // 
      // cbRatioCalculator
      // 
      this.cbRatioCalculator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cbRatioCalculator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbRatioCalculator.FormattingEnabled = true;
      this.cbRatioCalculator.Location = new System.Drawing.Point(514, 204);
      this.cbRatioCalculator.Name = "cbRatioCalculator";
      this.cbRatioCalculator.Size = new System.Drawing.Size(453, 21);
      this.cbRatioCalculator.TabIndex = 17;
      // 
      // cbFilterPeptide
      // 
      this.cbFilterPeptide.AutoSize = true;
      this.cbFilterPeptide.Location = new System.Drawing.Point(281, 126);
      this.cbFilterPeptide.Name = "cbFilterPeptide";
      this.cbFilterPeptide.Size = new System.Drawing.Size(254, 17);
      this.cbFilterPeptide.TabIndex = 18;
      this.cbFilterPeptide.Text = "Remove peptide by iTRAQ invalid probability <= ";
      this.cbFilterPeptide.UseVisualStyleBackColor = true;
      // 
      // cbModifiedOnly
      // 
      this.cbModifiedOnly.AutoSize = true;
      this.cbModifiedOnly.Location = new System.Drawing.Point(257, 177);
      this.cbModifiedOnly.Name = "cbModifiedOnly";
      this.cbModifiedOnly.Size = new System.Drawing.Size(270, 17);
      this.cbModifiedOnly.TabIndex = 19;
      this.cbModifiedOnly.Text = "Quantify peptides with modification characters only :";
      this.cbModifiedOnly.UseVisualStyleBackColor = true;
      // 
      // itraqIons
      // 
      this.itraqIons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.itraqIons.Location = new System.Drawing.Point(151, 89);
      this.itraqIons.Name = "itraqIons";
      this.itraqIons.SelectedIons = "114,115";
      this.itraqIons.Size = new System.Drawing.Size(905, 28);
      this.itraqIons.TabIndex = 10;
      this.itraqIons.IonCheckedChanged += new System.EventHandler(this.itraqIons_IonCheckedChanged);
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Experiment definition";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(25, 231);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.*)";
      this.pnlClassification.Size = new System.Drawing.Size(1033, 267);
      this.pnlClassification.TabIndex = 13;
      // 
      // txtModifiedCharacter
      // 
      this.txtModifiedCharacter.Location = new System.Drawing.Point(533, 175);
      this.txtModifiedCharacter.Name = "txtModifiedCharacter";
      this.txtModifiedCharacter.Size = new System.Drawing.Size(100, 20);
      this.txtModifiedCharacter.TabIndex = 20;
      // 
      // AbstractITraqProteinStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1080, 623);
      this.Controls.Add(this.txtModifiedCharacter);
      this.Controls.Add(this.cbModifiedOnly);
      this.Controls.Add(this.cbFilterPeptide);
      this.Controls.Add(this.txtValidProbability);
      this.Controls.Add(this.btnRLocation);
      this.Controls.Add(this.txtRLocation);
      this.Controls.Add(this.itraqIons);
      this.Controls.Add(this.cbRatioCalculator);
      this.Controls.Add(this.cbNormalize);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.label1);
      this.Name = "AbstractITraqProteinStatisticBuilderUI";
      this.TabText = "";
      this.Text = "Identified Protein iTRAQ Statistic Builder";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.cbNormalize, 0);
      this.Controls.SetChildIndex(this.cbRatioCalculator, 0);
      this.Controls.SetChildIndex(this.itraqIons, 0);
      this.Controls.SetChildIndex(this.txtRLocation, 0);
      this.Controls.SetChildIndex(this.btnRLocation, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.txtValidProbability, 0);
      this.Controls.SetChildIndex(this.cbFilterPeptide, 0);
      this.Controls.SetChildIndex(this.cbModifiedOnly, 0);
      this.Controls.SetChildIndex(this.txtModifiedCharacter, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected System.Windows.Forms.Button btnRLocation;
    protected System.Windows.Forms.TextBox txtRLocation;
    protected IsobaricIonField itraqIons;
    protected System.Windows.Forms.TextBox txtValidProbability;
    protected ClassificationPanel pnlClassification;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.CheckBox cbNormalize;
    protected System.Windows.Forms.Label label1;
    protected System.Windows.Forms.ComboBox cbRatioCalculator;
    protected System.Windows.Forms.CheckBox cbFilterPeptide;
    protected System.Windows.Forms.CheckBox cbModifiedOnly;
    protected System.Windows.Forms.TextBox txtModifiedCharacter;


  }
}