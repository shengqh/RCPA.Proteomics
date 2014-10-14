using System.Collections.Generic;
using RCPA.Gui;
using RCPA.Proteomics.Summary;

namespace RCPA.Proteomics.Sequest
{
  public class SequestOutsParser : AbstractSequestSpectrumParser
  {
    private readonly OutParser parser;

    public SequestOutsParser(bool raiseDuplicatedReferenceAbsentException)
    {
      this.parser = new OutParser(raiseDuplicatedReferenceAbsentException);
    }

    public SequestOutsParser(bool raiseDuplicatedReferenceAbsentException, double modificationDeltaScore)
    {
      this.parser = new ModificationOutParser(raiseDuplicatedReferenceAbsentException, modificationDeltaScore);
    }

    public SequestOutsParser() : this(true)
    {
    }

    public SequestOutsParser(double modificationDeltaScore) : this(true, modificationDeltaScore)
    {
    }

    public override List<IIdentifiedSpectrum> ReadFromFile(string fileName)
    {
      var result = new List<IIdentifiedSpectrum>();

      using (var reader = new OutsReader(fileName))
      {
        Progress.SetRange(1, reader.FileCount);

        int count = 0;
        while (reader.HasNext)
        {
          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          List<string> context = reader.NextContent();

          IIdentifiedSpectrum spectrum = this.parser.Parse(context);

          if (null != spectrum)
          {
            result.Add(spectrum);
          }

          Progress.SetPosition(count++);
        }

        Progress.End();
      }

      return result;
    }
  }
}