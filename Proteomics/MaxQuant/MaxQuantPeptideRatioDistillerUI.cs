using RCPA.Gui;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RCPA.Proteomics.MaxQuant
{
  public partial class MaxQuantPeptideRatioDistillerUI : AbstractFileProcessorUI
  {
    public static readonly string title = "MaxQuant Peptide File Ratio Distiller";

    public static readonly string version = "1.0.0";

    private RcpaIntegerField minCount;

    public MaxQuantPeptideRatioDistillerUI()
    {
      InitializeComponent();

      base.SetFileArgument("SourceFile", new OpenFileArgument("MaxQuant Peptide", "txt"));

      this.minCount = new RcpaIntegerField(txtCount, "MinCount", "Minimum quantified experiment count", 1, true);
      AddComponent(this.minCount);

      var funcs = new[]{
        new MaxQuantItemFunc(){
           ItemFunc = m => m.Ratio,
            Name = "Ratio H/L"},
        new MaxQuantItemFunc(){
           ItemFunc = m => m.Ratio_Norm,
            Name = "Ratio H/L Normalized"},
        new MaxQuantItemFunc(){
           ItemFunc = m => m.IntensityL,
            Name = "Intensity L"},
        new MaxQuantItemFunc(){
           ItemFunc = m => m.IntensityH,
            Name = "Intensity H"}
      };

      this.lbFuncItems.Items.AddRange(funcs);
      this.lbFuncItems.SetItemChecked(0, true);

      Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      if (lbFuncItems.CheckedItems.Count == 0)
      {
        MessageBox.Show("Select output entries first!");
        return null;
      }

      var checkedFuncs = new List<MaxQuantItemFunc>();
      foreach (MaxQuantItemFunc item in lbFuncItems.CheckedItems)
      {
        checkedFuncs.Add(item);
      }

      return new MaxQuantPeptideRatioDistiller2(this.minCount.Value, checkedFuncs);
    }

    #region Nested type: Command

    public class Command : IToolCommand
    {
      #region IToolCommand Members

      public string GetClassification()
      {
        return MenuCommandType.MaxQuant;
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
        new MaxQuantPeptideRatioDistillerUI().MyShow();
      }
      #endregion
    }
    #endregion
  }
}
