using System;
using System.Collections.Generic;
using System.Text;
using RCPA.Proteomics.Summary;
using RCPA.Utils;

namespace RCPA.Proteomics.Summary
{
  public class IdentifiedProteinTextWriter
  {
    private readonly List<string> _annotationKeys;

    private readonly IEnumerable<IIdentifiedProtein> _proteins;

    private readonly IPropertyConverter<IIdentifiedProtein> converter;

    public IdentifiedProteinTextWriter(IPropertyConverter<IIdentifiedProtein> converter)
    {
      this.converter = converter;
    }

    public IdentifiedProteinTextWriter(string proteinHeader)
    {
      this.converter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(proteinHeader, '\t');
    }

    public IdentifiedProteinTextWriter(string proteinHeader, IEnumerable<IIdentifiedProtein> proteins)
    {
      this._proteins = proteins;
      this._annotationKeys = AnnotationUtils.GetAnnotationKeys(this._proteins);

      var sb = new StringBuilder();
      sb.Append(proteinHeader);
      foreach (string key in this._annotationKeys)
      {
        sb.Append("\t" + key);
      }
      this.converter = IdentifiedProteinPropertyConverterFactory.GetInstance().GetConverters(sb.ToString(), '\t');
    }

    public String GetHeader()
    {
      return this.converter.Name;
    }

    public string GetString(IIdentifiedProtein mp)
    {
      return this.converter.GetProperty(mp);
    }
  }
}