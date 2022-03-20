using System.Collections.Generic;
using System.Linq;

namespace RCPA.Proteomics.Snp
{
  public enum VariantType { Unknown, SingleAminoacidPolymorphism, NTerminalLoss, CTerminalLoss, NTerminalExtension, CTerminalExtension }

  public class TargetVariant
  {
    public TargetVariant()
    {
      Source = string.Empty;
      Target = new HashSet<string>();
      DeltaMass = 0.0;
      TargetType = VariantType.Unknown;
    }

    public string Source { get; set; }

    public HashSet<string> Target { get; set; }

    public double DeltaMass { get; set; }

    public VariantType TargetType { get; set; }

    public bool IsDeamidatedMutation
    {
      get
      {
        return TargetType == VariantType.SingleAminoacidPolymorphism && MutationUtils.IsDeamidatedMutation(Source[0], Target.First()[0]);
      }
    }

    public bool IsSingleNucleotideMutation
    {
      get
      {
        return TargetType == VariantType.SingleAminoacidPolymorphism && MutationUtils.IsSingleNucleotideMutation(Source[0], Target.First()[0]);
      }
    }

  }
}
