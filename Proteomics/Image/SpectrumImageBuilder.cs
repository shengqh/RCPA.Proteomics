using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using RCPA.Gui.Image;
using RCPA.Proteomics.Spectrum;
using System.Drawing.Drawing2D;
using RCPA.Utils;

namespace RCPA.Proteomics.Image
{
  public class SpectrumPeakImageBuilder : ISpectrumImageBuilder
  {
    private static Font bigFont = new Font(FontFamily.GenericSansSerif, 9);
    private static Font smallFont = new Font(FontFamily.GenericSansSerif, 7);

    public Dictionary<IonType, Color> IonColorMap { get; set; }

    #region ISpectrumImageBuilder Members

    public void Draw(Graphics g2, Rectangle rec, IIdentifiedPeptideResult sr)
    {
      g2.CompositingQuality = CompositingQuality.HighQuality;
      g2.SmoothingMode = SmoothingMode.AntiAlias;
      g2.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

      Rectangle newRec = new Rectangle(rec.Left + 60, rec.Top, rec.Width - 80, rec.Height - 50 - bigFont.Height);

      Pen blackPen = new Pen(Color.Black, 1);

      Rectangle spectrumRec = GetSpectrumRectangle(g2, sr.ExperimentalPeakList, newRec);

      var maxMz =
        (from peak in sr.ExperimentalPeakList
         orderby peak.Mz descending
         select peak.Mz).FirstOrDefault();

      double maxMzWidth = ((int)maxMz / 100 + 1) * 100.0;

      int yFontShift = (int)(g2.MeasureString("M", bigFont).Height / 2);

      RectangleTransform rt = new RectangleTransform(spectrumRec, maxMzWidth, sr.ExperimentalPeakList.FindMaxIntensityPeak().Intensity);

      DrawXScale(g2, blackPen, spectrumRec, rt);

      DrawYScale(g2, blackPen, spectrumRec);

      foreach (MatchedPeak peak in sr.ExperimentalPeakList)
      {
        Brush brush;
        Pen pen;
        if (peak.Matched)
        {
          Color color = IonColorMap[peak.PeakType];
          brush = new SolidBrush(color);
          pen = new Pen (brush);
        }
        else
        {
          brush = Brushes.Black;
          pen = blackPen;
        }

        int x = rt.GetTransformX(peak.Mz);

        int y = rt.GetTransformY(peak.Intensity);

        g2.DrawLine(pen , new Point(x, spectrumRec.Bottom), new Point(x, y));

        if (peak.Matched)
        {
          DrawVerticalIonName(g2, brush, x - yFontShift, y, peak.Information);
        }
      }
    }

    private void DrawVerticalIonName(Graphics g2, Brush brush, int x, int y, string str)
    {
      g2.TranslateTransform(x, y);
      try
      {
        g2.RotateTransform(-90F);
        try
        {
          float curX = 0;
          bool bIonTypeNumber = true;
          for (int i = 0; i < str.Length; i++)
          {
            if (str[i] == ' ')
            {
              g2.DrawString(str.Substring(i), bigFont, brush, curX, 0);
              break;
            }

            string subStr = str.Substring(i, 1);
            if (char.IsDigit(str[i]))
            {
              if (bIonTypeNumber)
              {
                g2.DrawString(subStr, bigFont, brush, curX, 0);
                curX += GetCharWidth(g2, bigFont, str[i]);
              }
              else
              {
                g2.DrawString(subStr, smallFont, brush, curX, 6);
                curX += GetCharWidth(g2, smallFont, str[i]);
              }
            }
            else
            {
              if (i > 1)
              {
                bIonTypeNumber = false;
              }
              g2.DrawString(subStr, bigFont, brush, curX, 0);
              curX += GetCharWidth(g2, bigFont, str[i]);
            }
          }
        }
        finally
        {
          g2.RotateTransform(90F);
        }
      }
      finally
      {
        g2.TranslateTransform(-x, -y);
      }
    }


    private static void DrawXScale(Graphics g2, Pen blackPen, Rectangle spectrumRec, RectangleTransform rt)
    {
      int fontHeight = (int)g2.MeasureString("M", bigFont).Height;

      g2.DrawLine(blackPen, new Point(spectrumRec.Left, spectrumRec.Bottom), new Point(spectrumRec.Right, spectrumRec.Bottom));
      for (int i = 0; i < 10000; i += 50)
      {
        int curMz = rt.GetTransformX(i);
        if (curMz > spectrumRec.Right)
        {
          break;
        }

        if (i % 100 == 0)
        {
          g2.DrawLine(blackPen, curMz, spectrumRec.Bottom, curMz, spectrumRec.Bottom + 10);
          string number = i.ToString();
          float strWidth = GetStringWidth(g2, bigFont, number);
          g2.DrawString(number, bigFont, Brushes.Black, new PointF(curMz - strWidth / 2, spectrumRec.Bottom + 10));
        }
        else
        {
          g2.DrawLine(blackPen, curMz, spectrumRec.Bottom, curMz, spectrumRec.Bottom + 5);
        }
      }

      float mzWidth = GetStringWidth(g2, bigFont, "M/Z");
      g2.DrawString("M/Z", bigFont, Brushes.Black, new PointF((spectrumRec.Right + spectrumRec.Left - mzWidth) / 2, spectrumRec.Bottom + 10 + fontHeight));
    }

    private static void DrawYScale(Graphics g2, Pen blackPen, Rectangle spectrumRec)
    {
      int fontHeight = (int)g2.MeasureString("M", bigFont).Height;

      RectangleTransform rt2 = new RectangleTransform(spectrumRec, spectrumRec.Width, 100);

      g2.DrawLine(blackPen, new Point(spectrumRec.Left, spectrumRec.Bottom), new Point(spectrumRec.Left, spectrumRec.Top));
      for (int i = 0; i <= 100; i += 2)
      {
        int curPercent = rt2.GetTransformY(i);
        if (i % 10 == 0)
        {
          g2.DrawLine(blackPen, spectrumRec.Left, curPercent, spectrumRec.Left - 10, curPercent);
          string number = i.ToString();
          float strWidth = GetStringWidth(g2, bigFont, number);
          g2.DrawString(number, bigFont, Brushes.Black, new PointF(spectrumRec.Left - 10 - strWidth, curPercent - fontHeight / 2));
        }
        else
        {
          g2.DrawLine(blackPen, spectrumRec.Left, curPercent, spectrumRec.Left - 5, curPercent);
        }
      }

      g2.DrawVerticalString(bigFont, Brushes.Black, spectrumRec.Left - 10 - GetStringWidth(g2, bigFont, "100") - fontHeight / 2, (spectrumRec.Top + spectrumRec.Bottom) / 2, "Intensity");
    }

    private static float GetStringWidth(Graphics g2, Font font, string aString)
    {
      return g2.MeasureString(aString, font).Width;
    }

    private static float GetCharWidth(Graphics g2, Font font, char c)
    {
      return g2.MeasureString(StringUtils.RepeatChar(c, 2), font).Width - g2.MeasureString(StringUtils.RepeatChar(c, 1), font).Width;
    }

    private static Rectangle GetSpectrumRectangle(Graphics g2, PeakList<MatchedPeak> peaks, Rectangle rec)
    {
      MatchedPeak maxIntensityPeak = peaks.FindMaxIntensityPeak();

      MatchedPeak highestPeak = maxIntensityPeak;

      RectangleTransform rt = new RectangleTransform(rec, 1.0, highestPeak.Intensity);
      int top = rt.GetTransformY(highestPeak.Intensity);
      int highestTextLength = 0;
      if (highestPeak.Matched)
      {
        highestTextLength = 10 + (int)GetStringWidth(g2, bigFont, highestPeak.Information);
        top -= highestTextLength;
      }

      foreach (MatchedPeak peak in peaks)
      {
        if (!peak.Matched)
        {
          continue;
        }

        int textLength = 10 + (int) GetStringWidth(g2, bigFont, peak.Information);
        int totalLength = rt.GetTransformY(peak.Intensity) - textLength;

        if (totalLength < top)
        {
          highestTextLength = textLength;
          top = totalLength;
          highestPeak = peak;
        }
      }

      int peakIntensityLength = rec.Height - highestTextLength;
      int recHeight = (int)(peakIntensityLength * maxIntensityPeak.Intensity / highestPeak.Intensity);

      return new Rectangle(rec.Left, rec.Bottom - recHeight, rec.Width, recHeight);
    }

    #endregion
  }
}
