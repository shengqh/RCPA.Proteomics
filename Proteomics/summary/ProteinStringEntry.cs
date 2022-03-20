﻿using System.Collections.Generic;

namespace RCPA.Proteomics.Summary
{
  public class ProteinGroupEntry
  {
    private List<string> proteins = new List<string>();

    public List<string> Proteins { get { return proteins; } }

    private List<string> peptides = new List<string>();

    public List<string> Peptides { get { return peptides; } }
  }
}
