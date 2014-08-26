using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Quantification.IsobaricLabelling
{
  public static class IsobaricTypeFactory
  {
    private static IsobaricType[] _isobaricTypes;

    public static IsobaricType[] IsobaricTypes
    {
      get
      {
        if (_isobaricTypes == null)
        {
          var executeDir = FileUtils.GetAssemblyPath();
          if (Directory.Exists(executeDir))
          {
            string xmlFile = executeDir + "\\isobaric.xml";
            if (File.Exists(xmlFile))
            {
              _isobaricTypes = IsobaricTypeXmlFileReader.ReadFromFile(xmlFile).ToArray();
            }
          }
        }
        return _isobaricTypes;
      }
    }

    public static IsobaricType Find(string name)
    {
      Console.WriteLine("Find " + name);
      return IsobaricTypes.First(m => m.Name.Equals(name)).Clone() as IsobaricType;
    }
  }
}
