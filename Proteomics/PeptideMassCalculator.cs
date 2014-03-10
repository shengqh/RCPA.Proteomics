using System;

namespace RCPA.Proteomics
{
  public delegate double GetResiduesMassFunction(string sequence);

  public interface IPeptideMassCalculator
  {
    double GetMass(string peptide);
    double GetMz(string peptide, int charge);
  }

  public abstract class AbstractPeptideMassCalculator : IPeptideMassCalculator
  {
    private readonly double cterm;
    private readonly double nterm;

    public AbstractPeptideMassCalculator(double nterm, double cterm)
    {
      this.nterm = nterm;
      this.cterm = cterm;
    }

    #region IPeptideMassCalculator Members

    public double GetMass(string peptide)
    {
      return this.nterm + this.cterm + GetMassResidue(peptide);
    }

    public double GetMz(string peptide, int charge)
    {
      if (charge <= 0)
      {
        throw new ArgumentOutOfRangeException("Charge should larger than zero : current is " + charge);
      }

      return (GetMass(peptide) + charge*GetMassH())/charge;
    }

    #endregion

    protected abstract double GetMassResidue(string peptide);
    protected abstract double GetMassH();
  }

  public class MonoisotopicPeptideMassCalculator : AbstractPeptideMassCalculator
  {
    private static readonly double CTERM = Atom.H.MonoMass + Atom.O.MonoMass;
    private static readonly double NTERM = Atom.H.MonoMass;
    private readonly Aminoacids aas;

    public MonoisotopicPeptideMassCalculator(Aminoacids aas, double nterm, double cterm)
      : base(nterm, cterm)
    {
      this.aas = aas;
    }

    public MonoisotopicPeptideMassCalculator(Aminoacids aas)
      : base(NTERM, CTERM)
    {
      this.aas = aas;
    }

    protected override double GetMassResidue(string peptide)
    {
      return this.aas.MonoResiduesMass(peptide);
    }

    protected override double GetMassH()
    {
      return Atom.H.MonoMass;
    }
  }

  public class AveragePeptideMassCalculator : AbstractPeptideMassCalculator
  {
    private static readonly double CTERM = Atom.H.AverageMass + Atom.O.AverageMass;
    private static readonly double NTERM = Atom.H.AverageMass;
    private readonly Aminoacids aas;

    public AveragePeptideMassCalculator(Aminoacids aas, double nterm, double cterm)
      : base(nterm, cterm)
    {
      this.aas = aas;
    }

    public AveragePeptideMassCalculator(Aminoacids aas)
      : base(NTERM, CTERM)
    {
      this.aas = aas;
    }

    protected override double GetMassResidue(string peptide)
    {
      return this.aas.AverageResiduesMass(peptide);
    }

    protected override double GetMassH()
    {
      return Atom.H.AverageMass;
    }
  }
}