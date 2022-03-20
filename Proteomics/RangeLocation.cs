using System;
using System.Text.RegularExpressions;

/*
 *                    BioJava development code
 *
 * This code may be freely distributed and modified under the
 * terms of the GNU Lesser General Public Licence.  This should
 * be distributed with the code.  If you do not have a copy,
 * see:
 *
 *      http://www.gnu.org/copyleft/lesser.html
 *
 * Copyright for this code is held jointly by the individual
 * authors.  These should be listed in @author doc comments.
 *
 * For more information on the BioJava project and its aims,
 * or to join the biojava-l mailing list, visit the home page
 * at:
 *
 *      http://www.biojava.org/
 *
 */

namespace RCPA.Proteomics
{
  /**
   * A simple implementation of Location that contains all points between
   * getMin and getMax inclusive.
   * <p>
   * This will in practice be the most commonly used pure-java implementation.
   *
   * @author Matthew Pocock
   */

  public class RangeLocation
  {
    private int max;
    private int min;

    public RangeLocation(RangeLocation source)
    {
      this.min = source.min;
      this.max = source.max;
    }

    public RangeLocation(int min, int max)
    {
      if (max < min)
      {
        this.min = max;
        this.max = min;
      }
      else
      {
        this.min = min;
        this.max = max;
      }
    }


    public int Min
    {
      get { return this.min; }
      set
      {
        if (value > this.max)
        {
          this.min = this.max;
          this.max = value;
        }
        else
        {
          this.min = value;
        }
      }
    }

    public int Max
    {
      get { return this.max; }
      set
      {
        if (value < this.min)
        {
          this.max = this.min;
          this.min = value;
        }
        else
        {
          this.max = value;
        }
      }
    }

    public RangeLocation Translate(int dist)
    {
      return new RangeLocation(this.min + dist, this.max + dist);
    }

    public bool Contains(int value)
    {
      return value >= this.min && value <= this.max;
    }

    public override bool Equals(object obj)
    {
      if (obj == this)
      {
        return true;
      }

      if (!(obj is RangeLocation))
      {
        return false;
      }

      var rl = (RangeLocation)obj;
      return this.max == rl.max && this.min == rl.min;
    }

    public override int GetHashCode()
    {
      return this.min ^ this.max;
    }

    /**
     * example of line : 30 - 100 
     */

    public static RangeLocation Parse(String line)
    {
      var r = new Regex(@"([\d\.]+)[^\d\.]+([\d\.]+)");
      Match m = r.Match(line);
      if (!m.Success)
      {
        throw new ArgumentException("It's not valid argument of Parse : " + line);
      }

      try
      {
        double from = MyConvert.ToDouble(m.Groups[1].Value);
        double to = MyConvert.ToDouble(m.Groups[2].Value);
        return new RangeLocation((int)from, (int)to);
      }
      catch (Exception)
      {
        throw new ArgumentException("It's not valid argument of Parse : " + line);
      }
    }
  }
}