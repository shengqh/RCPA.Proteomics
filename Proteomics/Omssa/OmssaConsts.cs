using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RCPA.Proteomics.Omssa
{
  public static class OmssaConsts
  {
    public static Dictionary<string, string> _enzymeMap;
    public static Dictionary<string, string> EnzymeMap
    {
      get
      {
        if (_enzymeMap == null)
        {
          var omssaxsdFile = FileUtils.AppPath() + "\\OMSSA.xsd";
          XElement root = XElement.Load(omssaxsdFile);

          var enzymes = root.FindFirstDescendant("element", "name", "MSEnzymes");
          var restrictions = enzymes.FindFirstDescendant("restriction");
          _enzymeMap = (from ele in restrictions.FindElements("enumeration")
                        let index = ele.FindAttribute("intvalue").Value
                        let name = ele.FindAttribute("value").Value
                        select new { Index = index, Name = name }).ToDictionary(m => m.Index, m => m.Name);
        }
        return _enzymeMap;
      }
    }
  }
}
