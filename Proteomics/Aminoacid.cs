using System.Linq;
using System.Text;

namespace RCPA.Proteomics
{
  public delegate double GetAminoacidMassFunction(Aminoacid aa);

  public class Aminoacid
  {
    private double averageMass;
    private AtomComposition composition;
    private string description;
    private double monoMass;
    private int nominalMass;
    private char oneName;
    private string threeName;
    private bool visible;
    private string[] codes;

    public Aminoacid()
    {
      this.oneName = ' ';
      this.threeName = "";
      this.monoMass = 0.0;
      this.nominalMass = 0;
      this.averageMass = 0.0;
      this.description = "";
      this.composition = new AtomComposition("");
      this.visible = false;
      this.codes = new string[] { };
      this.ExcahangableHAtom = 0.0;
    }

    public Aminoacid(char aOneName, string aThreeName, double aMonoMass,
                     double aAverageMass, string aDescription, string compositionString, string[] codes, double exchangableHAtom)
    {
      this.oneName = aOneName;
      this.averageMass = aAverageMass;
      this.description = aDescription;
      this.visible = true;
      SetThreeName(aThreeName);
      SetMonoMass(aMonoMass);
      this.composition = new AtomComposition(compositionString);
      this.codes = codes;
      this.ExcahangableHAtom = exchangableHAtom;
    }

    public Aminoacid(Aminoacid source)
    {
      this.oneName = source.oneName;
      this.threeName = source.threeName;
      this.monoMass = source.monoMass;
      this.averageMass = source.averageMass;
      this.nominalMass = source.nominalMass;
      this.description = source.description;
      this.composition = new AtomComposition(source.ToString());
      this.visible = source.visible;
      this.codes = (string[])source.codes.Clone();
      this.ExcahangableHAtom = source.ExcahangableHAtom;
    }

    public string ThreeName
    {
      get { return this.threeName; }
    }

    public char OneName
    {
      get { return this.oneName; }
    }

    public double MonoMass
    {
      get { return this.monoMass; }
    }

    public double AverageMass
    {
      get { return this.averageMass; }
    }

    public int NominalMass
    {
      get { return this.nominalMass; }
    }

    public string Description
    {
      get { return this.description; }
    }

    public bool Visible
    {
      get { return this.visible; }
      set { this.visible = value; }
    }

    public AtomComposition Composition
    {
      get { return this.composition; }
    }

    public string[] Codes
    {
      get { return codes; }
    }

    public string CompositionStr
    {
      get
      {
        if (this.composition == null)
        {
          return "";
        }
        return this.composition.ToString();
      }
      set { this.composition = new AtomComposition(value); }
    }

    public double ExcahangableHAtom { get; private set; }

    public void ResetMass(double aMonoMass,
                          double aAverageMass)
    {
      this.averageMass = aAverageMass;
      SetMonoMass(aMonoMass);
    }

    public void Initialize(char aOneName, string aThreeName, double aMonoMass,
                           double aAverageMass, string aDescription, string compositionString, bool aVisible, string codes, double exchangableHAtom)
    {
      this.oneName = aOneName;
      this.averageMass = aAverageMass;
      SetThreeName(aThreeName);
      SetMonoMass(aMonoMass);
      this.description = aDescription;
      this.composition = new AtomComposition(compositionString);
      this.visible = aVisible;
      this.codes = (from c in codes.Split(',') select c.Trim()).ToArray();
      this.ExcahangableHAtom = exchangableHAtom;
    }

    //  void print(ostream& stream) const;

    private void SetMonoMass(double aMonoMass)
    {
      this.monoMass = aMonoMass;
      this.nominalMass = (int)(aMonoMass + 0.5);
    }

    private void SetThreeName(string aThreeName)
    {
      this.threeName = aThreeName.Substring(0, 3);
    }

    public double GetAtomMassPercent(Atom atom)
    {
      double atomMass = this.composition[atom] * atom.MonoMass;
      return atomMass / this.monoMass;
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.Append(this.oneName).Append(' ').Append(this.threeName).Append(' ').Append(MyConvert.Format("{0:0.####}",
                                                                                                  this.monoMass))
        .Append(' ').Append(MyConvert.Format("{0:0.####}", this.averageMass)).Append(' ').Append(this.description).Append(
        ' ').Append
        (this.composition.ToString());
      return sb.ToString();
    }

    public static double GetMonoMass(Aminoacid aa)
    {
      return aa.MonoMass;
    }

    public static double GetAverageMass(Aminoacid aa)
    {
      return aa.AverageMass;
    }
  };
}