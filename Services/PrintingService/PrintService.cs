using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;
using MedExam.Common.interfaces;

namespace PrintingService
{
    public class PrintService : IPrintService
    {
        public void PrintDocuments(IReportFlow[] reports, bool withShowDialog = false, string printerName = "")
        {
            var dialog = new PrintDialog();
            if (withShowDialog)
            {
                if (dialog.ShowDialog() != true)
                    return;
            }
            else if (!string.IsNullOrEmpty(printerName))
            {
                dialog.PrintQueue = new PrintQueue(new LocalPrintServer(), printerName);
            }

            var document = new CompositionReportsOnFlowDocument(reports);

            dialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Flow Document");
        }
    }
}