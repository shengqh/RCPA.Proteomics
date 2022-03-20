using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinGroupEnumerator : IEnumerator<IIdentifiedProteinGroup>
  {
    private Regex groupReg = new Regex(@"^\$(\d+)-");

    private Regex containGroupReg = new Regex(@"@(\d+)-");

    private StreamReader br;

    private IIdentifiedProteinGroup current;

    private String lastLine;

    public IdentifiedProteinGroupEnumerator(string filename, bool ignoreFasta = false)
    {
      if (!File.Exists(filename))
      {
        throw new FileNotFoundException("File not exist : " + filename);
      }

      string fastaFile = filename + ".fasta";
      if (!File.Exists(fastaFile) && !ignoreFasta)
      {
        throw new FileNotFoundException("File not exist : " + fastaFile);
      }

      br = new StreamReader(filename);

      Reset();
    }

    private LineFormat<IIdentifiedProtein> proteinFormat;
    public LineFormat<IIdentifiedProtein> ProteinFormat
    {
      get { return proteinFormat; }
    }

    private LineFormat<IIdentifiedSpectrum> peptideFormat;
    public LineFormat<IIdentifiedSpectrum> PeptideFormat
    {
      get { return peptideFormat; }
    }

    #region IEnumerator<IIdentifiedProteinGroup> Members

    public IIdentifiedProteinGroup Current
    {
      get { return current; }
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      br.Close();
    }

    #endregion

    #region IEnumerator Members

    object System.Collections.IEnumerator.Current
    {
      get { return current; }
    }

    public bool MoveNext()
    {
      bool bParsingProtein = true;
      current = new IdentifiedProteinGroup();
      while (lastLine != null)
      {
        if (0 == lastLine.Trim().Length)
        {
          break;
        }

        if (lastLine.StartsWith("$"))
        {
          if (!bParsingProtein)
          {
            break;
          }

          IIdentifiedProtein protein = proteinFormat.ParseString(lastLine);

          Match m = groupReg.Match(lastLine);
          if (!m.Success)
          {
            throw new Exception("Error when paring group index from " + lastLine);
          }

          protein.GroupIndex = int.Parse(m.Groups[1].Value);

          current.Add(protein);
        }
        else if (lastLine.StartsWith("@"))
        {
          continue;
        }
        else
        {
          bParsingProtein = false;
          IIdentifiedSpectrum pephit = peptideFormat.ParseString(lastLine);
          current.AddIdentifiedSpectrum(pephit);
        }

        lastLine = br.ReadLine();
      }

      if (current.Count > 0)
      {
        return true;
      }
      else
      {
        return false;
      }
    }

    public void Reset()
    {
      br.DiscardBufferedData();
      br.BaseStream.Seek(0, SeekOrigin.Begin);

      lastLine = br.ReadLine();
      proteinFormat = new LineFormat<IIdentifiedProtein>(IdentifiedProteinPropertyConverterFactory.GetInstance(), lastLine);

      lastLine = br.ReadLine();
      peptideFormat = new LineFormat<IIdentifiedSpectrum>(IdentifiedSpectrumPropertyConverterFactory.GetInstance(), lastLine);

      while ((lastLine = br.ReadLine()) != null)
      {
        if (lastLine.StartsWith("$"))
        {
          break;
        }
      }
    }

    #endregion
  }
}
