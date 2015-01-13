using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace RCPA.Proteomics.Modification
{
  public static class ModificationUtils
  {
    /// <summary>
    /// 从sequest的out文件中解析修饰定义
    /// </summary>
    /// <param name="line">out文件中包含修饰定义的一行，如下：
    /// <example>(STY* +79.96633) (M# +15.99492) (ST@ -18.00000) C=160.16523  Enzyme:Trypsin(KR) (1)</example>
    /// </param>
    /// <returns></returns>
    public static Dictionary<char, double> ParseFromOutFileLine(string line)
    {
      var result = new Dictionary<char, double>();

      var staticMod = new Regex(@"([A-Z])=(\d+\.{0,1}\d+)");
      var dynamicMod = new Regex(@"\(\S+(\S)\s([\+|-]\d+\.{0,1}\d+)\)");

      Match staticMatch = staticMod.Match(line);
      while (staticMatch.Success)
      {
        result[staticMatch.Groups[1].Value[0]] = MyConvert.ToDouble(staticMatch.Groups[2].Value);
        staticMatch = staticMatch.NextMatch();
      }

      Match dynamicMatch = dynamicMod.Match(line);
      while (dynamicMatch.Success)
      {
        result[dynamicMatch.Groups[1].Value[0]] = MyConvert.ToDouble(dynamicMatch.Groups[2].Value);
        dynamicMatch = dynamicMatch.NextMatch();
      }

      return result;
    }

    /// <summary>
    /// 判断给定字符是否表示修饰。当且仅当给定字符为大写字母（A-Z），才不会被判定为非修饰。也就是说，当给定一个
    /// 特殊字符，例如*,#等，或者给定一个小写字母（a,l,往往代表重标），都会被判定为修饰。
    /// </summary>
    /// <param name="c">给定字符</param>
    /// <returns>是否修饰字符</returns>
    public static bool IsModification(char c)
    {
      return !Char.IsUpper(c);
    }

    public static bool IsModifiedSequence(string sequence, string modifiedAminoacids)
    {
      for (int i = 0; i < sequence.Length; i++)
      {
        if (IsModification(sequence[i]))
        {
          char c = char.IsLetter(sequence[i]) ? char.ToUpper(sequence[i]) : sequence[i - 1];
          if (modifiedAminoacids.IndexOf(c) != -1)
          {
            return true;
          }
        }
      }

      return false;
    }

  }
}