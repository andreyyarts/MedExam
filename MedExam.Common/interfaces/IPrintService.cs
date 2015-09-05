using System.Collections.Generic;

namespace MedExam.Common.Interfaces
{
    public interface IPrintService
    {
        void PrintDocuments(IEnumerable<IReportFlow> reports, bool isPreview = false, bool withShowDialog = false, string printerName = "");
    }
}