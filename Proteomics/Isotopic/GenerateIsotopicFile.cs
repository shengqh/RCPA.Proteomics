using RCPA.Proteomics.Spectrum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Winista.Text.HtmlParser;
using Winista.Text.HtmlParser.Lex;
using Winista.Text.HtmlParser.Util;

namespace RCPA.Proteomics.Isotopic
{
  public class GenerateIsotopicFile : AbstractThreadFileProcessor
  {
    public class IsotopicAtom
    {
      public string Name { get; set; }

      public List<Peak> Isotopics { get; set; }

      public IsotopicAtom()
      {
        this.Isotopics = new List<Peak>();
      }
    }

    public class NameFilter : NodeFilter
    {
      private string name;
      public NameFilter(string name)
      {
        this.name = name;
      }
      #region NodeFilter Members

      public bool Accept(INode node)
      {
        if (node is ITag)
        {
          var tag = node as ITag;
          if (tag.RawTagName == name)
          {
            return true;
          }
        }

        return false;
      }

      #endregion
    }

    private string url;
    public GenerateIsotopicFile(string url)
    {
      this.url = url;
    }

    public INode FindFirstNode(NodeList parent, string name)
    {
      if (parent == null || parent.Count == 0)
      {
        return null;
      }

      for (int i = 0; i < parent.Count; i++)
      {
        INode node = parent[i];

        var ret = FindFirstNode(node, name);
        if (ret != null)
        {
          return ret;
        }
      }

      return null;
    }

    public INode FindFirstNode(INode parent, string name)
    {
      if (parent == null)
      {
        return null;
      }

      if (parent is ITag)
      {
        var tag = parent as ITag;

        if (!tag.IsEndTag())
        {
          if (tag.RawTagName == name)
          {
            return parent;
          }

          if (parent.FirstChild != null)
          {
            var ret = FindFirstNode(parent.FirstChild, name);
            if (ret != null)
            {
              return ret;
            }
          }
        }
      }

      if (parent.NextSibling != null)
      {
        return FindFirstNode(parent.NextSibling, name);
      }

      return null;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      System.Net.WebClient aWebClient = new System.Net.WebClient();
      aWebClient.Encoding = System.Text.Encoding.Default;
      string html = aWebClient.DownloadString(this.url);

      //string html = File.ReadAllText(fileName);
      Lexer lexer = new Lexer(html);
      Winista.Text.HtmlParser.Parser parser = new Winista.Text.HtmlParser.Parser(lexer);
      NodeList htmlNodes = parser.Parse(null);

      List<IsotopicAtom> atoms = new List<IsotopicAtom>();

      var node = FindFirstNode(htmlNodes, "tbody");
      INode nextNode;
      IsotopicAtom atom = null;
      while ((nextNode = FindFirstNode(node, "tr")) != null)
      {
        if (nextNode.Children != null)
        {
          var tds = nextNode.Children.ExtractAllNodesThatMatch(new NameFilter("td"));
          if (tds.Count == 1)
          {
            atom = null;
          }

          if (tds.Count >= 3 && tds[0].FirstChild != null)
          {
            var t1 = tds[0].FirstChild.GetText().Trim();
            var t2 = tds[1].FirstChild.GetText().Trim();

            if (Char.IsDigit(t1[0]) && Char.IsLetter(t2[0]))
            {
              atom = new IsotopicAtom();
              atoms.Add(atom);

              atom.Name = tds[1].FirstChild.GetText().Trim();
              Peak p = new Peak();
              p.Mz = GetDouble(tds[3]);
              p.Intensity = GetDouble(tds[4]);
              atom.Isotopics.Add(p);
            }
            else if (atom != null)
            {
              var txt = tds[0].FirstChild.GetText().Trim();
              if (txt.Length > 0 && Char.IsLetter(txt[0]))
              {
                tds.Remove(0);
              }

              Peak p = new Peak();
              p.Mz = GetDouble(tds[1]);
              p.Intensity = GetDouble(tds[2]);
              atom.Isotopics.Add(p);
            }
          }
        }

        node = nextNode.NextSibling;
        if (node == null)
        {
          break;
        }
      }

      atoms.ForEach(m => m.Isotopics.RemoveAll(n => n.Intensity == 0.0));

      atoms.RemoveAll(m => m.Isotopics.Count == 0);

      var dic = atoms.ToDictionary(m => m.Name);

      var x = new IsotopicAtom();
      x.Name = "X";
      x.Isotopics.Add(new Peak(1, 0.9));
      x.Isotopics.Add(new Peak(2, 0.1));
      atoms.Insert(0, x);

      atoms.Add(AddHevayAtom("(H2)", "H", 1, dic));
      atoms.Add(AddHevayAtom("(C13)", "C", 1, dic));
      atoms.Add(AddHevayAtom("(N15)", "N", 1, dic));
      atoms.Add(AddHevayAtom("(O18)", "O", 2, dic));

      using (StreamWriter sw = new StreamWriter(fileName))
      {
        atoms.ForEach(m =>
        {
          sw.WriteLine("{0}\t{1}", m.Name, m.Isotopics.Count);
          m.Isotopics.ForEach(n => sw.WriteLine("{0:0.000000}\t{1:0.000000}", n.Mz, n.Intensity));
          sw.WriteLine();
        });
      }
      return new string[] { fileName };
    }

    private IsotopicAtom AddHevayAtom(string name, string monoAtomName, int isotopicIndex, Dictionary<string, IsotopicAtom> dic)
    {
      var result = new IsotopicAtom()
      {
        Name = name
      };
      result.Isotopics.Add(new Peak(dic[monoAtomName].Isotopics[isotopicIndex].Mz, 1.0));

      return result;
    }

    private Regex reg = new Regex(@"([\d.]+)");
    private double GetDouble(INode subNode)
    {
      var txt = subNode.FirstChild.GetText().Trim();
      txt = txt.Replace("&nbsp;", "");
      var m = reg.Match(txt);
      if (m.Success)
      {
        return MyConvert.ToDouble(m.Groups[1].Value);
      }
      else
      {
        return 0.0;
      }
    }
  }
}
