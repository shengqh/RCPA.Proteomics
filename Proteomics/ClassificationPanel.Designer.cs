namespace RCPA.Proteomics
{
  partial class ClassificationPanel
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassificationPanel));
      this.splitContainer2 = new System.Windows.Forms.SplitContainer();
      this.label1 = new System.Windows.Forms.Label();
      this.btnClassify = new System.Windows.Forms.Button();
      this.txtPattern = new System.Windows.Forms.TextBox();
      this.lblDescription = new System.Windows.Forms.Label();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.btnSave = new System.Windows.Forms.Button();
      this.btnLoad = new System.Windows.Forms.Button();
      this.btnDown = new System.Windows.Forms.Button();
      this.btnUp = new System.Windows.Forms.Button();
      this.btnCollapse = new System.Windows.Forms.Button();
      this.btnReset = new System.Windows.Forms.Button();
      this.btnRename = new System.Windows.Forms.Button();
      this.btnMerge = new System.Windows.Forms.Button();
      this.tvClassifications = new TreeViewMS.TreeViewMS();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
      this.splitContainer2.Panel1.SuspendLayout();
      this.splitContainer2.Panel2.SuspendLayout();
      this.splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer2
      // 
      this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.splitContainer2.Location = new System.Drawing.Point(0, 0);
      this.splitContainer2.Name = "splitContainer2";
      this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      this.splitContainer2.Panel1.Controls.Add(this.label1);
      this.splitContainer2.Panel1.Controls.Add(this.btnClassify);
      this.splitContainer2.Panel1.Controls.Add(this.txtPattern);
      this.splitContainer2.Panel1.Controls.Add(this.lblDescription);
      // 
      // splitContainer2.Panel2
      // 
      this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
      this.splitContainer2.Size = new System.Drawing.Size(850, 438);
      this.splitContainer2.SplitterDistance = 30;
      this.splitContainer2.TabIndex = 28;
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(580, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(41, 13);
      this.label1.TabIndex = 30;
      this.label1.Text = "Pattern";
      // 
      // btnClassify
      // 
      this.btnClassify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClassify.Location = new System.Drawing.Point(770, 4);
      this.btnClassify.Name = "btnClassify";
      this.btnClassify.Size = new System.Drawing.Size(80, 23);
      this.btnClassify.TabIndex = 29;
      this.btnClassify.Text = "Classify";
      this.btnClassify.UseVisualStyleBackColor = true;
      this.btnClassify.Click += new System.EventHandler(this.btnClassify_Click);
      // 
      // txtPattern
      // 
      this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPattern.Location = new System.Drawing.Point(627, 6);
      this.txtPattern.Name = "txtPattern";
      this.txtPattern.Size = new System.Drawing.Size(139, 20);
      this.txtPattern.TabIndex = 28;
      // 
      // lblDescription
      // 
      this.lblDescription.AutoSize = true;
      this.lblDescription.Location = new System.Drawing.Point(3, 9);
      this.lblDescription.Name = "lblDescription";
      this.lblDescription.Size = new System.Drawing.Size(60, 13);
      this.lblDescription.TabIndex = 27;
      this.lblDescription.Text = "Description";
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
      this.splitContainer1.Location = new System.Drawing.Point(0, 0);
      this.splitContainer1.Name = "splitContainer1";
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.tvClassifications);
      this.splitContainer1.Panel1MinSize = 80;
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.btnSave);
      this.splitContainer1.Panel2.Controls.Add(this.btnLoad);
      this.splitContainer1.Panel2.Controls.Add(this.btnDown);
      this.splitContainer1.Panel2.Controls.Add(this.btnUp);
      this.splitContainer1.Panel2.Controls.Add(this.btnCollapse);
      this.splitContainer1.Panel2.Controls.Add(this.btnReset);
      this.splitContainer1.Panel2.Controls.Add(this.btnRename);
      this.splitContainer1.Panel2.Controls.Add(this.btnMerge);
      this.splitContainer1.Panel2MinSize = 80;
      this.splitContainer1.Size = new System.Drawing.Size(850, 404);
      this.splitContainer1.SplitterDistance = 766;
      this.splitContainer1.TabIndex = 28;
      // 
      // btnSave
      // 
      this.btnSave.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnSave.Location = new System.Drawing.Point(0, 161);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(80, 23);
      this.btnSave.TabIndex = 32;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnLoad
      // 
      this.btnLoad.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnLoad.Location = new System.Drawing.Point(0, 138);
      this.btnLoad.Name = "btnLoad";
      this.btnLoad.Size = new System.Drawing.Size(80, 23);
      this.btnLoad.TabIndex = 31;
      this.btnLoad.Text = "Load";
      this.btnLoad.UseVisualStyleBackColor = true;
      this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
      // 
      // btnDown
      // 
      this.btnDown.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnDown.Location = new System.Drawing.Point(0, 115);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(80, 23);
      this.btnDown.TabIndex = 30;
      this.btnDown.Text = "Down";
      this.btnDown.UseVisualStyleBackColor = true;
      this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
      // 
      // btnUp
      // 
      this.btnUp.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnUp.Location = new System.Drawing.Point(0, 92);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(80, 23);
      this.btnUp.TabIndex = 29;
      this.btnUp.Text = "Up";
      this.btnUp.UseVisualStyleBackColor = true;
      this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
      // 
      // btnCollapse
      // 
      this.btnCollapse.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnCollapse.Location = new System.Drawing.Point(0, 69);
      this.btnCollapse.Name = "btnCollapse";
      this.btnCollapse.Size = new System.Drawing.Size(80, 23);
      this.btnCollapse.TabIndex = 28;
      this.btnCollapse.Text = "Collapse";
      this.btnCollapse.UseVisualStyleBackColor = true;
      this.btnCollapse.Click += new System.EventHandler(this.btnCollapse_Click);
      // 
      // btnReset
      // 
      this.btnReset.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnReset.Location = new System.Drawing.Point(0, 46);
      this.btnReset.Name = "btnReset";
      this.btnReset.Size = new System.Drawing.Size(80, 23);
      this.btnReset.TabIndex = 27;
      this.btnReset.Text = "Reset";
      this.btnReset.UseVisualStyleBackColor = true;
      this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
      // 
      // btnRename
      // 
      this.btnRename.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnRename.Location = new System.Drawing.Point(0, 23);
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size(80, 23);
      this.btnRename.TabIndex = 25;
      this.btnRename.Text = "Rename";
      this.btnRename.UseVisualStyleBackColor = true;
      this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
      // 
      // btnMerge
      // 
      this.btnMerge.Dock = System.Windows.Forms.DockStyle.Top;
      this.btnMerge.Location = new System.Drawing.Point(0, 0);
      this.btnMerge.Name = "btnMerge";
      this.btnMerge.Size = new System.Drawing.Size(80, 23);
      this.btnMerge.TabIndex = 26;
      this.btnMerge.Text = "Merge";
      this.btnMerge.UseVisualStyleBackColor = true;
      this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
      // 
      // tvClassifications
      // 
      this.tvClassifications.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tvClassifications.HideSelection = false;
      this.tvClassifications.Location = new System.Drawing.Point(0, 0);
      this.tvClassifications.Name = "tvClassifications";
      this.tvClassifications.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("tvClassifications.SelectedNodes")));
      this.tvClassifications.Size = new System.Drawing.Size(766, 404);
      this.tvClassifications.TabIndex = 0;
      this.tvClassifications.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvClassifications_BeforeSelect);
      // 
      // ClassificationPanel
      // 
      this.Controls.Add(this.splitContainer2);
      this.Name = "ClassificationPanel";
      this.Size = new System.Drawing.Size(850, 438);
      this.splitContainer2.Panel1.ResumeLayout(false);
      this.splitContainer2.Panel1.PerformLayout();
      this.splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
      this.splitContainer2.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Label lblDescription;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Button btnReset;
    private System.Windows.Forms.Button btnRename;
    private System.Windows.Forms.Button btnMerge;
    private System.Windows.Forms.Button btnCollapse;
    private System.Windows.Forms.Button btnDown;
    private System.Windows.Forms.Button btnUp;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnClassify;
    private System.Windows.Forms.TextBox txtPattern;
    private TreeViewMS.TreeViewMS tvClassifications;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnLoad;
  }
}
