using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Spectrum;
using System.IO;
using System.Text.RegularExpressions;
using RCPA.Proteomics;
using System.Windows.Forms;

namespace RCPA.emass
{
  public class Pattern : List<Peak>
  { }

  public class SuperAtomList : List<Pattern>
  { }

  public class SuperAtomData : List<SuperAtomList>
  { }

  public class EmassCalculator
  {
    private SuperAtomData sad = new SuperAtomData();

    public ElemMap IsotopicMap { get; private set; }

    const double ELECTRON_MASS = 0.00054858;

    const double DUMMY_MASS = -10000000;

    private Regex eleReg = new Regex(@"(\S+)\s+(\S+)");

    public EmassCalculator(string filename)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException(filename);
      }

      IsotopicMap = new ElemMap();

      using (StreamReader f = new StreamReader(filename))
      {
        sad.Clear();
        IsotopicMap.Clear();

        string line;
        int elemindex = 0;
        int state = 0;
        while ((line = f.ReadLine()) != null)
        {
          string element;
          switch (state)
          {
            case 0: // new element
              var m0 = eleReg.Match(line);
              element = m0.Groups[1].Value;
              IsotopicMap[element] = elemindex;
              var pkl = new Pattern();
              var sal = new SuperAtomList();
              sal.Add(pkl);
              sad.Add(sal);
              elemindex++;
              state = 1;
              break;
            case 1: // isotope
              var m1 = eleReg.Match(line);
              if (m1.Success)
              {
                Peak p = new Peak();
                p.Mz = MyConvert.ToDouble(m1.Groups[1].Value);
                p.Intensity = MyConvert.ToDouble(m1.Groups[2].Value);
                Pattern idist = sad.Last().First();
                // fill the gaps in the patterns with zero abundancy peaks
                if (idist.Count > 0)
                {
                  double prevmass = idist.Last().Mz;
                  for (int i = 0; i < (int)(p.Mz - prevmass - 0.5); i++)
                  {
                    Peak filler = new Peak();
                    filler.Mz = DUMMY_MASS;
                    filler.Intensity = 0;
                    idist.Add(filler);
                  }
                }
                // insert the peak
                idist.Add(p);
              }
              else
              {
                state = 0;
              }
              break;
          }
        }
      }
    }

    // Merge two patterns to one.
    private void convolute_basic(Pattern target, Pattern source1, Pattern source2)
    {
      target.Clear();
      int g_n = source1.Count;
      int f_n = source2.Count;
      if (g_n == 0 || f_n == 0)
        return;
      for (int k = 0; k < g_n + f_n - 1; k++)
      {
        double sumweight = 0, summass = 0;
        int start = k < (f_n - 1) ? 0 : k - f_n + 1; // max(0, k-f_n+1)
        int end = k < (g_n - 1) ? k : g_n - 1;       // min(g_n - 1, k)
        for (int i = start; i <= end; i++)
        {
          double weight = source1[i].Intensity * source2[k - i].Intensity;
          double mass = source1[i].Mz + source2[k - i].Mz;
          sumweight += weight;
          summass += weight * mass;
        }
        Peak p = new Peak();
        if (sumweight == 0)
          p.Mz = DUMMY_MASS;
        else
          p.Mz = summass / sumweight;
        p.Intensity = sumweight;
        target.Add(p);
      }
    }

    // Prune the small peaks from both sides but
    // leave them within the pattern.
    private void Prune(Pattern f, double limit)
    {
      while (f.Count > 0 && f.First().Intensity <= limit)
      {
        f.RemoveAt(0);
      }

      while (f.Count > 0 && f.Last().Intensity <= limit)
      {
        f.RemoveAt(f.Count - 1);
      }
    }

    public Pattern Calculate(string formula, double limit, long charge)
    {
      AtomComposition ac = new AtomComposition(formula);

      return Calculate(ac, limit, charge);
    }

    public Pattern Calculate(AtomComposition ac, double limit, long charge)
    {
      FormMap fm = new FormMap();
      foreach (var key in ac.Keys)
      {
        if (IsotopicMap.ContainsKey(key.Symbol))
        {
          fm[IsotopicMap[key.Symbol]] = ac[key];
        }
        else
        {
          throw new Exception(MyConvert.Format("Atom {0} is not defined in EmassCalculator", key.Symbol));
        }
      }

      return Calculate(fm, limit, charge);
    }

    public Pattern Calculate(FormMap fm, double limit, long charge)
    {
      Pattern tmp = new Pattern();
      Pattern result = new Pattern();

      result.Add(new Peak() { Mz = 0.0, Intensity = 1.0 });

      foreach (var i in fm)
      {
        int atom_index = i.Key;
        SuperAtomList sal = sad[atom_index];
        int n = i.Value;
        int j = 0;
        while (n > 0)
        {
          int sz = sal.Count;
          if (j == sz)
          {
            sal.Add(new Pattern());
            convolute_basic(sal[j], sal[j - 1], sal[j - 1]);
            Prune(sal[j], limit);
          }
          if ((n & 1) == 1)
          { // digit is 1, convolute result
            convolute_basic(tmp, result, sal[j]);
            Prune(tmp, limit);
            result.Clear();
            result.AddRange(tmp);
          }
          n >>= 1;
          j++;
        }
      }

      // take charge into account
      foreach (var p in result)
      {
        if (charge > 0)
          p.Mz = p.Mz / Math.Abs(charge) - ELECTRON_MASS;
        else if (charge < 0)
          p.Mz = p.Mz / Math.Abs(charge) + ELECTRON_MASS;
      }

      if (result.Count == 0)
      {
        throw new Exception("Calculate profile failed");
      }
      return result;
    }
  }
}
