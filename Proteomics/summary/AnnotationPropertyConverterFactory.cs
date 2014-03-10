using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RCPA.Gui;
using RCPA.Proteomics.PropertyConverter;
using RCPA.Proteomics.PropertyConverter.Mascot;
using RCPA.Proteomics.Summary;
using RCPA.Utils;
using RCPA.Converter;

namespace RCPA.Proteomics.Summary
{
  public class AnnotationPropertyConverterFactory : AbstractPropertyConverterFactory<IAnnotation>
  {
    private HashSet<string> allowedHeaders;

    public AnnotationPropertyConverterFactory(IEnumerable<string> allowedHeaders)
    {
      this.allowedHeaders = new HashSet<string>(allowedHeaders);
    }

    public AnnotationPropertyConverterFactory()
    {
      this.allowedHeaders = null;
    }

    private bool IsAllowed(string name)
    {
      return allowedHeaders == null || allowedHeaders.Contains(name);
    }
    public override IPropertyConverter<IAnnotation> FindConverter(string name, string version)
    {
      if (IsAllowed(name))
      {
        return new AnnotationConverter<IAnnotation>(name);
      }
      else
      {
        return new EmptyConverter<IAnnotation>();
      }
    }

    public override IPropertyConverter<IAnnotation> GetConverters(string header, char delimiter, string version)
    {
      var parts = header.Split(new char[] { delimiter });

      var result = (from p in parts
                    select FindConverter(p, version)).ToList();

      return new CompositePropertyConverter<IAnnotation>(result, delimiter);
    }

    public override IPropertyConverter<IAnnotation> GetConverters(string header, char delimiter, string version, List<IAnnotation> items)
    {
      return GetConverters(header, delimiter, version); 
    }

    public override IAnnotation Allocate()
    {
      return new Annotation();
    }
  }

  public class AnnotationTextFormat : ProgressClass, IFileFormat<List<IAnnotation>>
  {
    public LineFormat<IAnnotation> Format { get; set; }

    private AnnotationPropertyConverterFactory factory;

    public AnnotationTextFormat(string[] allowedHeaders)
    {
      factory = new AnnotationPropertyConverterFactory(allowedHeaders);
    }

    public AnnotationTextFormat()
    {
      factory = new AnnotationPropertyConverterFactory();
    }

    #region IFileReader<List<IAnnotation>> Members

    public List<IAnnotation> ReadFromFile(string fileName)
    {
      if (!File.Exists(fileName))
      {
        throw new FileNotFoundException("File not exist : " + fileName);
      }

      var result = new List<IAnnotation>();

      long fileSize = new FileInfo(fileName).Length;

      Progress.SetRange(0, fileSize);

      char[] cs = new char[] { '\t' };
      using (var br = new StreamReader(fileName))
      {
        String line = br.ReadLine();

        this.Format = new LineFormat<IAnnotation>(factory, line);

        int lineCount = 0;
        while ((line = br.ReadLine()) != null)
        {
          if (0 == line.Trim().Length)
          {
            break;
          }

          if (Progress.IsCancellationPending())
          {
            throw new UserTerminatedException();
          }

          lineCount++;
          if (lineCount == 100)
          {
            lineCount = 0;
            Progress.SetPosition(br.GetCharpos());
          }

          result.Add(this.Format.ParseString(line));
        }
      }

      return result;
    }

    #endregion

    #region IFileWriter<List<IAnnotation>> Members

    public void WriteToFile(string fileName, List<IAnnotation> t)
    {
      if (this.Format == null)
      {
        var line = (from ann in t
                    from key in ann.Annotations.Keys
                    select key).Distinct().ToList();

        this.Format = new LineFormat<IAnnotation>(new AnnotationPropertyConverterFactory(), line.Merge('\t'));
      }

      using (StreamWriter sw = new StreamWriter(fileName))
      {
        sw.WriteLine(this.Format.GetHeader());
        t.ForEach(m => sw.WriteLine(this.Format.GetString(m)));
      }
    }

    #endregion
  }
}
