using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.Srm
{
  public class SrmViewOption
  {
    public bool ViewDecoy { get; set; }

    public bool ViewGreenLine { get; set; }

    public bool ViewValidOnly { get; set; }

    public bool ViewInvalidCompoundAsNA { get; set; }

    public bool ViewCurrentHighlight { get; set; }

    public DisplayType ViewType { get; set; }

    public SrmViewOption()
    {
      ViewDecoy = false;
      ViewGreenLine = false;
      ViewValidOnly = false;
      ViewInvalidCompoundAsNA = true;
      ViewCurrentHighlight = true;
      ViewType = DisplayType.PerfectSize;
    }
  }

  public delegate bool CompoundItemFilter(CompoundItem item);

  public delegate bool TransitionItemFilter(TransitionItem item);

  public static class SrmViewOptionExtension
  {
    public static CompoundItemFilter GetRejectCompoundFilter(this SrmViewOption option)
    {
      CompoundItemFilter result = null;

      if (!option.ViewDecoy)
      {
        result += new CompoundItemFilter(CompoundItemUtils.IsDecoy);
      }

      if (option.ViewValidOnly)
      {
        result += new CompoundItemFilter(CompoundItemUtils.IsInvalid);
      }

      if (result == null)
      {
        result += new CompoundItemFilter(CompoundItemUtils.AlwaysFalse);
      }

      return result;
    }

    public static TransitionItemFilter GetRejectTransitionFilter(this SrmViewOption option)
    {
      TransitionItemFilter result = null;

      if (!option.ViewDecoy)
      {
        result += new TransitionItemFilter(TransitionItemUtils.IsDecoy);
      }

      if (option.ViewValidOnly)
      {
        result += new TransitionItemFilter(TransitionItemUtils.IsInvalid);
      }

      if (result == null)
      {
        result += new TransitionItemFilter(TransitionItemUtils.AlwaysFalse);
      }

      return result;
    }
  }
}
