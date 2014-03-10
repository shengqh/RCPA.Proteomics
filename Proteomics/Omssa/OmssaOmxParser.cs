using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RCPA.Gui;
using RCPA.Proteomics.Summary;
using System.Xml;
using RCPA.Seq;
using RCPA.Utils;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Omssa
{
  public class OmssaOmxParser : ProgressClass
  {
    private ITitleParser parser;

    private IAccessNumberParser acParser;

    public OmssaOmxParser(ITitleParser parser, IAccessNumberParser acParser)
    {
      if (parser == null)
      {
        throw new ArgumentNullException("parser");
      }

      if (acParser == null)
      {
        throw new ArgumentNullException("acParser");
      }

      this.parser = parser;
      this.acParser = acParser;
    }

    /// <summary>
    /// 
    /// Get top one peptide list from OMSSA omx file
    /// 
    /// </summary>
    /// <param name="xmlFilename">xml filename</param>
    /// <returns>List of IIdentifiedSpectrum</returns>
    public List<IIdentifiedSpectrum> ParsePeptides(string xmlFilename)
    {
      List<IIdentifiedSpectrum> result = new List<IIdentifiedSpectrum>();

      //XmlDocument doc = new XmlDocument();
      //doc.Load(xmlFilename);

      //this.xmlHelper = new XmlHelper(doc);

      //XmlNode root = doc.DocumentElement;

      //string sourceFilename = GetSourceFilename(root);
      //Match mSource = Regex.Match(sourceFilename, @"(.+)\.(?:RAW)", RegexOptions.IgnoreCase);
      //if (mSource.Success)
      //{
      //  sourceFilename = mSource.Groups[1].Value;
      //}
      //else
      //{
      //  mSource = Regex.Match(sourceFilename, @"(.+?)\.");
      //  if (mSource.Success)
      //  {
      //    sourceFilename = mSource.Groups[1].Value;
      //  }
      //}

      //XmlNode parameters = xmlHelper.GetFirstChildByNameAndAttribute(root, "group", "label", "input parameters");
      //ParseParameters(parameters);

      //string rawFilePath = GetSourceFilename(root);
      //int pos = rawFilePath.LastIndexOfAny(new char[] { '/', '\\' });

      return result;
    }
  }
}
