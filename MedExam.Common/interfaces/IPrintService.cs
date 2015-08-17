namespace MedExam.Common.interfaces
{
    public interface IPrintService
    {
        void PrintDocuments(IReportFlow[] reports, bool withShowDialog = false, string printerName = "");
    }
}