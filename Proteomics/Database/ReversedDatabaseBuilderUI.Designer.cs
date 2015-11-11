namespace RCPA.Proteomics.Database
{
  partial class ReversedDatabaseBuilderUI
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
      this.cbReversedDatabaseOnly = new System.Windows.Forms.CheckBox();
      this.btnContaminantFile = new System.Windows.Forms.Button();
      this.txtContaminantFile = new System.Windows.Forms.TextBox();
      this.cbContaminantFile = new System.Windows.Forms.CheckBox();
      this.cbSwitch = new System.Windows.Forms.CheckBox();
      this.txtTermini = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.cbPrior = new System.Windows.Forms.ComboBox();
      this.label2 = new System.Windows.Forms.Label();
      this.sourceDatabase = new RCPA.Gui.FileField();
      this.decoyKey = new RCPA.Gui.TextField();
      this.label3 = new System.Windows.Forms.Label();
      this.cbDecoyType = new System.Windows.Forms.ComboBox();
      this.pnlButton.SuspendLayout();
      this.SuspendLayout();
      // 
      // lblProgress
      // 
      this.lblProgress.Location = new System.Drawing.Point(0, 191);
      this.lblProgress.Size = new System.Drawing.Size(1237, 23);
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(0, 214);
      this.progressBar.Size = new System.Drawing.Size(1237, 23);
      // 
      // pnlButton
      // 
      this.pnlButton.Location = new System.Drawing.Point(0, 237);
      this.pnlButton.Size = new System.Drawing.Size(1237, 39);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(666, 8);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(581, 8);
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(496, 8);
      // 
      // cbReversedDatabaseOnly
      // 
      this.cbReversedDatabaseOnly.AutoSize = true;
      this.cbReversedDatabaseOnly.Location = new System.Drawing.Point(30, 123);
      this.cbReversedDatabaseOnly.Name = "cbReversedDatabaseOnly";
      this.cbReversedDatabaseOnly.Size = new System.Drawing.Size(141, 17);
      this.cbReversedDatabaseOnly.TabIndex = 7;
      this.cbReversedDatabaseOnly.Text = "Reversed database only";
      this.cbReversedDatabaseOnly.UseVisualStyleBackColor = true;
      // 
      // btnContaminantFile
      // 
      this.btnContaminantFile.Location = new System.Drawing.Point(199, 55);
      this.btnContaminantFile.Name = "btnContaminantFile";
      this.btnContaminantFile.Size = new System.Drawing.Size(49, 25);
      this.btnContaminantFile.TabIndex = 8;
      this.btnContaminantFile.Text = "...";
      this.btnContaminantFile.UseVisualStyleBackColor = true;
      // 
      // txtContaminantFile
      // 
      this.txtContaminantFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtContaminantFile.Location = new System.Drawing.Point(259, 57);
      this.txtContaminantFile.Name = "txtContaminantFile";
      this.txtContaminantFile.Size = new System.Drawing.Size(942, 20);
      this.txtContaminantFile.TabIndex = 9;
      // 
      // cbContaminantFile
      // 
      this.cbContaminantFile.AutoSize = true;
      this.cbContaminantFile.Location = new System.Drawing.Point(30, 60);
      this.cbContaminantFile.Name = "cbContaminantFile";
      this.cbContaminantFile.Size = new System.Drawing.Size(163, 17);
      this.cbContaminantFile.TabIndex = 10;
      this.cbContaminantFile.Text = "Include contaminant Proteins";
      this.cbContaminantFile.UseVisualStyleBackColor = true;
      // 
      // cbSwitch
      // 
      this.cbSwitch.AutoSize = true;
      this.cbSwitch.Location = new System.Drawing.Point(30, 91);
      this.cbSwitch.Name = "cbSwitch";
      this.cbSwitch.Size = new System.Drawing.Size(135, 17);
      this.cbSwitch.TabIndex = 11;
      this.cbSwitch.Text = "Switch protease termini";
      this.cbSwitch.UseVisualStyleBackColor = true;
      // 
      // txtTermini
      // 
      this.txtTermini.Location = new System.Drawing.Point(186, 89);
      this.txtTermini.Name = "txtTermini";
      this.txtTermini.Size = new System.Drawing.Size(62, 20);
      this.txtTermini.TabIndex = 12;
      this.txtTermini.Text = "KR";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(256, 92);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(26, 13);
      this.label1.TabIndex = 13;
      this.label1.Text = "with";
      // 
      // cbPrior
      // 
      this.cbPrior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbPrior.FormattingEnabled = true;
      this.cbPrior.Items.AddRange(new object[] {
            "previous",
            "next"});
      this.cbPrior.Location = new System.Drawing.Point(288, 88);
      this.cbPrior.Name = "cbPrior";
      this.cbPrior.Size = new System.Drawing.Size(121, 21);
      this.cbPrior.TabIndex = 14;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(415, 92);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(58, 13);
      this.label2.TabIndex = 15;
      this.label2.Text = "amino acid";
      // 
      // sourceDatabase
      // 
      this.sourceDatabase.AfterBrowseFileEvent = null;
      this.sourceDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.sourceDatabase.FullName = "";
      this.sourceDatabase.Key = "SourceDatabase";
      this.sourceDatabase.Location = new System.Drawing.Point(30, 26);
      this.sourceDatabase.Name = "sourceDatabase";
      this.sourceDatabase.OpenButtonText = "Browse All File ...";
      this.sourceDatabase.PreCondition = null;
      this.sourceDatabase.Size = new System.Drawing.Size(1171, 23);
      this.sourceDatabase.TabIndex = 16;
      this.sourceDatabase.WidthOpenButton = 226;
      // 
      // decoyKey
      // 
      this.decoyKey.Caption = "Add decoy key";
      this.decoyKey.CaptionWidth = 90;
      this.decoyKey.DefaultValue = "REVERSED";
      this.decoyKey.Description = "";
      this.decoyKey.Key = "TextField";
      this.decoyKey.Location = new System.Drawing.Point(16, 148);
      this.decoyKey.Name = "decoyKey";
      this.decoyKey.PreCondition = null;
      this.decoyKey.Required = false;
      this.decoyKey.Size = new System.Drawing.Size(236, 23);
      this.decoyKey.TabIndex = 17;
      this.decoyKey.TextWidth = 127;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(247, 152);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(16, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "to";
      // 
      // cbDecoyType
      // 
      this.cbDecoyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbDecoyType.FormattingEnabled = true;
      this.cbDecoyType.Items.AddRange(new object[] {
            "previous",
            "next"});
      this.cbDecoyType.Location = new System.Drawing.Point(269, 148);
      this.cbDecoyType.Name = "cbDecoyType";
      this.cbDecoyType.Size = new System.Drawing.Size(445, 21);
      this.cbDecoyType.TabIndex = 14;
      // 
      // ReversedDatabaseBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1237, 276);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.decoyKey);
      this.Controls.Add(this.sourceDatabase);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.cbDecoyType);
      this.Controls.Add(this.cbPrior);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtTermini);
      this.Controls.Add(this.cbSwitch);
      this.Controls.Add(this.txtContaminantFile);
      this.Controls.Add(this.cbContaminantFile);
      this.Controls.Add(this.btnContaminantFile);
      this.Controls.Add(this.cbReversedDatabaseOnly);
      this.Name = "ReversedDatabaseBuilderUI";
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.cbReversedDatabaseOnly, 0);
      this.Controls.SetChildIndex(this.btnContaminantFile, 0);
      this.Controls.SetChildIndex(this.cbContaminantFile, 0);
      this.Controls.SetChildIndex(this.txtContaminantFile, 0);
      this.Controls.SetChildIndex(this.progressBar, 0);
      this.Controls.SetChildIndex(this.lblProgress, 0);
      this.Controls.SetChildIndex(this.cbSwitch, 0);
      this.Controls.SetChildIndex(this.txtTermini, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbPrior, 0);
      this.Controls.SetChildIndex(this.cbDecoyType, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.sourceDatabase, 0);
      this.Controls.SetChildIndex(this.decoyKey, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.pnlButton.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox cbReversedDatabaseOnly;
    private System.Windows.Forms.Button btnContaminantFile;
    private System.Windows.Forms.TextBox txtContaminantFile;
    private System.Windows.Forms.CheckBox cbContaminantFile;
    private System.Windows.Forms.CheckBox cbSwitch;
    private System.Windows.Forms.TextBox txtTermini;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cbPrior;
    private System.Windows.Forms.Label label2;
    private Gui.FileField sourceDatabase;
    private Gui.TextField decoyKey;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox cbDecoyType;
  }
}
