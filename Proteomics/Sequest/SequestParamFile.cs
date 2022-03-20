namespace RCPA.Proteomics.Sequest
{
  public class SequestParamFile : ISequestParamFile
  {
    #region ISequestParamFile Members

    public SequestParam ReadFromFile(string paramFilename)
    {
      return Guess(paramFilename).ReadFromFile(paramFilename);
    }

    #endregion

    private static ISequestParamFile Guess(string paramFilename)
    {
      using (var sr = StreamUtils.GetParameterFileStream(paramFilename))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          if (line.StartsWith("enzyme_info"))
          {
            return new SequestParamFile32();
          }

          if (line.StartsWith("[SEQUEST_ENZYME_INFO]"))
          {
            return new SequestParamFile31();
          }

          if (line.StartsWith("[SEQUEST_OUT]"))
          {
            break;
          }
        }
      }

      return new SequestParamFile31();
    }
  }
}