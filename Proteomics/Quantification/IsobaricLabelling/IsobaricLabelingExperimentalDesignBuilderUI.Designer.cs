namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  partial class IsobaricLabelingExperimentalDesignBuilderUI
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
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.refChannels = new RCPA.Proteomics.Quantification.IsobaricLabelling.IsobaricChannelField();
      this.txtIsobaricXmlFile = new System.Windows.Forms.TextBox();
      this.btnIsobaricXmlFile = new System.Windows.Forms.Button();
      this.btnDataFile = new System.Windows.Forms.Button();
      this.txtDataFile = new System.Windows.Forms.TextBox();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 501);
      this.pnlButton.Size = new System.Drawing.Size(1080, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(588, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Enabled = true;
      this.btnCancel.Location = new System.Drawing.Point(503, 8);
      this.btnCancel.Text = "&Load";
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(418, 8);
      this.btnGo.Text = "&Save";
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Experiment definition";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(25, 124);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.*)";
      this.pnlClassification.Size = new System.Drawing.Size(1033, 336);
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
      // txtIsobaricXmlFile
      // 
      this.txtIsobaricXmlFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIsobaricXmlFile.Location = new System.Drawing.Point(251, 60);
      this.txtIsobaricXmlFile.Name = "txtIsobaricXmlFile";
      this.txtIsobaricXmlFile.Size = new System.Drawing.Size(724, 20);
      this.txtIsobaricXmlFile.TabIndex = 9;
      this.txtIsobaricXmlFile.TextChanged += new System.EventHandler(this.txtXmlFile_TextChanged);
      // 
      // btnIsobaricXmlFile
      // 
      this.btnIsobaricXmlFile.Location = new System.Drawing.Point(25, 57);
      this.btnIsobaricXmlFile.Name = "btnIsobaricXmlFile";
      this.btnIsobaricXmlFile.Size = new System.Drawing.Size(226, 25);
      this.btnIsobaricXmlFile.TabIndex = 8;
      this.btnIsobaricXmlFile.Text = "btnXmlFile";
      this.btnIsobaricXmlFile.UseVisualStyleBackColor = true;
      // 
      // btnDataFile
      // 
      this.btnDataFile.Location = new System.Drawing.Point(25, 24);
      this.btnDataFile.Name = "btnDataFile";
      this.btnDataFile.Size = new System.Drawing.Size(226, 25);
      this.btnDataFile.TabIndex = 22;
      this.btnDataFile.Text = "btnDataFile";
      this.btnDataFile.UseVisualStyleBackColor = true;
      // 
      // txtDataFile
      // 
      this.txtDataFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDataFile.Location = new System.Drawing.Point(251, 27);
      this.txtDataFile.Name = "txtDataFile";
      this.txtDataFile.Size = new System.Drawing.Size(724, 20);
      this.txtDataFile.TabIndex = 23;
      // 
      // IsobaricLabelingExperimentalDesignBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1080, 540);
      this.Controls.Add(this.btnDataFile);
      this.Controls.Add(this.txtDataFile);
      this.Controls.Add(this.refChannels);
      this.Controls.Add(this.btnIsobaricXmlFile);
      this.Controls.Add(this.txtIsobaricXmlFile);
      this.Controls.Add(this.pnlClassification);
      this.Name = "IsobaricLabelingExperimentalDesignBuilderUI";
      this.TabText = "";
      this.Text = "Isobaric Labeling Experimental Design Builder";
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.txtIsobaricXmlFile, 0);
      this.Controls.SetChildIndex(this.btnIsobaricXmlFile, 0);
      this.Controls.SetChildIndex(this.refChannels, 0);
      this.Controls.SetChildIndex(this.txtDataFile, 0);
      this.Controls.SetChildIndex(this.btnDataFile, 0);
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ClassificationPanel pnlClassification;
    private IsobaricChannelField refChannels;
    private System.Windows.Forms.TextBox txtIsobaricXmlFile;
    private System.Windows.Forms.Button btnIsobaricXmlFile;
    private System.Windows.Forms.Button btnDataFile;
    private System.Windows.Forms.TextBox txtDataFile;
  }
}