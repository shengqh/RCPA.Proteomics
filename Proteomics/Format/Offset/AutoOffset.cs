using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Statistic;
using RCPA.Utils;

namespace RCPA.Proteomics.Format.Offset
{
  public class AutoOffset : ProgressClass, IOffset
  {
    private string fileName;

    private List<OffsetEntry> _offsets;

    private double[] siliconePolymers;

    private double maxShiftPPM;

    private double rtWindow;

    private bool bShiftPrecursor;

    private bool bShiftProductIon;

    public AutoOffset(double[] siliconePolymers, double maxShiftPPM, double rtWindow, IProgressCallback callBack, string fileName, bool bShiftPrecursor, bool bShiftProductIon)
    {
      this.siliconePolymers = siliconePolymers;
      this.maxShiftPPM = maxShiftPPM;
      this.rtWindow = rtWindow;
      this.fileName = fileName;
      this.Progress = callBack;
      this.bShiftPrecursor = bShiftPrecursor;
      this.bShiftProductIon = bShiftProductIon;

      InitOffsets();
    }

    private void InitOffsets()
    {
      var calc = new MassOffsetCalculator(this.siliconePolymers, this.maxShiftPPM, this.rtWindow);
      calc.Progress = this.Progress;
      _offsets = calc.GetOffsets(fileName);
    }

    #region IOffset Members

    public double GetPrecursorOffset(string fileName, int scan)
    {
      if (bShiftPrecursor)
      {
        return GetFileScanOffset(fileName, scan);
      }
      else
      {
        return 0.0;
      }
    }

    public double GetProductIonOffset(string fileName, int scan)
    {
      if (bShiftProductIon)
      {
        return GetFileScanOffset(fileName, scan);
      }
      else
      {
        return 0.0;
      }
    }

    #endregion

    private double GetFileScanOffset(string fileName, int scan)
    {
      for (int i = _offsets.Count - 1; i >= 0; i--)
      {
        if (_offsets[i].Scan < scan)
        {
          return _offsets[i].InitValue.MedianInWindow;
        }
      }

      return 0.0;
    }

    public override string ToString()
    {
      return string.Format("SiliconePolymers={0};MaxShiftPPM={1:0.0000}ppm;RetentionTimeWindow={2:0.00}min",
        (from poly in this.siliconePolymers
         select string.Format("{0:0.000000}", poly)).Merge("/"), this.maxShiftPPM, this.rtWindow);
    }
  }
}
