using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RCPA.Proteomics.Raw
{
  public interface IScanLevelBuilder
  {
    List<ScanLevel> GetScanLevels(IRawFile rawFile);
  }

  public static class IScanLevelBuilderExtension
  {
    public static List<ScanLevel> GetScanLevels(this IScanLevelBuilder builder, string fileName)
    {
      using (var reader = RawFileFactory.GetRawFileReader(fileName))
      {
        return builder.GetScanLevels(reader);
      }
    }
  }
}
