using System;
using System.Collections.Generic;
using System.Linq;

namespace RCPA.emass
{
  // map from element abbreviation to index in the elements table
  public class ElemMap : Dictionary<string, int>
  { }

  // map from element index to the count of occurences in the formula
  public class FormMap : Dictionary<int, int>, ICloneable
  {
    public void CleanUp()
    {
      var erasable = (from f in this
                      where f.Value == 0
                      select f.Key).ToList();

      foreach (var key in erasable)
      {
        this.Remove(key);
      }
    }

    public void Subtract(FormMap sub)
    {
      foreach (var key in sub.Keys)
      {
        if (this.ContainsKey(key))
        {
          this[key] = this[key] - sub[key];
        }
        else
        {
          this[key] = -sub[key];
        }
      }
      CleanUp();
    }

    public void Add(FormMap add)
    {
      foreach (var key in add.Keys)
      {
        if (this.ContainsKey(key))
        {
          this[key] = this[key] + add[key];
        }
        else
        {
          this[key] = add[key];
        }
      }
      CleanUp();
    }

    public void Negate()
    {
      foreach (var key in this.Keys)
      {
        this[key] = -this[key];
      }
    }

    public bool IsReal()
    {
      foreach (var f in this)
      {
        if (f.Value < 0)
        {
          return false;
        }
      }
      return true;
    }

    #region ICloneable Members

    public object Clone()
    {
      FormMap result = new FormMap();
      foreach (var key in this.Keys)
      {
        result[key] = this[key];
      }
      return result;
    }

    #endregion
  }
}
