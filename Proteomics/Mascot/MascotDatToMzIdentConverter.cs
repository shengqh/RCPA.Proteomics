using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RCPA.Proteomics.Mascot
{
  public class MascotDatToMzIdentConverter : AbstractThreadProcessor
  {
    protected MascotDatToMzIdentConverterOptions options;

    public MascotDatToMzIdentConverter(MascotDatToMzIdentConverterOptions options)
    {
      this.options = options;
    }

    public override IEnumerable<string> Process()
    {
      var result = new List<string>();
      var titleParser = options.GetTitleParser();
      var parser = new MascotDatSpectrumParser(titleParser);
      foreach (var datFile in options.InputFiles)
      {
        Progress.SetMessage("Parsing the data file : " + datFile + " ...");
        var spectra = parser.ReadFromFile(datFile);
        var proteins = parser.ParseSection(datFile, "proteins");
        var targetFile = options.GetOutputFile(datFile);
        var dbName = "mascot_db";

        using (var sw = new StreamWriter(targetFile, false, System.Text.Encoding.UTF8))
        {
          sw.Write(@"<?xml version=""1.0"" encoding=""utf-8""?>
<MzIdentML id=""report"" version=""1.1.0"" 
  xsi:schemaLocation=""http://psidev.info/psi/pi/mzIdentML/1.1 http://www.psidev.info/files/mzIdentML1.1.0.xsd"" 
  xmlns=""http://psidev.info/psi/pi/mzIdentML/1.1"" 
  xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
  creationDate=""{0:yyyy-MM-dd}T{0:hh:mm:ss}"">
  <cvList xmlns=""http://psidev.info/psi/pi/mzIdentML/1.1"">
    <cv id=""PSI-MS"" uri=""http://psidev.cvs.sourceforge.net/viewvc/*checkout*/psidev/psi/psi-ms/mzML/controlledVocabulary/psi-ms.obo"" version=""3.30.0"" fullName=""PSI-MS"" />
    <cv id=""UNIMOD"" uri=""http://www.unimod.org/obo/unimod.obo"" fullName=""UNIMOD"" />
    <cv id=""UO"" uri=""http://obo.cvs.sourceforge.net/*checkout*/obo/obo/ontology/phenotype/unit.obo"" fullName=""UNIT-ONTOLOGY"" />
  </cvList>
  <AnalysisSoftwareList>
    <AnalysisSoftware id=""AS_mascot_server"" name=""Mascot Server"" version=""2.4.1"" uri=""http://www.matrixscience.com/search_form_select.html"">
      <SoftwareName>
        <cvParam accession=""MS:1001207"" cvRef=""PSI-MS"" name=""Mascot"" />
      </SoftwareName>
    </AnalysisSoftware>
  </AnalysisSoftwareList>
", DateTime.Now);
          foreach (var seqName in proteins.Keys.OrderBy(m => m))
          {
            var description = proteins[seqName].StringAfter(",");
            sw.Write(@"    <DBSequence id={0} searchDatabase_ref=""{1}"" accession={0}>
      <cvParam accession=""MS:1001088"" name=""protein description"" cvRef=""PSI-MS"" value={2} />
    </DBSequence>
", seqName, dbName, description);
          }

        }
        result.Add(targetFile);
      }

      return result;
    }
  }
}
