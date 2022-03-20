using RCPA.Proteomics.Summary;
using System.Linq;

namespace RCPA.Proteomics
{
  public interface IProtease
  {
    bool IsSemiSpecific { get; set; }

    string CleaveageResidues { get; }

    string Name { get; }

    string NotCleaveResidues { get; }

    bool IsEndoProtease { get; }

    bool IsCleavageSite(char firstChar, char secondChar, char terminalChar);

    int GetMissCleavageSiteCount(string sequence);

    int GetProteaseTerminiCount(char beforeChar, string sequence, char afterChar, char terminalChar);
  }

  public static class ProteaseExtensions
  {
    public static int GetNumProteaseTermini(this IProtease protease, char beforeChar, string pureSeq, char afterChar, char terminalChar, int positionInProtein)
    {
      int result = 0;
      if (beforeChar == 'M' && positionInProtein == 2)
      {
        result++;
      }
      else if (protease.IsCleavageSite(beforeChar, pureSeq[0], terminalChar))
      {
        result++;
      }

      if (protease.IsCleavageSite(pureSeq[pureSeq.Length - 1], afterChar, terminalChar))
      {
        result++;
      }

      return result;
    }

    public static void AssignNumProteaseTermini(this IProtease protease, IIdentifiedSpectrum s)
    {
      var counts = (from p in s.Peptides
                    let beforeChar = p.Sequence[0]
                    let afterChar = p.Sequence[p.Sequence.Length - 1]
                    let c = protease.GetProteaseTerminiCount(beforeChar, p.PureSequence, afterChar, '-')
                    select c).Distinct().ToList();

      s.NumProteaseTermini = counts.Max();
    }
  }
}