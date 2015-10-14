namespace MedExam.Common.Interfaces
{
    public interface IPrintService
    {
        void PrintDocuments(ReportFlowBase[] reports, bool isPreview = false, bool withShowDialog = false, string printerName = "");
    }
}