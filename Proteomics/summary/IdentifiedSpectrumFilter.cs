using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Proteomics.Summary;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedSpectrumRankFilter : IFilter<IIdentifiedSpectrum>
  {
    private int _maxRank;

    public IdentifiedSpectrumRankFilter(int maxRank)
    {
      this._maxRank = maxRank;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return e.Rank <= _maxRank;
    }

    public override string ToString()
    {
      return "Rank <= " + _maxRank.ToString();
    }
  }

  public class IdentifiedSpectrumSpRankFilter : IFilter<IIdentifiedSpectrum>
  {
    private int _maxRank;

    public IdentifiedSpectrumSpRankFilter(int maxRank)
    {
      this._maxRank = maxRank;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return e.SpRank <= _maxRank;
    }

    public override string ToString()
    {
      return "SpRank <= " + _maxRank.ToString();
    }
  }

  public class IdentifiedSpectrumScoreFilter : IFilter<IIdentifiedSpectrum>
  {
    private double _minScore;

    public IdentifiedSpectrumScoreFilter(double minScore)
    {
      this._minScore = minScore;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return e.Score >= _minScore;
    }

    public override string ToString()
    {
      return string.Format("Score >= {0:0.####}", _minScore);
    }
  }

  public class IdentifiedSpectrumDeltaScoreFilter : IFilter<IIdentifiedSpectrum>
  {
    private double _minScore;

    public IdentifiedSpectrumDeltaScoreFilter(double minScore)
    {
      this._minScore = minScore;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return e.DeltaScore >= _minScore;
    }

    public override string ToString()
    {
      return string.Format("DeltaScore >= {0:0.####}", _minScore);
    }
  }

  public class IdentifiedSpectrumExpectValueFilter : IFilter<IIdentifiedSpectrum>
  {
    private double _maxExpectValue;

    public IdentifiedSpectrumExpectValueFilter(double maxExpectValue)
    {
      this._maxExpectValue = maxExpectValue;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return e.ExpectValue <= _maxExpectValue;
    }

    public override string ToString()
    {
      return string.Format("ExpectValue <= {0:0.######}", _maxExpectValue);
    }
  }

  public class IdentifiedSpectrumMinProbabilityFilter : IFilter<IIdentifiedSpectrum>
  {
    private double minProbability;

    public IdentifiedSpectrumMinProbabilityFilter(double minProbability)
    {
      this.minProbability = minProbability;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return e.Probability >= minProbability;
    }

    public override string ToString()
    {
      return string.Format("Probability >= {0:0.######}", minProbability);
    }
  }

  public class IdentifiedSpectrumPrecursorFilter : IFilter<IIdentifiedSpectrum>
  {
    private double _ppmTolerance;

    private Func<IIdentifiedSpectrum, bool> acceptFunc;

    public IdentifiedSpectrumPrecursorFilter(double ppmTolerance, bool filterIsotopic)
    {
      this._ppmTolerance = ppmTolerance;

      if (filterIsotopic)
      {
        acceptFunc = m => Math.Abs(PrecursorUtils.mz2ppm(m.ExperimentalMass, m.TheoreticalMinusExperimentalMass)) <= _ppmTolerance ||
          Math.Abs(PrecursorUtils.mz2ppm(m.ExperimentalMass, m.TheoreticalMinusExperimentalMass + IsotopicConsts.AVERAGE_ISOTOPIC_MASS)) <= _ppmTolerance ||
          Math.Abs(PrecursorUtils.mz2ppm(m.ExperimentalMass, m.TheoreticalMinusExperimentalMass + IsotopicConsts.DOUBLE_AVERAGE_ISOTOPIC_MASS)) <= _ppmTolerance;
      }
      else
      {
        acceptFunc = m => Math.Abs(PrecursorUtils.mz2ppm(m.ExperimentalMass, m.TheoreticalMinusExperimentalMass)) <= _ppmTolerance;
      }
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      return acceptFunc(e);
    }

    public override string ToString()
    {
      return string.Format("Precursor tolerance <= {0:0.######}ppm", _ppmTolerance);
    }
  }

  public class IdentifiedSpectrumProteinNameRegexFilter : IFilter<IIdentifiedSpectrum>
  {
    private Regex nameRegex;

    private Func<IIdentifiedSpectrum, bool> acceptFunc;

    public IdentifiedSpectrumProteinNameRegexFilter(string namePattern, bool forAll)
    {
      this.nameRegex = new Regex(namePattern);

      if (forAll)
      {
        acceptFunc = t => t.Proteins.All(m => nameRegex.Match(m).Success);
      }
      else
      {
        acceptFunc = t => t.Proteins.Any(m => nameRegex.Match(m).Success); ;
      }
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return acceptFunc(t);
    }

    #endregion

    public override string ToString()
    {
      return string.Format("Protein name patern = {0}", nameRegex.ToString());
    }
  }

  public class IdentifiedSpectrumProteinNameMapFilter : IFilter<IIdentifiedSpectrum>
  {
    private IStringParser<string> acParser;

    private HashSet<string> conMap;

    public IdentifiedSpectrumProteinNameMapFilter(IStringParser<string> acParser, HashSet<string> contaminationNameMap)
    {
      this.acParser = acParser;
      this.conMap = contaminationNameMap;
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      foreach (var p in t.Proteins)
      {
        var ac = acParser.GetValue(p);
        return conMap.Contains(ac);
      }

      return false;
    }

    public string FilterCondition
    {
      get { return "Contamination Protein Filter"; }
    }

    #endregion

    public override string ToString()
    {
      return string.Format("Protein name map filter");
    }
  }

  public class IdentifiedSpectrumChargeScoreFilter : IFilter<IIdentifiedSpectrum>
  {
    private double[] minScores;

    public IdentifiedSpectrumChargeScoreFilter(double[] minScores)
    {
      this.minScores = minScores;
    }

    public bool Accept(IIdentifiedSpectrum e)
    {
      int charge = e.Query.Charge > minScores.Length ? minScores.Length - 1 : e.Query.Charge - 1;
      return e.Score >= minScores[charge];
    }

    public override string ToString()
    {
      return string.Format("Score vs charge filter");
    }
  }

  public class IdentifiedSpectrumSequenceFilter : IFilter<IIdentifiedSpectrum>
  {
    private string token;
    public IdentifiedSpectrumSequenceFilter(string token)
    {
      this.token = token;
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return t.Sequence.Contains(token);
    }

    #endregion

    public override string ToString()
    {
      return string.Format("Sequence token = " + token.ToString());
    }
  }

  public class IdentifiedSpectrumSequenceLengthFilter : IFilter<IIdentifiedSpectrum>
  {
    private int minLength;
    public IdentifiedSpectrumSequenceLengthFilter(int minLength)
    {
      this.minLength = minLength;
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      if (null == t.Peptide)
      {
        return false;
      }

      return t.Peptide.PureSequence.Length >= minLength;
    }

    #endregion

    public override string ToString()
    {
      return string.Format("Sequence length >= " + minLength.ToString());
    }
  }

  public interface ISpectrumFilter : IFilter<IIdentifiedSpectrum>
  {
    void SetCriteria(object value);
  }

  public class NumOfMissCleavageFilter : ISpectrumFilter
  {
    private int miss;

    public NumOfMissCleavageFilter(int miss)
    {
      this.miss = miss;
    }

    public override string ToString()
    {
      return "NumMissCleavage=" + miss.ToString();
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return t.NumMissedCleavages == this.miss;
    }

    #endregion

    #region ISpectrumFilter Members

    public void SetCriteria(object value)
    {
      this.miss = (int)value;
    }

    #endregion
  }

  public class NumOfProteaseTerminiFilter : ISpectrumFilter
  {
    private int npt;

    public NumOfProteaseTerminiFilter(int npt)
    {
      this.npt = npt;
    }

    public override string ToString()
    {
      return "NumProteaseTermini=" + npt.ToString();
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return t.NumProteaseTermini == this.npt;
    }

    #endregion

    #region ISpectrumFilter Members

    public void SetCriteria(object value)
    {
      this.npt = (int)value;
    }

    #endregion
  }

  public class IdentifiedSpectrumModificationFilter : ISpectrumFilter
  {
    private bool mod;

    Func<IIdentifiedSpectrum, bool> IsModified = m => m.Annotations["Modified"].Equals("True");

    public IdentifiedSpectrumModificationFilter(bool mod)
    {
      this.mod = mod;
    }

    public override string ToString()
    {
      return "IsModified=" + mod.ToString();
    }


    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return IsModified(t) == this.mod;
    }

    #endregion

    #region ISpectrumFilter Members

    public void SetCriteria(object value)
    {
      this.mod = (bool)value;
    }

    #endregion
  }

  public class ChargeFilter : ISpectrumFilter
  {
    private int charge;

    private Func<IIdentifiedSpectrum, bool> func;

    public ChargeFilter(int charge)
    {
      this.charge = charge;

      if (charge == 3)
      {
        this.func = m => m.Charge >= 3;
      }
      else
      {
        this.func = m => m.Charge == this.charge;
      }
    }


    public override string ToString()
    {
      if (charge == 3)
      {
        return "Charge>=3";
      }
      else
      {
        return "Charge=" + this.charge.ToString();
      }
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return func(t);
    }

    #endregion

    #region ISpectrumFilter Members

    public void SetCriteria(object value)
    {
      this.charge = (int)value;
    }

    #endregion
  }

  public class AndSpectrumFilter : AndFilter<IIdentifiedSpectrum>
  {
    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      foreach (var filter in Filters)
      {
        sb.Append(";" + filter.ToString());
      }

      if (sb.Length > 0)
      {
        return sb.ToString().Substring(1);
      }

      return "";
    }
  }

  public class TagFilter : ISpectrumFilter
  {
    private string tag;
    private bool allSpectra;

    private Func<IIdentifiedSpectrum, bool> func;

    public TagFilter(string tag, bool allSpectra)
    {
      this.tag = tag;
      this.allSpectra = allSpectra;

      SetFunction();
    }

    private void SetFunction()
    {
      if (this.allSpectra)
      {
        this.func = m => true;
      }
      else
      {
        this.func = m => m.Tag == tag;
      }
    }

    public override string ToString()
    {
      if (allSpectra)
      {
        return "All";
      }
      else
      {
        return "Tag=" + tag;
      }
    }

    #region IFilter<IIdentifiedSpectrum> Members

    public bool Accept(IIdentifiedSpectrum t)
    {
      return func(t);
    }

    #endregion

    #region ISpectrumFilter Members

    public void SetCriteria(object value)
    {
      if ((string)value == "All")
      {
        this.allSpectra = true;
      }
      else
      {
        this.allSpectra = false;
        this.tag = (string)value;
      }
      SetFunction();
    }

    #endregion
  }


}
