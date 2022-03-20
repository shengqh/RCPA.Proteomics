using CommandLine;
using RCPA.Commandline;
using RCPA.Seq;
using System.IO;

namespace RCPA.Proteomics.Database
{
  public class DecoyType
  {
    public string Name { get; private set; }
    public string Description { get; private set; }

    private DecoyType(string name, string description)
    {
      this.Name = name;
      this.Description = description;
    }

    public override string ToString()
    {
      return Name + " : " + Description;
    }

    public static DecoyType Start = new DecoyType("Start", "Add decoy key in start of protein name and start of protein description");
    public static DecoyType Middle = new DecoyType("Middle", "Add decoy key after '|' or ':' of protein name and start of protein description");
    public static DecoyType Index = new DecoyType("Index", "Replace protein name with decoy key and index, no description");

    public static DecoyType[] Items = new[] { Start, Middle, Index };
  }

  public class ReversedDatabaseBuilderOptions : AbstractOptions
  {
    [Option('r', "reversedOnly", Required = true, MetaValue = "BOOL", HelpText = "Output reversed only")]
    public bool ReversedOnly { get; set; }

    public bool IsPseudoAminoacid { get; set; }

    public string PseudoAminoacids { get; set; }

    public bool IsPseudoForward { get; set; }

    public string ContaminantFile { get; set; }

    [Option('i', "inputFile", Required = true, MetaValue = "FILE", HelpText = "Input fasta file")]
    public string InputFile { get; set; }

    [Option('o', "outputFile", Required = true, MetaValue = "FILE", HelpText = "Output fasta file")]
    public string OutputFile { get; set; }

    public string DecoyKey { get; set; }

    public DecoyType DecoyType { get; set; }

    public PseudoSequenceBuilder PseudoAminoacidBuilder { get; private set; }

    public ReversedDatabaseBuilderOptions()
    {
      ReversedOnly = false;
      IsPseudoAminoacid = true;
      PseudoAminoacids = "KR";
      IsPseudoForward = false;
      ContaminantFile = string.Empty;
      PseudoAminoacidBuilder = null;
      this.DecoyKey = "REVERSED";
      this.DecoyType = DecoyType.Start;
    }

    public override bool PrepareOptions()
    {
      if (!string.IsNullOrWhiteSpace(ContaminantFile) && !File.Exists(ContaminantFile))
      {
        ParsingErrors.Add(string.Format("Contaminant file not exists: {0}", ContaminantFile));
      }

      if (!File.Exists(InputFile))
      {
        ParsingErrors.Add(string.Format("Input file not exists: {0}", InputFile));
      }

      if (string.IsNullOrWhiteSpace(this.OutputFile))
      {
        if (ReversedOnly)
        {
          OutputFile = FileUtils.ChangeExtension(InputFile, this.DecoyKey + "_ONLY.fasta");
        }
        else
        {
          OutputFile = FileUtils.ChangeExtension(InputFile, this.DecoyKey + ".fasta");
        }
      }

      if (IsPseudoAminoacid)
      {
        PseudoAminoacidBuilder = new PseudoSequenceBuilder(PseudoAminoacids, IsPseudoForward);
      }

      return ParsingErrors.Count == 0;
    }
  }
}
