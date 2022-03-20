using RCPA.Proteomics.Summary;
using System.Collections.Generic;

namespace RCPA.Proteomics.Quantification
{
  public static class QuantificationConsts
  {
    public static string EXTENDED_KEY = "EXTENDED";

    public static string DUPLICATED_SPECTRA_KEY = "DUPLICATED_SPECTRA";

    public static string DATASET_KEY = "DATASET";

    public static string GetDataset(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(DATASET_KEY))
      {
        return ann.Annotations[DATASET_KEY] as string;
      }

      return "DEFAULT_DATASET";
    }

    public static void SetDataset(this IAnnotation ann, string value)
    {
      ann.Annotations[DATASET_KEY] = value;
    }

    public static bool IsExtendedIdentification(this IAnnotation ann)
    {
      return ann.Annotations.ContainsKey(EXTENDED_KEY) && ann.Annotations[EXTENDED_KEY].Equals(true.ToString());
    }

    public static void SetExtendedIdentification(this IAnnotation ann, bool value)
    {
      ann.Annotations[EXTENDED_KEY] = value.ToString();
    }

    public static List<IIdentifiedSpectrum> GetDuplicatedSpectra(this IAnnotation ann)
    {
      if (ann.Annotations.ContainsKey(DUPLICATED_SPECTRA_KEY))
      {
        return ann.Annotations[DUPLICATED_SPECTRA_KEY] as List<IIdentifiedSpectrum>;
      }

      return new List<IIdentifiedSpectrum>();
    }

    public static void AddDuplicatedSpectrum(this IAnnotation ann, IIdentifiedSpectrum spectrum)
    {
      if (!ann.Annotations.ContainsKey(DUPLICATED_SPECTRA_KEY))
      {
        ann.Annotations[DUPLICATED_SPECTRA_KEY] = new List<IIdentifiedSpectrum>();
      }
      (ann.Annotations[DUPLICATED_SPECTRA_KEY] as List<IIdentifiedSpectrum>).Add(spectrum);
    }
  }
}
