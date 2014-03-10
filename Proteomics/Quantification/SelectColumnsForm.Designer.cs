namespace RCPA.Proteomics.Quantification
{
  partial class SelectColumnsForm
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
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.cbProteins = new RCPA.Gui.RcpaSelectList();
      this.cbPeptides = new RCPA.Gui.RcpaSelectList();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.decimalDiff = new RCPA.Gui.IntegerField();
      this.decimalScore = new RCPA.Gui.IntegerField();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.decimalScore);
      this.splitContainer1.Panel2.Controls.Add(this.decimalDiff);
      this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
      this.splitContainer1.Panel2.Controls.Add(this.btnOk);
      this.splitContainer1.Size = new System.Drawing.Size(472, 615);
      this.splitContainer1.SplitterDistance = 496;
      this.splitContainer1.TabIndex = 0;
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.cbProteins);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.cbPeptides);
      this.splitContainer2.Size = new System.Drawing.Size(472, 496);
      this.splitContainer2.SplitterDistance = 235;
      this.splitContainer2.TabIndex = 0;
      // 
      // cbProteins
      // 
      this.cbProteins.Description = "Protein headers";
      this.cbProteins.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cbProteins.LoadButtonVisible = false;
      this.cbProteins.Location = new System.Drawing.Point(0, 0);
      this.cbProteins.Name = "cbProteins";
      this.cbProteins.SaveButtonVisible = false;
      this.cbProteins.Size = new System.Drawing.Size(235, 496);
      this.cbProteins.TabIndex = 0;
      // 
      // cbPeptides
      // 
      this.cbPeptides.Description = "Peptide headers";
      this.cbPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cbPeptides.LoadButtonVisible = false;
      this.cbPeptides.Location = new System.Drawing.Point(0, 0);
      this.cbPeptides.Name = "cbPeptides";
      this.cbPeptides.SaveButtonVisible = false;
      this.cbPeptides.Size = new System.Drawing.Size(233, 496);
      this.cbPeptides.TabIndex = 0;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(385, 78);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 25);
      this.btnCancel.TabIndex = 1;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOk.Location = new System.Drawing.Point(304, 78);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 25);
      this.btnOk.TabIndex = 0;
      this.btnOk.Text = "&Ok";
      this.btnOk.UseVisualStyleBackColor = true;
      // 
      // decimalDiff
      // 
      this.decimalDiff.Caption = "Number of Diff(MH+) decimal";
      this.decimalDiff.CaptionWidth = 193;
      this.decimalDiff.DefaultValue = "3";
      this.decimalDiff.Description = "";
      this.decimalDiff.Key = "DecimalDiff";
      this.decimalDiff.Location = new System.Drawing.Point(46, 46);
      this.decimalDiff.Name = "decimalDiff";
      this.decimalDiff.PreCondition = null;
      this.decimalDiff.Required = false;
      this.decimalDiff.Size = new System.Drawing.Size(340, 23);
      this.decimalDiff.TabIndex = 3;
      this.decimalDiff.TextWidth = 147;
      // 
      // decimalScore
      // 
      this.decimalScore.Caption = "Number of score decimal";
      this.decimalScore.CaptionWidth = 193;
      this.decimalScore.DefaultValue = "0";
      this.decimalScore.Description = "";
      this.decimalScore.Key = "DecimalScore";
      this.decimalScore.Location = new System.Drawing.Point(46, 17);
      this.decimalScore.Name = "decimalScore";
      this.decimalScore.PreCondition = null;
      this.decimalScore.Required = false;
      this.decimalScore.Size = new System.Drawing.Size(340, 23);
      this.decimalScore.TabIndex = 4;
      this.decimalScore.TextWidth = 147;
      // 
      // SelectColumnsForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(472, 615);
      this.Controls.Add(this.splitContainer1);
      this.Name = "SelectColumnsForm";
      this.Text = "Select Items";
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private Gui.RcpaSelectList cbProteins;
    private Gui.RcpaSelectList cbPeptides;
    private Gui.IntegerField decimalScore;
    private Gui.IntegerField decimalDiff;
  }
}