using RCPA.Gui;
using RCPA.Proteomics.Summary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RCPA.Tools.Modification
{
  public partial class AbstractMultipleLabellingSeparatorUI : AbstractFileProcessorUI
  {
    private static readonly string DEFAULT_PATTERN = "(K# R~ : K4R6) (K@ R^ : K8R10) (DEFAULT : K0R0)";

    private RcpaTextField modifiedAminoacids;

    public AbstractMultipleLabellingSeparatorUI()
    {
      InitializeComponent();

      this.modifiedAminoacids = new RcpaTextField(txtModifiedAminoacids, "ModifiedAminoacids", "Modified Aminoacids", DEFAULT_PATTERN, true);
      this.AddComponent(this.modifiedAminoacids);
    }

    protected Dictionary<IFilter<IIdentifiedSpectrum>, string> GetFilterMap()
    {
      Dictionary<IFilter<IIdentifiedSpectrum>, string> map = new Dictionary<IFilter<IIdentifiedSpectrum>, string>();

      Regex reg = new Regex(@"\(([^()]+?)\:([^()]+?)\)");

      List<IFilter<IIdentifiedSpectrum>> matchedFilters = new List<IFilter<IIdentifiedSpectrum>>();

      string defaultTitle = null;

      Match match = reg.Match(modifiedAminoacids.Text);
      if (!match.Success)
      {
        throw new Exception("Couldn't recognize modification pattern, it should like \n" + DEFAULT_PATTERN);
      }

      while (match.Success)
      {
        if (match.Groups[1].Value.Trim().Equals("DEFAULT"))
        {
          defaultTitle = match.Groups[2].Value.Trim();
        }
        else
        {
          string title = match.Groups[2].Value.Trim();

          string[] parts = Regex.Split(match.Groups[1].Value, @"\s+");

          List<IFilter<IIdentifiedSpectrum>> filters = new List<IFilter<IIdentifiedSpectrum>>();

          foreach (string part in parts)
          {
            if (part.Length > 0)
            {
              filters.Add(new IdentifiedSpectrumSequenceFilter(part));
            }
          }

          OrFilter<IIdentifiedSpectrum> filter = new OrFilter<IIdentifiedSpectrum>(filters);

          map[filter] = title;

          matchedFilters.Add(filter);
        }
        match = match.NextMatch();
      }

      if (defaultTitle == null)
      {
        throw new Exception("No default name defined, the pattern should like \n" + DEFAULT_PATTERN);
      }

      NotFilter<IIdentifiedSpectrum> notFilter = new NotFilter<IIdentifiedSpectrum>(new OrFilter<IIdentifiedSpectrum>(matchedFilters));
      map[notFilter] = defaultTitle;
      return map;
    }
  }
}
