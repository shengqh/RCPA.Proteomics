using RCPA.Proteomics.Sequest;
using RCPA.Proteomics.Summary;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Analysis
{
  public class ProteinClassificationFilter
  {
    private readonly Color plotColor;
    private readonly Regex regex;
    private readonly string title;

    public ProteinClassificationFilter(string title, string pattern, Color plotColor)
    {
      this.title = title;
      this.regex = new Regex(pattern);
      this.plotColor = plotColor;
    }

    public string Title
    {
      get { return this.title; }
    }

    public Color PlotColor
    {
      get { return this.plotColor; }
    }

    public bool Accept(IIdentifiedSpectrum sph)
    {
      foreach (string protein in sph.Peptides[0].Proteins)
      {
        if (this.regex.Match(protein).Success)
        {
          return true;
        }
      }
      return false;
    }
  }

  public class ScoreDeltaScorePlotProcessor : IFileProcessor
  {
    private readonly List<ProteinClassificationFilter> pcfs;
    private int height = 600;
    private int left = 40;
    private int top = 40;
    private int width = 800;

    public ScoreDeltaScorePlotProcessor(List<ProteinClassificationFilter> pcfs)
    {
      this.pcfs = pcfs;
    }

    #region IFileProcessor Members

    public IEnumerable<string> Process(string filename)
    {
      List<IIdentifiedSpectrum> sphs = new SequestPeptideTextFormat().ReadFromFile(filename);
      Dictionary<int, List<IIdentifiedSpectrum>> chargeSphMap = IdentifiedSpectrumUtils.GetChargeMap(sphs);
      var result = new List<string>();
      foreach (int charge in chargeSphMap.Keys)
      {
        List<IIdentifiedSpectrum> sphList = chargeSphMap[charge];

        double maxDeltaScore = 1.0;

        double maxScore = 0.0;
        foreach (IIdentifiedSpectrum sph in sphList)
        {
          if (sph.Score > maxScore)
          {
            maxScore = sph.Score;
          }
        }
        maxScore += 1.0;

        string resultFilename = filename + "." + charge + ".png";
        var bmp = new Bitmap(this.width, this.height);
        Graphics g = Graphics.FromImage(bmp);
        g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, this.width, this.height));
        var font = new Font("Times New Roman", 8);
        double fontHeight = font.GetHeight(g);

        g.DrawString("Score", font, new SolidBrush(Color.Black), GetX(maxScore, maxScore) - 20,
                     GetY(maxDeltaScore, 0) + 10);
        g.DrawString("DeltaScore", font, new SolidBrush(Color.Black), GetX(maxScore, 0) - 20,
                     GetY(maxDeltaScore, maxDeltaScore) - (int)fontHeight - 5);

        DrawXScale(maxScore, maxDeltaScore, g, font);
        DrawYScale(maxScore, maxDeltaScore, g, font);

        var colors = new HashSet<Color>();
        foreach (IIdentifiedSpectrum sph in sphList)
        {
          Color color = GetColor(sph);
          if (!colors.Contains(color))
          {
            colors.Add(color);
          }

          int x = GetX(maxScore, sph.Score);
          var y = (int)(this.height - sph.DeltaScore / maxDeltaScore * this.height - this.top);
          g.FillEllipse(new SolidBrush(color), new Rectangle(x - 1, y - 1, 3, 3));
          //Console.WriteLine(MyConvert.Format("{0:0.0000}\t{1:0.0000}\t{2}\t{3}\t{4}\t{5}",
          //  sph.Score,
          //  sph.DeltaScore,
          //  color.Name,
          //  x,
          //  y,
          //  sph.PeptideInfo[0].Proteins[0]));
        }

        DrawColorTitle(maxScore, maxDeltaScore, g, font, colors);

        g.Save();
        bmp.Save(resultFilename, ImageFormat.Png);
        result.Add(resultFilename);
      }
      return result;
    }

    #endregion

    private void DrawXScale(double maxScore, double maxDeltaScore, Graphics g, Font font)
    {
      int bottomY = GetY(maxDeltaScore, 0.0);
      g.DrawLine(new Pen(Color.Black), GetX(maxScore, 0), bottomY, GetX(maxScore, maxScore), bottomY);
      for (double Score = 0.0; Score < maxScore - 0.5; Score += 0.2)
      {
        int x = GetX(maxScore, Score);
        g.DrawLine(new Pen(Color.Black), x, bottomY, x, bottomY + 3);
        g.DrawString(MyConvert.Format("{0:0.0}", Score), font, new SolidBrush(Color.Black), x - 8, bottomY + 10);
      }
    }

    private void DrawYScale(double maxScore, double maxDeltaScore, Graphics g, Font font)
    {
      int leftX = GetX(maxScore, 0.0);
      double fontHeight = font.GetHeight(g);

      g.DrawLine(new Pen(Color.Black), leftX, GetY(maxDeltaScore, 0.0), leftX, GetY(maxDeltaScore, maxDeltaScore));
      for (double DeltaScore = 0.0; DeltaScore < maxDeltaScore; DeltaScore += 0.1)
      {
        int y = GetY(maxDeltaScore, DeltaScore);
        g.DrawLine(new Pen(Color.Black), leftX, y, leftX - 3, y);
        g.DrawString(MyConvert.Format("{0:0.0}", DeltaScore), font, new SolidBrush(Color.Black), leftX - 20,
                     y - (int)(fontHeight / 2));
      }
    }

    private void DrawColorTitle(double maxScore, double maxDeltaScore, Graphics g, Font font, HashSet<Color> colors)
    {
      int leftX = this.left + (this.width - 2 * this.left) * 4 / 5;
      int topY = this.top + (this.height - 2 * this.top) * 4 / 5;
      double fontHeight = font.GetHeight(g);

      int count = 0;
      foreach (ProteinClassificationFilter pcf in this.pcfs)
      {
        if (colors.Contains(pcf.PlotColor))
        {
          int nextY = topY + (int)(count * fontHeight * 2);
          g.FillRectangle(new SolidBrush(pcf.PlotColor), leftX, nextY, 15, (int)(fontHeight));
          g.DrawString(pcf.Title, font, new SolidBrush(Color.Black), leftX + 20, nextY);
          count++;
        }
      }
    }

    private int GetX(double maxScore, double Score)
    {
      return (int)(this.left + Score / maxScore * (this.width - 2 * this.left));
    }

    private int GetY(double maxDeltaScore, double DeltaScore)
    {
      return (int)(this.height - this.top - DeltaScore / maxDeltaScore * (this.height - 2 * this.top));
    }

    private Color GetColor(IIdentifiedSpectrum sph)
    {
      foreach (ProteinClassificationFilter pcf in this.pcfs)
      {
        if (pcf.Accept(sph))
        {
          return pcf.PlotColor;
        }
      }

      return Color.Black;
    }
  }
}