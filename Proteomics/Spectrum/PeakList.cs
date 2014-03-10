using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RCPA.Proteomics.Raw;

namespace RCPA.Proteomics.Spectrum
{
  public class PeakList<T> : List<T>, IComparer<PeakList<T>>, IAnnotation where T : IPeak
  {
    protected static readonly double C13 = 1.003355;

    private readonly Dictionary<string, Object> annotations = new Dictionary<string, object>();

    private readonly List<ScanTime> scanTimes = new List<ScanTime>();

    public PrecursorPeak Precursor { get; set; }

    public string ScanMode { get; set; }

    public double PrecursorOffsetPPM { get; set; }

    public double ProductOffsetPPM { get; set; }

    public PeakList()
    {
      ScanMode = string.Empty;
      PrecursorPercentage = 0.0;
      Precursor = new PrecursorPeak();
    }

    public PeakList(T source)
      : this()
    {
      Add(source);
    }

    public PeakList(IEnumerable<T> source)
      : this()
    {
      AddRange(source);
    }

    public PeakList(PeakList<T> source)
      : this()
    {
      AssignInforamtion(source);
      AddRange(source);
      foreach (String key in source.annotations.Keys)
      {
        this.annotations.Add(key, source.annotations[key]);
      }
    }

    public int FirstScan
    {
      get
      {
        if (ScanTimes.Count > 0)
        {
          return ScanTimes[0].Scan;
        }
        return 0;
      }
      set
      {
        if (ScanTimes.Count > 0)
        {
          ScanTimes[0].Scan = value;
        }
        else
        {
          ScanTimes.Add(new ScanTime(value, 0.0));
        }
      }
    }

    public bool IsIdentified { get; set; }

    public int MsLevel { get; set; }

    public bool IsFullMs
    {
      get
      {
        return 1 == this.MsLevel;
      }
    }

    public double PrecursorMZ
    {
      get
      {
        if (Precursor.MonoIsotopicMass == 0.0)
        {
          return Precursor.IsolationMass;
        }
        else
        {
          return Precursor.MonoIsotopicMass;
        }
      }
      set
      {
        Precursor.MonoIsotopicMass = value;
      }
    }

    public int PrecursorCharge
    {
      get
      {
        return Precursor.Charge;
      }
      set
      {
        Precursor.Charge = value;
      }
    }

    public double PrecursorIntensity
    {
      get
      {
        return Precursor.Intensity;
      }
      set
      {
        Precursor.Intensity = value;
      }
    }

    /// <summary>
    /// 母离子及其isotopic占isolation width范围内所有离子的丰度比。
    /// </summary>
    public double PrecursorPercentage { get; set; }

    public string Experimental { get; set; }

    public List<ScanTime> ScanTimes
    {
      get { return this.scanTimes; }
    }

    #region IAnnotation Members

    public Dictionary<string, object> Annotations
    {
      get { return this.annotations; }
    }

    #endregion

    #region IComparer<PeakList<T>> Members

    public int Compare(PeakList<T> x, PeakList<T> y)
    {
      return x.GetSequestDtaName().CompareTo(y.GetSequestDtaName());
    }

    #endregion

    public new void Clear()
    {
      base.Clear();
      this.scanTimes.Clear();
      this.annotations.Clear();
    }

    public void AssignInforamtion(PeakList<T> source)
    {
      this.MsLevel = source.MsLevel;
      this.Precursor = new PrecursorPeak(source.Precursor);
      this.Experimental = source.Experimental;
      this.scanTimes.Clear();
      this.scanTimes.AddRange(source.scanTimes);
    }

    public Dictionary<int, PeakList<T>> GetChargeMap()
    {
      var result = new Dictionary<int, PeakList<T>>();
      foreach (T peak in this)
      {
        if (!result.ContainsKey(peak.Charge))
        {
          PeakList<T> pkl = NewSubPeakList();
          result.Add(peak.Charge, pkl);
        }

        result[peak.Charge].Add(peak);
      }
      return result;
    }

    public PeakList<T> Filter(IFilter<T> filter)
    {
      PeakList<T> result = NewSubPeakList();
      foreach (T peak in this)
      {
        if (filter.Accept(peak))
        {
          result.Add(peak);
        }
      }
      return result;
    }

    public PeakList<T> FindCharge(int charge)
    {
      if (0 == charge)
      {
        throw new ArgumentException("Charge cannot be zero.");
      }

      PeakList<T> result = NewSubPeakList();
      foreach (T peak in this)
      {
        if (charge == peak.Charge)
        {
          result.Add(peak);
        }
      }

      return result;
    }

    private PeakList<T> NewSubPeakList()
    {
      var result = new PeakList<T>();
      result.AssignInforamtion(this);
      return result;
    }

    public PeakList<T> FindPeak(double mz, double mzTolerance)
    {
      PeakList<T> result = NewSubPeakList();

      double minMz = mz - mzTolerance;
      double maxMz = mz + mzTolerance;

      foreach (var p in this)
      {
        if (p.Mz < minMz)
        {
          continue;
        }

        if (p.Mz > maxMz)
        {
          break;
        }
        result.Add(p);
      }

      return result;
    }

    public PeakList<T> FindPeak(double mz, int charge, double mzTolerance)
    {
      PeakList<T> result = NewSubPeakList();

      double minMz = mz - mzTolerance;
      double maxMz = mz + mzTolerance;

      foreach (var p in this)
      {
        if (p.Mz < minMz)
        {
          continue;
        }

        if (p.Mz > maxMz)
        {
          break;
        }

        if (p.Charge != charge)
        {
          continue;
        }

        result.Add(p);
      }

      return result;
    }

    public T FindMaxIntensityPeak()
    {
      return (from p in this
              orderby p.Intensity descending
              select p).FirstOrDefault();
    }

    public void FilterByMinCharge(int minCharge)
    {
      this.RemoveAll(m => m.Charge < minCharge);
    }

    private double GetGap(PeakList<T> pkl, int highestIndex)
    {
      double totalDistance = 0.0;
      double totalWeight = 0.0;
      for (int i = 0; i < pkl.Count; i++)
      {
        if (i == highestIndex)
        {
          continue;
        }

        var gapCount = Math.Abs(i - highestIndex);
        var weight = 1 / gapCount;
        totalDistance += Math.Abs(pkl[i].Mz - pkl[highestIndex].Mz) / gapCount * weight;
        totalWeight += weight;
      }
      return totalDistance / totalWeight;
    }

    public PeakList<T> FindEnvelope(IPeak peak, double mzTolerance, bool forwardOnly)
    {
      return FindEnvelope(peak.Mz, peak.Charge, mzTolerance, forwardOnly);
    }

    public PeakList<T> FindEnvelope(double ionMass, int ionCharge, double mzTolerance, bool forwardOnly)
    {
      PeakList<T> result = NewSubPeakList();

      PeakList<T> curPeaks = null;
      int charge = ionCharge;

      if (0 != charge)
      {
        curPeaks = FindPeak(ionMass, charge, mzTolerance);
        if (0 == curPeaks.Count)
        {
          charge = 0;
        }
      }

      if (0 == charge)
      {
        curPeaks = FindPeak(ionMass, mzTolerance);
      }

      if (curPeaks.Count == 0)
      {
        return result;
      }
      else
      {
        result.Add(curPeaks.FindMaxIntensityPeak());

        //Console.WriteLine("1 : {0:0.0000}", result[0].Mz - peak.Mz);
      }

      if (0 == ionCharge)
      {
        return result;
      }

      var gapMz = ChargeDeconvolution.C_GAP / ionCharge;
      int highestIndex = 0;

      double gapTolerance = 0.0001;

      bool bCalculateGap = true;

      int count = 1;
      if (!forwardOnly)
      {
        //backward
        while (true)
        {
          double expectMz = result[highestIndex].Mz - count * gapMz;
          count++;

          PeakList<T> findPeaks = 0 == charge ? FindPeak(expectMz, mzTolerance) : FindPeak(expectMz, charge, mzTolerance);
          if (findPeaks.Count > 0)
          {
            var curPeak = findPeaks.FindMaxIntensityPeak();
            result.Insert(0, curPeak);
            highestIndex++;

            if (curPeak.Intensity > result[highestIndex].Intensity && 1 == highestIndex)
            {
              highestIndex = 0;
              count = 1;
              bCalculateGap = true;
            }

            if (bCalculateGap)
            {
              var newGap = GetGap(result, highestIndex);
              if (Math.Abs(newGap - gapMz) < gapTolerance)
              {
                bCalculateGap = false;
              }
              gapMz = newGap;
            }
          }
          else
          {
            break;
          }
        }
      }

      //forward
      count = result.Count - highestIndex;
      bCalculateGap = true;
      while (true)
      {
        double expectMz = result[highestIndex].Mz + gapMz * count;
        count++;

        PeakList<T> findPeaks = 0 == charge ? FindPeak(expectMz, mzTolerance) : FindPeak(expectMz, charge, mzTolerance);
        if (findPeaks.Count > 0)
        {
          var curPeak = findPeaks.FindMaxIntensityPeak();
          result.Add(curPeak);

          if (curPeak.Intensity > result[highestIndex].Intensity && 2 == count)
          {
            highestIndex = result.Count - 1;
            count = 1;
            bCalculateGap = true;
          }

          if (bCalculateGap)
          {
            var newGap = GetGap(result, highestIndex);
            if (Math.Abs(newGap - gapMz) < gapTolerance)
            {
              bCalculateGap = false;
            }
            gapMz = newGap;
          }

          //Console.WriteLine("{0} : {1:0.0000} : {2:0.0000}", result.Count, gapMz, result[result.Count - 1].Mz - expectMz);
        }
        else
        {
          break;
        }
      }

      //Console.WriteLine("{0}\t{1:0.0000}\t{2}\t{3:0.000000}\t{4}", result.FirstScan, peak.Mz, peak.Charge, gapMz * peak.Charge, result.Count);

      return result;
    }

    public int SearchFirstLarger(double mz)
    {

      int low = 0;
      int high = this.Count - 1;
      while (low <= high)
      {
        int middle = (low + high) / 2;
        if (mz < this[middle].Mz)
          high = middle - 1;
        else
          low = middle + 1;
      }

      return high + 1;
    }

    public PeakList<T> FindEnvelopeDirectly(List<double> profile, int maxProfile, double mzTolerance, Func<T> allocator)
    {
      var result = new PeakList<T>();
      result.scanTimes.AddRange(this.scanTimes);

      int start = SearchFirstLarger(profile[0] - mzTolerance);

      int iPro = 0;
      int iThis = start;
      int lenPro = Math.Min(profile.Count, maxProfile);
      int lenThis = this.Count;

      if (iThis < lenThis)
      {
        var nMzTolerance = -mzTolerance;

        T curProPeak = default(T);
        while (true)
        {
          double dis = this[iThis].Mz - profile[iPro];
          if (dis < nMzTolerance)
          {
            iThis++;
            if (iThis == lenThis)
            {
              break;
            }

            continue;
          }

          if (dis > mzTolerance)
          {
            if (curProPeak == null)
            {
              T p = allocator();
              p.Mz = profile[iPro];
              p.Intensity = 0.0;
              result.Add(p);
            }
            else
            {
              result.Add(curProPeak);
            }

            iPro++;
            if (iPro == lenPro)
            {
              break;
            }

            curProPeak = default(T);
            continue;
          }

          if (curProPeak == null || curProPeak.Intensity < this[iThis].Intensity)
          {
            curProPeak = this[iThis];
          }

          iThis++;
          if (iThis == lenThis)
          {
            if (curProPeak == null)
            {
              T p = allocator();
              p.Mz = profile[iPro];
              p.Intensity = 0.0;
              result.Add(p);
            }
            else
            {
              result.Add(curProPeak);
            }
            break;
          }
        }
      }

      for (int i = result.Count; i < profile.Count; i++)
      {
        T p = allocator();
        p.Mz = profile[i];
        p.Intensity = 0.0;
        result.Add(p);
      }

      return result;
    }

    //Get the peak in special envelope directly, ignore the charge of peak, 
    //missed peak will be assumed as zero intensity
    public PeakList<T> FindEnvelopeDirectly(double mz, int charge, double mzTolerance, int profileLength, Func<T> allocator)
    {
      Envelope envelope = new Envelope(mz, charge, profileLength);

      return FindEnvelopeDirectly(envelope, profileLength, mzTolerance, allocator);
    }

    public PeakList<T> FindEnvelopeDirectly(List<double> envelope, double mzTolerance, Func<T> allocator)
    {
      return FindEnvelopeDirectly(envelope, envelope.Count, mzTolerance, allocator);
    }

    public PeakList<T> FindEnvelopeDirectly(List<Peak> profile, int maxProfile, double mzTolerance, Func<T> allocator)
    {
      return FindEnvelopeDirectly(profile.ConvertAll(m => m.Mz).ToList(), maxProfile, mzTolerance, allocator);
    }

    public PeakList<T> FindEnvelopeDirectly(List<Peak> profile, double mzTolerance, Func<T> allocator)
    {
      return FindEnvelopeDirectly(profile.ConvertAll(m => m.Mz).ToList(), profile.Count, mzTolerance, allocator);
    }

    public PeakList<T> _FindEnvelopeDirectly(List<Peak> profile, double mzTolerance, Func<T> allocator)
    {
      var result = new PeakList<T>();
      result.scanTimes.AddRange(this.scanTimes);

      foreach (var curMz in profile)
      {
        PeakList<T> find = FindPeak(curMz.Mz, mzTolerance);
        if (find.Count > 0)
        {
          result.Add(find.FindMaxIntensityPeak());
        }
        else
        {
          T p = allocator();
          p.Mz = curMz.Mz;
          p.Intensity = 0.0;
          result.Add(p);
        }
      }

      return result;
    }

    public List<PeakList<T>> GetEnvelopes(double ppmTolerance)
    {
      var tmp = new PeakList<T>(this);

      var result = new List<PeakList<T>>();
      while (tmp.Count > 0)
      {
        double mzTolerance = 2 * PrecursorUtils.ppm2mz(tmp[0].Mz, ppmTolerance);
        PeakList<T> envelope = tmp.FindEnvelope(tmp[0], mzTolerance, true);
        result.Add(envelope);

        foreach (T peak in envelope)
        {
          //Remove all peaks around current peak
          PeakList<T> findPeaks = tmp.FindPeak(peak.Mz, peak.Charge, mzTolerance);
          foreach (T findPeak in findPeaks)
          {
            tmp.Remove(findPeak);
          }
        }
      }

      result.Sort(new PeakListMzAscendingComparer<T>());

      int current = 0;

      while (current < result.Count - 1)
      {
        PeakList<T> currentPkl = result[current];
        if (currentPkl[0].Charge > 0)
        {
          double expectMz = currentPkl[currentPkl.Count - 1].Mz + ChargeDeconvolution.C_GAP / currentPkl[0].Charge;
          double mzTolerance = 4 * PrecursorUtils.ppm2mz(currentPkl[0].Mz, ppmTolerance);
          int next = current + 1;
          while (next < result.Count)
          {
            double gap = result[next][0].Mz - expectMz;
            if (gap >= 2.0)
            {
              break;
            }

            if (Math.Abs(gap) > mzTolerance)
            {
              next++;
              continue;
            }

            if (result[next][0].Charge != currentPkl[0].Charge)
            {
              next++;
              continue;
            }


            currentPkl.AddRange(result[next]);

            result.RemoveAt(next);
          }
        }

        current++;
      }

      return result;
    }

    public ScanTime GetFirstScanTime()
    {
      if (this.scanTimes.Count == 0)
      {
        return new ScanTime(0, 0.0);
      }
      else
      {
        return this.scanTimes[0];
      }
    }

    public ScanTime GetLastScanTime()
    {
      if (this.scanTimes.Count == 0)
      {
        return new ScanTime(0, 0.0);
      }
      else
      {
        return this.scanTimes[this.scanTimes.Count - 1];
      }
    }

    public bool IsSameScan(PeakList<T> another)
    {
      if (!this.Experimental.Equals(another.Experimental))
      {
        return false;
      }

      ScanTime firstThis = GetFirstScanTime();
      ScanTime firstAnother = another.GetFirstScanTime();
      ScanTime lastThis = GetLastScanTime();
      ScanTime lastAnother = another.GetLastScanTime();

      return firstThis.Scan != 0 && firstThis.Scan == firstAnother.Scan &&
             lastThis.Scan == lastAnother.Scan;
    }

    public string GetSequestDtaName()
    {
      var sb = new StringBuilder();
      sb.Append(this.Experimental).Append('.').Append(GetFirstScanTime().Scan).Append('.').Append(GetLastScanTime().Scan)
        .Append('.').Append(this.PrecursorCharge).Append(".dta");
      return sb.ToString();
    }

    //Find peak list constains max intensity peak whose mz is equals to assigned peak and charge is zero or equals to assigned peak
    public static PeakList<T> FindEnvelopeInList(List<PeakList<T>> list, T peak, double mzTolerance)
    {
      PeakList<T> result = null;

      double minMz = peak.Mz - mzTolerance;
      double maxMz = peak.Mz + mzTolerance;

      foreach (var pkl in list)
      {
        if (pkl[pkl.Count - 1].Mz < minMz)
        {
          continue;
        }

        if (pkl[0].Mz > maxMz)
        {
          break;
        }

        PeakList<T> peaks = pkl.FindPeak(peak.Mz, mzTolerance);
        if (0 == peaks.Count)
        {
          continue;
        }

        if (0 != pkl[0].Charge && peak.Charge != pkl[0].Charge)
        {
          continue;
        }

        if (null == result)
        {
          result = pkl;
          continue;
        }

        if (result.FindMaxIntensityPeak().Intensity < pkl.FindMaxIntensityPeak().Intensity)
        {
          result = pkl;
          continue;
        }
      }

      return result;
    }

    public void SetFragmentIonCharge(int charge)
    {
      foreach (T peak in this)
      {
        peak.Charge = charge;
      }
    }

    public void FilterByTolerance(double ppmTolerance)
    {
      Sort((m1, m2) => m1.Mz.CompareTo(m2.Mz));

      for (int start = 0; start < Count - 1; )
      {
        double mzTolerance = 2 * PrecursorUtils.ppm2mz(this[start].Mz, ppmTolerance);

        int next = start + 1;
        while (next < Count)
        {
          if (this[next].Mz - this[start].Mz > mzTolerance)
          {
            start++;
            break;
          }

          if (this[start].Intensity > this[next].Intensity)
          {
            RemoveAt(next);
          }
          else
          {
            RemoveAt(start);
          }
        }
      }
    }

    public PeakList<T> GetRange(double minMz, double maxMz)
    {
      if (minMz > maxMz)
      {
        throw new ArgumentException(MyConvert.Format("minMz {0} should less than maxMz {1}", minMz, maxMz));
      }

      var result = new PeakList<T>();
      foreach (T peak in this)
      {
        if (peak.Mz < minMz)
        {
          continue;
        }
        if (peak.Mz > maxMz)
        {
          break;
        }
        result.Add(peak);
      }

      return result;
    }

    private void MergePeak(T p, List<T> other, int iOtherTopIndex, double ppmTolerance)
    {
      double mzTolerance = PrecursorUtils.ppm2mz(p.Mz, ppmTolerance);

      //consider merge with peak having large intensity first.
      int i = iOtherTopIndex;
      while (i < other.Count)
      {
        T o = other[i];
        double gap = Math.Abs(p.Mz - o.Mz);
        if (gap <= mzTolerance)
        {
          if (p.Charge == o.Charge || 0 == p.Charge || 0 == o.Charge)
          {
            double mz = (p.Mz * p.Intensity + o.Mz * o.Intensity) / (p.Intensity + o.Intensity);

            p.Mz = mz;
            p.Intensity = p.Intensity + o.Intensity;
            p.Charge = Math.Max(p.Charge, o.Charge);
            other.RemoveAt(i);
            continue;
          }
        }
        i++;
      }
    }


    public void MergeByMZFirst(PeakList<T> other, double ppmTolerance)
    {
      if (0 == other.Count)
      {
        return;
      }

      if (0 == Count)
      {
        AddRange(other);
        return;
      }

      Sort(new PeakMzAscendingComparer<T>());
      other.Sort(new PeakMzAscendingComparer<T>());

      var result = new List<T>();
      int iThisIndex = 0;
      int iOtherIndex = 0;
      while (iThisIndex < Count && iOtherIndex < other.Count)
      {
        double mzThisTolerance = PrecursorUtils.ppm2mz(this[0].Mz, ppmTolerance);
        double gap = this[iThisIndex].Mz - other[iOtherIndex].Mz;
        if (Math.Abs(gap) < mzThisTolerance)
        {
          if (this[iThisIndex].Charge == other[iOtherIndex].Charge ||
              0 == this[iThisIndex].Charge ||
              0 == other[iOtherIndex].Charge)
          {
            double mz = (this[iThisIndex].Mz * this[iThisIndex].Intensity +
                         other[iOtherIndex].Mz * other[iOtherIndex].Intensity)
                        / (this[iThisIndex].Intensity + other[iOtherIndex].Intensity);

            this[iThisIndex].Mz = mz;
            this[iThisIndex].Intensity = this[iThisIndex].Intensity + other[iOtherIndex].Intensity;
            this[iThisIndex].Charge = Math.Max(this[iThisIndex].Charge, other[iOtherIndex].Charge);
            iOtherIndex++;
            continue;
          }
        }

        if (gap < 0)
        {
          result.Add(this[iThisIndex]);
          iThisIndex++;
        }
        else
        {
          result.Add(other[iOtherIndex]);
          iOtherIndex++;
        }
      }

      while (iThisIndex < Count)
      {
        result.Add(this[iThisIndex]);
        iThisIndex++;
      }

      while (iOtherIndex < other.Count)
      {
        result.Add(other[iOtherIndex]);
        iOtherIndex++;
      }

      base.Clear();
      base.AddRange(result);
      this.scanTimes.AddRange(other.scanTimes);
      this.PrecursorIntensity += other.PrecursorIntensity;
    }

    /**
     * For each peak in this peaklist, only the max intensity peak within mzTolerance in argument peakList
     * will be merged. And, the mz, charge will not be changed, intensity will be added. 
     */

    public void AddToCurrentPeakListIntensity(PeakList<T> peakList, double mzTolerance)
    {
      foreach (T peak in this)
      {
        PeakList<T> find = peakList.FindPeak(peak.Mz, mzTolerance);
        if (find.Count > 0)
        {
          T maxIntensity = find.FindMaxIntensityPeak();
          peak.Intensity = peak.Intensity + maxIntensity.Intensity;
        }
      }
    }

    public void SetIntensity(double p)
    {
      this.ForEach(peak => peak.Intensity = p);
    }

    public double GetTotalIntensity()
    {
      return (from p in this
              select p.Intensity).Sum();
    }

    public void SortByMz()
    {
      Sort(new PeakMzAscendingComparer<T>());
    }

    public void SortByIntensity()
    {
      Sort(new PeakIntensityDescendingComparer<T>());
    }

    public void KeepTop(int count)
    {
      SortByIntensity();
      if (this.Count > count)
      {
        this.RemoveRange(count, this.Count - count);
      }
      SortByMz();
    }

    public void KeepTopXInWindow(int topX, double window)
    {
      var index = TopXIndices(topX, window);
      var kept = new HashSet<T>(from ind in index select this[ind]);

      this.RemoveAll(m => !kept.Contains(m));
    }

    /// <summary>
    /// Copy from MaxQuant, Spectrum.cs
    /// </summary>
    /// <param name="topx"></param>
    /// <param name="window"></param>
    /// <returns></returns>
    private List<int> TopXIndices(int topx, double window)
    {
      int[] top = new int[topx];
      for (int n = 0; n < topx; n++)
      {
        top[n] = -1;
      }
      List<int> result = new List<int>();
      int leftSide = 0;
      for (int i = 0; i < Count; i++)
      {
        if (this[i].Mz - this[leftSide].Mz <= window)
        {
          for (int j = 0; j < topx; j++)
          {
            if (top[j] == -1 || this[i].Intensity >= this[top[j]].Intensity)
            {
              for (int k = topx - 1; k > j; k--)
              {
                top[k] = top[k - 1];
              }
              top[j] = i;
              break;
            }
          }
        }
        else
        {
          Array.Sort(top);
          for (int id = 0; id < topx; id++)
          {
            if (top[id] >= 0)
            {
              result.Add(top[id]);
            }
          }
          for (int n = 0; n < topx; n++)
          {
            top[n] = -1;
          }
          leftSide = i;
          i--;
        }
        if (i + 1 == Count)
        {
          Array.Sort(top);
          for (int id = 0; id < topx; id++)
          {
            if (top[id] >= 0)
            {
              result.Add(top[id]);
            }
          }
          for (int n = 0; n < topx; n++)
          {
            top[n] = -1;
          }
          leftSide = i;
        }
      }

      return result;
    }
  }

  public class PeakListMzAscendingComparer<T> : IComparer<PeakList<T>> where T : IPeak
  {
    #region IComparer<PeakList<T>> Members

    public int Compare(PeakList<T> x, PeakList<T> y)
    {
      if (0 == x.Count)
      {
        return -1;
      }

      if (0 == y.Count)
      {
        return 1;
      }

      if (x[0].Mz < y[0].Mz)
      {
        return -1;
      }

      if (x[0].Mz > y[0].Mz)
      {
        return 1;
      }

      return 0;
    }

    #endregion
  }
}