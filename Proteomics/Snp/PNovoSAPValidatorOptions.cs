﻿using RCPA.Proteomics.Summary;
using RCPA.Seq;

namespace RCPA.Proteomics.Snp
{
  public class PNovoSAPValidatorOptions
  {
    public string[] PnovoFiles { get; set; }

    public string TargetFastaFile { get; set; }

    public string DatabaseFastaFile { get; set; }

    public ITitleParser TitleParser { get; set; }

    public IAccessNumberParser AccessNumberParser { get; set; }

    public Protease Enzyme { get; set; }

    public double MinScore { get; set; }

    public int ThreadCount { get; set; }

    public bool IgnoreNtermMutation { get; set; }

    public bool IgnoreDeamidatedMutation { get; set; }

    public bool IgnoreMultipleNucleotideMutation { get; set; }

    public int MinLength { get; set; }

    public string TargetDirectory { get; set; }
  }
}
