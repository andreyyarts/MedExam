using System.Collections.Generic;

namespace MedExam.Common.Interfaces
{
    public interface IPrintService
    {
        void PrintDocuments(IEnumerable<ReportFlow<IReportData>> reports, bool isPreview = false, bool withShowDialog = false, string printerName = "");
    }
}