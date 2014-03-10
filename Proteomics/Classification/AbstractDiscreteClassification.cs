using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Classification
{
  public abstract class AbstractDiscreteClassification<E, V> : IClassification<E> where V : IComparable<V>
  {
    abstract protected V DoGetClassification(E obj);

    #region IClassification<E> Members

    public abstract string GetPrinciple();

    public virtual string GetClassification(E obj)
    {
      V result = DoGetClassification(obj);
      return result.ToString();
    }

    #endregion
  }
}
