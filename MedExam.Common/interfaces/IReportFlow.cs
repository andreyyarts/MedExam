using System.Windows.Documents;

namespace MedExam.Common.interfaces
{
    public interface IReportFlow
    {
        int CountAtWidth { get; }
        int CountAtHeight { get; }
        Block Report { get; }
    }
}