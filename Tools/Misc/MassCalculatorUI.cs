using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Proteomics;
using RCPA.Proteomics.Utils;

namespace RCPA.Tools.Misc
{
  public class MassCalculatorUI : AbstractUI
  {
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtSequence;
    private System.Windows.Forms.TextBox txtModification;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtResult;
    private System.Windows.Forms.CheckBox cbIsMH;
    private System.Windows.Forms.CheckBox cbIsMonoisotopic;

    private void InitializeComponent()
    {
      this.label1 = new System.Windows.Forms.Label();
      this.txtSequence = new System.Windows.Forms.TextBox();
      this.txtModification = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.txtResult = new System.Windows.Forms.TextBox();
      this.cbIsMonoisotopic = new System.Windows.Forms.CheckBox();
      this.cbIsMH = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // btnGo
      // 
      this.btnGo.Location = new System.Drawing.Point(241, 139);
      // 
      // btnClose
      // 
      this.btnClose.Location = new System.Drawing.Point(411, 139);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(326, 139);
      this.btnCancel.Visible = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(54, 28);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 12);
      this.label1.TabIndex = 7;
      this.label1.Text = "Input Peptide Sequence";
      // 
      // txtSequence
      // 
      this.txtSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSequence.Location = new System.Drawing.Point(211, 25);
      this.txtSequence.Name = "txtSequence";
      this.txtSequence.Size = new System.Drawing.Size(479, 21);
      this.txtSequence.TabIndex = 8;
      // 
      // txtModification
      // 
      this.txtModification.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtModification.Location = new System.Drawing.Point(211, 52);
      this.txtModification.Name = "txtModification";
      this.txtModification.Size = new System.Drawing.Size(479, 21);
      this.txtModification.TabIndex = 8;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(78, 55);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(113, 12);
      this.label2.TabIndex = 7;
      this.label2.Text = "Input Modification";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(150, 104);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(41, 12);
      this.label3.TabIndex = 7;
      this.label3.Text = "Result";
      // 
      // txtResult
      // 
      this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtResult.Location = new System.Drawing.Point(211, 101);
      this.txtResult.Name = "txtResult";
      this.txtResult.ReadOnly = true;
      this.txtResult.Size = new System.Drawing.Size(479, 21);
      this.txtResult.TabIndex = 8;
      // 
      // cbIsMonoisotopic
      // 
      this.cbIsMonoisotopic.AutoSize = true;
      this.cbIsMonoisotopic.Location = new System.Drawing.Point(211, 79);
      this.cbIsMonoisotopic.Name = "cbIsMonoisotopic";
      this.cbIsMonoisotopic.Size = new System.Drawing.Size(114, 16);
      this.cbIsMonoisotopic.TabIndex = 9;
      this.cbIsMonoisotopic.Text = "Is monoisotopic";
      this.cbIsMonoisotopic.UseVisualStyleBackColor = true;
      // 
      // cbMH
      // 
      this.cbIsMH.AutoSize = true;
      this.cbIsMH.Location = new System.Drawing.Point(359, 79);
      this.cbIsMH.Name = "cbMH";
      this.cbIsMH.Size = new System.Drawing.Size(42, 16);
      this.cbIsMH.TabIndex = 9;
      this.cbIsMH.Text = "M+H";
      this.cbIsMH.UseVisualStyleBackColor = true;
      // 
      // MassCalculatorUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
      this.ClientSize = new System.Drawing.Size(727, 180);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtSequence);
      this.Controls.Add(this.txtModification);
      this.Controls.Add(this.cbIsMonoisotopic);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtResult);
      this.Controls.Add(this.cbIsMH);
      this.Controls.Add(this.label3);
      this.Name = "MassCalculatorUI";
      this.Controls.SetChildIndex(this.label3, 0);
      this.Controls.SetChildIndex(this.cbIsMH, 0);
      this.Controls.SetChildIndex(this.txtResult, 0);
      this.Controls.SetChildIndex(this.label2, 0);
      this.Controls.SetChildIndex(this.cbIsMonoisotopic, 0);
      this.Controls.SetChildIndex(this.txtModification, 0);
      this.Controls.SetChildIndex(this.txtSequence, 0);
      this.Controls.SetChildIndex(this.label1, 0);



      this.ResumeLayout(false);
      this.PerformLayout();

    }

    private RcpaTextField sequence;
    private RcpaTextField modification;
    private RcpaCheckBox isMonoisotopic;
    private RcpaCheckBox isMH;

    public static readonly string Title = "Peptide Mass Calculator";

    public static readonly string Version = "1.0.0";

    public MassCalculatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(Title, Version);

      sequence = new RcpaTextField(txtSequence, "Sequence", "Peptide Sequence", "", true);
      AddComponent(sequence);

      modification = new RcpaTextField(txtModification, "Modification", "Modification Definition", "(STY* +79.96633) (M# +15.99492) C=160.16523", false);
      AddComponent(modification);

      isMonoisotopic = new RcpaCheckBox(cbIsMonoisotopic, "IsMonoisotopic", false);
      AddComponent(isMonoisotopic);

      isMH = new RcpaCheckBox(cbIsMH, "IsMH", false);
      AddComponent(isMH);

    }

    protected override void DoRealGo()
    {
      Aminoacids aas = Aminoacids.ParseModificationFromOutFileLine(modification.Text);

      IPeptideMassCalculator calc;
      if (isMonoisotopic.Checked)
      {
        calc = new MonoisotopicPeptideMassCalculator(aas);
      }
      else
      {
        calc = new AveragePeptideMassCalculator(aas);
      }

      string seq = PeptideUtils.GetMatchedSequence(sequence.Text);
      double value;
      if (isMH.Checked)
      {
        value = calc.GetMz(seq, 1);
      }
      else
      {
        value = calc.GetMass(seq);
      }
      txtResult.Text = value.ToString();
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Misc;
      }

      public string GetCaption()
      {
        return MassCalculatorUI.Title;
      }

      public string GetVersion()
      {
        return MassCalculatorUI.Version;
      }

      public void Run()
      {
        new MassCalculatorUI().MyShow();
      }

      #endregion
    }


  }
}
