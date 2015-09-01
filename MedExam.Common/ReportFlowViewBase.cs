using System.Windows.Documents;
using MedExam.Common.Interfaces;

namespace MedExam.Common
{
    public class ReportFlowViewBase : BlockUIContainer, IReportView
    {
        public string Title { get; set; }
    }
}