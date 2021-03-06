namespace RCPA.Proteomics.Quantification
{
  partial class AbstractQuantificationSummaryViewerUI
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
      this.btnExport = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.clientContainer = new System.Windows.Forms.SplitContainer();
      this.splitContainer6 = new System.Windows.Forms.SplitContainer();
      this.lvProteins = new System.Windows.Forms.ListView();
      this.lvPeptides = new System.Windows.Forms.ListView();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabProteins = new System.Windows.Forms.TabPage();
      this.zgcProteins = new ZedGraph.ZedGraphControl();
      this.tabPeptides = new System.Windows.Forms.TabPage();
      this.zgcPeptides = new ZedGraph.ZedGraphControl();
      this.tabProtein = new System.Windows.Forms.TabPage();
      this.zgcProtein = new ZedGraph.ZedGraphControl();
      this.tabPeptide = new System.Windows.Forms.TabPage();
      this.zgcExperimentalScans = new ZedGraph.ZedGraphControl();
      this.btnView = new System.Windows.Forms.Button();
      this.filePanel = new System.Windows.Forms.Panel();
      this.txtSummaryFile = new System.Windows.Forms.TextBox();
      this.btnSummaryFile = new System.Windows.Forms.Button();
      this.pnlButton.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.clientContainer)).BeginInit();
      this.clientContainer.Panel1.SuspendLayout();
      this.clientContainer.Panel2.SuspendLayout();
      this.clientContainer.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).BeginInit();
      this.splitContainer6.Panel1.SuspendLayout();
      this.splitContainer6.Panel2.SuspendLayout();
      this.splitContainer6.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabProteins.SuspendLayout();
      this.tabPeptides.SuspendLayout();
      this.tabProtein.SuspendLayout();
      this.tabPeptide.SuspendLayout();
      this.filePanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // pnlButton
      // 
      this.pnlButton.Controls.Add(this.btnView);
      this.pnlButton.Controls.Add(this.btnExport);
      this.pnlButton.Controls.Add(this.btnSave);
      this.pnlButton.Location = new System.Drawing.Point(0, 702);
      this.pnlButton.Size = new System.Drawing.Size(1362, 39);
      this.pnlButton.Controls.SetChildIndex(this.btnSave, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnGo, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnExport, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnCancel, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnClose, 0);
      this.pnlButton.Controls.SetChildIndex(this.btnView, 0);
      // 
      // btnClose
      // 
      this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnClose.Location = new System.Drawing.Point(729, 7);
      this.btnClose.Size = new System.Drawing.Size(75, 25);
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnCancel.Location = new System.Drawing.Point(644, 7);
      this.btnCancel.Size = new System.Drawing.Size(75, 25);
      this.btnCancel.Visible = false;
      // 
      // btnGo
      // 
      this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnGo.Location = new System.Drawing.Point(559, 7);
      this.btnGo.Size = new System.Drawing.Size(75, 25);
      this.btnGo.Text = "&Load";
      // 
      // btnExport
      // 
      this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnExport.Location = new System.Drawing.Point(171, 9);
      this.btnExport.Name = "btnExport";
      this.btnExport.Size = new System.Drawing.Size(75, 25);
      this.btnExport.TabIndex = 22;
      this.btnExport.Text = "&Export...";
      this.btnExport.UseVisualStyleBackColor = true;
      this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnSave.Location = new System.Drawing.Point(90, 9);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 25);
      this.btnSave.TabIndex = 21;
      this.btnSave.Text = "&Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // clientContainer
      // 
      this.clientContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.clientContainer.Location = new System.Drawing.Point(0, 25);
      this.clientContainer.Name = "clientContainer";
      // 
      // clientContainer.Panel1
      // 
      this.clientContainer.Panel1.Controls.Add(this.splitContainer6);
      // 
      // clientContainer.Panel2
      // 
      this.clientContainer.Panel2.Controls.Add(this.tabControl1);
      this.clientContainer.Size = new System.Drawing.Size(1362, 677);
      this.clientContainer.SplitterDistance = 1013;
      this.clientContainer.TabIndex = 23;
      // 
      // splitContainer6
      // 
      this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer6.Location = new System.Drawing.Point(0, 0);
      this.splitContainer6.Name = "splitContainer6";
      this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer6.Panel1
      // 
      this.splitContainer6.Panel1.Controls.Add(this.lvProteins);
      // 
      // splitContainer6.Panel2
      // 
      this.splitContainer6.Panel2.Controls.Add(this.lvPeptides);
      this.splitContainer6.Size = new System.Drawing.Size(1013, 677);
      this.splitContainer6.SplitterDistance = 315;
      this.splitContainer6.TabIndex = 0;
      // 
      // lvProteins
      // 
      this.lvProteins.CheckBoxes = true;
      this.lvProteins.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvProteins.FullRowSelect = true;
      this.lvProteins.GridLines = true;
      this.lvProteins.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvProteins.HideSelection = false;
      this.lvProteins.Location = new System.Drawing.Point(0, 0);
      this.lvProteins.Name = "lvProteins";
      this.lvProteins.ShowItemToolTips = true;
      this.lvProteins.Size = new System.Drawing.Size(1013, 315);
      this.lvProteins.TabIndex = 22;
      this.lvProteins.UseCompatibleStateImageBehavior = false;
      this.lvProteins.View = System.Windows.Forms.View.Details;
      this.lvProteins.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvProteins_ItemCheck);
      this.lvProteins.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvProteins_ItemChecked);
      this.lvProteins.SelectedIndexChanged += new System.EventHandler(this.lvProteins_SelectedIndexChanged);
      // 
      // lvPeptides
      // 
      this.lvPeptides.CheckBoxes = true;
      this.lvPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lvPeptides.FullRowSelect = true;
      this.lvPeptides.GridLines = true;
      this.lvPeptides.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this.lvPeptides.HideSelection = false;
      this.lvPeptides.Location = new System.Drawing.Point(0, 0);
      this.lvPeptides.MultiSelect = false;
      this.lvPeptides.Name = "lvPeptides";
      this.lvPeptides.ShowItemToolTips = true;
      this.lvPeptides.Size = new System.Drawing.Size(1013, 358);
      this.lvPeptides.TabIndex = 23;
      this.lvPeptides.UseCompatibleStateImageBehavior = false;
      this.lvPeptides.View = System.Windows.Forms.View.Details;
      this.lvPeptides.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvPeptides_ItemCheck);
      this.lvPeptides.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvPeptides_ItemChecked);
      this.lvPeptides.SelectedIndexChanged += new System.EventHandler(this.lvPeptides_SelectedIndexChanged);
      this.lvPeptides.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPeptides_MouseDoubleClick);
      this.lvPeptides.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvPeptides_MouseDown);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabProteins);
      this.tabControl1.Controls.Add(this.tabPeptides);
      this.tabControl1.Controls.Add(this.tabProtein);
      this.tabControl1.Controls.Add(this.tabPeptide);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(345, 677);
      this.tabControl1.TabIndex = 1;
      this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
      // 
      // tabProteins
      // 
      this.tabProteins.Controls.Add(this.zgcProteins);
      this.tabProteins.Location = new System.Drawing.Point(4, 22);
      this.tabProteins.Name = "tabProteins";
      this.tabProteins.Padding = new System.Windows.Forms.Padding(3);
      this.tabProteins.Size = new System.Drawing.Size(337, 651);
      this.tabProteins.TabIndex = 0;
      this.tabProteins.Text = "Proteins";
      this.tabProteins.UseVisualStyleBackColor = true;
      // 
      // zgcProteins
      // 
      this.zgcProteins.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcProteins.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.zgcProteins.Location = new System.Drawing.Point(3, 3);
      this.zgcProteins.Name = "zgcProteins";
      this.zgcProteins.ScrollGrace = 0D;
      this.zgcProteins.ScrollMaxX = 0D;
      this.zgcProteins.ScrollMaxY = 0D;
      this.zgcProteins.ScrollMaxY2 = 0D;
      this.zgcProteins.ScrollMinX = 0D;
      this.zgcProteins.ScrollMinY = 0D;
      this.zgcProteins.ScrollMinY2 = 0D;
      this.zgcProteins.Size = new System.Drawing.Size(331, 645);
      this.zgcProteins.TabIndex = 4;
      this.zgcProteins.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zgcProteins_MouseClick);
      // 
      // tabPeptides
      // 
      this.tabPeptides.Controls.Add(this.zgcPeptides);
      this.tabPeptides.Location = new System.Drawing.Point(4, 22);
      this.tabPeptides.Name = "tabPeptides";
      this.tabPeptides.Padding = new System.Windows.Forms.Padding(3);
      this.tabPeptides.Size = new System.Drawing.Size(358, 719);
      this.tabPeptides.TabIndex = 2;
      this.tabPeptides.Text = "Peptides";
      this.tabPeptides.UseVisualStyleBackColor = true;
      // 
      // zgcPeptides
      // 
      this.zgcPeptides.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcPeptides.Location = new System.Drawing.Point(3, 3);
      this.zgcPeptides.Name = "zgcPeptides";
      this.zgcPeptides.ScrollGrace = 0D;
      this.zgcPeptides.ScrollMaxX = 0D;
      this.zgcPeptides.ScrollMaxY = 0D;
      this.zgcPeptides.ScrollMaxY2 = 0D;
      this.zgcPeptides.ScrollMinX = 0D;
      this.zgcPeptides.ScrollMinY = 0D;
      this.zgcPeptides.ScrollMinY2 = 0D;
      this.zgcPeptides.Size = new System.Drawing.Size(352, 713);
      this.zgcPeptides.TabIndex = 18;
      this.zgcPeptides.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zgcPeptides_MouseClick);
      this.zgcPeptides.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zgcPeptides_MouseDoubleClick);
      // 
      // tabProtein
      // 
      this.tabProtein.Controls.Add(this.zgcProtein);
      this.tabProtein.Location = new System.Drawing.Point(4, 22);
      this.tabProtein.Name = "tabProtein";
      this.tabProtein.Padding = new System.Windows.Forms.Padding(3);
      this.tabProtein.Size = new System.Drawing.Size(358, 719);
      this.tabProtein.TabIndex = 3;
      this.tabProtein.Text = "Protein";
      this.tabProtein.UseVisualStyleBackColor = true;
      // 
      // zgcProtein
      // 
      this.zgcProtein.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcProtein.Location = new System.Drawing.Point(3, 3);
      this.zgcProtein.Name = "zgcProtein";
      this.zgcProtein.ScrollGrace = 0D;
      this.zgcProtein.ScrollMaxX = 0D;
      this.zgcProtein.ScrollMaxY = 0D;
      this.zgcProtein.ScrollMaxY2 = 0D;
      this.zgcProtein.ScrollMinX = 0D;
      this.zgcProtein.ScrollMinY = 0D;
      this.zgcProtein.ScrollMinY2 = 0D;
      this.zgcProtein.Size = new System.Drawing.Size(352, 713);
      this.zgcProtein.TabIndex = 4;
      this.zgcProtein.MouseClick += new System.Windows.Forms.MouseEventHandler(this.zgcProtein_MouseClick);
      this.zgcProtein.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.zgcProtein_MouseDoubleClick);
      // 
      // tabPeptide
      // 
      this.tabPeptide.Controls.Add(this.zgcExperimentalScans);
      this.tabPeptide.Location = new System.Drawing.Point(4, 22);
      this.tabPeptide.Name = "tabPeptide";
      this.tabPeptide.Padding = new System.Windows.Forms.Padding(3);
      this.tabPeptide.Size = new System.Drawing.Size(358, 719);
      this.tabPeptide.TabIndex = 1;
      this.tabPeptide.Text = "Spectrum";
      this.tabPeptide.UseVisualStyleBackColor = true;
      // 
      // zgcExperimentalScans
      // 
      this.zgcExperimentalScans.Dock = System.Windows.Forms.DockStyle.Fill;
      this.zgcExperimentalScans.Location = new System.Drawing.Point(3, 3);
      this.zgcExperimentalScans.Name = "zgcExperimentalScans";
      this.zgcExperimentalScans.ScrollGrace = 0D;
      this.zgcExperimentalScans.ScrollMaxX = 0D;
      this.zgcExperimentalScans.ScrollMaxY = 0D;
      this.zgcExperimentalScans.ScrollMaxY2 = 0D;
      this.zgcExperimentalScans.ScrollMinX = 0D;
      this.zgcExperimentalScans.ScrollMinY = 0D;
      this.zgcExperimentalScans.ScrollMinY2 = 0D;
      this.zgcExperimentalScans.Size = new System.Drawing.Size(352, 713);
      this.zgcExperimentalScans.TabIndex = 17;
      // 
      // btnView
      // 
      this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnView.Enabled = false;
      this.btnView.Location = new System.Drawing.Point(252, 9);
      this.btnView.Name = "btnView";
      this.btnView.Size = new System.Drawing.Size(75, 25);
      this.btnView.TabIndex = 24;
      this.btnView.Text = "&View...";
      this.btnView.UseVisualStyleBackColor = true;
      this.btnView.Click += new System.EventHandler(this.btnView_Click);
      // 
      // filePanel
      // 
      this.filePanel.Controls.Add(this.txtSummaryFile);
      this.filePanel.Controls.Add(this.btnSummaryFile);
      this.filePanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.filePanel.Location = new System.Drawing.Point(0, 0);
      this.filePanel.Name = "filePanel";
      this.filePanel.Size = new System.Drawing.Size(1362, 25);
      this.filePanel.TabIndex = 24;
      // 
      // txtSummaryFile
      // 
      this.txtSummaryFile.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSummaryFile.Location = new System.Drawing.Point(308, 0);
      this.txtSummaryFile.Name = "txtSummaryFile";
      this.txtSummaryFile.Size = new System.Drawing.Size(1054, 20);
      this.txtSummaryFile.TabIndex = 10;
      // 
      // btnSummaryFile
      // 
      this.btnSummaryFile.Dock = System.Windows.Forms.DockStyle.Left;
      this.btnSummaryFile.Location = new System.Drawing.Point(0, 0);
      this.btnSummaryFile.Name = "btnSummaryFile";
      this.btnSummaryFile.Size = new System.Drawing.Size(308, 25);
      this.btnSummaryFile.TabIndex = 9;
      this.btnSummaryFile.Text = "button1";
      this.btnSummaryFile.UseVisualStyleBackColor = true;
      // 
      // AbstractQuantificationSummaryViewerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.ClientSize = new System.Drawing.Size(1362, 741);
      this.Controls.Add(this.clientContainer);
      this.Controls.Add(this.filePanel);
      this.Name = "AbstractQuantificationSummaryViewerUI";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.AbstractQuantificationSummaryViewerUI_Load);
      this.Controls.SetChildIndex(this.filePanel, 0);
      this.Controls.SetChildIndex(this.pnlButton, 0);
      this.Controls.SetChildIndex(this.clientContainer, 0);
      this.pnlButton.ResumeLayout(false);
      this.clientContainer.Panel1.ResumeLayout(false);
      this.clientContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.clientContainer)).EndInit();
      this.clientContainer.ResumeLayout(false);
      this.splitContainer6.Panel1.ResumeLayout(false);
      this.splitContainer6.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer6)).EndInit();
      this.splitContainer6.ResumeLayout(false);
      this.tabControl1.ResumeLayout(false);
      this.tabProteins.ResumeLayout(false);
      this.tabPeptides.ResumeLayout(false);
      this.tabProtein.ResumeLayout(false);
      this.tabPeptide.ResumeLayout(false);
      this.filePanel.ResumeLayout(false);
      this.filePanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.SplitContainer clientContainer;
    private System.Windows.Forms.SplitContainer splitContainer6;
    private System.Windows.Forms.ListView lvProteins;
    private System.Windows.Forms.ListView lvPeptides;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabProteins;
    private System.Windows.Forms.TabPage tabPeptide;
    protected ZedGraph.ZedGraphControl zgcExperimentalScans;
    private System.Windows.Forms.TabPage tabPeptides;
    protected ZedGraph.ZedGraphControl zgcPeptides;
    protected ZedGraph.ZedGraphControl zgcProteins;
    private System.Windows.Forms.TabPage tabProtein;
    protected ZedGraph.ZedGraphControl zgcProtein;
    private System.Windows.Forms.Button btnView;
    private System.Windows.Forms.Panel filePanel;
    private System.Windows.Forms.TextBox txtSummaryFile;
    private System.Windows.Forms.Button btnSummaryFile;
  }
}
