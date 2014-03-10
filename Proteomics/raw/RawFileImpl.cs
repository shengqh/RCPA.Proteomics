using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using RCPA.Proteomics.Spectrum;
using System.Text.RegularExpressions;
using MSFileReaderLib;

namespace RCPA.Proteomics.Raw
{
  public delegate void GetValueByTrailer(int scan, string label, ref object value);

  public delegate void GetValueByScan<T>(int scan, ref T value);

  public delegate void GetValueByRef<T>(ref T value);

  public delegate void GetValueByTwoValue<I1, I2, T>(I1 i1, I2 i2, ref T value);

  public class RawFileImpl : AbstractRawFile
  {
    private readonly IXRawfile3 rawFile;

    private bool valid;

    private Regex msTypeReg = new Regex(@".*@(.+?)\d");

    private Regex MsTypReg { get { return msTypeReg; } }

    private HashSet<int> badDataScans = new HashSet<int>();

    private string BadDataScanFile
    {
      get
      {
        return FileName + ".baddatascan";
      }
    }

    public RawFileImpl()
    {
      try
      {
        this.rawFile = new MSFileReader_XRawfile() as IXRawfile3;
      }
      catch (Exception)
      {
        throw new Exception("Reading Thermo RAW files requires MSFileReader to be installed. It is available for download at: http://sjsupport.thermofinnigan.com/public/detail.asp?id=703");
      }
      this.valid = false;
      this.FileName = string.Empty;
    }

    public RawFileImpl(string rawFilename)
    {
      try
      {
        this.rawFile = new MSFileReader_XRawfile() as IXRawfile3;
      }
      catch (Exception)
      {
        throw new Exception("Reading Thermo RAW files requires MSFileReader to be installed. It is available for download at: http://sjsupport.thermofinnigan.com/public/detail.asp?id=703");
      }

      this.valid = false;
      Open(rawFilename);
    }

    public string GetFilterForScanNum(int scan)
    {
      return GetValue<string>(this.rawFile.GetFilterForScanNum, scan);
    }

    public double GetResolution(int scan)
    {
      return GetTrialerValue(scan, "FT Resolution:", 0.0);
    }

    public PeakList<Peak> GetLabelData(int scan)
    {
      var result = new PeakList<Peak>();
      if (IsBadDataScan(scan))
      {
        Console.WriteLine("scan {0} of {1} is defined as ignore.", scan, FileName);
        return result;
      }

      object varLabels = null;
      object varFlags = null;

      int curScan = scan;

      try
      {
        this.rawFile.GetLabelData(ref varLabels, ref varFlags, ref curScan);
      }
      catch
      {
        Console.WriteLine("reading from scan {0} of {1} error, skipped.", scan, FileName);
        WriteIgnoreScan(curScan);

        this.Open(this.FileName);
        return result;
        //throw new RawReadException(scan);
      }

      CheckErrorCode("GetLabelData " + scan + " Error");

      var labels = (double[,])varLabels;

      int dim = labels.GetLength(1);
      for (int inx = 0; inx < dim; inx++)
      {
        var charge = (int)labels[5, inx];
        double mz = labels[0, inx];
        double intensity = labels[1, inx];
        result.Add(new Peak(mz, intensity, charge));
      }

      return result;
    }

    private void WriteIgnoreScan(int curScan)
    {
      try
      {
        using (StreamWriter sw = new StreamWriter(BadDataScanFile, true))
        {
          sw.WriteLine(curScan);
        }
      }
      catch { }
    }

    public PeakList<Peak> GetLabelData(int scan, double minMz, double maxMz)
    {
      var result = new PeakList<Peak>();
      if (IsBadDataScan(scan))
      {
        return result;
      }

      object varLabels = null;
      object varFlags = null;

      int curScan = scan;

      try
      {
        this.rawFile.GetLabelData(ref varLabels, ref varFlags, ref curScan);
      }
      catch
      {
        Console.WriteLine("reading from scan {0} of {1} error, skipped.", scan, FileName);
        WriteIgnoreScan(curScan);
        return result;
      }

      CheckErrorCode("GetLabelData " + scan + " Error");

      var labels = (double[,])varLabels;

      int dim = labels.GetLength(1);
      for (int inx = 0; inx < dim; inx++)
      {
        double mz = labels[0, inx];
        if (mz < minMz)
        {
          continue;
        }

        if (mz > maxMz)
        {
          break;
        }

        var charge = (int)labels[5, inx];
        double intensity = labels[1, inx];

        result.Add(new Peak(mz, intensity, charge));
      }

      return result;
    }


    public PeakList<Peak> GetMassListFromScanNum(int scan, bool centroid, double minMz, double maxMz)
    {
      var result = new PeakList<Peak>();
      if (IsBadDataScan(scan))
      {
        return result;
      }

      object varMassList = null;
      object varFlags = null;
      int curScan = scan;
      int arraySize = 0;

      int centroidResult = centroid ? 1 : 0;
      if (IsCentroidScanForScanNum(scan))
      {
        centroidResult = 0;
      }
      double centroidWidth = 0.0;

      string massRange = MyConvert.Format("{0:0.00}-{1:0.00}", minMz, maxMz);

      try
      {
        this.rawFile.GetMassListRangeFromScanNum(
          ref curScan,
          null,
          0,
          0,
          0,
          centroidResult,
          ref centroidWidth,
          ref varMassList,
          ref varFlags,
          massRange,
          ref arraySize);
      }
      catch
      {
        Console.WriteLine("reading from scan {0} of {1} error, skipped.", scan, FileName);
        WriteIgnoreScan(curScan);
        return result;
      }

      CheckErrorCode("GetMassListRangeFromScanNum  of " + scan + " Error");

      var datas = (double[,])varMassList;
      for (int inx = 0; inx < arraySize; inx++)
      {
        result.Add(new Peak(datas[0, inx], datas[1, inx], 0));
      }

      return result;
    }

    public PeakList<Peak> GetMassListFromScanNum(int scan, bool centroid)
    {
      var result = new PeakList<Peak>();
      if (IsBadDataScan(scan))
      {
        return result;
      }

      object varMassList = null;
      object varFlags = null;
      int curScan = scan;
      int arraySize = 0;

      int centroidResult = centroid ? 1 : 0;
      if (IsCentroidScanForScanNum(scan))
      {
        centroidResult = 0;
      }
      double centroidWidth = 0.0;

      try
      {
        this.rawFile.GetMassListFromScanNum(
          ref curScan,
          null,
          0,
          0,
          0,
          centroidResult,
          ref centroidWidth,
          ref varMassList,
          ref varFlags,
          ref arraySize);
      }
      catch
      {
        Console.WriteLine("reading from scan {0} of {1} error, skipped.", scan, FileName);
        WriteIgnoreScan(curScan);
        return result;
      }

      CheckErrorCode("GetMassListFromScanNum  of " + scan.ToString() + " Error");

      if (varMassList == null)
      {
        return result;
      }

      var datas = (double[,])varMassList;
      for (int inx = 0; inx < arraySize; inx++)
      {
        result.Add(new Peak(datas[0, inx], datas[1, inx], 0));
      }

      return result;
    }

    public void GetPeakListInfo<T>(int scan, PeakList<T> spectrum) where T : Peak, new()
    {
      spectrum.ScanTimes.Clear();
      spectrum.ScanTimes.Add(new ScanTime(scan, ScanToRetentionTime(scan)));

      spectrum.MsLevel = GetMsLevel(scan);
      spectrum.PrecursorMZ = 0.0;
      spectrum.PrecursorCharge = 0;
      spectrum.PrecursorIntensity = 0.0;

      if (!spectrum.IsFullMs)
      {
        spectrum.Precursor = GetPrecursorPeakWithMasterScan(scan);
      }
    }

    public List<PeakList<Peak>> GetFullMS()
    {
      var result = new List<PeakList<Peak>>();

      int firstScan = GetFirstSpectrumNumber();
      int lastScan = GetLastSpectrumNumber();

      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        if (GetMsLevel(scan) == 1)
        {
          PeakList<Peak> fullMsPeaks = GetLabelData(scan);
          result.Add(fullMsPeaks);
        }
      }

      return result;
    }

    public List<int> GetFullMSScan()
    {
      var result = new List<int>();

      int firstScan = GetFirstSpectrumNumber();
      int lastScan = GetLastSpectrumNumber();
      for (int scan = firstScan; scan <= lastScan; scan++)
      {
        if (GetMsLevel(scan) == 1)
        {
          result.Add(scan);
        }
      }

      return result;
    }


    public string GetInstModel()
    {
      return GetValue<string>(this.rawFile.GetInstModel);
    }

    public string GetInstSoftwareVersion()
    {
      return GetValue<string>(this.rawFile.GetInstSoftwareVersion);
    }

    public void GetScanHeaderInfoForScanNum(int scan, ref int numPakets, ref double RT, ref double lowMass,
                                            ref double highMass, ref double TIC, ref double basePeakMass,
                                            ref double basePeakIntensity, ref int channel, ref int uniformTime,
                                            ref double frequency)
    {
      this.rawFile.GetScanHeaderInfoForScanNum(
        scan,
        ref numPakets,
        ref RT,
        ref lowMass,
        ref highMass,
        ref TIC,
        ref basePeakMass,
        ref basePeakIntensity,
        ref channel,
        ref uniformTime,
        ref frequency);

      if (IsError())
      {
        throw new Exception("GetScanHeaderInfoForScanNum Error!");
      }
    }

    #region IRawFile Members

    public override ScanTime GetScanTime(int scan)
    {
      var result = base.GetScanTime(scan);
      result.IonInjectionTime = GetIonInjectionTime(scan);
      return result;
    }

    public override bool IsScanValid(int scan)
    {
      return (scan >= GetFirstSpectrumNumber() && scan <= GetLastSpectrumNumber());
    }

    public override int GetFirstSpectrumNumber()
    {
      return GetValue<int>(this.rawFile.GetFirstSpectrumNumber);
    }

    public override int GetLastSpectrumNumber()
    {
      return GetValue<int>(this.rawFile.GetLastSpectrumNumber);
    }

    public override Peak GetPrecursorPeak(int scan)
    {
      Peak result = new Peak();

      result.Mz = GetPrecursorMzFromTrailerExtraValue(scan);
      result.Charge = GetPrecursorChargeFromTrailerExtraValue(scan);

      if (0.0 == result.Mz && 0 != result.Charge)
      {
        var mz = GetIsolationMass(scan);
        var parentScan = GetMasterScanPeakList(scan);
        if (parentScan == null)
        {
          result.Mz = mz;
        }
        else
        {
          var resolution = GetResolution(parentScan.FirstScan);
          var deltamass = mz / resolution;
          var peaks = parentScan.FindEnvelope(mz, result.Charge, deltamass, false);
          result.Mz = peaks[0].Mz;
        }
      }
      else
      {
        result.Mz = GetIsolationMass(scan);
      }

      return result;
    }

    public override bool IsProfileScanForScanNum(int scan)
    {
      int result = GetValue<int>(this.rawFile.IsProfileScanForScanNum, scan);

      return 0 != result;
    }

    public override bool IsCentroidScanForScanNum(int scan)
    {
      int result = 0;

      this.rawFile.IsCentroidScanForScanNum(scan, ref result);

      if (IsError())
      {
        throw new Exception("IsCentroidScanForScanNum Error!");
      }

      return 0 != result;
    }

    public override void Open(string szFileName)
    {
      Close();

      if (!File.Exists(szFileName))
      {
        throw new ArgumentException("File not exists : " + szFileName);
      }

      this.rawFile.Open(szFileName);

      CheckErrorCode("Cannot open file : " + szFileName);

      var lRet = GetValue<int>(this.rawFile.GetNumberOfControllers);

      if (0 == lRet)
      {
        this.rawFile.Close();
        throw new ArgumentException("Are you sure it's raw file ? There is no controller information in : " + szFileName);
      }

      // default to first controller here
      int nDetectorType = 0;
      int nControllerType = 0; // 0 == mass spec device
      int nContorllerNumber = 1; // first MS device
      this.rawFile.SetCurrentController(nControllerType, nContorllerNumber);

      if (IsError())
      {
        this.rawFile.GetControllerType(0, ref nDetectorType);

        //        CheckErrorCode("GetControllerType Error");

        if (0 == nDetectorType)
        {
          this.rawFile.Close();
          throw new ArgumentException("Are you sure it's raw file ? There is no detector information in : " + szFileName);
        }

        this.rawFile.SetCurrentController(nDetectorType, 1);

        if (IsError())
        {
          this.rawFile.Close();
          throw new ArgumentException("SetCurrentController Error : \n" + GetErrorMsg(GetErrorCode()));
        }
      }

      int count = GetNumSpectra();
      if (0 == count)
      {
        this.rawFile.Close();
        throw new ArgumentException("Are you sure it's raw file ? There is no spectrum in : " + szFileName);
      }

      this.valid = true;

      this.FileName = szFileName;

      if (File.Exists(BadDataScanFile))
      {
        try
        {
          this.badDataScans = new HashSet<int>(from line in File.ReadAllLines(BadDataScanFile)
                                              where line.Trim() != ""
                                              let scan = int.Parse(line.Trim())
                                              select scan);
        }
        catch
        {
          this.badDataScans = new HashSet<int>();
        }
      }

      return;
    }

    public override bool Close()
    {
      if (IsValid())
      {
        this.rawFile.Close();
      }
      this.valid = false;
      this.FileName = string.Empty;
      return true;
    }

    public override bool IsValid()
    {
      return this.valid;
    }

    public override int GetNumSpectra()
    {
      return GetValue<int>(this.rawFile.GetNumSpectra);
    }

    public override PeakList<Peak> GetPeakList(int scan)
    {
      PeakList<Peak> result = GetLabelData(scan);

      if (result.Count == 0)
      {
        result = GetMassListFromScanNum(scan, true);
      }

      if (result.ScanTimes.Count == 0)
      {
        result.ScanTimes.Add(new ScanTime(scan, ScanToRetentionTime(scan)));
      }
      return result;
    }

    public override PeakList<Peak> GetPeakList(int scan, double minMz, double maxMz)
    {
      PeakList<Peak> result = GetLabelData(scan, minMz, maxMz);

      if (result.Count == 0)
      {
        result = GetMassListFromScanNum(scan, true, minMz, maxMz);
      }

      return result;
    }

    public override int GetMsLevel(int scan)
    {
      var filter = new RawScanFilter();

      filter.Filter = GetFilterForScanNum(scan);

      return filter.MsLevel;
    }

    public override string GetScanMode(int scan)
    {
      string filter = GetFilterForScanNum(scan);
      Match m = MsTypReg.Match(filter);
      if (m.Success)
      {
        return m.Groups[1].Value;
      }
      else
      {
        return "";
      }
    }

    public override double ScanToRetentionTime(int scan)
    {
      return GetValue<double>(this.rawFile.RTFromScanNum, scan);
    }

    public override double GetIsolationWidth(int scan)
    {
      //var msOrder = GetMSOrderForScanNum(scan);
      //if (msOrder <= 0)
      //{
      //  return 0.0;
      //}

      //var masterScan = GetMasterScan(scan);
      //if (masterScan >= 0)
      //{
      //  var result = GetValue<int, int, double>(this.rawFile.GetIsolationWidthForScanNum, scan, msOrder);
      //  if (0.0 != result)
      //  {
      //    return result;
      //  }
      //}

      var msOrder = GetMsLevel(scan);
      var key = string.Format("MS{0} Isolation Width:", 1 == msOrder ? "" : msOrder.ToString());

      return GetTrialerValue(scan, key, 0.0);
    }

    public override double GetIsolationMass(int scan)
    {
      var rsf = new RawScanFilter();
      rsf.Filter = GetFilterForScanNum(scan);
      return rsf.PrecursorMZ;
    }

    public override PrecursorPeak GetPrecursorPeakWithMasterScan(int scan)
    {
      PrecursorPeak result = new PrecursorPeak();

      result.IsolationMass = GetIsolationMass(scan);

      result.MonoIsotopicMass = GetPrecursorMzFromTrailerExtraValue(scan);
      if (0.0 != result.MonoIsotopicMass)
      {
        result.Charge = GetPrecursorChargeFromTrailerExtraValue(scan);
      }
      else
      {
        result.MonoIsotopicMass = result.IsolationMass;
      }

      result.MasterScan = GetMasterScan(scan);
      result.IsolationWidth = GetIsolationWidth(scan);

      return result;
    }

    #endregion

    public IXRawfile3 AsIXRawfile()
    {
      return this.rawFile;
    }

    public T GetValue<I1, I2, T>(GetValueByTwoValue<I1, I2, T> refFunc, I1 i1, I2 i2)
    {
      T result = default(T);

      refFunc(i1, i2, ref result);

      if (IsError())
      {
        int returnCode = GetErrorCode();
        throw new ArgumentException("Call " + refFunc.Method.Name + " error : \n" + GetErrorMsg(returnCode));
      }

      return result;
    }

    public object GetValue(GetValueByTrailer refFunc, int scan, string label)
    {
      object result = null;

      refFunc(scan, label, ref result);

      if (IsError())
      {
        int returnCode = GetErrorCode();
        throw new ArgumentException("Call " + refFunc.Method.Name + " error : \n" + GetErrorMsg(returnCode));
      }

      return result;
    }

    public double GetTrialerValue(int scan, string key, double defaultValue)
    {
      object varValue = null;

      this.rawFile.GetTrailerExtraValueForScanNum(scan, key, ref varValue);
      if (!IsError())
      {
        return MyConvert.ToDouble(MyConvert.Format("{0}", varValue));
      }
      else
      {
        return defaultValue;
      }
    }

    public int GetTrialerValue(int scan, string key, int defaultValue)
    {
      object varValue = null;

      this.rawFile.GetTrailerExtraValueForScanNum(scan, key, ref varValue);
      if (!IsError())
      {
        return int.Parse(varValue.ToString());
      }
      else
      {
        return defaultValue;
      }
    }

    public string GetTrialerValue(int scan, string key, string defaultValue = "")
    {
      object varValue = null;

      this.rawFile.GetTrailerExtraValueForScanNum(scan, key, ref varValue);
      if (!IsError())
      {
        return varValue.ToString();
      }
      else
      {
        return defaultValue;
      }
    }

    public T GetValue<T>(GetValueByScan<T> refFunc, int scan)
    {
      T result = default(T);

      refFunc(scan, ref result);

      if (IsError())
      {
        int returnCode = GetErrorCode();
        throw new ArgumentException("Call " + refFunc.Method.Name + " error for scan " + scan + ": \n" +
                                    GetErrorMsg(returnCode));
      }

      return result;
    }

    public T GetValue<T>(GetValueByRef<T> refFunc)
    {
      T result = default(T);

      refFunc(ref result);

      if (IsError())
      {
        int returnCode = GetErrorCode();
        throw new ArgumentException("Call " + refFunc.Method.Name + " error : \n" + GetErrorMsg(returnCode));
      }

      return result;
    }

    private string GetErrorMsg(int errorCode)
    {
      string result = "ErrorCode=" + errorCode + " : ";
      switch (errorCode)
      {
        case 2:
          return result +
                 "This code does not typically indicate an error. This code may be returned if optional data is not contained in the current raw file.";
        case 0:
          return result +
                 "This code indicates that a general error has occurred. This code may be returned whenever an error of indeterminate origin occurs.";
        case -1:
          return result + "This code will be returned if no valid raw file is currently open.";
        case -2:
          return result + "This code will be returned if no current controller has been specified.";
        case -3:
          return result +
                 "This code will be returned if the requested action is inappropriate for the currently defined controller. Some functions only apply to specific controllers. This code may also be returned if a parameter is passed in a call that is not supported by the current controller. For example, scan filters may only be passed in calls when the current controller is of mass spectrometer type (MS_DEVICE).";
        case -4:
          return result +
                 "This code will be returned if an invalid parameter is passed in a function call to the OCX. This can occur if a parameter is out of range or initialized incorrectly.";
        case -5:
          return result +
                 "This code will be returned if an incorrectly formatted scan filter is passed in a function call. See the topic scan filters ¨C format, definition for scan filter format specifications.";
        case -6:
          return result +
                 "This code will be returned if an incorrectly formatted mass range is passed in a function call. Mass ranges should be have the same format as entered in Xcalibur applications.";
        default:
          return result + "Unknown error";
      }
    }

    private int GetErrorCode()
    {
      int result = 1;

      this.rawFile.GetErrorCode(ref result);

      return result;
    }

    private bool IsError()
    {
      int isError = 0;

      this.rawFile.IsError(ref isError);

      return 0 != isError;
    }

    private void CheckErrorCode(String errorMsg)
    {
      if (IsError())
      {
        int returnCode = GetErrorCode();
        throw new ArgumentException(errorMsg + " : \n" + GetErrorMsg(returnCode));
      }
    }

    public double GetPrecursorMzFromTrailerExtraValue(int scan)
    {
      object varValue = null;

      this.rawFile.GetTrailerExtraValueForScanNum(scan, "Monoisotopic M/Z:", ref varValue);
      if (!IsError())
      {
        return MyConvert.ToDouble(MyConvert.Format("{0}", varValue));
      }
      else
      {
        return 0.0;
      }
    }

    public override double GetIonInjectionTime(int scan)
    {
      return GetTrialerValue(scan, "Ion Injection Time (ms):", 0.0);
    }

    public int GetPrecursorChargeFromTrailerExtraValue(int scan)
    {
      object varValue = null;
      this.rawFile.GetTrailerExtraValueForScanNum(scan, "Charge State:", ref varValue);
      if (!IsError())
      {
        return int.Parse(varValue.ToString());
      }
      return 0;
    }

    public override bool IsBadDataScan(int scan)
    {
      return this.badDataScans.Contains(scan);
    }

    //public int GetMSOrderForScanNum(int scan)
    //{
    //  return GetValue<int>(this.rawFile.GetMSOrderForScanNum, scan);
    //}
  }
}