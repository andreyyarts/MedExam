using System.Collections.Generic;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using MedExam.Common.Interfaces;

namespace PrintingService
{
    public class PrintService : IPrintService
    {
        public void PrintDocuments(IEnumerable<IReportFlow> reports, bool isPreview = false, bool withShowDialog = false, string printerName = "")
        {
            if (isPreview)
            {
                var document = new CompositionReportsOnFlowDocument(reports);
                var view = new FlowDocumentPreView
                {
                    DataContext = new FlowDocumentPreViewModel(document)
                };

                var window = new Window
                {
                    Content = view,
                    SizeToContent = SizeToContent.Width,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                window.Show();
            }
            else
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
}