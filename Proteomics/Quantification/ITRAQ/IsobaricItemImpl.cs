using System;
using System.Linq;

namespace RCPA.Proteomics.Quantification.ITraq
{
  /// <summary>
  /// isobaric离子的实现类
  /// </summary>
  public class IsobaricItemImpl
  {
    private IsobaricItem _parent;

    private IsobaricDefinition _definition;

    private double[] _ions;

    private int _baseIndex;

    public IsobaricItemImpl(IsobaricItem parent, IsobaricDefinition definition)
    {
      this._parent = parent;

      this._definition = definition;

      this._baseIndex = definition.Items.Min(m => m.Index);
      var totalLength = definition.Items.Max(m => m.Index) - _baseIndex + 1;

      _ions = new double[totalLength];
    }

    protected void AddCount(double value, ref int result)
    {
      if (value > ITraqConsts.NULL_INTENSITY)
      {
        result++;
      }
    }

    public double[] ObservedIons()
    {
      return (from def in _definition.Items
              select _ions[def.AbsoluteIndex]).ToArray();
    }

    public virtual double this[int index]
    {
      get
      {
        if (_definition.IsValid(index))
        {
          return _ions[index - _baseIndex];
        }
        else
        {
          throw new ArgumentOutOfRangeException(MyConvert.Format("index {0} out of range", index));
        }
      }
      set
      {
        if (_definition.IsValid(index))
        {
          _ions[index - _baseIndex] = value;
        }
        else
        {
          throw new ArgumentOutOfRangeException(MyConvert.Format("index {0} out of range", index));
        }
      }
    }

    public virtual int PeakCount()
    {
      int result = 0;

      foreach (var item in _definition.Items)
      {
        AddCount(_ions[item.AbsoluteIndex], ref result);
      }

      return result;
    }

    public virtual void DevideIntensityByInjectionTime()
    {
      if (_parent.Scan != null && _parent.Scan.IonInjectionTime != 0.0)
      {
        for (int i = 0; i < _ions.Length; i++)
        {
          _ions[i] /= _parent.Scan.IonInjectionTime;
        }
      }
    }

    public virtual void MultiplyIntensityByInjectionTime()
    {
      if (_parent.Scan != null && _parent.Scan.IonInjectionTime != 0.0)
      {
        for (int i = 0; i < _ions.Length; i++)
        {
          _ions[i] *= _parent.Scan.IonInjectionTime;
        }
      }
    }

    public IsobaricDefinition Definition
    {
      get
      {
        return _definition;
      }
    }
  }
}
