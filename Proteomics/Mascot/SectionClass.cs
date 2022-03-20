namespace RCPA.Proteomics.Mascot
{
  public class SectionClass
  {
    private string section;

    public SectionClass(string sectionName)
    {
      this.section = "name=\"" + sectionName + "\"";
    }

    public bool IsStartLine(string line)
    {
      return line.StartsWith("Content-Type") && line.Contains(section);
    }
  }
}
