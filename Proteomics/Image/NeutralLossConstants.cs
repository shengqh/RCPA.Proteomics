using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Image
{
  public static class NeutralLossConstants
  {
    public static readonly double MassH = Atom.H.MonoMass;

    public static readonly double MassO = Atom.O.MonoMass;

    public static readonly double MassN = Atom.N.MonoMass;

    public static readonly double MassP = Atom.P.MonoMass;

    public static readonly double MassH2O = MassH * 2 + MassO;

    public static readonly double MassNH3 = MassH * 3 + MassN;

    public static readonly double MassH3PO4 = MassH * 3 + MassP + MassO * 4;

    public static readonly double MassHPO3 = MassH + MassP + MassO * 3;

    public static readonly INeutralLossType NL_AMMONIA = new NeutralLossType(MassNH3, "NH3", false);

    public static readonly INeutralLossType NL_WATER = new NeutralLossType(MassH2O, "H2O", false);

    public static readonly INeutralLossType NL_H3PO4 = new NeutralLossType(MassH3PO4, "H3PO4", true);

    public static readonly INeutralLossType NL_HPO3 = new NeutralLossType(MassHPO3, "HPO3", true);

  }
}
