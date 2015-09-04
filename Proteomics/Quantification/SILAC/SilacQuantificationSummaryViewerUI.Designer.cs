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
      this.minRSquare = new RCPA.Gui.DoubleField();
      this.btnUpdate = new System.Windows.Forms.Button();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnUpdate);
      this.pnlButton.Controls.Add(this.minRSquare);
      this.pnlButton.Location = new System.Drawing.Point(0, 611);
      this.pnlButton.Size = new System.Drawing.Size(1100, 39);
      this.pnlButton.Controls.SetChildIndex(this.btnGo, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnCancel, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnClose, 0);
      this.pnlButton.Controls.SetChildIndex(this.minRSquare, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnUpdate, 0);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(741, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(656, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(316, 7);
      // 
      // minRSquare
      // 
      this.minRSquare.Caption = "Filter by minimum R sqaure : ";
      this.minRSquare.CaptionWidth = 150;
      this.minRSquare.DefaultValue = "0.9";
      this.minRSquare.Description = "";
      this.minRSquare.Key = "TextField";
      this.minRSquare.Location = new System.Drawing.Point(12, 9);
      this.minRSquare.Name = "minRSquare";
      this.minRSquare.PreCondition = null;
      this.minRSquare.Required = false;
      this.minRSquare.Size = new System.Drawing.Size(200, 23);
      this.minRSquare.TabIndex = 25;
      this.minRSquare.TextWidth = 50;
      this.minRSquare.Value = 0.9D;
      // 
      // btnUpdate
      // 
      this.btnUpdate.Location = new System.Drawing.Point(218, 9);
      this.btnUpdate.Name = "btnUpdate";
      this.btnUpdate.Size = new System.Drawing.Size(75, 23);
      this.btnUpdate.TabIndex = 26;
      this.btnUpdate.Text = "Update";
      this.btnUpdate.UseVisualStyleBackColor = true;
      this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
      // 
      // SilacQuantificationSummaryViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1100, 650);
      this.Name = "SilacQuantificationSummaryViewerUI";
      this.Load += new System.EventHandler(this.SilacQuantificationSummaryViewerUI_Load);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnUpdate;
    private Gui.DoubleField minRSquare;


  }
}
