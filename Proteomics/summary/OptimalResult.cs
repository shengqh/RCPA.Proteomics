using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class OptimalResult
  {
    public OptimalResult(double score, double deltaScore, int peptideCountFromTargetDB, int peptideCountFromDecoyDB, double falseDiscoveryRate)
    {
      this._score = score;
      this._deltaScore = deltaScore;
      this._peptideCountFromTargetDB = peptideCountFromTargetDB;
      this._peptideCountFromDecoyDB = peptideCountFromDecoyDB;
      this._falseDiscoveryRate = falseDiscoveryRate;
    }

    public OptimalResult()
      : this(0.0, 0.0, 0, 0, 0.0)
    { }

    public OptimalResult(double deltaScore)
      : this(0.0, deltaScore, 0, 0, 0.0)
    { }

    public OptimalResult(OptimalResult source)
    {
      this._score = source._score;
      this._deltaScore = source._deltaScore;
      this._peptideCountFromTargetDB = source._peptideCountFromTargetDB;
      this._peptideCountFromDecoyDB = source._peptideCountFromDecoyDB;
      this._falseDiscoveryRate = source._falseDiscoveryRate;
    }

    private double _score;

    public double Score
    {
      get { return _score; }
      set { _score = value; }
    }

    private double _deltaScore;

    public double DeltaScore
    {
      get { return _deltaScore; }
      set { _deltaScore = value; }
    }

    private int _peptideCountFromTargetDB;

    public int PeptideCountFromTargetDB
    {
      get { return _peptideCountFromTargetDB; }
      set { _peptideCountFromTargetDB = value; }
    }

    private int _peptideCountFromDecoyDB;

    public int PeptideCountFromDecoyDB
    {
      get { return _peptideCountFromDecoyDB; }
      set { _peptideCountFromDecoyDB = value; }
    }

    private double _falseDiscoveryRate;

    public double FalseDiscoveryRate
    {
      get { return _falseDiscoveryRate; }
      set { _falseDiscoveryRate = value; }
    }

    public double ExpectValue { get; set; }

    public override string ToString()
    {
      return MyConvert.Format("{0:0.####}\t{1:0.####}\t{2}\t{3}\t{4:0.####}",
        Score,
        DeltaScore,
        PeptideCountFromTargetDB,
        PeptideCountFromDecoyDB,
        FalseDiscoveryRate);
    }
  }

  public class OptimalResults : List<OptimalResult>
  {
    private double _defaultScore;

    private double _defaultDeltaScore;

    public OptimalResults(string scoreName, string deltaScoreName, double defaultScore, double defaultDeltaScore)
    {
      this._scoreName = scoreName;
      this._deltaScoreName = deltaScoreName;
      this._defaultScore = defaultScore;
      this._defaultDeltaScore = defaultDeltaScore;
    }

    private string _scoreName;

    public string ScoreName
    {
      get { return _scoreName; }
      set { _scoreName = value; }
    }

    private string _deltaScoreName;

    public string DeltaScoreName
    {
      get { return _deltaScoreName; }
      set { _deltaScoreName = value; }
    }

    public OptimalResult FindMaxPeptideCountFromTargetDB()
    {
      OptimalResult result = new OptimalResult(_defaultScore, _defaultDeltaScore, 0, 0, 1.0);
      foreach (OptimalResult or in this)
      {
        if (or.PeptideCountFromTargetDB > result.PeptideCountFromTargetDB)
        {
          result = or;
        }
      }
      return result;
    }

    public int GetPeptideCountFromTargetDB()
    {
      int result = 0;
      foreach (OptimalResult or in this)
      {
        result += or.PeptideCountFromTargetDB;
      }
      return result;
    }

    public int GetPeptideCountFromDecoyDB()
    {
      int result = 0;
      foreach (OptimalResult or in this)
      {
        result += or.PeptideCountFromDecoyDB;
      }
      return result;
    }
  }
}
