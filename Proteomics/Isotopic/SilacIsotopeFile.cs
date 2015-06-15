using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Isotopic
{
  public interface ISilacIsotopeFile
  {
    IAtomCompositionCalculator SampleAtomCompositionCalculator { get; }

    IAtomCompositionCalculator ReferenceAtomCompositionCalculator { get; }

    IPeptideMassCalculator SampleCalculator { get; }

    IPeptideMassCalculator ReferenceCalculator { get; }

    bool IsModificationDefined(char modifiedChar);
  }

  public abstract class AbstractSilacIsotopeFile : ISilacIsotopeFile
  {
    protected GetMassFromCompositionFunction getMassFromComposition;

    protected IAtomCompositionCalculator referenceAtomCompositionCalculator;
    protected IPeptideMassCalculator referenceCalculator;
    protected IAtomCompositionCalculator sampleAtomCompositionCalculator;
    protected IPeptideMassCalculator sampleCalculator;
    private List<SectionInfo> sections;

    public AbstractSilacIsotopeFile(string filename)
    {
      ReadFromFile(filename);
    }

    #region ISilacIsotopeFile Members

    public IAtomCompositionCalculator SampleAtomCompositionCalculator
    {
      get
      {
        if (this.sampleAtomCompositionCalculator == null)
        {
          InitCalculator();
        }
        return this.sampleAtomCompositionCalculator;
      }
    }

    public IAtomCompositionCalculator ReferenceAtomCompositionCalculator
    {
      get
      {
        if (this.referenceAtomCompositionCalculator == null)
        {
          InitCalculator();
        }
        return this.referenceAtomCompositionCalculator;
      }
    }

    public IPeptideMassCalculator SampleCalculator
    {
      get
      {
        if (this.sampleCalculator == null)
        {
          InitCalculator();
        }
        return this.sampleCalculator;
      }
    }

    public IPeptideMassCalculator ReferenceCalculator
    {
      get
      {
        if (this.sampleCalculator == null)
        {
          InitCalculator();
        }
        return this.referenceCalculator;
      }
    }

    #endregion

    private void ReadFromFile(string filename)
    {
      this.sections = new List<SectionInfo>();
      using (var filein = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
      {
        SectionInfo si;
        while ((si = ReadNextSection(filein)) != null)
        {
          this.sections.Add(si);
        }
      }
    }

    private SectionInfo ReadNextSection(StreamReader filein)
    {
      string line;
      while ((line = filein.ReadLine()) != null)
      {
        if (line.Length == 0)
        {
          continue;
        }
        break;
      }

      if (line == null)
      {
        return null;
      }

      Match match = Regex.Match(line, "<(.+)>");
      if (!match.Success)
      {
        return null;
      }

      var result = new SectionInfo();
      result.ItemName = match.Groups[1].Value;

      AtomComposition sampleMap;
      AtomComposition referenceMap;

      if (line.IndexOf("SAM") < line.IndexOf("REF"))
      {
        ReadAtomMap(filein, out sampleMap, out referenceMap);
      }
      else
      {
        ReadAtomMap(filein, out referenceMap, out sampleMap);
      }

      result.SampleAtomMap = sampleMap;
      result.ReferenceAtomMap = referenceMap;

      //如果是修饰，那么要求sample和reference定义一样的atom composition
      if (!Char.IsLetter(result.ItemName[0])) 
      {
        if (result.SampleAtomMap.Values.Sum() != result.ReferenceAtomMap.Values.Sum())
        {
          throw new ArgumentException(MyConvert.Format("Read SILAC information of {0} from SILAC configuration file error : the atom composition definitions for sample and refernce are different!", result.ItemName));
        }
      }

      return result;
    }

    private void ReadAtomMap(StreamReader filein, out AtomComposition leftMap, out AtomComposition rightMap)
    {
      leftMap = new AtomComposition("");
      rightMap = new AtomComposition("");
      string line;
      while ((line = filein.ReadLine()) != null)
      {
        if (line.Trim().Length == 0)
        {
          break;
        }
        Match atomMatch = Regex.Match(line, @"(\d*)(\S+)\s(\d+)\s(\d+)");
        if (!atomMatch.Success)
        {
          throw new Exception("Cannot parse atom information from " + line);
        }

        string atomSymbol;
        if (atomMatch.Groups[1].Length > 0)
        {
          atomSymbol = "(" + atomMatch.Groups[2].Value + atomMatch.Groups[1].Value + ")";
        }
        else
        {
          atomSymbol = atomMatch.Groups[2].Value;
        }

        int leftCount = int.Parse(atomMatch.Groups[3].Value);
        int rightCount = int.Parse(atomMatch.Groups[4].Value);

        Atom atom = Atom.ValueOf(atomSymbol);
        if (0 != leftCount)
        {
          leftMap[atom] = leftCount;
        }

        if (0 != rightCount)
        {
          rightMap[atom] = rightCount;
        }
      }
    }

    protected void InitCalculator()
    {
      {
        var sample = new Aminoacids();
        sample.SetVisible(false);
        var reference = new Aminoacids();
        reference.SetVisible(false);

        var sampleNterm = new AtomComposition("");
        var sampleCterm = new AtomComposition("");
        var refNterm = new AtomComposition("");
        var refCterm = new AtomComposition("");

        foreach (SectionInfo si in this.sections)
        {
          if (si.ItemName.Length == 1)
          {
            char aa = si.ItemName[0];
            sample[aa].CompositionStr = si.SampleAtomMap.ToString();
            sample[aa].ResetMass(Atom.GetMonoMass(si.SampleAtomMap), Atom.GetAverageMass(si.SampleAtomMap));
            sample[aa].Visible = true;
            reference[aa].CompositionStr = si.ReferenceAtomMap.ToString();
            reference[aa].ResetMass(Atom.GetMonoMass(si.ReferenceAtomMap), Atom.GetAverageMass(si.ReferenceAtomMap));
            reference[aa].Visible = true;
          }
          else if (si.ItemName.Equals("NTERM"))
          {
            sampleNterm = si.SampleAtomMap;
            refNterm = si.ReferenceAtomMap;
          }
          else if (si.ItemName.Equals("CTERM"))
          {
            sampleCterm = si.SampleAtomMap;
            refCterm = si.ReferenceAtomMap;
          }
          else
          {
            throw new Exception("What is it? " + si.ItemName[0]);
          }
        }

        this.sampleCalculator = AllocatePeptideMassCalculator(sample, this.getMassFromComposition(sampleNterm),
                                                              this.getMassFromComposition(sampleCterm));
        this.referenceCalculator = AllocatePeptideMassCalculator(reference, this.getMassFromComposition(refNterm),
                                                                 this.getMassFromComposition(refCterm));
        this.sampleAtomCompositionCalculator = new PeptideAtomCompositionCalculator(sampleNterm, sampleCterm, sample);
        this.referenceAtomCompositionCalculator = new PeptideAtomCompositionCalculator(refNterm, refCterm, reference);
      }
    }

    public bool IsModificationDefined(char modifiedChar)
    {
      var modification = (from s in sections
                          where s.ItemName[0].Equals(modifiedChar)
                          select s).FirstOrDefault();

      if (modification == null)
      {
        return false;
      }

      if (modification.SampleAtomMap.Values.Sum() == 0 || modification.ReferenceAtomMap.Values.Sum() == 0)
      {
        return false;
      }

      return true;
    }

    protected abstract IPeptideMassCalculator AllocatePeptideMassCalculator(Aminoacids sample, double sampleNterm,
                                                                            double sampleCterm);

    #region Nested type: SectionInfo

    protected class SectionInfo
    {
      public string ItemName { get; set; }

      public AtomComposition SampleAtomMap { get; set; }

      public AtomComposition ReferenceAtomMap { get; set; }
    }

    #endregion
  }

  public class MonoisotopicSilacIsotopeFile : AbstractSilacIsotopeFile
  {
    public MonoisotopicSilacIsotopeFile(string filename)
      : base(filename)
    {
      getMassFromComposition = Atom.GetMonoMass;
    }

    protected override IPeptideMassCalculator AllocatePeptideMassCalculator(Aminoacids aas, double nterm, double cterm)
    {
      return new MonoisotopicPeptideMassCalculator(aas, nterm, cterm);
    }
  }

  public class AverageSilacIsotopeFile : AbstractSilacIsotopeFile
  {
    public AverageSilacIsotopeFile(string filename)
      : base(filename)
    {
      getMassFromComposition = Atom.GetAverageMass;
    }

    protected override IPeptideMassCalculator AllocatePeptideMassCalculator(Aminoacids aas, double nterm, double cterm)
    {
      return new AveragePeptideMassCalculator(aas, nterm, cterm);
    }
  }
}