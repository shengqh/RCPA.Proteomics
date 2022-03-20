using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System;
using System.Collections.Generic;

namespace RCPA.Proteomics.Database
{
  public partial class TheoreticalDigestionStatisticCalculatorUI : AbstractFileProcessorUI
  {
    public static readonly string title = "Theoretical Digestion Statistic Calculator";

    public static readonly string version = "1.0.0";

    private RcpaDoubleField minMass, maxMass;

    private RcpaIntegerField minLength, maxMissCleavage;

    private RcpaTextField ignoreAminoacids;

    public TheoreticalDigestionStatisticCalculatorUI()
    {
      InitializeComponent();

      SetFileArgument("FastaFile", new OpenFileArgument("Protein Sequence", "fasta"));

      lbProteases.Items.AddRange(ProteaseManager.GetNames().ToArray());

      AddComponent(lbSelectedProteases);

      this.minLength = new RcpaIntegerField(txtMinLength, "minlength", "Minimum peptide length", 6, true);
      AddComponent(minLength);

      this.maxMissCleavage = new RcpaIntegerField(txtMaxMissCleavage, "maxmisscleavage", "Maximum miss cleavage", 2, true);
      AddComponent(maxMissCleavage);

      this.minMass = new RcpaDoubleField(txtMinMass, "minmass", "Minimum peptide mass", 400, true);
      AddComponent(minMass);

      this.maxMass = new RcpaDoubleField(txtMaxMass, "maxmass", "Maximum peptide mass", 5000, true);
      AddComponent(maxMass);

      ignoreAminoacids = new RcpaTextField(txtIgnoreAminoacids, "ignoreaminoacids", "Ignore amino acids", "XB", true);
      AddComponent(ignoreAminoacids);

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      var proteases = new List<Protease>();
      foreach (var obj in lbSelectedProteases.Items)
      {
        proteases.Add(ProteaseManager.GetProteaseByName((string)obj));
      }

      return new TheoreticalDigestionStatisticCalculator(
        proteases,
        minLength.Value,
        minMass.Value,
        maxMass.Value,
        maxMissCleavage.Value,
        ignoreAminoacids.Text);
    }

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.Database;
      }

      public string GetCaption()
      {
        return title;
      }

      public string GetVersion()
      {
        return version;
      }

      public void Run()
      {
        new TheoreticalDigestionStatisticCalculatorUI().MyShow();
      }

      #endregion
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      foreach (var item in lbProteases.SelectedItems)
      {
        if (!lbSelectedProteases.Items.Contains(item))
        {
          lbSelectedProteases.ListBoxItems.Add(item);
        }
      }
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      lbSelectedProteases.RemoveSelectItems();
    }
  }
}

