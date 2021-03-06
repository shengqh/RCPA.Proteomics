﻿namespace RCPA.Proteomics.Summary.Uniform
{
  partial class TitleDatasetPanel
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.splitContainer3 = new System.Windows.Forms.SplitContainer();
      this.btnFind = new System.Windows.Forms.Button();
      this.label7 = new System.Windows.Forms.Label();
      this.cbTitleFormat = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
      this.splitContainer3.Panel2.SuspendLayout();
      this.splitContainer3.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer2
      // 
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
      // 
      // splitContainer3
      // 
      this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer3.Location = new System.Drawing.Point(0, 0);
      this.splitContainer3.Name = "splitContainer3";
      this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer3.Panel2
      // 
      this.splitContainer3.Panel2.Controls.Add(this.btnFind);
      this.splitContainer3.Panel2.Controls.Add(this.label7);
      this.splitContainer3.Panel2.Controls.Add(this.cbTitleFormat);
      this.splitContainer3.Size = new System.Drawing.Size(1016, 295);
      this.splitContainer3.SplitterDistance = 260;
      this.splitContainer3.TabIndex = 27;
      // 
      // btnFind
      // 
      this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnFind.Location = new System.Drawing.Point(888, 2);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new System.Drawing.Size(125, 25);
      this.btnFind.TabIndex = 29;
      this.btnFind.Text = "Find title format";
      this.btnFind.UseVisualStyleBackColor = true;
      this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
      // 
      // label7
      // 
      this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(8, 8);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(65, 13);
      this.label7.TabIndex = 28;
      this.label7.Text = "Title format :";
      // 
      // cbTitleFormat
      // 
      this.cbTitleFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbTitleFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbTitleFormat.FormattingEnabled = true;
      this.cbTitleFormat.Location = new System.Drawing.Point(79, 5);
      this.cbTitleFormat.Name = "cbTitleFormat";
      this.cbTitleFormat.Size = new System.Drawing.Size(803, 21);
      this.cbTitleFormat.TabIndex = 27;
      // 
      // TitleDatasetPanel
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "TitleDatasetPanel";
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer3.Panel2.ResumeLayout(false);
      this.splitContainer3.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
      this.splitContainer3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    protected System.Windows.Forms.SplitContainer splitContainer3;
    protected System.Windows.Forms.Label label7;
    protected System.Windows.Forms.ComboBox cbTitleFormat;
    private System.Windows.Forms.Button btnFind;


  }
}
