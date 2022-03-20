using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Proteomics;
using System.Text;

namespace RCPA.Tools.Glycan
{
  public partial class NGlycanPeptideMassCalculatorUI : AbstractUI
  {
    public readonly static string title = "NGlycan Peptide Mass Calculator";
    public readonly static string version = "1.0.0";

    private RcpaTextField peptideSequence;
    private RcpaTextField peptideModification;
    private RcpaTextField glycanStructure;

    public NGlycanPeptideMassCalculatorUI()
    {
      InitializeComponent();

      this.Text = Constants.GetSQHTitle(title, version);

      this.peptideSequence = new RcpaTextField(txtPeptideSequence, "PeptideSequence", "Peptide Sequence", "", false);
      this.peptideModification = new RcpaTextField(txtPeptideModification, "PeptideModification", "Peptide Modification", "(STY* +79.96633) (M# +15.99492) C=160.16523", false);
      this.glycanStructure = new RcpaTextField(txtGlycanStructure, "GlycanStructure", "Glycan Structure", "", false);

      this.AddComponent(peptideSequence);
      this.AddComponent(peptideModification);
      this.AddComponent(glycanStructure);
    }

    protected override void DoRealGo()
    {
      MassCalculator averageCalc = new MassCalculator(Atom.AverageMassFunction);
      MassCalculator monoCalc = new MassCalculator(Atom.MonoMassFunction);

      Aminoacids aas = new Aminoacids();
      aas['C'].ResetMass(aas['C'].MonoMass + 57.0215, aas['C'].AverageMass + 57.0215);

      StringBuilder sb = new StringBuilder();

      double totalMass = 0.0;

      if (peptideSequence.Text.Length > 0)
      {
        double averageMass = aas.AveragePeptideMass(peptideSequence.Text);
        sb.Append(MyConvert.Format("PeptideMass={0:0.0000}; ", averageMass));

        totalMass = averageMass;
      }

      if (glycanStructure.Text.Length > 0)
      {
        AtomComposition ac = new AtomComposition(glycanStructure.Text);
        double averageMass = averageCalc.GetMass(ac);
        sb.Append(MyConvert.Format("GlycanMass={0:0.0000}; ", averageMass));

        if (totalMass > 0)
        {
          totalMass += averageMass - 18.0;
          sb.Append(MyConvert.Format("TotalMass={0:0.0000}; ", totalMass));

          for (int charge = 2; charge <= 4; charge++)
          {
            sb.Append(MyConvert.Format("Charge{0}={1:0.0000}; ", charge, (totalMass + charge) / charge));
          }
        }
      }

      txtResultInfo.Text = sb.ToString();
    }
  }

  public class NGlycanPeptideMassCalculatorCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return MenuCommandType.Misc;
    }

    public string GetCaption()
    {
      return NGlycanPeptideMassCalculatorUI.title;
    }

    public string GetVersion()
    {
      return NGlycanPeptideMassCalculatorUI.version;
    }

    public void Run()
    {
      new NGlycanPeptideMassCalculatorUI().MyShow();
    }

    #endregion
  }
}

