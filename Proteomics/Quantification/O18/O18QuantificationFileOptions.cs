﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using System.IO;

namespace RCPA.Proteomics.Quantification.O18
{
  public class O18QuantificationFileOptions : AbstractO18QuantificationOption
  {
    [RcpaOptionAttribute("ProteinFile", RcpaOptionType.String)]
    public string ProteinFile { get; set; }

    [RcpaOptionAttribute("RawDirectory", RcpaOptionType.String)]
    public string RawDirectory { get; set; }

    [RcpaOptionAttribute("RawExtension", RcpaOptionType.String)]
    public string RawExtension { get; set; }

    [RcpaOptionAttribute("PPMTolerance", RcpaOptionType.Double)]
    public double PPMTolerance { get; set; }

    [RcpaOptionAttribute("PurityOfO18Water", RcpaOptionType.Double)]
    public double PurityOfO18Water { get; set; }

    [RcpaOptionAttribute("IsPostDigestionLabelling", RcpaOptionType.Boolean)]
    public bool IsPostDigestionLabelling { get; set; }

    [RcpaOptionAttribute("SoftwareVersion", RcpaOptionType.String)]
    public string SoftwareVersion { get; set; }

    [RcpaOptionAttribute("IsScanLimited", RcpaOptionType.Boolean)]
    public bool IsScanLimited { get; set; }

    [RcpaOptionAttribute("ScanPercentageStart", RcpaOptionType.Double)]
    public double ScanPercentageStart { get; set; }

    [RcpaOptionAttribute("ScanPercentageEnd", RcpaOptionType.Double)]
    public double ScanPercentageEnd { get; set; }

    public O18QuantificationFileOptions()
    {
      this.IsScanLimited = false;
      this.ScanPercentageStart = 0.0;
      this.ScanPercentageEnd = 100.0;
      this.RawExtension = ".raw";
    }

    public double GetScanPercentageStart()
    {
      if (IsScanLimited)
      {
        return this.ScanPercentageStart;
      }

      return 0.0;
    }

    public double GetScanPercentageEnd()
    {
      if (IsScanLimited)
      {
        return this.ScanPercentageEnd;
      }

      return 100.0;
    }

    public static O18QuantificationFileOptions Load(string fileName)
    {
      O18QuantificationFileOptions result = new O18QuantificationFileOptions();
      RcpaOptionAttributeUtils.LoadFromXml(result, fileName);
      return result;
    }

    public void Save(string fileName)
    {
      RcpaOptionAttributeUtils.SaveToXml(this, fileName);
    }

    public override IProteinRatioCalculator GetProteinRatioCalculator()
    {
      var result = base.GetProteinRatioCalculator();
      result.SummaryFileDirectory = Path.GetDirectoryName(this.ProteinFile);
      return result;
    }
  }
}
