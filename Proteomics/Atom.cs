using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RCPA.Proteomics
{
  public delegate double GetAtomMassFunction(Atom atom);

  public delegate double GetMassFromCompositionFunction(Dictionary<Atom, int> atomCount);

  public struct Atom : IComparable<Atom>
  {
    public static readonly Atom Ag = new Atom("Ag", "Silver", 106.905092, 107.8682);
    public static readonly Atom Au = new Atom("Au", "Gold", 196.966543, 196.96655);
    public static readonly Atom Br = new Atom("Br", "Bromine", 78.9183361, 79.904);
    public static readonly Atom C = new Atom("C", "Carbon", 12, 12.0107);
    public static readonly Atom C13 = new Atom("(C13)", "Carbon13", 13.00335483, 13.00335483);
    public static readonly Atom Cx = new Atom("Cx", "Carbon13", 13.00335483, 13.00335483);
    public static readonly Atom Ca = new Atom("Ca", "Calcium", 39.9625906, 40.078);
    public static readonly Atom Cl = new Atom("Cl", "Chlorine", 34.96885272, 35.453);
    public static readonly Atom Cu = new Atom("Cu", "Copper", 62.9295989, 63.546);
    public static readonly Atom F = new Atom("F", "Fluorine", 18.99840322, 18.9984032);
    public static readonly Atom Fe = new Atom("Fe", "Iron", 55.9349393, 55.845);
    public static readonly Atom H = new Atom("H", "Hydrogen", 1.007825035, 1.00794);
    public static readonly Atom H2 = new Atom("(H2)", "Deuterium", 2.014101779, 2.014101779);
    public static readonly Atom Hx = new Atom("Hx", "Deuterium", 2.014101779, 2.014101779);
    public static readonly Atom Hg = new Atom("Hg", "Mercury", 201.970617, 200.59);
    public static readonly Atom I = new Atom("I", "Iodine", 126.904473, 126.90447);
    public static readonly Atom K = new Atom("K", "Potassium", 38.9637074, 39.0983);
    public static readonly Atom Li = new Atom("Li", "Lithium", 7.016003, 6.941);
    public static readonly Atom Mo = new Atom("Mo", "Molybdenum", 97.9054073, 95.94);
    public static readonly Atom N = new Atom("N", "Nitrogen", 14.003074, 14.0067);
    public static readonly Atom N15 = new Atom("(N15)", "Nitrogen15", 15.00010897, 15.00010897);
    public static readonly Atom Nx = new Atom("Nx", "Nitrogen15", 15.00010897, 15.00010897);
    public static readonly Atom Na = new Atom("Na", "Sodium", 22.9897677, 22.98977);
    public static readonly Atom Ni = new Atom("Ni", "Nickel", 57.9353462, 58.6934);
    public static readonly Atom O = new Atom("O", "Oxygen", 15.99491463, 15.9994);
    public static readonly Atom O18 = new Atom("(O18)", "Oxygen18", 17.9991603, 17.9991603);
    public static readonly Atom Ox = new Atom("Ox", "Oxygen18", 17.9991603, 17.9991603);
    public static readonly Atom P = new Atom("P", "Phosphorous", 30.973762, 30.973761);
    public static readonly Atom S = new Atom("S", "Sulfur", 31.9720707, 32.065);
    public static readonly Atom Se = new Atom("Se", "Selenium", 79.9165196, 78.96);
    public static readonly Atom Zn = new Atom("Zn", "Zinc", 63.9291448, 65.409);

    public static readonly double ElectronMass = 0.00054858;
    public static readonly double IonHMass = H.MonoMass - ElectronMass;

    private static readonly List<Atom> itemCHNOS = new List<Atom>(new[] { C, H, N, O, S });

    private static readonly List<Atom> items =
      new List<Atom>(new[]
                       {
                         H, H2, Li, C, C13, N, N15, O, O18, F, Na, P, S, Cl, K, Ca, Fe, Ni, Cu, Zn, Br, Se, Mo, Ag, I, Au
                         , Hg, Cx, Nx, Hx, Ox
                       });

    private static Dictionary<string, Atom> atomMap;
    private readonly double averageMass;
    private readonly double monoMass;
    private readonly string name;
    private readonly string symbol;

    public Atom(string symbol, string name, double monoMass, double averageMass)
    {
      this.symbol = symbol;
      this.name = name;
      this.monoMass = monoMass;
      this.averageMass = averageMass;
    }

    public string Symbol
    {
      get { return this.symbol; }
    }

    public string Name
    {
      get { return this.name; }
    }

    public double MonoMass
    {
      get { return this.monoMass; }
    }

    public double AverageMass
    {
      get { return this.averageMass; }
    }

    public static ReadOnlyCollection<Atom> ItemCHNOS
    {
      get { return itemCHNOS.AsReadOnly(); }
    }

    public static Dictionary<string, Atom> AtomMap
    {
      get
      {
        if (atomMap == null)
        {
          atomMap = new Dictionary<string, Atom>();
          foreach (Atom atom in items)
          {
            atomMap[atom.Name] = atom;
          }
        }
        return atomMap;
      }
    }

    #region IComparable<Atom> Members

    public int CompareTo(Atom other)
    {
      return this.symbol.CompareTo(other.symbol);
    }

    #endregion

    public static ReadOnlyCollection<Atom> Items()
    {
      return items.AsReadOnly();
    }

    public static double GetMass(Dictionary<Atom, int> atomCount, GetAtomMassFunction getMass)
    {
      double result = 0.0;
      foreach (Atom atom in atomCount.Keys)
      {
        result += atomCount[atom] * getMass(atom);
      }
      return result;
    }

    public static double GetMonoMass(Dictionary<Atom, int> atomCount)
    {
      return GetMass(atomCount, MonoMassFunction);
    }

    public static double GetAverageMass(Dictionary<Atom, int> atomCount)
    {
      return GetMass(atomCount, AverageMassFunction);
    }

    public static Atom ValueOf(string symbol)
    {
      foreach (Atom atom in items)
      {
        if (atom.symbol.Equals(symbol))
        {
          return atom;
        }
      }
      throw new Exception("No such atom defined : " + symbol);
    }

    public static double MonoMassFunction(Atom atom)
    {
      return atom.MonoMass;
    }

    public static double AverageMassFunction(Atom atom)
    {
      return atom.AverageMass;
    }
  }
}