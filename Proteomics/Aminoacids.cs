using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Proteomics.Modification;
using RCPA.Proteomics.Utils;

namespace RCPA.Proteomics
{
  public class Aminoacids
  {
    private List<Aminoacid> mAminoacids;

    public Aminoacids()
    {
      Initialize();
    }

    public Aminoacids(Aminoacids source)
    {
      this.mAminoacids = source.mAminoacids;
    }

    public Aminoacid this[int index]
    {
      get { return this.mAminoacids[index]; }
    }

    public Aminoacid this[char amino]
    {
      get { return this.mAminoacids[amino]; }
    }

    public int Count
    {
      get { return this.mAminoacids.Count; }
    }

    ~Aminoacids()
    {
      this.mAminoacids.Clear();
    }

    public void SetVisible(bool visible)
    {
      foreach (Aminoacid aa in this.mAminoacids)
      {
        aa.Visible = visible;
      }
    }

    public void Initialize()
    {
      this.mAminoacids = new List<Aminoacid>();
      for (int i = 0; i < 128; i++)
      {
        this.mAminoacids.Add(new Aminoacid());
      }

      //http://www.matrixscience.com/help/aa_help.html
      this.mAminoacids['G'].Initialize('G', "Gly", 57.021464, 57.0513, "Glycine", "C2H3NO", true, "GGU, GGC, GGA, GGG");
      this.mAminoacids['A'].Initialize('A', "Ala", 71.037114, 71.0779, "Alanine", "C3H5NO", true, "GCU, GCC, GCA, GCG");
      this.mAminoacids['S'].Initialize('S', "Ser", 87.032028, 87.0773, "Serine", "C3H5NO2", true, "UCU, UCC, UCA, UCG, AGU, AGC");
      this.mAminoacids['P'].Initialize('P', "Pro", 97.052764, 97.1152, "Proline", "C5H7NO", true, "CCU, CCC, CCA, CCG");
      this.mAminoacids['V'].Initialize('V', "Val", 99.068414, 99.1311, "Valine", "C5H9NO", true, "GUU, GUC, GUA, GUG");
      this.mAminoacids['T'].Initialize('T', "Thr", 101.047679, 101.1039, "Threonine", "C4H7NO2", true, "ACU, ACC, ACA, ACG");
      this.mAminoacids['C'].Initialize('C', "Cys", 103.009185, 103.1429, "Cysteine", "C3H5NOS", true, "UGU, UGC");
      this.mAminoacids['I'].Initialize('I', "Ile", 113.084064, 113.1576, "Isoleucine", "C6H11NO", true, "AUU, AUC, AUA");
      this.mAminoacids['L'].Initialize('L', "Leu", 113.084064, 113.1576, "Leucine", "C6H11NO", true, "UUA, UUG, CUU, CUC, CUA, CUG");
      this.mAminoacids['N'].Initialize('N', "Asn", 114.042927, 114.1026, "Asparagine", "C4H6N2O2", true, "AAU, AAC");
      this.mAminoacids['D'].Initialize('D', "Asp", 115.026943, 115.0874, "Aspartic acid", "C4H5NO3", true, "GAU, GAC");
      this.mAminoacids['Q'].Initialize('Q', "Gln", 128.058578, 128.1292, "Glutamine", "C5H8N2O2", true, "CAA, CAG");
      this.mAminoacids['K'].Initialize('K', "Lys", 128.094963, 128.1723, "Lysine", "C6H12N2O", true, "AAA, AAG");
      this.mAminoacids['E'].Initialize('E', "Glu", 129.042593, 129.114, "Glutamic acid", "C5H7NO3", true, "GAA, GAG");
      this.mAminoacids['M'].Initialize('M', "Met", 131.040485, 131.1961, "Methionine", "C5H9NOS", true, "AUG");
      this.mAminoacids['H'].Initialize('H', "His", 137.058912, 137.1393, "Histidine", "C6H7N3O", true, "CAU, CAC");
      this.mAminoacids['F'].Initialize('F', "Phe", 147.068414, 147.1739, "Phenylalanine", "C9H9NO", true, "UUU, UUC");
      this.mAminoacids['R'].Initialize('R', "Arg", 156.101111, 156.1857, "Arginine", "C6H12N4O", true, "CGU, CGC, CGA, CGG, AGA, AGG");
      this.mAminoacids['Y'].Initialize('Y', "Tyr", 163.06332, 163.1733, "Tyrosine", "C9H9NO2", true, "UAU, UAC");
      this.mAminoacids['W'].Initialize('W', "Trp", 186.079313, 186.2099, "Tryptophan", "C11H10N2O", true, "UGG");
    }

    public double AverageResiduesMass(string sequence)
    {
      double result = 0.0;
      for (int i = 0; i < sequence.Length; i++)
        result += this.mAminoacids[sequence[i]].AverageMass;
      return result;
    }

    public double MonoResiduesMass(string sequence)
    {
      double result = 0.0;
      for (int i = 0; i < sequence.Length; i++)
        result += this.mAminoacids[sequence[i]].MonoMass;
      return result;
    }

    public double AveragePeptideMass(string sequence)
    {
      double result = Atom.H.AverageMass * 2 + Atom.O.AverageMass;
      for (int i = 0; i < sequence.Length; i++)
        result += this.mAminoacids[sequence[i]].AverageMass;
      return result;
    }

    public double MonoPeptideMass(string sequence)
    {
      double result = Atom.H.MonoMass * 2 + Atom.O.MonoMass;
      for (int i = 0; i < sequence.Length; i++)
        result += this.mAminoacids[sequence[i]].MonoMass;
      return result;
    }

    public int AtomCount(string sequence, Atom atom)
    {
      int result = 0;
      foreach (char c in sequence)
      {
        Aminoacid aa = this.mAminoacids[c];
        if (aa.Composition.ContainsKey(atom))
        {
          result += aa.Composition[atom];
        }
      }
      return result;
    }

    public AtomComposition GetPeptideAtomComposition(string sequence)
    {
      return GetPeptideAtomComposition(sequence, "H2O");
    }

    public AtomComposition GetPeptideAtomComposition(string sequence, string terminal)
    {
      var result = new AtomComposition(terminal);
      foreach (char c in sequence)
      {
        Aminoacid aa = this.mAminoacids[c];
        result.Add(aa.Composition);
      }
      return result;
    }

    public Aminoacid GetMaxCarbonPercentAminoacid()
    {
      Aminoacid result = null;
      double maxCarbonPercent = 0.0;
      for (int i = 0; i < 128; i++)
      {
        if (this[i].Visible)
        {
          double curCarbonPercent = this[i].GetAtomMassPercent(Atom.C);
          if (curCarbonPercent > maxCarbonPercent)
          {
            maxCarbonPercent = curCarbonPercent;
            result = this[i];
          }
        }
      }
      return result;
    }

    public Aminoacid GetMinCarbonPercentAminoacid()
    {
      Aminoacid result = null;
      double minCarbonPercent = 1.0;
      for (int i = 0; i < 128; i++)
      {
        if (this[i].Visible)
        {
          double curCarbonPercent = this[i].GetAtomMassPercent(Atom.C);
          if (curCarbonPercent < minCarbonPercent)
          {
            minCarbonPercent = curCarbonPercent;
            result = this[i];
          }
        }
      }
      return result;
    }

    public string GetVisibleAminoacids()
    {
      StringBuilder sb = new StringBuilder();
      mAminoacids.ForEach(m =>
      {
        if (m.Visible)
        {
          sb.Append(m.OneName);
        }
      });
      return sb.ToString();
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      for (int i = 0; i < this.mAminoacids.Count; i++)
      {
        if (this.mAminoacids[i].Visible)
        {
          sb.Append(this.mAminoacids[i]).AppendLine();
        }
      }
      return sb.ToString();
    }

    //(STY* +79.96633) (M# +15.99492) (ST@ -18.00000) C=160.16523  Enzyme:Trypsin(KR) (1)
    public static Aminoacids ParseModificationFromOutFileLine(string line)
    {
      Dictionary<char, double> map = ModificationUtils.ParseFromOutFileLine(line);

      var result = new Aminoacids();

      foreach (char c in map.Keys)
      {
        result[c].ResetMass(map[c], map[c]);
      }

      return result;
    }

    /// <summary>
    /// 根据序列构建氨基酸信息列表。该信息列表中已将修饰质量加到氨基酸上，可方便地用于构建离子列表。
    /// </summary>
    /// <param name="sequence">氨基酸序列，包含修饰字符。</param>
    /// <returns>氨基酸信息列表</returns>
    public List<AminoacidInfo> BuildInfo(string sequence)
    {
      var matchedSequence = PeptideUtils.GetMatchedSequence(sequence);

      var result = new List<AminoacidInfo>();
      foreach (char c in matchedSequence)
      {
        if (char.IsLetter(c) && char.IsUpper(c))
        {
          var aai = new AminoacidInfo();
          aai.Aminoacid = c;
          aai.Modification = ' ';
          aai.Mass = this[c].MonoMass;
          result.Add(aai);
        }
        else
        {
          AminoacidInfo aai = result.Last();
          aai.Modification = c;
          aai.Mass += this[c].MonoMass;
        }
      }
      return result;
    }

    /// <summary>
    /// 设置修饰。对于固定修饰而言，key值就是氨基酸。对于动态修饰而言，key值就是特征字符（例如*,#等等）
    /// </summary>
    /// <param name="mods">修饰表</param>
    public void SetModification(Dictionary<char, double> mods)
    {
      foreach (var sm in mods)
      {
        this[sm.Key].ResetMass(sm.Value, sm.Value);
      }
    }
  }
}