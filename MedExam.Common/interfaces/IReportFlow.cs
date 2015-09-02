using System.Collections.Generic;

namespace MedExam.Common.Interfaces
{
    public interface IReportFlow
    {
        string Title { get; }
        int CountInWidth { get; }
        int CountInHeight { get; }
        IEnumerable<object> Datas { get; }
        ReportFlowViewBase Report { get; }
        void SetItems(long[] itemIds);
    }
}