namespace RCPA.Proteomics.Statistic
{
  partial class AbstractDistributionUI
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
      this.btnProteinFile = new System.Windows.Forms.Button();
      this.txtProteinFile = new System.Windows.Forms.TextBox();
      this.btnLoad = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.txtClassificationTitle = new System.Windows.Forms.TextBox();
      this.cbFilterType = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.txtLoopFrom = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtLoopTo = new System.Windows.Forms.TextBox();
      this.txtLoopStep = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.cbModifiedOnly = new System.Windows.Forms.CheckBox();
      this.txtModifiedAminoacids = new System.Windows.Forms.TextBox();
      this.pnlClassification = new RCPA.Proteomics.ClassificationPanel();
      this.cbClassifiedByTag = new RCPA.Gui.RcpaCheckField();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 575);
      this.lblProgress.Size = new System.Drawing.Size(1040, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 596);
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
      // btnProteinFile
      // 
      this.btnProteinFile.Location = new System.Drawing.Point(38, 31);
      this.btnProteinFile.Name = "btnProteinFile";
      this.btnProteinFile.Size = new System.Drawing.Size(199, 23);
      this.btnProteinFile.TabIndex = 7;
      this.btnProteinFile.Text = "button1";
      this.btnProteinFile.UseVisualStyleBackColor = true;
      // 
      // txtProteinFile
      // 
      this.txtProteinFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtProteinFile.Location = new System.Drawing.Point(243, 33);
      this.txtProteinFile.Name = "txtProteinFile";
      this.txtProteinFile.Size = new System.Drawing.Size(679, 21);
      this.txtProteinFile.TabIndex = 8;
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
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(70, 64);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(161, 12);
      this.label2.TabIndex = 13;
      this.label2.Text = "Input classification title";
      // 
      // txtClassificationTitle
      // 
      this.txtClassificationTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtClassificationTitle.Location = new System.Drawing.Point(243, 61);
      this.txtClassificationTitle.Name = "txtClassificationTitle";
      this.txtClassificationTitle.Size = new System.Drawing.Size(679, 21);
      this.txtClassificationTitle.TabIndex = 14;
      // 
      // cbFilterType
      // 
      this.cbFilterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbFilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFilterType.FormattingEnabled = true;
      this.cbFilterType.Location = new System.Drawing.Point(243, 88);
      this.cbFilterType.Name = "cbFilterType";
      this.cbFilterType.Size = new System.Drawing.Size(679, 20);
      this.cbFilterType.TabIndex = 16;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(118, 92);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(113, 12);
      this.label3.TabIndex = 15;
      this.label3.Text = "Select filter type";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(70, 121);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(167, 12);
      this.label4.TabIndex = 17;
      this.label4.Text = "Input loop definition, from";
      // 
      // txtLoopFrom
      // 
      this.txtLoopFrom.Location = new System.Drawing.Point(243, 118);
      this.txtLoopFrom.Name = "txtLoopFrom";
      this.txtLoopFrom.Size = new System.Drawing.Size(100, 21);
      this.txtLoopFrom.TabIndex = 18;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(349, 121);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(17, 12);
      this.label5.TabIndex = 19;
      this.label5.Text = "to";
      // 
      // txtLoopTo
      // 
      this.txtLoopTo.Location = new System.Drawing.Point(373, 118);
      this.txtLoopTo.Name = "txtLoopTo";
      this.txtLoopTo.Size = new System.Drawing.Size(100, 21);
      this.txtLoopTo.TabIndex = 18;
      // 
      // txtLoopStep
      // 
      this.txtLoopStep.Location = new System.Drawing.Point(517, 118);
      this.txtLoopStep.Name = "txtLoopStep";
      this.txtLoopStep.Size = new System.Drawing.Size(100, 21);
      this.txtLoopStep.TabIndex = 18;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(482, 121);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(29, 12);
      this.label6.TabIndex = 19;
      this.label6.Text = "step";
      // 
      // cbModifiedOnly
      // 
      this.cbModifiedOnly.AutoSize = true;
      this.cbModifiedOnly.Location = new System.Drawing.Point(243, 148);
      this.cbModifiedOnly.Name = "cbModifiedOnly";
      this.cbModifiedOnly.Size = new System.Drawing.Size(318, 16);
      this.cbModifiedOnly.TabIndex = 20;
      this.cbModifiedOnly.Text = "Modified peptide only, input modified amino acids";
      this.cbModifiedOnly.UseVisualStyleBackColor = true;
      this.cbModifiedOnly.Visible = false;
      // 
      // txtModifiedAminoacids
      // 
      this.txtModifiedAminoacids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModifiedAminoacids.Location = new System.Drawing.Point(568, 146);
      this.txtModifiedAminoacids.Name = "txtModifiedAminoacids";
      this.txtModifiedAminoacids.Size = new System.Drawing.Size(354, 21);
      this.txtModifiedAminoacids.TabIndex = 8;
      this.txtModifiedAminoacids.Visible = false;
      // 
      // pnlClassification
      // 
      this.pnlClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlClassification.Description = "Description";
      this.pnlClassification.GetName = null;
      this.pnlClassification.Location = new System.Drawing.Point(45, 198);
      this.pnlClassification.Name = "pnlClassification";
      this.pnlClassification.Pattern = "(.+)_(\\d){1,2}";
      this.pnlClassification.Size = new System.Drawing.Size(958, 337);
      this.pnlClassification.TabIndex = 21;
      // 
      // cbClassifiedByTag
      // 
      this.cbClassifiedByTag.AutoSize = true;
      this.cbClassifiedByTag.Key = "ClassifiedByTag";
      this.cbClassifiedByTag.Location = new System.Drawing.Point(243, 170);
      this.cbClassifiedByTag.Name = "cbClassifiedByTag";
      this.cbClassifiedByTag.PreCondition = null;
      this.cbClassifiedByTag.Size = new System.Drawing.Size(156, 16);
      this.cbClassifiedByTag.TabIndex = 23;
      this.cbClassifiedByTag.Text = "Classified by tag name";
      this.cbClassifiedByTag.UseVisualStyleBackColor = true;
      this.cbClassifiedByTag.CheckedChanged += new System.EventHandler(this.cbClassifiedByTag_CheckedChanged);
      // 
      // AbstractDistributionUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1040, 653);
      this.Controls.Add(this.cbClassifiedByTag);
      this.Controls.Add(this.pnlClassification);
      this.Controls.Add(this.cbModifiedOnly);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtLoopStep);
      this.Controls.Add(this.txtLoopTo);
      this.Controls.Add(this.txtLoopFrom);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbFilterType);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtClassificationTitle);
      this.Controls.Add(this.btnProteinFile);
      this.Controls.Add(this.txtModifiedAminoacids);
      this.Controls.Add(this.txtProteinFile);
      this.Controls.Add(this.btnLoad);
      this.Controls.Add(this.label2);
      this.Name = "AbstractDistributionUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.btnLoad, 0);
      this.Controls.SetChildIndex(this.txtProteinFile, 0);
      this.Controls.SetChildIndex(this.txtModifiedAminoacids, 0);
      this.Controls.SetChildIndex(this.btnProteinFile, 0);
      this.Controls.SetChildIndex(this.txtClassificationTitle, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbFilterType, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.txtLoopFrom, 0);
      this.Controls.SetChildIndex(this.txtLoopTo, 0);
      this.Controls.SetChildIndex(this.txtLoopStep, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.cbModifiedOnly, 0);
      this.Controls.SetChildIndex(this.pnlClassification, 0);
      this.Controls.SetChildIndex(this.cbClassifiedByTag, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    protected System.Windows.Forms.Button btnProteinFile;
    protected System.Windows.Forms.TextBox txtProteinFile;
    protected System.Windows.Forms.Button btnLoad;
    protected System.Windows.Forms.Label label2;
    protected System.Windows.Forms.TextBox txtClassificationTitle;
    protected System.Windows.Forms.ComboBox cbFilterType;
    protected System.Windows.Forms.Label label3;
    protected System.Windows.Forms.Label label4;
    protected System.Windows.Forms.TextBox txtLoopFrom;
    protected System.Windows.Forms.Label label5;
    protected System.Windows.Forms.TextBox txtLoopTo;
    protected System.Windows.Forms.TextBox txtLoopStep;
    protected System.Windows.Forms.Label label6;
    protected System.Windows.Forms.CheckBox cbModifiedOnly;
    protected System.Windows.Forms.TextBox txtModifiedAminoacids;
    protected RCPA.Proteomics.ClassificationPanel pnlClassification;
    private Gui.RcpaCheckField cbClassifiedByTag;
  }
}
