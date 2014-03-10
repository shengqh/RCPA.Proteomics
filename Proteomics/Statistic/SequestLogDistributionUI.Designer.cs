namespace RCPA.Proteomics.Statistic
{
  partial class SequestLogDistributionUI
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
      this.outsGroup = new System.Windows.Forms.GroupBox();
      this.lvFiles = new System.Windows.Forms.ListView();
      this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
      this.btnAddSubDirectories = new System.Windows.Forms.Button();
      this.zgc = new ZedGraph.ZedGraphControl();
      this.outsGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // outsGroup
      // 
      this.outsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.outsGroup.Controls.Add(this.lvFiles);
      this.outsGroup.Controls.Add(this.btnAddSubDirectories);
      this.outsGroup.Location = new System.Drawing.Point(12, 12);
      this.outsGroup.Name = "outsGroup";
      this.outsGroup.Size = new System.Drawing.Size(879, 323);
      this.outsGroup.TabIndex = 24;
      this.outsGroup.TabStop = false;
      this.outsGroup.Text = "Select directories you want to extract peptides (only selected directories will b" +
          "e used)";
      // 
      // lvDirectories
      // 
      this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.lvFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
      this.lvFiles.FullRowSelect = true;
      this.lvFiles.HideSelection = false;
      this.lvFiles.Location = new System.Drawing.Point(20, 26);
      this.lvFiles.Name = "lvDirectories";
      this.lvFiles.Size = new System.Drawing.Size(734, 291);
      this.lvFiles.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.lvFiles.TabIndex = 15;
      this.lvFiles.UseCompatibleStateImageBehavior = false;
      this.lvFiles.View = System.Windows.Forms.View.Details;
      this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvDirectories_SelectedIndexChanged);
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "Log Files";
      this.columnHeader1.Width = 723;
      // 
      // btnAddSubDirectories
      // 
      this.btnAddSubDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAddSubDirectories.Location = new System.Drawing.Point(760, 26);
      this.btnAddSubDirectories.Name = "btnAddSubDirectories";
      this.btnAddSubDirectories.Size = new System.Drawing.Size(97, 23);
      this.btnAddSubDirectories.TabIndex = 12;
      this.btnAddSubDirectories.Text = "Add Files";
      this.btnAddSubDirectories.UseVisualStyleBackColor = true;
      this.btnAddSubDirectories.Click += new System.EventHandler(this.btnAddSubDirectories_Click);
      // 
      // zgc
      // 
      this.zgc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.zgc.Location = new System.Drawing.Point(12, 341);
      this.zgc.Name = "zgc";
      this.zgc.ScrollGrace = 0;
      this.zgc.ScrollMaxX = 0;
      this.zgc.ScrollMaxY = 0;
      this.zgc.ScrollMaxY2 = 0;
      this.zgc.ScrollMinX = 0;
      this.zgc.ScrollMinY = 0;
      this.zgc.ScrollMinY2 = 0;
      this.zgc.Size = new System.Drawing.Size(879, 254);
      this.zgc.TabIndex = 25;
      // 
      // SequestLogDistributionUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(903, 607);
      this.Controls.Add(this.zgc);
      this.Controls.Add(this.outsGroup);
      this.Name = "SequestLogDistributionUI";
      this.Text = "SequestLogDistributionUI";
      this.outsGroup.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox outsGroup;
    private System.Windows.Forms.ListView lvFiles;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.Button btnAddSubDirectories;
    private ZedGraph.ZedGraphControl zgc;

  }
}