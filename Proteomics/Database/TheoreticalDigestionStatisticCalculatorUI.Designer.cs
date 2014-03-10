namespace RCPA.Proteomics.Database
{
  partial class TheoreticalDigestionStatisticCalculatorUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TheoreticalDigestionStatisticCalculatorUI));
      this.lbSelectedProteases = new RCPA.RcpaListBox();
      this.lbProteases = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnAdd = new System.Windows.Forms.Button();
      this.btnRemove = new System.Windows.Forms.Button();
      this.txtMinLength = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.txtMinMass = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtMaxMass = new System.Windows.Forms.TextBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtMaxMissCleavage = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.txtIgnoreAminoacids = new System.Windows.Forms.TextBox();
      this.pnlFile.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(35, 12);
      this.pnlFile.Size = new System.Drawing.Size(920, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(228, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(692, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(228, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 490);
      this.lblProgress.Size = new System.Drawing.Size(993, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 511);
      this.progressBar.Size = new System.Drawing.Size(993, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(544, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(459, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(374, 7);
      // 
      // lbSelectedProteases
      // 
      this.lbSelectedProteases.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbSelectedProteases.CheckedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("lbSelectedProteases.CheckedItems")));
      this.lbSelectedProteases.Items = ((System.Collections.Generic.List<object>)(resources.GetObject("lbSelectedProteases.Items")));
      this.lbSelectedProteases.Key = "Proteases";
      this.lbSelectedProteases.Location = new System.Drawing.Point(501, 190);
      this.lbSelectedProteases.Name = "lbSelectedProteases";
      this.lbSelectedProteases.SelectedItems = ((System.Collections.Generic.List<object>)(resources.GetObject("lbSelectedProteases.SelectedItems")));
      this.lbSelectedProteases.Size = new System.Drawing.Size(457, 257);
      this.lbSelectedProteases.TabIndex = 32;
      this.lbSelectedProteases.Title = "Selected proteases";
      // 
      // lbProteases
      // 
      this.lbProteases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.lbProteases.FormattingEnabled = true;
      this.lbProteases.ItemHeight = 12;
      this.lbProteases.Location = new System.Drawing.Point(35, 220);
      this.lbProteases.Name = "lbProteases";
      this.lbProteases.Size = new System.Drawing.Size(379, 220);
      this.lbProteases.TabIndex = 33;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(33, 200);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(59, 12);
      this.label1.TabIndex = 34;
      this.label1.Text = "Proteases";
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(420, 221);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 35;
      this.btnAdd.Text = "==>";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnRemove
      // 
      this.btnRemove.Location = new System.Drawing.Point(420, 250);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(75, 23);
      this.btnRemove.TabIndex = 35;
      this.btnRemove.Text = "<==";
      this.btnRemove.UseVisualStyleBackColor = true;
      this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
      // 
      // txtMinLength
      // 
      this.txtMinLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinLength.Location = new System.Drawing.Point(260, 58);
      this.txtMinLength.Name = "txtMinLength";
      this.txtMinLength.Size = new System.Drawing.Size(698, 21);
      this.txtMinLength.TabIndex = 37;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(159, 61);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(95, 12);
      this.label3.TabIndex = 36;
      this.label3.Text = "Minmum length :";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(171, 88);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(83, 12);
      this.label2.TabIndex = 36;
      this.label2.Text = "Minmum mass :";
      // 
      // txtMinMass
      // 
      this.txtMinMass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMinMass.Location = new System.Drawing.Point(260, 85);
      this.txtMinMass.Name = "txtMinMass";
      this.txtMinMass.Size = new System.Drawing.Size(698, 21);
      this.txtMinMass.TabIndex = 37;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(165, 115);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(89, 12);
      this.label4.TabIndex = 36;
      this.label4.Text = "Maximum mass :";
      // 
      // txtMaxMass
      // 
      this.txtMaxMass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMaxMass.Location = new System.Drawing.Point(260, 112);
      this.txtMaxMass.Name = "txtMaxMass";
      this.txtMaxMass.Size = new System.Drawing.Size(698, 21);
      this.txtMaxMass.TabIndex = 37;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(111, 142);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(143, 12);
      this.label5.TabIndex = 36;
      this.label5.Text = "Maximum miss-cleavage :";
      // 
      // txtMaxMissCleavage
      // 
      this.txtMaxMissCleavage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtMaxMissCleavage.Location = new System.Drawing.Point(260, 139);
      this.txtMaxMissCleavage.Name = "txtMaxMissCleavage";
      this.txtMaxMissCleavage.Size = new System.Drawing.Size(698, 21);
      this.txtMaxMissCleavage.TabIndex = 37;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(99, 169);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(155, 12);
      this.label6.TabIndex = 36;
      this.label6.Text = "Ignore peptide contains :";
      // 
      // txtIgnoreAminoacids
      // 
      this.txtIgnoreAminoacids.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtIgnoreAminoacids.Location = new System.Drawing.Point(260, 166);
      this.txtIgnoreAminoacids.Name = "txtIgnoreAminoacids";
      this.txtIgnoreAminoacids.Size = new System.Drawing.Size(698, 21);
      this.txtIgnoreAminoacids.TabIndex = 37;
      // 
      // TheoreticalDigestionStatisticCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(993, 568);
      this.Controls.Add(this.txtIgnoreAminoacids);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.txtMaxMissCleavage);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.txtMaxMass);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtMinMass);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtMinLength);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnRemove);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.lbProteases);
      this.Controls.Add(this.lbSelectedProteases);
      this.Name = "TheoreticalDigestionStatisticCalculatorUI";
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.lbSelectedProteases, 0);
      this.Controls.SetChildIndex(this.lbProteases, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.btnAdd, 0);
      this.Controls.SetChildIndex(this.btnRemove, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.txtMinLength, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.txtMinMass, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.txtMaxMass, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.txtMaxMissCleavage, 0);
      this.Controls.SetChildIndex(this.label6, 0);
      this.Controls.SetChildIndex(this.txtIgnoreAminoacids, 0);
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RcpaListBox lbSelectedProteases;
    private System.Windows.Forms.ListBox lbProteases;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.TextBox txtMinLength;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtMinMass;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox txtMaxMass;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtMaxMissCleavage;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtIgnoreAminoacids;



  }
}
