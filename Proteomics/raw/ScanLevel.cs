using System.Collections.Generic;

namespace RCPA.Proteomics.Raw
{
  public class ScanLevel
  {
    public ScanLevel()
    {
      Children = new List<ScanLevel>();
    }

    public int Scan { get; set; }
    private ScanLevel _parent;

    public ScanLevel Parent
    {
      get
      {
        return _parent;
      }
      set
      {
        if (value == _parent)
        {
          return;
        }

        if (_parent != null)
        {
          _parent.Children.Remove(this);
          _parent = null;
        }

        if (value != null)
        {
          if (!value.Children.Contains(this))
          {
            value.Children.Add(this);
            _parent = value;
          }
        }
      }
    }

    public List<ScanLevel> Children { get; private set; }
    public int Level { get; set; }
  }
}
