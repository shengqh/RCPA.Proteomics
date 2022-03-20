using RCPA.Proteomics;
using RCPA.Proteomics.Quantification;
using RCPA.Proteomics.Quantification.Lipid;
using RCPA.Proteomics.Raw;
using RCPA.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace RCPA.Tools.Quantification.Lipid
{
  public class LipidQuantificationProcessor : AbstractThreadFileProcessor
  {
    private string rawFileName;

    private double productIonPPM;

    private double precursorPPM;

    public LipidQuantificationProcessor(string rawFileName, double productIonPPM, double precursorPPM)
    {
      this.rawFileName = rawFileName;
      this.productIonPPM = productIonPPM;
      this.precursorPPM = precursorPPM;
    }

    public override IEnumerable<string> Process(string fileName)
    {
      var result = new List<string>();

      QueryItemListFormat format = new QueryItemListFormat();

      FileInfo rawFile = new FileInfo(rawFileName);

      Progress.SetMessage("Reading query product ion from " + fileName);
      var queryItems = format.ReadFromFile(fileName);

      using (IRawFile reader = RawFileFactory.GetRawFileReader(rawFileName))
      {
        reader.Open(rawFileName);

        int firstSpectrumNumber = reader.GetFirstSpectrumNumber();

        LipidPrecursorQuery queryFunc = new LipidPrecursorQuery(reader);
        queryFunc.Progress = this.Progress;

        char[] chars = new char[] { '\t' };

        int curIndex = 0;
        foreach (var query in queryItems)
        {
          Progress.SetMessage(MyConvert.Format("{0}/{1} - Querying precursor ions for product ion {2} ...", ++curIndex, queryItems.Count, query.ProductIonMz));

          string savedQueryFile = GetQueryFileName(rawFileName, query.ProductIonMz, productIonPPM, precursorPPM);

          PrecursorItemListXmlFormat pilFormat = new PrecursorItemListXmlFormat();
          List<PrecursorItem> precursors;
          if (!File.Exists(savedQueryFile))
          {
            precursors = queryFunc.QueryPrecursorFromProductIon(query, productIonPPM, precursorPPM);
            pilFormat.WriteToFile(savedQueryFile, precursors);
          }
          else
          {
            precursors = pilFormat.ReadFromFile(savedQueryFile);
          }

          result.Add(savedQueryFile);


          var precursorMzs = (from item in precursors
                              group item by item.PrecursorMZ into mzGroup
                              let count = mzGroup.Where(m => m.PrecursorIntensity > 0).Count()
                              orderby count descending
                              select new PrecursorArea { PrecursorMz = mzGroup.Key, ScanCount = count, Area = 0.0 }).ToList();

          //去掉冗余的precursor
          for (int i = precursorMzs.Count - 1; i >= 0; i--)
          {
            if (precursorMzs[i].ScanCount >= 5)
            {
              break;
            }

            for (int j = 0; j < i; j++)
            {
              if (precursorMzs[j].ScanCount < 5)
              {
                break;
              }

              double ppm = PrecursorUtils.mz2ppm(precursorMzs[i].PrecursorMz, Math.Abs(precursorMzs[i].PrecursorMz - precursorMzs[j].PrecursorMz));
              if (ppm < precursorPPM)
              {
                precursorMzs.RemoveAt(i);
                break;
              }
            }
          }

          string savedDetailDir = FileUtils.ChangeExtension(savedQueryFile, "");
          if (!Directory.Exists(savedDetailDir))
          {
            Directory.CreateDirectory(savedDetailDir);
          }

          for (int i = 0; i < precursorMzs.Count; i++)
          {
            Progress.SetMessage(MyConvert.Format("{0}/{1} {2} - {3}/{4} Get chromotograph for precursor {5} ...", curIndex, queryItems.Count, query.ProductIonMz, i + 1, precursorMzs.Count, precursorMzs[i].PrecursorMz));

            string targetFile = savedDetailDir + "\\" + GetDetailFileName(rawFileName, precursorMzs[i].PrecursorMz);

            var itemFormat = new LabelFreeSummaryItemXmlFormat();

            LabelFreeSummaryItem item;
            if (File.Exists(targetFile))
            {
              item = itemFormat.ReadFromFile(targetFile);
            }
            else
            {
              var ions = queryFunc.QueryChromotograph(precursorMzs[i].PrecursorMz, precursorPPM);

              int continueCount = 0;
              int firstIndex = -1;
              for (int j = 0; j < ions.Count; j++)
              {
                if (ions[j].Intensity > 0)
                {
                  if (firstIndex == -1)
                  {
                    firstIndex = j;
                    continueCount = 1;
                  }
                  else
                  {
                    continueCount++;
                    if (continueCount >= 5)
                    {
                      break;
                    }
                  }
                }
                else
                {
                  firstIndex = -1;
                  continueCount = 0;
                }
              }

              if (continueCount >= 5)
              {
                ions.RemoveRange(0, firstIndex);
              }

              continueCount = 0;
              int lastIndex = -1;
              for (int j = ions.Count - 1; j >= 0; j--)
              {
                if (ions[j].Intensity > 0)
                {
                  if (lastIndex == -1)
                  {
                    lastIndex = j;
                    continueCount = 1;
                  }
                  else
                  {
                    continueCount++;
                    if (continueCount >= 5)
                    {
                      break;
                    }
                  }
                }
                else
                {
                  lastIndex = -1;
                  continueCount = 0;
                }
              }

              if (continueCount >= 5)
              {
                ions.RemoveRange(lastIndex + 1, ions.Count - lastIndex - 1);
              }

              //get full ms corresponding to identified ms/ms
              var identified = new HashSet<int>();
              foreach (var p in precursors)
              {
                if (p.PrecursorMZ == precursorMzs[i].PrecursorMz)
                {
                  for (int j = p.Scan - 1; j >= firstSpectrumNumber; j--)
                  {
                    if (reader.GetMsLevel(j) == 1)
                    {
                      identified.Add(j);
                      break;
                    }
                  }
                }
              }

              ions.ForEach(m =>
              {
                m.Identified = identified.Contains(m.Scan);
                m.Enabled = true;
              });

              Debug.Assert(ions.FindAll(m => m.Identified == true).Count == identified.Count);

              item = new LabelFreeSummaryItem()
              {
                RawFilename = rawFileName,
                Sequence = MyConvert.Format("{0:0.0000}; {1:0.00}", query.ProductIonMz, query.MinRelativeIntensity)
              };
              item.AddRange(ions);
              item.CalculatePPM(precursorMzs[i].PrecursorMz);

              new LabelFreeSummaryItemXmlFormat().WriteToFile(targetFile, item);
            }

            precursorMzs[i].Area = item.GetArea();
            string savedAreaFile = GetAreaFileName(rawFileName, query.ProductIonMz, productIonPPM, precursorPPM);
            new PrecursorAreaListTextFormat().WriteToFile(savedAreaFile, precursorMzs);
          }
        }

        return result;
      }
    }

    public static string GetQueryFileName(string rawFileName, double productIonMz, double productIonPPM, double precursorPPM)
    {
      return MyConvert.Format("{0}_{1:0.0000}_{2:0.#}ppm_{3:0.#}ppm.precursors", rawFileName, productIonMz, productIonPPM, precursorPPM);
    }

    public static string GetAreaFileName(string rawFileName, double productIonMz, double productIonPPM, double precursorPPM)
    {
      return MyConvert.Format("{0}_{1:0.0000}_{2:0.#}ppm_{3:0.#}ppm.area", rawFileName, productIonMz, productIonPPM, precursorPPM);
    }

    public static string GetDetailFileName(string rawFileName, double precursorMz)
    {
      return MyConvert.Format("{0}_{1:0.0000}.txt", new FileInfo(rawFileName).Name, precursorMz);
    }
  }
}
