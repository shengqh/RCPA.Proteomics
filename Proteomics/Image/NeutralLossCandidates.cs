using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Proteomics.Utils;

namespace RCPA.Proteomics.Image
{
  public interface INeutralLossCandidates
  {
    bool CanLossWater { get; }

    bool CanLossAmmonia { get; }

    bool[] BLossWater { get; }

    bool[] BLossAmmonia { get; }

    bool[] YLossWater { get; }

    bool[] YLossAmmonia { get; }
  }

  public class NeutralLossCandidates : INeutralLossCandidates
  {
    private bool canLossWater;

    private bool canLossAmmonia;

    private bool[] bLossWater;

    private bool[] bLossAmmonia;

    private bool[] yLossWater;

    private bool[] yLossAmmonia;

    public bool CanLossWater { get { return canLossWater; } }

    public bool CanLossAmmonia { get { return canLossAmmonia; } }

    public bool[] BLossWater { get { return bLossWater; } }

    public bool[] BLossAmmonia { get { return bLossAmmonia; } }

    public bool[] YLossWater { get { return yLossWater; } }

    public bool[] YLossAmmonia { get { return yLossAmmonia; } }

    public NeutralLossCandidates(string peptide)
    {
      canLossWater = true;
      canLossAmmonia = false;

      string pureSeq = PeptideUtils.GetPureSequence(peptide);

      bLossWater = new bool[pureSeq.Length];
      bLossAmmonia = new bool[pureSeq.Length];
      yLossWater = new bool[pureSeq.Length];
      yLossAmmonia = new bool[pureSeq.Length];

      for (int i = 0; i < pureSeq.Length; i++)
      {
        if (NeutralLossUtils.CanLossWater(pureSeq[i]))
        {
          for (int j = i; j < pureSeq.Length; j++)
          {
            bLossWater[j] = true;
          }
          break;
        }
        bLossWater[i] = false;
      }

      for (int i = pureSeq.Length - 1; i >= 0; i--)
      {
        if (NeutralLossUtils.CanLossWater(pureSeq[i]))
        {
          for (int j = i; j >= 0; j--)
          {
            yLossWater[pureSeq.Length - 1 - j] = true;
          }
          break;
        }
        yLossWater[pureSeq.Length - 1 - i] = false;
      }

      for (int i = 0; i < pureSeq.Length; i++)
      {
        if (NeutralLossUtils.CanLossAmmonia(pureSeq[i]))
        {
          for (int j = i; j < pureSeq.Length; j++)
          {
            bLossAmmonia[j] = true;
            canLossAmmonia = true;
          }
          break;
        }
        bLossAmmonia[i] = false;
      }

      for (int i = pureSeq.Length - 1; i >= 0; i--)
      {
        if (NeutralLossUtils.CanLossAmmonia(pureSeq[i]))
        {
          for (int j = i; j >= 0; j--)
          {
            yLossAmmonia[pureSeq.Length - 1 - j] = true;
          }
          break;
        }
        yLossAmmonia[pureSeq.Length - 1 - i] = false;
      }
    }
  }
}
