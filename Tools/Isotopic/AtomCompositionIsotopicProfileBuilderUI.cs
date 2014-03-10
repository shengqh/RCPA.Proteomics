using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RCPA.Proteomics.Mascot;
using RCPA.Gui.Command;
using RCPA.Gui.FileArgument;
using RCPA.Gui;

namespace RCPA.Tools.Isotopic
{
  public partial class AtomCompositionIsotopicProfileBuilderUI : AbstractFileProcessorUI
  {
    public const string title = "Isotopic Profile Builder";
    public const string version = "1.0.0";

    public AtomCompositionIsotopicProfileBuilderUI()
    {
      InitializeComponent();

      base.SetFileArgument("AtomCompositionFile",new OpenFileArgument ("Atom Composition", "atomcomposition"));

      this.Text = Constants.GetSQHTitle(title, version);
    }

    protected override IFileProcessor GetFileProcessor()
    {
      return new AtomCompositionIsotopicProfileBuilder();
    }
  }

  public class AtomCompositionIsotopicProfileBuilderCommand : IToolCommand
  {
    #region IToolCommand Members

    public string GetClassification()
    {
      return "Isotopic";
    }

    public string GetCaption()
    {
      return AtomCompositionIsotopicProfileBuilderUI.title;
    }

    public string GetVersion()
    {
      return AtomCompositionIsotopicProfileBuilderUI.version;
    }

    public void Run()
    {
      new AtomCompositionIsotopicProfileBuilderUI().MyShow();
    }

    #endregion
  }
}

