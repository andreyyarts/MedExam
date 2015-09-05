using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Practices.Prism.Commands;

namespace PrintingService
{
    public class FlowDocumentPreViewModel
    {
        private readonly FlowDocument _document;

        public FlowDocumentPreViewModel(FlowDocument document)
        {
            _document = document;
            
            PrintDocument = new DelegateCommand(OnPrintDocument);
        }

        public FlowDocument Document
        {
            get { return _document; }
        }

        public DelegateCommand PrintDocument { get; set; }

        private void OnPrintDocument()
        {
            var dialog = new PrintDialog();
            if (dialog.ShowDialog() != true)
                return;

            dialog.PrintDocument(((IDocumentPaginatorSource)_document).DocumentPaginator, "Flow Document");
        }
    }
}