namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  partial class AbstractIsobaricProteinStatisticBuilderUI
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
      this.btnIsobaricXmlFile = new System.Windows.Forms.Button();
      this.txtIsobaricXmlFile = new System.Windows.Forms.TextBox();
      this.btnLoad = new System.Windows.Forms.Button();
      this.cbNormalize = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbRatioCalculator = new System.Windows.Forms.ComboBox();
      this.cbModifiedOnly = new System.Windows.Forms.CheckBox();
      this.txtModifiedCharacter = new System.Windows.Forms.TextBox();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.refChannels = new RCPA.Proteomics.Quantification.IsobaricLabelling.IsobaricChannelField();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(25, 24);
      this.pnlFile.Size = new System.Drawing.Size(950, 24);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(226, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(724, 20);
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
      // btnIsobaricXmlFile
      // 
      this.btnIsobaricXmlFile.Location = new System.Drawing.Point(25, 57);
      this.btnIsobaricXmlFile.Name = "btnIsobaricXmlFile";
      this.btnIsobaricXmlFile.Size = new System.Drawing.Size(226, 25);
      this.btnIsobaricXmlFile.TabIndex = 8;
      this.btnIsobaricXmlFile.Text = "button1";
      this.btnIsobaricXmlFile.UseVisualStyleBackColor = true;
      // 
      // txtIsobaricXmlFile
      // 
      this.txtIsobaricXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIsobaricXmlFile.Location = new System.Drawing.Point(251, 60);
      this.txtIsobaricXmlFile.Name = "txtIsobaricXmlFile";
      this.txtIsobaricXmlFile.Size = new System.Drawing.Size(724, 20);
      this.txtIsobaricXmlFile.TabIndex = 9;
      this.txtIsobaricXmlFile.TextChanged += new System.EventHandler(this.txtRLocation_TextChanged);
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
      this.cbNormalize.Location = new System.Drawing.Point(249, 124);
      this.cbNormalize.Name = "cbNormalize";
      this.cbNormalize.Size = new System.Drawing.Size(234, 17);
      this.cbNormalize.TabIndex = 15;
      this.cbNormalize.Text = "Normalize channels by cyclic loess algorithm";
      this.cbNormalize.UseVisualStyleBackColor = true;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(248, 177);
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
      this.cbRatioCalculator.Location = new System.Drawing.Point(450, 173);
      this.cbRatioCalculator.Name = "cbRatioCalculator";
      this.cbRatioCalculator.Size = new System.Drawing.Size(525, 21);
      this.cbRatioCalculator.TabIndex = 17;
      // 
      // cbModifiedOnly
      // 
      this.cbModifiedOnly.AutoSize = true;
      this.cbModifiedOnly.Location = new System.Drawing.Point(249, 147);
      this.cbModifiedOnly.Name = "cbModifiedOnly";
      this.cbModifiedOnly.Size = new System.Drawing.Size(270, 17);
      this.cbModifiedOnly.TabIndex = 19;
      this.cbModifiedOnly.Text = "Quantify peptides with modification characters only :";
      this.cbModifiedOnly.UseVisualStyleBackColor = true;
      this.cbModifiedOnly.Visible = false;
      // 
      // txtModifiedCharacter
      // 
      this.txtModifiedCharacter.Location = new System.Drawing.Point(525, 147);
      this.txtModifiedCharacter.Name = "txtModifiedCharacter";
      this.txtModifiedCharacter.Size = new System.Drawing.Size(100, 20);
      this.txtModifiedCharacter.TabIndex = 20;
      this.txtModifiedCharacter.Visible = false;
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Experiment definition";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(25, 200);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.*)";
      this.pnlClassification.Size = new System.Drawing.Size(1033, 298);
      this.pnlClassification.TabIndex = 13;
      // 
      // refChannels
      // 
      this.refChannels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.refChannels.Checked = false;
      this.refChannels.Description = "Select references";
      this.refChannels.Location = new System.Drawing.Point(152, 88);
      this.refChannels.Name = "refChannels";
      this.refChannels.PlexType = null;
      this.refChannels.SelectedIons = "";
      this.refChannels.Size = new System.Drawing.Size(823, 30);
      this.refChannels.TabIndex = 21;
      // 
      // AbstractIsobaricProteinStatisticBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1080, 623);
      this.Controls.Add(this.refChannels);
      this.Controls.Add(this.txtModifiedCharacter);
      this.Controls.Add(this.cbModifiedOnly);
      this.Controls.Add(this.btnIsobaricXmlFile);
      this.Controls.Add(this.txtIsobaricXmlFile);
      this.Controls.Add(this.cbRatioCalculator);
      this.Controls.Add(this.cbNormalize);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.label1);
      this.Name = "AbstractIsobaricProteinStatisticBuilderUI";
      this.TabText = "";
      this.Text = "Identified Protein iTRAQ Statistic Builder";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.cbNormalize, 0);
      this.Controls.SetChildIndex(this.cbRatioCalculator, 0);
      this.Controls.SetChildIndex(this.txtIsobaricXmlFile, 0);
      this.Controls.SetChildIndex(this.btnIsobaricXmlFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.cbModifiedOnly, 0);
      this.Controls.SetChildIndex(this.txtModifiedCharacter, 0);
      this.Controls.SetChildIndex(this.refChannels, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected System.Windows.Forms.Button btnIsobaricXmlFile;
    protected System.Windows.Forms.TextBox txtIsobaricXmlFile;
    protected ClassificationPanel pnlClassification;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.CheckBox cbNormalize;
    protected System.Windows.Forms.Label label1;
    protected System.Windows.Forms.ComboBox cbRatioCalculator;
    protected System.Windows.Forms.CheckBox cbModifiedOnly;
    protected System.Windows.Forms.TextBox txtModifiedCharacter;
    private IsobaricChannelField refChannels;


  }
}