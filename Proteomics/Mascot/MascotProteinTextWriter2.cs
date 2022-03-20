using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Proteomics.Mascot
{
  public class MascotProteinTextWriter2 : IdentifiedProteinTextWriter
  {
    public MascotProteinTextWriter2(IPropertyConverter<IIdentifiedProtein> converter) : base(converter) { }

    public MascotProteinTextWriter2(string proteinHeader) : base(proteinHeader) { }

    public MascotProteinTextWriter2(string proteinHeader, IEnumerable<IIdentifiedProtein> proteins) : base(proteinHeader, proteins) { }

    public MascotProteinTextWriter2(IEnumerable<IIdentifiedProtein> proteins) : base(MascotHeader.MASCOT_PROTEIN_HEADER, proteins) { }
  }
}