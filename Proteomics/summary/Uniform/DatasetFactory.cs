using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace RCPA.Proteomics.Summary.Uniform
{
  public class DatasetFactory
  {
    public List<IDatasetFactory> Factories { get; private set; }

    private DatasetFactory()
    {
      Factories = new List<IDatasetFactory>();
      Initialize();
    }

    private static DatasetFactory instance = null;

    public static DatasetFactory GetInstance()
    {
      if (instance == null)
      {
        instance = new DatasetFactory();
      }

      return instance;
    }

    private void Initialize()
    {
      DirectoryInfo executeDir = new FileInfo(Application.ExecutablePath).Directory;
      if (executeDir != null)
      {
        string optionFile = executeDir.FullName + "\\MiscOptions.xml";
        if (File.Exists(optionFile))
        {
          XElement ele = XElement.Load(optionFile);

          XElement factories = ele.Element("DatasetDefinitions");
          if (factories != null)
          {
            var eles = factories.Elements("Dataset");
            foreach (var fEle in eles)
            {
              string name = fEle.GetChildValue("name", string.Empty);
              string assembly = fEle.GetChildValue("assembly", string.Empty);
              string classname = fEle.GetChildValue("class", string.Empty);

              if (name.Length == 0 && classname.Length == 0)
              {
                continue;
              }

              object obj;
              if (assembly.Length > 0)
              {
                string assemblyFile = executeDir.FullName + "\\plugin\\assembly" + ".dll";
                Assembly target;
                if (!File.Exists(assemblyFile))
                {
                  target = Assembly.Load(assembly);
                }
                else
                {
                  target = Assembly.LoadFrom(assemblyFile);
                }
                obj = target.CreateInstance(Type.GetType(classname).FullName);
              }
              else
              {
                obj = Activator.CreateInstance(Type.GetType(classname));
              }
              IDatasetFactory factory = (IDatasetFactory)obj;
              Factories.Add(factory);
            }
          }

          return;
        }
      }
    }
  }
}
