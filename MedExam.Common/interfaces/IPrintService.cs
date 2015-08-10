using System.Collections.Generic;

namespace MedExam.Common.interfaces
{
    public interface IPrintService
    {
        void PrintDocument(IEnumerable<IReportFlow> reports, bool withShowDialog = false, string printerName = "");
    }
}