using System.Collections.Generic;
using System.Printing;
using System.Windows.Controls;
using System.Windows.Documents;
using MedExam.Common.interfaces;

namespace PrintingService
{
    public class PrintService
    {
        private readonly string _printerName;

        public PrintService(string printerName = "")
        {
            _printerName = printerName;
        }

        public void PrintDocument(IEnumerable<IReportFlow> reports, bool withShowDialog = false)
        {
            var dialog = new PrintDialog();
            if (withShowDialog)
            {
                if (dialog.ShowDialog() != true)
                    return;
            }
            else if (!string.IsNullOrEmpty(_printerName))
            {
                dialog.PrintQueue = new PrintQueue(new LocalPrintServer(), _printerName);
            }

            var document = new CompositeFlowDocument(reports);

            dialog.PrintDocument(((IDocumentPaginatorSource)document).DocumentPaginator, "Flow Document");
        }
    }
}