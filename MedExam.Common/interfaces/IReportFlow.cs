using System.Windows.Documents;

namespace MedExam.Common.interfaces
{
    public interface IReportFlow
    {
        int CountInWidth { get; }
        int CountInHeight { get; }
        Block Report { get; }
    }
}