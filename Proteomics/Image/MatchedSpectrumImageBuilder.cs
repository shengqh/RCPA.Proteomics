using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace RCPA.Proteomics.Image
{
  class TextItem
  {
    public TextItem(int x, int lineX, int y, Color textColor,
        String text)
    {
      this.X = x;
      this.LineX = lineX;
      this.Y = y;
      this.TextColor = textColor;
      this.Text = text;
    }

    public int X { get; set; }

    public int Y { get; set; }

    public int LineX { get; set; }

    public Color TextColor { get; set; }

    public string Text { get; set; }
  }
  /*
  public class MatchedSpectrumImageBuilder
  {
    private Point dimension = new Point(800, 700);

    public IIdentifiedPeptideResult IdentifiedResult { get; set; }

    private List<IPeakMatcher> matchers = new ArrayList<IPeakMatcher>();

    private List<IPeptideDrawer> drawers = new ArrayList<IPeptideDrawer>();

    private List<TextItem> texts = new ArrayList<TextItem>();

    public IIdentifiedPeptideResult IdentifiedResult{get;set;}

    public void addMatcher(IPeakMatcher matcher)
    {
      this.matchers.add(matcher);
    }

    public void removeMatcher(IPeakMatcher matcher)
    {
      this.matchers.remove(matcher);
    }

    public void clearMatcher()
    {
      this.matchers.clear();
    }

    public void addDrawer(IPeptideDrawer drawer)
    {
      this.drawers.add(drawer);
    }

    public void removeDrawer(IPeptideDrawer drawer)
    {
      this.drawers.remove(drawer);
    }

    public void clearDrawer()
    {
      this.drawers.clear();
    }

    private static DecimalFormat df = new DecimalFormat("0.00");

    private static double NINETY_DEGREES = Math.toRadians(90.0);

    private int maxMz = Integer.MAX_VALUE;

    private int maxIntensity = Integer.MAX_VALUE;

    private BufferedImage image;

    private Graphics2D g2;

    protected Graphics2D getG2()
    {
      return g2;
    }

    private int fontHeight;

    protected int getFontHeight()
    {
      return fontHeight;
    }

    private FontRenderContext frc;

    private int drawBottom;

    private int top;

    protected int getTop()
    {
      return top;
    }

    private int bottom;

    protected int getBottom()
    {
      return bottom;
    }

    private int left;

    protected int getLeft()
    {
      return left;
    }

    private int right;

    protected int getRight()
    {
      return right;
    }

    private double scaleWidth;

    private double scaleHeight;

    private static int[] steps = new int[] { 10, 25, 50 };

    private void initIonSeries()
    {
      PeakList<MatchedPeak> peaks = pepResult.getPeakList();

      maxMz = (int)peaks.getMaxPeak();
      maxMz = (maxMz / 100 + 1) * 100;

      maxIntensity = (int)peaks.getMaxIntensity();
    }

    private void initImage()
    {
      image = new BufferedImage(dimension.width, dimension.height,
          BufferedImage.TYPE_INT_RGB);
      g2 = image.createGraphics();

      initImageVariables();

      clearImage();

      drawXScale();

      drawYScale();

      drawPeaks();

      drawPeakTexts();

      drawPeptides();

      drawOther();

      g2.dispose();
    }

    private void drawPeptides()
    {
      int scale = 1;
      foreach (IPeptideDrawer drawer in drawers)
      {
        drawer.draw(this, pepResult, drawBottom - scale * fontHeight);
        scale++;
      }
    }

    public void drawPeptide(List<MatchedPeak> peaks, String peptide,
        int yPosition) {
		final Color oldColor = g2.getColor();
		final int height = fontHeight / 2;

		int lastPos = getTransformedX(0);
		g2.drawLine(lastPos, yPosition - height / 2, lastPos, yPosition
				+ height / 2);

		String ions = peaks.get(0).getIonType() + " Ions";
		g2.drawString(ions, lastPos - getStringWidth(ions) - 5, yPosition
				+ height / 2);

		for (int i = 0, j = 0; i < peaks.size(); i++) {
			MatchedPeak peak = peaks.get(i);
			StringBuilder sb = new StringBuilder();
			sb.append(peptide.charAt(j++));
			if (j < peptide.length() && !Character.isLetter(peptide.charAt(j))) {
				sb.append(peptide.charAt(j++));
			}
			String aminoacid = sb.toString();

			Color color = Color.BLACK;
			if (peak.isMatched()) {
				color = IonTypeColor.getColor(peak.getIonType());
			}
			g2.setColor(color);
			int endPos = getTransformedX(peak.getMz());
			int strWidth = getStringWidth(aminoacid);
			final int space = 2;
			int firstEnd = (endPos + lastPos - strWidth - 2 * space) / 2;
			int secondBegin = firstEnd + strWidth + 2 * space;

			g2.drawLine(lastPos, yPosition, firstEnd, yPosition);
			g2.drawString(aminoacid, firstEnd + space, yPosition + height / 2);
			g2.drawLine(secondBegin, yPosition, endPos, yPosition);

			g2.drawLine(endPos, yPosition - height / 2, endPos, yPosition
					+ height / 2);

			lastPos = endPos;
		}
		g2.setColor(oldColor);
	}

    private void doDrawPeak(MatchedPeak peak)
    {
      int curMz = getTransformedX(peak.getMz());
      int curIntensity = getTransformedY(peak.getIntensity());

      Color color = drawPeakLine(peak, curMz, curIntensity);

      if (peak.isMatched())
      {
        String ionName = "";

        String charge = "";
        if (peak.getCharge() > 1)
        {
          charge = RcpaStringUtils.getRepeatChar('+', peak.getCharge());
        }

        if (null != peak.getDisplayName()
            && 0 != peak.getDisplayName().length())
        {
          ionName = peak.getDisplayName() + charge + "   "
              + df.format(peak.getMz());
        }
        else if (IonType.PRECURSOR == peak.getIonType()
            || IonType.PRECURSOR_NEUTRAL_LOSS == peak.getIonType())
        {
          ionName = peak.getIonType() + "-" + df.format(peak.getMz());
        }
        else
        {
          String name;
          if (peak.getIonType() == IonType.B2)
          {
            name = "b";
          }
          else if (peak.getIonType() == IonType.Y2)
          {
            name = "y";
          }
          else
          {
            name = StringUtils.lowerCase(peak.getIonType().toString());
          }
          ionName = name + peak.getIonIndex() + charge + "   "
              + df.format(peak.getMz());
        }
        drawVerticalTextInPeakImage(ionName, curMz, curIntensity, color);
      }
    }

    private Color drawPeakLine(MatchedPeak peak, int curMz, int curIntensity)
    {
      if (peak.isMatched() && peak.getIonType() == null)
      {
        throw new IllegalStateException("Undifined IonType of peak "
            + peak.getMz() + " : " + peak.getIntensity());
      }

      Color color = peak.isMatched() ? IonTypeColor.getColor(peak
          .getIonType()) : Color.BLACK;
      g2.setColor(color);
      g2.drawLine(curMz, bottom, curMz, curIntensity);
      return color;
    }

    private void drawPeaks() {
		matchPeaks();

		texts.clear();

		Color oldColor = g2.getColor();
		for (MatchedPeak peak : pepResult.getPeakList().getPeaks()) {
			doDrawPeak(peak);
		}
		g2.setColor(oldColor);
	}

    private void drawPeakTexts() {
		Color oldColor = g2.getColor();
		
		for(TextItem item : texts){
			g2.setColor(item.textColor);
			drawVerticalText2(item.text, item.x,item.y);
		}

		g2.setColor(oldColor);
	}

    public void matchPeaks() {
		MatchedPeakUtils.unmatchAll(pepResult.getPeakList().getPeaks());
		for (IPeakMatcher matcher : matchers) {
			matcher.match(pepResult);
		}
	}

    private void drawYScale() {
		g2.setColor(Color.BLACK);
		g2.drawLine(left, bottom, left, top);
		for (int i = 0; i <= 100; i += 2) {
			int curPercent = getPercentY(i);
			if (i % 10 == 0) {
				g2.drawLine(left, curPercent, left - 10, curPercent);
				final String number = Integer.toString(i);
				int strWidth = getStringWidth(number);
				g2.drawString(number, left - 10 - strWidth, curPercent
						+ fontHeight / 3);
			} else {
				g2.drawLine(left, curPercent, left - 5, curPercent);
			}
		}

		drawVerticalTextCenter("Intensity", left - 35, (top + bottom) / 2);
	}

    private void drawVerticalTextInPeakImage(String str, int originX,
        int originY, Color textColor)
    {
      int totalHeight = originY - 10 - getStringWidth(str);
      if (totalHeight < drawBottom)
      {
        texts.add(new TextItem(originX + 10 + fontHeight / 6, originX,
            drawBottom + 10 + +getStringWidth(str), textColor, str));
      }
      else
      {
        texts.add(new TextItem(originX + fontHeight / 6, originX,
            originY - 10, textColor, str));
      }
    }

    private void drawVerticalText(String str, int x, int y)
    {
      g2.translate(x, y);
      g2.rotate(-NINETY_DEGREES);
      g2.drawString(str, 0, 0);
      g2.rotate(NINETY_DEGREES);
      g2.translate(-x, -y);
    }

    private void drawVerticalText2(String str, int x, int y)
    {
      g2.translate(x, y);
      g2.rotate(-NINETY_DEGREES);
      Font oldFont = g2.getFont();
      Font newFont = oldFont.deriveFont((float)(oldFont.getSize() / 1.3));
      int curX = 0;
      boolean bIonTypeNumber = true;
      for (int i = 0; i < str.length(); i++)
      {
        if (str.charAt(i) == ' ')
        {
          g2.drawString(str.substring(i), curX, 0);
          break;
        }

        String subStr = str.substring(i, i + 1);
        if (Character.isDigit(str.charAt(i)))
        {
          if (bIonTypeNumber)
          {
            g2.drawString(subStr, curX, 0);
            curX += getStringWidth(oldFont, subStr);
          }
          else
          {
            g2.setFont(newFont);
            g2.drawString(subStr, curX, getFontHeight() / 8);
            g2.setFont(oldFont);
            curX += getStringWidth(newFont, subStr);
          }
        }
        else
        {
          if (i > 1)
          {
            bIonTypeNumber = false;
          }
          g2.drawString(subStr, curX, 0);
          curX += getStringWidth(oldFont, subStr);
        }
      }
      g2.rotate(NINETY_DEGREES);
      g2.translate(-x, -y);
    }

    private void drawVerticalTextCenter(String str, int x, int y)
    {
      int strWidth = getStringWidth(str);
      drawVerticalText(str, x + fontHeight / 2, y + strWidth / 2);
    }

    private int getTransformedX(double mz)
    {
      return (int)(left + mz * scaleWidth);
    }

    private int getTransformedY(double intensity)
    {
      return (int)(bottom - intensity * scaleHeight);
    }

    private int getPercentY(int percentY)
    {
      return bottom - percentY * (bottom - top) / 100;
    }

    private void drawXScale() {
		g2.setColor(Color.BLACK);
		g2.drawLine(left, bottom, right, bottom);

		int xStep = calculateXStep();

		for (int i = 0; i < maxMz; i += xStep) {
			int curMz = getTransformedX(i);
			if (i % 100 == 0) {
				g2.drawLine(curMz, bottom, curMz, bottom + 10);
				final String number = Integer.toString(i);
				int strWidth = getStringWidth(number);
				g2.drawString(number, curMz - strWidth / 2, bottom + 10
						+ fontHeight);
			} else if (i % 50 == 0) {
				g2.drawLine(curMz, bottom, curMz, bottom + 7);
			} else {
				g2.drawLine(curMz, bottom, curMz, bottom + 4);
			}
		}

		int mzWidth = getStringWidth("M/Z");
		g2.drawString("M/Z", (right + left - mzWidth) / 2, bottom + 10 + 2
				* fontHeight);
	}

    protected int getStringWidth(String str)
    {
      return getStringWidth(g2.getFont(), str);
    }

    protected int getStringWidth(Font font, String str)
    {
      Rectangle2D bounds = font.getStringBounds(str, frc);
      return (int)bounds.getWidth();
    }

    private int calculateXStep()
    {
      int scale = dimension.width / 800;
      if (scale < 1)
      {
        scale = 1;
      }
      int result = 10;
      int stepIndex = 0;
      while (stepIndex < steps.length)
      {
        result = steps[stepIndex];
        if (maxMz / (result * scale) <= 50)
        {
          break;
        }
        stepIndex++;
      }
      return result;
    }

    private void clearImage()
    {
      g2.setColor(Color.white);
      g2.fillRect(0, 0, dimension.width, dimension.height);
    }

    private void initImageVariables()
    {
      fontHeight = g2.getFontMetrics().getHeight();
      frc = g2.getFontRenderContext();

      drawBottom = fontHeight * (drawers.size() + 1);
      top = drawBottom + 100;
      left = 50;
      bottom = dimension.height - top - 50;
      right = dimension.width - 20;

      scaleWidth = (double)(right - left) / maxMz;
      scaleHeight = (double)(bottom - top) / maxIntensity;
    }

    public void drawImage(OutputStream os)
    {
      initIonSeries();

      initImage();

      ImageIO.write(image, "jpg", os);
    }

    public void drawOther()
    {
    }
  }
   */ 
}
