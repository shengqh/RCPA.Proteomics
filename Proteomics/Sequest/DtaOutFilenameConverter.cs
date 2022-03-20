namespace RCPA.Proteomics.Sequest
{
  public class DtaOutFilenameConverter
  {
    public string PureName { get; set; }

    public bool IsZip { get; set; }

    public bool IsDtasOutsFile { get; set; }

    public DtaOutFilenameConverter(string oldName)
    {
      this.PureName = oldName;

      this.IsZip = this.PureName.ToLower().EndsWith(".zip");
      if (this.IsZip)
      {
        this.PureName = FileUtils.RemoveExtension(this.PureName);
      }

      this.IsDtasOutsFile = this.PureName.ToLower().EndsWith(".dtas") || this.PureName.ToLower().EndsWith(".outs");

      if (IsDtasOutsFile)
      {
        this.PureName = FileUtils.RemoveExtension(this.PureName);
      }
    }

    public string GetOutsFilename()
    {
      var result = this.PureName;

      if (this.IsDtasOutsFile)
      {
        result = result + ".outs";
      }

      if (this.IsZip)
      {
        result = result + ".zip";
      }

      return result;
    }

    public string GetDtasFilename()
    {
      var result = this.PureName;

      if (this.IsDtasOutsFile)
      {
        result = result + ".dtas";
      }

      if (this.IsZip)
      {
        result = result + ".zip";
      }

      return result;
    }
  }
}
