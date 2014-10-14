using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RCPA.Gui;
using RCPA.Gui.FileArgument;
using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using RCPA.Utils;
using RCPA.Proteomics.Modification;
using RCPA.Gui.Command;
using RCPA.Proteomics.Mascot;

namespace RCPA.Proteomics.Analysis
{
  public partial class ScoreDistributionBuilderUI : AbstractUI
  {
    private static readonly string title = "Score Distribution Builder";
    private static readonly string version = "1.0.0";

    private class FdrResult
    {
      public FdrResult()
        : this(0, 0, 0)
      { }

      public FdrResult(double score, uint targetCount, uint semiCount)
      {
        this.Score = score;
        this.TargetCount = targetCount;
        this.SemiCount = semiCount;
      }

      public double Score { get; set; }

      public uint TargetCount { get; set; }

      public uint SemiCount { get; set; }

      public uint FullCount { get { return TargetCount - SemiCount; } }

      public void AddCount(FdrResult fr)
      {
        this.TargetCount += fr.TargetCount;
        this.SemiCount += fr.SemiCount;
      }
    }

    private RcpaFileField peptidesFile;
    private RcpaComboBox<SearchEngineType> searchEngine;

    private RcpaCheckBox fixCharge;
    private RcpaIntegerField fixChargeValue;

    private RcpaCheckBox fixMissCleavage;
    private RcpaIntegerField fixMissCleavageValue;

    private RcpaCheckBox fixNumProteaseTermini;
    private RcpaIntegerField fixNumProteaseTerminiValue;

    private RcpaCheckBox fixModification;
    private RcpaComboBox<bool> fixModificationValue;

    private RcpaCheckBox fixTag;
    private RcpaComboBox<string> fixTagValue;

    private RcpaCheckBox reassignModification;
    private RcpaTextField modificationAminoacids;

    private string lastFilename = null;
    private long lastFilesize = 0;

    public enum ClassificationType { Charge, NumMissCleavage, Modification, NumProteaseTermini, Category, None };

    private RcpaComboBox<ClassificationType> classification;

    Func<IIdentifiedSpectrum, object> getCharge = m => Math.Min(3, m.Charge);
    Func<IIdentifiedSpectrum, object> getNumMissCleavage = m => m.NumMissedCleavages;

    private RcpaComboBox<FalseDiscoveryRateType> fdrType;

    public ScoreDistributionBuilderUI()
    {
      InitializeComponent();
      peptidesFile = new RcpaFileField(btnFile, textBox1, "PeptideFile", new OpenFileArgument("Peptides", new string[] { "txt", "peptides" }), true);
      AddComponent(peptidesFile);

      searchEngine = new RcpaComboBox<SearchEngineType>(cbEngine, "SearchEngine", EnumUtils.EnumToArray<SearchEngineType>(), 0);
      AddComponent(searchEngine);

      fixCharge = new RcpaCheckBox(cbCharge, "FixCharge", false);
      AddComponent(fixCharge);

      fixChargeValue = new RcpaIntegerField(txtCharge, "Charge", "Charge", 2, false);
      AddComponent(fixChargeValue);

      fixMissCleavage = new RcpaCheckBox(cbNumMissCleavage, "fixMissCleavage", false);
      AddComponent(fixMissCleavage);

      fixMissCleavageValue = new RcpaIntegerField(txtMissCleavage, "fixMissCleavageValue", "Number of Miss Cleavage", 0, false);
      AddComponent(fixMissCleavageValue);

      fixNumProteaseTermini = new RcpaCheckBox(cbNumProteaseTermini, "fixNumProteaseTermini", false);
      AddComponent(fixNumProteaseTermini);

      fixNumProteaseTerminiValue = new RcpaIntegerField(txtNumProteaseTermini, "fixNumProteaseTerminiValue", "Number of Protease Termini", 2, false);
      AddComponent(fixNumProteaseTerminiValue);

      fixModification = new RcpaCheckBox(cbModification, "fixModification", false);
      AddComponent(fixModification);

      fixModificationValue = new RcpaComboBox<bool>(cbModificationValue, "fixModificationValue", new bool[] { true, false }, 0);
      AddComponent(fixModificationValue);

      fixTag = new RcpaCheckBox(cbTag, "fixTag", false);
      AddComponent(fixTag);

      fixTagValue = new RcpaComboBox<string>(cbTagValue, "fixTagValue", new string[] { "ALL" }, 0);
      AddComponent(fixTagValue);

      classification = new RcpaComboBox<ClassificationType>(cbBasedOn, "BasedOn", EnumUtils.EnumToArray<ClassificationType>(), 0);
      AddComponent(classification);

      reassignModification = new RcpaCheckBox(cbReassignModification, "ReassignModification", true);
      AddComponent(reassignModification);

      modificationAminoacids = new RcpaTextField(txtModification, "ModAminoacids", "Modification amino acids", "STY", false);
      AddComponent(modificationAminoacids);

      this.fdrType = new RcpaComboBox<FalseDiscoveryRateType>(this.cbFdrType, "FdrType",
                                                              new[]
                                                                {
                                                                  FalseDiscoveryRateType.Target,
                                                                  FalseDiscoveryRateType.Total
                                                                },
                                                              new[]
                                                                {
                                                                  "Target : N(decoy) / N(target)",
                                                                  "Global : N(decoy) * 2 / (N(decoy) + N(target))"
                                                                }, 0);
      AddComponent(this.fdrType);

      InsertButton(2, btnBatch);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override void DoBeforeValidate()
    {
      fixChargeValue.Required = fixCharge.Checked;
      fixMissCleavageValue.Required = fixMissCleavage.Checked;
      fixNumProteaseTerminiValue.Required = fixNumProteaseTermini.Checked;
      modificationAminoacids.Required = reassignModification.Checked;
    }

    private bool hasData = true;

    private List<IIdentifiedSpectrum> spectra;

    private IFalseDiscoveryRateCalculator FdrCalc
    {
      get
      {
        return fdrType.SelectedItem.GetCalculator();
      }
    }

    protected override void DoRealGo()
    {
      hasData = false;

      if (lastFilename != peptidesFile.FullName || lastFilesize != new FileInfo(peptidesFile.FullName).Length)
      {
        var format = new SequestPeptideTextFormat();
        this.spectra = format.ReadFromFile(peptidesFile.FullName);
        var tags = (from s in spectra
                    select s.Tag).Distinct().ToList();
        tags.Sort();
        if (tags.Count > 1)
        {
          fixTagValue.ResetItems(tags.ToArray());
          cbTag.Enabled = true;
          cbTagValue.Enabled = true;
        }
        else
        {
          fixTagValue.ResetItems(new string[] { "All" });
          cbTag.Enabled = false;
          cbTagValue.Enabled = false;
        }

        this.lastFilename = peptidesFile.FullName;
        this.lastFilesize = new FileInfo(peptidesFile.FullName).Length;
      }

      if (!spectra[0].Annotations.ContainsKey("Modified") || reassignModification.Checked)
      {
        IModificationCountCalculator calc = new ModificationCountCalculator(modificationAminoacids.Text);
        spectra.ForEach(m => m.Annotations["Modified"] = (calc.Calculate(m.Sequence) > 0).ToString());
      }

      if (!spectra.Any(m => m.FromDecoy))
      {
        DecoyPeptideBuilder.AssignDecoy<IIdentifiedSpectrum>(spectra, "^REV|REV_", true);
      }

      var engineType = searchEngine.SelectedItem;

      AndSpectrumFilter filters = GetSpectrumFilter();

      var filteredSpectra = spectra.Where(m => filters.Accept(m)).ToList();

      chartScore.Series.Clear();
      chartScore.ChartAreas.Clear();
      chartScore.Legends.Clear();
      chartScore.Titles.Clear();

      if (filteredSpectra.Count == 0)
      {
        chartScore.Tag = new FdrResult();
        return;
      }

      var titletext = FileUtils.RemoveExtension(new FileInfo(peptidesFile.FullName).Name.ToUpper());

      var filterCondition = filters.ToString();
      if (filterCondition.Length > 0)
      {
        titletext = titletext + " : " + filterCondition;
      }

      var title = chartScore.Titles.Add(titletext);

      object[] values = null;
      ISpectrumFilter filter = null;

      FdrResult fdrResult = new FdrResult();
      switch (classification.SelectedItem)
      {
        case ClassificationType.Charge:
          {
            if (fixCharge.Checked)
            {
              values = new object[] { fixChargeValue.Value };
            }
            else
            {
              values = new object[] { 1, 2, 3 };
            }
            filter = new ChargeFilter(1);

            break;
          }
        case ClassificationType.NumMissCleavage:
          {
            if (fixMissCleavage.Checked)
            {
              values = new object[] { fixMissCleavageValue.Value };
            }
            else
            {
              values = new object[] { 0, 1, 2 };
            }
            filter = new NumOfMissCleavageFilter(0);

            break;
          }
        case ClassificationType.Modification:
          {
            if (fixModification.Checked)
            {
              values = new object[] { fixModificationValue.SelectedItem };
            }
            else
            {
              values = new object[] { true, false };
            }
            filter = new IdentifiedSpectrumModificationFilter(true);

            break;
          }
        case ClassificationType.NumProteaseTermini:
          {
            if (fixNumProteaseTermini.Checked)
            {
              values = new object[] { fixNumProteaseTerminiValue.Value };
            }
            else
            {
              values = new object[] { 1, 2 };
            }

            filter = new NumOfProteaseTerminiFilter(2);

            break;
          }
        case ClassificationType.Category:
          {
            if (fixTag.Checked && cbTag.Enabled)
            {
              values = new object[] { fixTagValue.SelectedItem };
            }
            else
            {
              values = (from item in fixTagValue.Items
                        select (object)item).ToArray();
            }
            filter = new TagFilter("All", true);

            break;
          }
        case ClassificationType.None:
          {
            var name = "None";

            fdrResult = DrawChart(engineType, name, filteredSpectra);
            break;
          }
      }

      if (values != null)
      {
        fdrResult = DrawPicture(engineType, filteredSpectra, values, filter);
      }

      title.Text = title.Text + "; T=" + fdrResult.TargetCount.ToString();
      chartScore.Tag = fdrResult;
      hasData = true;
    }

    private FdrResult DrawPicture(SearchEngineType engineType, List<IIdentifiedSpectrum> filteredSpectra, object[] values, ISpectrumFilter filter)
    {
      ElementPosition position = null;
      if (values.Length > 1)
      {
        var count = DrawChart(engineType, "None", filteredSpectra);
        var area1 = chartScore.ChartAreas[0];
        area1.Position = new ElementPosition(0, 10, 50, 45);

        var fdrSpectra = (from s in filteredSpectra
                          where s.Score >= count.Score
                          select s).ToList();

        Dictionary<string, Dictionary<bool, int>> group = new Dictionary<string, Dictionary<bool, int>>();
        foreach (var value in values)
        {
          filter.SetCriteria(value);
          var dic = new Dictionary<bool, int>();
          dic[true] = 0;
          dic[false] = 0;
          group[filter.ToString()] = dic;

          foreach (var s in fdrSpectra)
          {
            if (filter.Accept(s))
            {
              dic[s.FromDecoy] = dic[s.FromDecoy] + 1;
            }
          }
        }

        DrawPie("", group);

        position = new ElementPosition(50, 10, 50, 90 / values.Length);
      }

      FdrResult result = new FdrResult();
      ChartArea first = null;
      for (int i = 0; i < values.Length; i++)
      {
        filter.SetCriteria(values[i]);
        var curSpectra = (from s in filteredSpectra
                          where filter.Accept(s)
                          select s).ToList();

        ElementPosition curPosition = null;
        if (position != null)
        {
          curPosition = new ElementPosition(position.X, position.Y + i * position.Height, position.Width, position.Height);
        }

        result.AddCount(DrawChart(engineType, filter.ToString(), curSpectra, first, curPosition));

        if (first == null)
        {
          first = chartScore.ChartAreas.Last();
        }
      }
      return result;
    }

    private AndSpectrumFilter GetSpectrumFilter()
    {
      AndSpectrumFilter filters = new AndSpectrumFilter();

      if (fixCharge.Checked)
      {
        filters.AddFilter(new ChargeFilter(fixChargeValue.Value));
      }

      if (fixMissCleavage.Checked)
      {
        filters.AddFilter(new NumOfMissCleavageFilter(fixMissCleavageValue.Value));
      }

      if (fixModification.Checked)
      {
        filters.AddFilter(new IdentifiedSpectrumModificationFilter(fixModificationValue.SelectedItem));
      }

      if (fixNumProteaseTermini.Checked)
      {
        filters.AddFilter(new NumOfProteaseTerminiFilter(fixNumProteaseTerminiValue.Value));
      }

      return filters;
    }

    private void DrawPie(string p, Dictionary<string, Dictionary<bool, int>> spectraGroup)
    {
      var position = new ElementPosition(0, 55, 50, 45);

      //创建外面区域
      ChartArea outerArea = new ChartArea("OUTER_AREA");
      outerArea.Position = position;
      outerArea.InnerPlotPosition = new ElementPosition(-5, 10, 80, 80);

      outerArea.BackColor = Color.Transparent;
      //增加区域
      chartScore.ChartAreas.Add(outerArea);

      var legend = new Legend("OUTER_AREA");
      legend.Position = new ElementPosition(30, 70, 20, 10);
      chartScore.Legends.Add(legend);

      //创建外部series
      Series outerSeries = new Series("OUTER_SERIES");
      outerSeries.ChartArea = "OUTER_AREA";
      outerSeries.ChartType = SeriesChartType.Pie;
      outerSeries["PieDrawingStyle"] = "SoftEdge";
      outerSeries["PieLabelStyle"] = "Inside";
      outerSeries.Palette = ChartColorPalette.Pastel;
      outerSeries.Legend = legend.Name;

      var totalCounts = (from s in spectraGroup.Values
                         from v in s.Values
                         select (int)v).Sum();
      //添加数据
      foreach (var atype in spectraGroup.Keys)
      {
        var counts = spectraGroup[atype];

        double fdr = FdrCalc.Calculate(counts[true], counts[false]);

        var c = (from a in counts.Values
                 select (int)a).Sum();

        var percentage = c * 100.0 / totalCounts;

        var dp = new DataPoint()
        {
          Label = string.Format("{0:0.00}%", percentage),
          LegendText = string.Format("{0},FDR={1:0.00}%", atype, fdr * 100),
          YValues = new double[] { counts[true] + counts[false] },
        };

        outerSeries.Points.Add(dp);
      }

      //将series 添加到 chart 中
      chartScore.Series.Add(outerSeries);
    }

    private FdrResult DrawChart(SearchEngineType engineType, string name, List<IIdentifiedSpectrum> curSpectra)
    {
      return DrawChart(engineType, name, curSpectra, null, null);
    }

    private class ScoreFunctions : AbstractScoreFunctions
    {
      public ScoreFunctions()
        : base("Score")
      { }

      public override double GetScoreBin(IIdentifiedSpectrum spectrum)
      {
        return Math.Floor(spectrum.Score * 5) / 5;
      }
    }

    private class LogScoreFunctions : AbstractScoreFunctions
    {
      public LogScoreFunctions()
        : base("log(Score)")
      { }

      public override double GetScoreBin(IIdentifiedSpectrum spectrum)
      {
        return Math.Floor(Math.Log(spectrum.Score) * 5) / 5;
      }
    }

    private FdrResult DrawChart(SearchEngineType engineType, string name, List<IIdentifiedSpectrum> curSpectra, ChartArea alignArea, ElementPosition position)
    {
      if (curSpectra.Count == 0)
      {
        return new FdrResult(double.MaxValue, 0, 0);
      }

      var area = chartScore.ChartAreas.Add(name);

      IScoreFunctions getScore;
      if (engineType == SearchEngineType.SEQUEST)
      {
        getScore = new ScoreFunctions();
      }
      else
      {
        getScore = new LogScoreFunctions();
      }

      area.AxisX.Title = getScore.ScoreName;
      area.AxisY.Title = "Spectrum Count";
      area.Axes[0].MajorGrid.Enabled = false;
      area.Axes[1].MajorGrid.Enabled = false;
      area.Axes[2].MajorGrid.Enabled = false;
      area.Axes[3].MajorGrid.Enabled = false;
      area.AxisX.Minimum = 0;
      area.AxisX.Maximum = 8;
      area.AxisX.Interval = 1;
      area.AxisY2.Minimum = 0.0;
      area.AxisY2.Maximum = 1.2;
      area.AxisY2.Interval = 0.2;
      area.AxisY2.Title = "FDR";
      area.AxisY2.TextOrientation = TextOrientation.Rotated270;

      if (alignArea != null)
      {
        area.AlignWithChartArea = alignArea.Name;
        area.AlignmentStyle = AreaAlignmentStyles.PlotPosition;
      }

      if (position != null)
      {
        area.Position = position;
        //area.InnerPlotPosition = new ElementPosition(15, 15, 70, 70);
      }

      var legend = new Legend(name);
      legend.IsDockedInsideChartArea = true;
      legend.DockedToChartArea = area.Name;
      legend.Docking = Docking.Right;
      legend.Title = name;
      chartScore.Legends.Add(legend);

      Dictionary<double, int> targetBin = AddChart(area, legend, curSpectra, getScore, m => !m.FromDecoy, "Target", Color.Red);
      Dictionary<double, int> decoyBin = AddChart(area, legend, curSpectra, getScore, m => m.FromDecoy, "Decoy", Color.Blue);
      return AddFdrChart(area, legend, curSpectra, getScore, "FDR", Color.Green, targetBin, decoyBin);
    }

    private Dictionary<double, int> AddChart(ChartArea area, Legend legend, List<IIdentifiedSpectrum> spectra, IScoreFunctions getScore, Func<IIdentifiedSpectrum, bool> filter, string name, Color color)
    {
      Dictionary<double, int> counts = BuildScoreBin(spectra, getScore, filter);
      if (counts.Count == 0)
      {
        return null;
      }

      var line = chartScore.Series.Add(area.Name + name);
      line.ChartArea = area.Name;
      line.ChartType = SeriesChartType.Spline;
      line.Color = color;
      line.Legend = legend.Name;
      line.LegendText = name;

      var keys = counts.Keys.ToList();
      keys.Sort();

      foreach (var key in keys)
      {
        if (!double.IsInfinity(key))
        {
          line.Points.AddXY(key, counts[key]);
        }
      }

      return counts;
    }

    public static Dictionary<double, int> BuildScoreBin(List<IIdentifiedSpectrum> spectra, IScoreFunctions getScore, Func<IIdentifiedSpectrum, bool> filter)
    {
      Dictionary<double, int> counts = new Dictionary<double, int>();

      var filtered = from s in spectra
                     where filter(s)
                     select s;

      foreach (var s in filtered)
      {
        double score = getScore.GetScoreBin(s);
        if (!counts.ContainsKey(score))
        {
          counts[score] = 1;
        }
        else
        {
          counts[score] = counts[score] + 1;
        }
      }
      return counts;
    }

    private FdrResult AddFdrChart(ChartArea area, Legend legend, List<IIdentifiedSpectrum> spectra, IScoreFunctions getScore, string name, Color color, Dictionary<double, int> targetBin, Dictionary<double, int> decoyBin)
    {
      FdrResult result = new FdrResult();

      if (decoyBin == null)
      {
        result.TargetCount = (uint)(targetBin == null ? 0 : targetBin.Sum(m => m.Value));
        return result;
      }

      var line = chartScore.Series.Add(area.Name + name);
      line.ChartArea = area.Name;
      line.ChartType = SeriesChartType.Spline;
      line.Color = color;
      line.Legend = legend.Name;
      line.LegendText = name;
      line.YAxisType = AxisType.Secondary;

      var keys = (from k in targetBin.Keys
                  select k).Union(from k in decoyBin.Keys
                                  select k).Distinct().OrderBy(k => k);

      foreach (var key in keys)
      {
        if (!double.IsInfinity(key))
        {
          var targetCount = (from e in targetBin
                             where e.Key >= key
                             select (int)e.Value).Sum();

          var decoyCount = (from e in decoyBin
                            where e.Key >= key
                            select (int)e.Value).Sum();

          var fdr = FdrCalc.Calculate(decoyCount, targetCount);

          line.Points.AddXY(key, fdr);
        }
      }

      IdentifiedSpectrumUtils.CalculateQValue(spectra, getScore, FdrCalc);
      for (int i = spectra.Count - 1; i >= 0; i--)
      {
        if (spectra[i].QValue <= 0.01)
        {
          uint targetCount = 0;
          uint npt1Count = 0;
          for (int j = i; j >= 0; j--)
          {
            if (!spectra[j].FromDecoy)
            {
              targetCount++;

              if (spectra[j].NumProteaseTermini == 1)
              {
                npt1Count++;
              }
            }
          }

          var score = getScore.GetScore(spectra[i]);
          var scoreBin = getScore.GetScoreBin(spectra[i]);
          result = new FdrResult(score, targetCount, npt1Count);

          var aname = area.Name + name + "_0.01";
          var aline = chartScore.Series.Add(aname);
          aline.ChartArea = area.Name;
          aline.ChartType = SeriesChartType.Spline;
          aline.Color = Color.Black;
          aline.Legend = legend.Name;
          aline.LegendText = string.Format("{0}_0.01(T={1})", name, result.TargetCount);
          aline.YAxisType = AxisType.Secondary;
          aline.Points.AddXY(scoreBin, 0);
          aline.Points.AddXY(scoreBin, 1.0);

          break;
        }
      }

      return result;
    }

    private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.lastFilename == null)
      {
        return;
      }

      var target = GetTargetFilename();

      saveFileDialog1.FileName = target;
      saveFileDialog1.InitialDirectory = new FileInfo(this.lastFilename).Directory.FullName;
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        chartScore.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Png);
      }
    }

    private string GetTargetFilename()
    {
      StringBuilder sb = new StringBuilder();
      if (fixCharge.Checked)
      {
        var charge = fixChargeValue.Value;
        sb.Append("_Charge=" + charge.ToString());
      }

      if (fixMissCleavage.Checked)
      {
        var miss = fixMissCleavageValue.Value;
        sb.Append("_NumMissCleavage=" + miss.ToString());
      }

      if (fixModification.Checked)
      {
        var modi = fixModificationValue.SelectedItem;
        sb.Append("_IsModified=" + modi.ToString());
      }

      var target = this.lastFilename + "_" + cbBasedOn.Text + sb.ToString() + ".png";
      return target;
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Statistic;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new ScoreDistributionBuilderUI().MyShow();
      }

      #endregion
    }

    private void btnBatch_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string[] files = openFileDialog1.FileNames;
        foreach (var file in files)
        {
          this.textBox1.Text = file;
          int[] charges = new int[] { 1, 2, 3 };
          int[] missList = new int[] { 0, 1, 2 };
          bool[] modList = new bool[] { true, false };

          using (StreamWriter sw = new StreamWriter(file + ".power"))
          {
            fixCharge.Checked = false;
            fixMissCleavage.Checked = false;
            fixModification.Checked = false;
            fixNumProteaseTermini.Checked = false;
            fixTag.Checked = false;

            //No classification
            classification.SelectedItem = ClassificationType.None;
            FdrResult noneCount = GoAndSave();

            string[] tags = fixTagValue.Items;

            bool hasSemiResult = this.spectra.Any(m => m.NumProteaseTermini == 1);

            //Classification by charge
            classification.SelectedItem = ClassificationType.Charge;
            FdrResult chargeCount = GoAndSave();

            FdrResult chargeNmcCount = new FdrResult();
            FdrResult chargeNmcModCount = new FdrResult();
            FdrResult chargeNmcModNpcCount = new FdrResult();
            FdrResult chargeNmcModTagCount = new FdrResult();
            //fixed charge 
            fixCharge.Checked = true;
            foreach (int charge in charges)
            {
              fixCharge.Checked = true;
              fixChargeValue.Value = charge;

              //fix charge only
              fixMissCleavage.Checked = false;
              fixModification.Checked = false;
              fixNumProteaseTermini.Checked = false;

              //Classification by number of miss cleavage
              classification.SelectedItem = ClassificationType.NumMissCleavage;
              chargeNmcCount.AddCount(GoAndSave());

              foreach (int nmc in missList)
              {
                fixMissCleavage.Checked = true;
                fixMissCleavageValue.Value = nmc;

                //fix charge and NMC
                fixModification.Checked = false;
                fixNumProteaseTermini.Checked = false;

                classification.SelectedItem = ClassificationType.Modification;
                chargeNmcModCount.AddCount(GoAndSave());

                if (tags.Length > 1)
                {
                  fixNumProteaseTermini.Checked = false;
                  foreach (var mod in modList)
                  {
                    fixModification.Checked = true;
                    fixModificationValue.SelectedItem = mod;

                    fixTag.Checked = false;

                    classification.SelectedItem = ClassificationType.Category;
                    chargeNmcModTagCount.AddCount(GoAndSave());
                  }
                }

                if (hasSemiResult)
                {
                  fixTag.Checked = false;
                  foreach (var mod in modList)
                  {
                    fixModification.Checked = true;
                    fixModificationValue.SelectedItem = mod;

                    //fix charge/NMC/mod
                    fixNumProteaseTermini.Checked = false;

                    classification.SelectedItem = ClassificationType.NumProteaseTermini;
                    chargeNmcModNpcCount.AddCount(GoAndSave());
                  }
                }
              }
            }

            if (hasSemiResult)
            {
              sw.WriteLine("\ttrypsin\tsemiTrypsin");
              sw.WriteLine("None\t{0}\t{1}", noneCount.FullCount, noneCount.SemiCount);
              sw.WriteLine("C\t{0}\t{1}", chargeCount.FullCount, chargeCount.SemiCount);
              sw.WriteLine("C+NMC\t{0}\t{1}", chargeNmcCount.FullCount, chargeNmcCount.SemiCount);
              sw.WriteLine("C+NMC+MOD\t{0}\t{1}", chargeNmcModCount.FullCount, chargeNmcModCount.SemiCount);
              sw.WriteLine("C+NMC+MOD+NPT\t{0}\t{1}", chargeNmcModNpcCount.FullCount, chargeNmcModNpcCount.SemiCount);

              if (tags.Length > 1)
              {
                sw.WriteLine("C+NMC+MOD+Tag\t{0}\t{1}", chargeNmcModTagCount.FullCount, chargeNmcModTagCount.SemiCount);
              }
            }
            else
            {
              sw.WriteLine("\ttrypsin");
              sw.WriteLine("None\t{0}", noneCount.TargetCount);
              sw.WriteLine("C\t{0}", chargeCount.TargetCount);
              sw.WriteLine("C+NMC\t{0}", chargeNmcCount.TargetCount);
              sw.WriteLine("C+NMC+MOD\t{0}", chargeNmcModCount.TargetCount);
              if (tags.Length > 1)
              {
                sw.WriteLine("C+NMC+MOD+Tag\t{0}", chargeNmcModTagCount.FullCount);
              }
            }
          }
        }

        MessageBox.Show("Finished");
      }
    }

    private FdrResult GoAndSave()
    {
      DoRealGo();

      if (hasData)
      {
        var target = GetTargetFilename();
        chartScore.SaveImage(target, ChartImageFormat.Png);
      }

      return (FdrResult)chartScore.Tag;
    }
  }
}
