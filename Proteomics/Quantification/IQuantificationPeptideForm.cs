namespace RCPA.Proteomics.Quantification
{
  public interface IQuantificationPeptideForm
  {
    UpdateEvent UpdateParent { get; set; }

    void Show();

    void SetSummaryFilename(string summaryFilename, IAnnotation parentObj);

    void BringToFront();

    bool IsDisposed { get; }
  }
}
