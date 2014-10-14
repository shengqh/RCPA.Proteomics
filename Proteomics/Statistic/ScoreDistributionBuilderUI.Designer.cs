namespace RCPA.Proteomics.Analysis
{
  partial class ScoreDistributionBuilderUI
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
      this.components = new System.ComponentModel.Container();
      this.btnFile = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.chartScore = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.cbBasedOn = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.txtCharge = new System.Windows.Forms.TextBox();
      this.cbCharge = new System.Windows.Forms.CheckBox();
      this.txtMissCleavage = new System.Windows.Forms.TextBox();
      this.cbNumMissCleavage = new System.Windows.Forms.CheckBox();
      this.txtModification = new System.Windows.Forms.TextBox();
      this.cbModification = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.cbModificationValue = new System.Windows.Forms.ComboBox();
      this.label4 = new System.Windows.Forms.Label();
      this.btnBatch = new System.Windows.Forms.Button();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.cbEngine = new System.Windows.Forms.ComboBox();
      this.label5 = new System.Windows.Forms.Label();
      this.cbNumProteaseTermini = new System.Windows.Forms.CheckBox();
      this.txtNumProteaseTermini = new System.Windows.Forms.TextBox();
      this.cbReassignModification = new System.Windows.Forms.CheckBox();
      this.cbTag = new System.Windows.Forms.CheckBox();
      this.cbTagValue = new System.Windows.Forms.ComboBox();
      this.cbFdrType = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.chartScore)).BeginInit();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(380, 630);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(550, 630);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(465, 630);
      // 
      // btnFile
      // 
      this.btnFile.Location = new System.Drawing.Point(12, 12);
      this.btnFile.Name = "btnFile";
      this.btnFile.Size = new System.Drawing.Size(246, 23);
      this.btnFile.TabIndex = 0;
      this.btnFile.Text = "button1";
      this.btnFile.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(264, 14);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(729, 21);
      this.textBox1.TabIndex = 1;
      // 
      // chartScore
      // 
      this.chartScore.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.chartScore.ContextMenuStrip = this.contextMenuStrip1;
      this.chartScore.Location = new System.Drawing.Point(12, 114);
      this.chartScore.Name = "chartScore";
      this.chartScore.Size = new System.Drawing.Size(981, 500);
      this.chartScore.TabIndex = 8;
      this.chartScore.Text = "chart1";
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(130, 26);
      // 
      // saveAsToolStripMenuItem
      // 
      this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
      this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
      this.saveAsToolStripMenuItem.Text = "Save as...";
      this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
      // 
      // cbBasedOn
      // 
      this.cbBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbBasedOn.FormattingEnabled = true;
      this.cbBasedOn.Items.AddRange(new object[] {
            "Charge",
            "Miss cleavage",
            "Modification",
            "NumTrypsinTermini"});
      this.cbBasedOn.Location = new System.Drawing.Point(84, 88);
      this.cbBasedOn.Name = "cbBasedOn";
      this.cbBasedOn.Size = new System.Drawing.Size(148, 20);
      this.cbBasedOn.TabIndex = 13;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 91);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(71, 12);
      this.label1.TabIndex = 14;
      this.label1.Text = "Classify by";
      // 
      // txtCharge
      // 
      this.txtCharge.Location = new System.Drawing.Point(141, 40);
      this.txtCharge.Name = "txtCharge";
      this.txtCharge.Size = new System.Drawing.Size(45, 21);
      this.txtCharge.TabIndex = 16;
      this.txtCharge.Text = "2";
      // 
      // cbCharge
      // 
      this.cbCharge.AutoSize = true;
      this.cbCharge.Location = new System.Drawing.Point(78, 43);
      this.cbCharge.Name = "cbCharge";
      this.cbCharge.Size = new System.Drawing.Size(66, 16);
      this.cbCharge.TabIndex = 15;
      this.cbCharge.Text = "Charge=";
      this.cbCharge.UseVisualStyleBackColor = true;
      // 
      // txtMissCleavage
      // 
      this.txtMissCleavage.Location = new System.Drawing.Point(345, 40);
      this.txtMissCleavage.Name = "txtMissCleavage";
      this.txtMissCleavage.Size = new System.Drawing.Size(44, 21);
      this.txtMissCleavage.TabIndex = 18;
      this.txtMissCleavage.Text = "0";
      // 
      // cbNumMissCleavage
      // 
      this.cbNumMissCleavage.AutoSize = true;
      this.cbNumMissCleavage.Location = new System.Drawing.Point(192, 42);
      this.cbNumMissCleavage.Name = "cbNumMissCleavage";
      this.cbNumMissCleavage.Size = new System.Drawing.Size(156, 16);
      this.cbNumMissCleavage.TabIndex = 17;
      this.cbNumMissCleavage.Text = "Num of miss-cleavage= ";
      this.cbNumMissCleavage.UseVisualStyleBackColor = true;
      // 
      // txtModification
      // 
      this.txtModification.Location = new System.Drawing.Point(543, 64);
      this.txtModification.Name = "txtModification";
      this.txtModification.Size = new System.Drawing.Size(53, 21);
      this.txtModification.TabIndex = 20;
      this.txtModification.Text = "STY";
      // 
      // cbModification
      // 
      this.cbModification.AutoSize = true;
      this.cbModification.Location = new System.Drawing.Point(398, 42);
      this.cbModification.Name = "cbModification";
      this.cbModification.Size = new System.Drawing.Size(102, 16);
      this.cbModification.TabIndex = 19;
      this.cbModification.Text = "Modification=";
      this.cbModification.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 44);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(47, 12);
      this.label2.TabIndex = 21;
      this.label2.Text = "Fixed :";
      // 
      // cbModificationValue
      // 
      this.cbModificationValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbModificationValue.FormattingEnabled = true;
      this.cbModificationValue.Items.AddRange(new object[] {
            "True",
            "False"});
      this.cbModificationValue.Location = new System.Drawing.Point(496, 40);
      this.cbModificationValue.Name = "cbModificationValue";
      this.cbModificationValue.Size = new System.Drawing.Size(106, 20);
      this.cbModificationValue.TabIndex = 23;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(12, 67);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(59, 12);
      this.label4.TabIndex = 25;
      this.label4.Text = "Options :";
      // 
      // btnBatch
      // 
      this.btnBatch.Location = new System.Drawing.Point(754, 630);
      this.btnBatch.Name = "btnBatch";
      this.btnBatch.Size = new System.Drawing.Size(75, 21);
      this.btnBatch.TabIndex = 26;
      this.btnBatch.Text = "&Batch";
      this.btnBatch.UseVisualStyleBackColor = true;
      this.btnBatch.Click += new System.EventHandler(this.btnBatch_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.Filter = "peptides file|*.peptides|peptides file|*.txt|All files|*.*";
      this.openFileDialog1.Multiselect = true;
      this.openFileDialog1.Title = "Select peptides file";
      // 
      // cbEngine
      // 
      this.cbEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbEngine.FormattingEnabled = true;
      this.cbEngine.Location = new System.Drawing.Point(164, 64);
      this.cbEngine.Name = "cbEngine";
      this.cbEngine.Size = new System.Drawing.Size(121, 20);
      this.cbEngine.TabIndex = 28;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(75, 68);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(83, 12);
      this.label5.TabIndex = 29;
      this.label5.Text = "Search engine";
      // 
      // cbNumProteaseTermini
      // 
      this.cbNumProteaseTermini.AutoSize = true;
      this.cbNumProteaseTermini.Location = new System.Drawing.Point(608, 44);
      this.cbNumProteaseTermini.Name = "cbNumProteaseTermini";
      this.cbNumProteaseTermini.Size = new System.Drawing.Size(168, 16);
      this.cbNumProteaseTermini.TabIndex = 30;
      this.cbNumProteaseTermini.Text = "Num of Protease Termini=";
      this.cbNumProteaseTermini.UseVisualStyleBackColor = true;
      // 
      // txtNumProteaseTermini
      // 
      this.txtNumProteaseTermini.Location = new System.Drawing.Point(782, 41);
      this.txtNumProteaseTermini.Name = "txtNumProteaseTermini";
      this.txtNumProteaseTermini.Size = new System.Drawing.Size(44, 21);
      this.txtNumProteaseTermini.TabIndex = 18;
      this.txtNumProteaseTermini.Text = "0";
      // 
      // cbReassignModification
      // 
      this.cbReassignModification.AutoSize = true;
      this.cbReassignModification.Location = new System.Drawing.Point(308, 67);
      this.cbReassignModification.Name = "cbReassignModification";
      this.cbReassignModification.Size = new System.Drawing.Size(234, 16);
      this.cbReassignModification.TabIndex = 31;
      this.cbReassignModification.Text = "Reassign modification, amino acids=";
      this.cbReassignModification.UseVisualStyleBackColor = true;
      // 
      // cbTag
      // 
      this.cbTag.AutoSize = true;
      this.cbTag.Location = new System.Drawing.Point(832, 44);
      this.cbTag.Name = "cbTag";
      this.cbTag.Size = new System.Drawing.Size(48, 16);
      this.cbTag.TabIndex = 33;
      this.cbTag.Text = "Tag=";
      this.cbTag.UseVisualStyleBackColor = true;
      // 
      // cbTagValue
      // 
      this.cbTagValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTagValue.FormattingEnabled = true;
      this.cbTagValue.Items.AddRange(new object[] {
            "True",
            "False"});
      this.cbTagValue.Location = new System.Drawing.Point(876, 41);
      this.cbTagValue.Name = "cbTagValue";
      this.cbTagValue.Size = new System.Drawing.Size(106, 20);
      this.cbTagValue.TabIndex = 34;
      // 
      // cbFdrType
      // 
      this.cbFdrType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbFdrType.FormattingEnabled = true;
      this.cbFdrType.Location = new System.Drawing.Point(398, 91);
      this.cbFdrType.Name = "cbFdrType";
      this.cbFdrType.Size = new System.Drawing.Size(498, 20);
      this.cbFdrType.TabIndex = 113;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(255, 94);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(137, 12);
      this.label3.TabIndex = 112;
      this.label3.Text = "False Discovery Rate =";
      // 
      // ScoreDistributionBuilderUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1005, 671);
      this.Controls.Add(this.cbFdrType);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cbTagValue);
      this.Controls.Add(this.cbTag);
      this.Controls.Add(this.cbReassignModification);
      this.Controls.Add(this.cbNumProteaseTermini);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.cbEngine);
      this.Controls.Add(this.btnBatch);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.cbModificationValue);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtModification);
      this.Controls.Add(this.cbModification);
      this.Controls.Add(this.txtNumProteaseTermini);
      this.Controls.Add(this.txtMissCleavage);
      this.Controls.Add(this.cbNumMissCleavage);
      this.Controls.Add(this.txtCharge);
      this.Controls.Add(this.cbCharge);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbBasedOn);
      this.Controls.Add(this.chartScore);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.btnFile);
      this.Name = "ScoreDistributionBuilderUI";
      this.TabText = "ScoreDistributionForm";
      this.Text = "ScoreDistributionForm";
      this.Controls.SetChildIndex(this.btnFile, 0);
      this.Controls.SetChildIndex(this.textBox1, 0);
      this.Controls.SetChildIndex(this.chartScore, 0);
      this.Controls.SetChildIndex(this.cbBasedOn, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.cbCharge, 0);
      this.Controls.SetChildIndex(this.txtCharge, 0);
      this.Controls.SetChildIndex(this.cbNumMissCleavage, 0);
      this.Controls.SetChildIndex(this.txtMissCleavage, 0);
      this.Controls.SetChildIndex(this.txtNumProteaseTermini, 0);
      this.Controls.SetChildIndex(this.cbModification, 0);
      this.Controls.SetChildIndex(this.txtModification, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.cbModificationValue, 0);
      this.Controls.SetChildIndex(this.label4, 0);
      this.Controls.SetChildIndex(this.btnBatch, 0);
      this.Controls.SetChildIndex(this.cbEngine, 0);
      this.Controls.SetChildIndex(this.label5, 0);
      this.Controls.SetChildIndex(this.cbNumProteaseTermini, 0);
      this.Controls.SetChildIndex(this.cbReassignModification, 0);
      this.Controls.SetChildIndex(this.cbTag, 0);
      this.Controls.SetChildIndex(this.cbTagValue, 0);
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbFdrType, 0);
      ((System.ComponentModel.ISupportInitialize)(this.chartScore)).EndInit();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnFile;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.DataVisualization.Charting.Chart chartScore;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ComboBox cbBasedOn;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtCharge;
    private System.Windows.Forms.CheckBox cbCharge;
    private System.Windows.Forms.TextBox txtMissCleavage;
    private System.Windows.Forms.CheckBox cbNumMissCleavage;
    private System.Windows.Forms.TextBox txtModification;
    private System.Windows.Forms.CheckBox cbModification;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox cbModificationValue;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnBatch;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.ComboBox cbEngine;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox cbNumProteaseTermini;
    private System.Windows.Forms.TextBox txtNumProteaseTermini;
    private System.Windows.Forms.CheckBox cbReassignModification;
    private System.Windows.Forms.CheckBox cbTag;
    private System.Windows.Forms.ComboBox cbTagValue;
    private System.Windows.Forms.ComboBox cbFdrType;
    private System.Windows.Forms.Label label3;
  }
}