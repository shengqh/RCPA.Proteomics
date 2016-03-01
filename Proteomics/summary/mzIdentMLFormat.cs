using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using System.Xml;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public class mzIdentMLFormat : ProgressClass, IFileFormat<IIdentifiedResult>
  {
    private string softname;
    private string softversion;

    public mzIdentMLFormat(string name, string version)
    {
      this.softname = name;
      this.softversion = version;
    }

    #region IFileReader<IIdentifiedResult> Members

    public IIdentifiedResult ReadFromFile(string fileName)
    {
      throw new NotImplementedException();
    }

    #endregion

    #region IFileWriter<IIdentifiedResult> Members

    public void WriteToFile(string fileName, IIdentifiedResult t)
    {
      Dictionary<string, string> engineMap = new Dictionary<string, string>();

      using (var w = XmlUtils.CreateWriter(fileName))
      {
        StartDocument(w);

        WriteCvList(w);

        WriteSoftwareList(w, t, engineMap);

        EndDocument(w);
      }
    }

    private void WriteSoftwareList(XmlTextWriter w, IIdentifiedResult t, Dictionary<string, string> engineMap)
    {
      w.WriteStartElement("AnalysisSoftwareList");

      var engines = (from g in t
                     from p in g
                     from pep in p.Peptides
                     select pep.Spectrum.Engine).Distinct().ToList();

      Dictionary<string, string> map = new Dictionary<string, string>();

      engines.ForEach(m => map[m] = "unknown");
      map[softname] = softversion;

      foreach (var item in map)
      {
        var name = item.Key;
        var version = item.Value;

        var id = name.Replace(' ', '_').Replace('\t', '_');
        engineMap[name] = id;

        w.WriteStartElement("AnalysisSoftware");
        w.WriteAttribute("id", id);
        w.WriteAttribute("name", name);
        w.WriteAttribute("version", version);

        w.WriteStartElement("SoftwareName");
        WriteCvParam(w, GetAccessionOfSoftware(name), name);
        w.WriteEndElement();

        w.WriteEndElement();
      }

      w.WriteEndElement();

    }

    private string GetAccessionOfSoftware(string engine)
    {
      return "MS:1001456";
    }

    private static void WriteCvParam(XmlTextWriter w, string accession, string name)
    {
      w.WriteStartElement("cvParam");
      w.WriteAttribute("accession", accession);
      w.WriteAttribute("name", name);
      w.WriteAttribute("cvRef", "PSI-MS");
      w.WriteEndElement();
    }

    private static void WriteCvList(XmlTextWriter w)
    {
      w.WriteStartElement("cvList");
      w.WriteAttribute("xmlns", "http://psidev.info/psi/pi/mzIdentML/1.1");

      w.WriteStartElement("cv");
      w.WriteAttribute("id", "PSI-MS");
      w.WriteAttribute("uri", "http://psidev.cvs.sourceforge.net/viewvc/*checkout*/psidev/psi/psi-ms/mzML/controlledVocabulary/psi-ms.obo");
      w.WriteAttribute("version", "2.25.0");
      w.WriteAttribute("fullName", "PSI-MS");
      w.WriteEndElement();

      w.WriteStartElement("cv");
      w.WriteAttribute("id", "UNIMOD");
      w.WriteAttribute("uri", "http://www.unimod.org/obo/unimod.obo");
      w.WriteAttribute("fullName", "UNIMOD");
      w.WriteEndElement();

      w.WriteEndElement();

    }

    private static void StartDocument(XmlTextWriter w)
    {
      w.WriteStartDocument();
      w.WriteStartElement("MzIdentML");
      w.WriteAttribute("id", 1);
      w.WriteAttribute("version", "1.1.0");
      w.WriteAttribute("xmlns", "http://psidev.info/psi/pi/mzIdentML/1.1");
      w.WriteAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
      w.WriteAttribute("xsi:schemaLocation", "http://www.psidev.info/files/mzIdentML1.1.0.xsd");
      w.WriteAttributeFormat("creationDate", "{0:s}", DateTime.Now);
    }

    private static void EndDocument(XmlTextWriter w)
    {
      w.WriteEndElement();
      w.WriteEndDocument();
    }

    #endregion
  }
}
