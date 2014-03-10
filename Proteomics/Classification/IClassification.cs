using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Classification
{
  public interface IClassification<E>
  {
    string GetPrinciple();

    string GetClassification(E obj);
  }
}
