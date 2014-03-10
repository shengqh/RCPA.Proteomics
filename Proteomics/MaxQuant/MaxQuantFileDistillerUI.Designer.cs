namespace RCPA.Proteomics.MaxQuant
{
  partial class MaxQuantFileDistillerUI
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
      this.txtMaxEvalue = new System.Windows.Forms.TextBox();
      this.cbFilterByEvalue = new System.Windows.Forms.CheckBox();
      this.txtMinScore = new System.Windows.Forms.TextBox();
      this.cbFilterByScore = new System.Windows.Forms.CheckBox();
      this.btnTarget = new System.Windows.Forms.Button();
      this.txtTarget = new System.Windows.Forms.TextBox();
      this.btnLoadSource = new System.Windows.Forms.Button();
      this.btnLoadTarget = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.txtSplitChar = new System.Windows.Forms.TextBox();
      this.cbIsMultiple = new System.Windows.Forms.CheckBox();
      this.btnAdd = new System.Windows.Forms.Button();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.lbSource = new System.Windows.Forms.ListBox();
      this.lbTarget = new System.Windows.Forms.ListBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoadRoles = new System.Windows.Forms.Button();
      this.btnRemove = new System.Windows.Forms.Button();
      this.lbRoles = new System.Windows.Forms.ListBox();
      this.pnlFile.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlFile
      // 
      this.pnlFile.Location = new System.Drawing.Point(16, 9);
      this.pnlFile.Size = new System.Drawing.Size(918, 22);
      // 
      // txtOriginalFile
      // 
      this.txtOriginalFile.Location = new System.Drawing.Point(242, 0);
      this.txtOriginalFile.Size = new System.Drawing.Size(676, 21);
      // 
      // btnOriginalFile
      // 
      this.btnOriginalFile.Size = new System.Drawing.Size(242, 22);
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 486);
      this.lblProgress.Size = new System.Drawing.Size(1027, 21);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 507);
      this.progressBar.Size = new System.Drawing.Size(1027, 21);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(561, 7);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(476, 7);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(391, 7);
      // 
      // txtMaxEvalue
      // 
      this.txtMaxEvalue.Location = new System.Drawing.Point(185, 165);
      this.txtMaxEvalue.Name = "txtMaxEvalue";
      this.txtMaxEvalue.Size = new System.Drawing.Size(94, 21);
      this.txtMaxEvalue.TabIndex = 47;
      // 
      // cbFilterByEvalue
      // 
      this.cbFilterByEvalue.AutoSize = true;
      this.cbFilterByEvalue.Location = new System.Drawing.Point(14, 167);
      this.cbFilterByEvalue.Name = "cbFilterByEvalue";
      this.cbFilterByEvalue.Size = new System.Drawing.Size(174, 16);
      this.cbFilterByEvalue.TabIndex = 46;
      this.cbFilterByEvalue.Text = "Filter by Expect value = ";
      this.cbFilterByEvalue.UseVisualStyleBackColor = true;
      // 
      // txtMinScore
      // 
      this.txtMinScore.Location = new System.Drawing.Point(142, 141);
      this.txtMinScore.Name = "txtMinScore";
      this.txtMinScore.Size = new System.Drawing.Size(94, 21);
      this.txtMinScore.TabIndex = 45;
      // 
      // cbFilterByScore
      // 
      this.cbFilterByScore.AutoSize = true;
      this.cbFilterByScore.Location = new System.Drawing.Point(14, 143);
      this.cbFilterByScore.Name = "cbFilterByScore";
      this.cbFilterByScore.Size = new System.Drawing.Size(132, 16);
      this.cbFilterByScore.TabIndex = 44;
      this.cbFilterByScore.Text = "Filter by Score = ";
      this.cbFilterByScore.UseVisualStyleBackColor = true;
      // 
      // btnTarget
      // 
      this.btnTarget.Location = new System.Drawing.Point(16, 37);
      this.btnTarget.Name = "btnTarget";
      this.btnTarget.Size = new System.Drawing.Size(242, 23);
      this.btnTarget.TabIndex = 11;
      this.btnTarget.Text = "button1";
      this.btnTarget.UseVisualStyleBackColor = true;
      // 
      // txtTarget
      // 
      this.txtTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtTarget.Location = new System.Drawing.Point(258, 40);
      this.txtTarget.Name = "txtTarget";
      this.txtTarget.Size = new System.Drawing.Size(676, 21);
      this.txtTarget.TabIndex = 12;
      // 
      // btnLoadSource
      // 
      this.btnLoadSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoadSource.Location = new System.Drawing.Point(940, 10);
      this.btnLoadSource.Name = "btnLoadSource";
      this.btnLoadSource.Size = new System.Drawing.Size(75, 23);
      this.btnLoadSource.TabIndex = 13;
      this.btnLoadSource.Text = "Load";
      this.btnLoadSource.UseVisualStyleBackColor = true;
      this.btnLoadSource.Click += new System.EventHandler(this.btnLoadSource_Click);
      // 
      // btnLoadTarget
      // 
      this.btnLoadTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoadTarget.Location = new System.Drawing.Point(940, 37);
      this.btnLoadTarget.Name = "btnLoadTarget";
      this.btnLoadTarget.Size = new System.Drawing.Size(75, 23);
      this.btnLoadTarget.TabIndex = 14;
      this.btnLoadTarget.Text = "Load";
      this.btnLoadTarget.UseVisualStyleBackColor = true;
      this.btnLoadTarget.Click += new System.EventHandler(this.btnLoadTarget_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.txtSplitChar);
      this.groupBox1.Controls.Add(this.cbIsMultiple);
      this.groupBox1.Controls.Add(this.btnAdd);
      this.groupBox1.Controls.Add(this.splitContainer1);
      this.groupBox1.Location = new System.Drawing.Point(16, 67);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(999, 244);
      this.groupBox1.TabIndex = 23;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Select corresponding tag and click add";
      // 
      // txtSplitChar
      // 
      this.txtSplitChar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.txtSplitChar.Location = new System.Drawing.Point(410, 219);
      this.txtSplitChar.Name = "txtSplitChar";
      this.txtSplitChar.Size = new System.Drawing.Size(59, 21);
      this.txtSplitChar.TabIndex = 27;
      this.txtSplitChar.Text = ";";
      // 
      // cbIsMultiple
      // 
      this.cbIsMultiple.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.cbIsMultiple.AutoSize = true;
      this.cbIsMultiple.Location = new System.Drawing.Point(87, 222);
      this.cbIsMultiple.Name = "cbIsMultiple";
      this.cbIsMultiple.Size = new System.Drawing.Size(324, 16);
      this.cbIsMultiple.TabIndex = 23;
      this.cbIsMultiple.Text = "If it\'s multiple line, then the split character is";
      this.cbIsMultiple.UseVisualStyleBackColor = true;
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnAdd.Location = new System.Drawing.Point(6, 218);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 24;
      this.btnAdd.Text = "Add role";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer1.Location = new System.Drawing.Point(6, 17);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.lbSource);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.lbTarget);
      this.splitContainer1.Size = new System.Drawing.Size(912, 202);
      this.splitContainer1.SplitterDistance = 452;
      this.splitContainer1.TabIndex = 25;
      // 
      // lbSource
      // 
      this.lbSource.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbSource.FormattingEnabled = true;
      this.lbSource.ItemHeight = 12;
      this.lbSource.Location = new System.Drawing.Point(0, 0);
      this.lbSource.Name = "lbSource";
      this.lbSource.Size = new System.Drawing.Size(452, 202);
      this.lbSource.TabIndex = 0;
      // 
      // lbTarget
      // 
      this.lbTarget.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbTarget.FormattingEnabled = true;
      this.lbTarget.ItemHeight = 12;
      this.lbTarget.Location = new System.Drawing.Point(0, 0);
      this.lbTarget.Name = "lbTarget";
      this.lbTarget.Size = new System.Drawing.Size(456, 202);
      this.lbTarget.TabIndex = 2;
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox2.Controls.Add(this.btnSave);
      this.groupBox2.Controls.Add(this.btnLoadRoles);
      this.groupBox2.Controls.Add(this.btnRemove);
      this.groupBox2.Controls.Add(this.lbRoles);
      this.groupBox2.Location = new System.Drawing.Point(16, 317);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(999, 146);
      this.groupBox2.TabIndex = 24;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Match roles";
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Location = new System.Drawing.Point(924, 74);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 23;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnLoadRoles
      // 
      this.btnLoadRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnLoadRoles.Location = new System.Drawing.Point(923, 45);
      this.btnLoadRoles.Name = "btnLoadRoles";
      this.btnLoadRoles.Size = new System.Drawing.Size(75, 23);
      this.btnLoadRoles.TabIndex = 22;
      this.btnLoadRoles.Text = "Load";
      this.btnLoadRoles.UseVisualStyleBackColor = true;
      this.btnLoadRoles.Click += new System.EventHandler(this.btnLoadRoles_Click);
      // 
      // btnRemove
      // 
      this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRemove.Location = new System.Drawing.Point(923, 16);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(75, 23);
      this.btnRemove.TabIndex = 21;
      this.btnRemove.Text = "Remove";
      this.btnRemove.UseVisualStyleBackColor = true;
      this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
      // 
      // lbRoles
      // 
      this.lbRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.lbRoles.FormattingEnabled = true;
      this.lbRoles.ItemHeight = 12;
      this.lbRoles.Location = new System.Drawing.Point(6, 16);
      this.lbRoles.Name = "lbRoles";
      this.lbRoles.Size = new System.Drawing.Size(911, 124);
      this.lbRoles.TabIndex = 20;
      // 
      // MaxQuantFileDistillerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(1027, 564);
      this.Controls.Add(this.btnLoadTarget);
      this.Controls.Add(this.btnLoadSource);
      this.Controls.Add(this.txtTarget);
      this.Controls.Add(this.btnTarget);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "MaxQuantFileDistillerUI";
      this.Controls.SetChildIndex(this.pnlFile, 0);
      this.Controls.SetChildIndex(this.groupBox1, 0);
      this.Controls.SetChildIndex(this.groupBox2, 0);
      this.Controls.SetChildIndex(this.btnTarget, 0);
      this.Controls.SetChildIndex(this.txtTarget, 0);
      this.Controls.SetChildIndex(this.btnLoadSource, 0);
      this.Controls.SetChildIndex(this.btnLoadTarget, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.pnlFile.ResumeLayout(false);
      this.pnlFile.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtMaxEvalue;
    private System.Windows.Forms.CheckBox cbFilterByEvalue;
    private System.Windows.Forms.TextBox txtMinScore;
    private System.Windows.Forms.CheckBox cbFilterByScore;
    private System.Windows.Forms.Button btnTarget;
    private System.Windows.Forms.TextBox txtTarget;
    private System.Windows.Forms.Button btnLoadSource;
    private System.Windows.Forms.Button btnLoadTarget;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox txtSplitChar;
    private System.Windows.Forms.CheckBox cbIsMultiple;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ListBox lbSource;
    private System.Windows.Forms.ListBox lbTarget;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.ListBox lbRoles;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoadRoles;
  }
}
