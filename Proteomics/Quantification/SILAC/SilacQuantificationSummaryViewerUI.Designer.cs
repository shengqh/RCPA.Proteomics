namespace RCPA.Proteomics.Quantification.SILAC
{
  partial class SilacQuantificationSummaryViewerUI
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
      this.peptideRSquare = new RCPA.Gui.DoubleField();
      this.proteinRSquare = new RCPA.Gui.DoubleField();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // zgcExperimentalScans
      // 
      this.zgcExperimentalScans.Size = new System.Drawing.Size(352, 692);
      // 
      // zgcPeptides
      // 
      this.zgcPeptides.Size = new System.Drawing.Size(352, 692);
      // 
      // zgcProteins
      // 
      this.zgcProteins.Size = new System.Drawing.Size(368, 671);
      // 
      // zgcProtein
      // 
      this.zgcProtein.Size = new System.Drawing.Size(352, 692);
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnUpdate);
      this.pnlButton.Controls.Add(this.proteinRSquare);
      this.pnlButton.Controls.Add(this.peptideRSquare);
      this.pnlButton.Location = new System.Drawing.Point(0, 728);
      this.pnlButton.Size = new System.Drawing.Size(1508, 60);
      this.pnlButton.Controls.SetChildIndex(this.btnGo, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnCancel, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnClose, 0);
      this.pnlButton.Controls.SetChildIndex(this.peptideRSquare, 0);
      this.pnlButton.Controls.SetChildIndex(this.proteinRSquare, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnUpdate, 0);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(991, 17);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(906, 17);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(566, 17);
      // 
      // peptideRSquare
      // 
      this.peptideRSquare.Caption = "Manually filter peptide of current protein by minimum R square : ";
      this.peptideRSquare.CaptionWidth = 320;
      this.peptideRSquare.DefaultValue = "0.9";
      this.peptideRSquare.Description = "";
      this.peptideRSquare.Key = "PeptideRSquare";
      this.peptideRSquare.Location = new System.Drawing.Point(5, 30);
      this.peptideRSquare.Name = "peptideRSquare";
      this.peptideRSquare.PreCondition = null;
      this.peptideRSquare.Required = false;
      this.peptideRSquare.Size = new System.Drawing.Size(350, 23);
      this.peptideRSquare.TabIndex = 25;
      this.peptideRSquare.TextWidth = 30;
      this.peptideRSquare.Value = 0.9D;
      // 
      // proteinRSquare
      // 
      this.proteinRSquare.Caption = "Automatially filter protein by minimum R square : ";
      this.proteinRSquare.CaptionWidth = 320;
      this.proteinRSquare.DefaultValue = "0.9";
      this.proteinRSquare.Description = "";
      this.proteinRSquare.Key = "ProteinRSquare";
      this.proteinRSquare.Location = new System.Drawing.Point(5, 7);
      this.proteinRSquare.Name = "proteinRSquare";
      this.proteinRSquare.PreCondition = null;
      this.proteinRSquare.Required = false;
      this.proteinRSquare.Size = new System.Drawing.Size(350, 23);
      this.proteinRSquare.TabIndex = 25;
      this.proteinRSquare.TextWidth = 30;
      this.proteinRSquare.Value = 0.9D;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Location = new System.Drawing.Point(361, 30);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(75, 23);
      this.btnUpdate.TabIndex = 26;
      this.btnUpdate.Text = "&Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // SilacQuantificationSummaryViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1508, 788);
      this.Name = "SilacQuantificationSummaryViewerUI";
      this.Load += new System.EventHandler(this.SilacQuantificationSummaryViewerUI_Load);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Gui.DoubleField peptideRSquare;
    private Gui.DoubleField proteinRSquare;
    private System.Windows.Forms.Button btnUpdate;


  }
}
