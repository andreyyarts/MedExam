using System.Collections.Generic;

namespace MedExam.Common.Interfaces
{
    public interface IReportFlow
    {
        string Title { get; }
        int CountInWidth { get; }
        int CountInHeight { get; }
        ReportFlowViewBase Report { get; }
        IEnumerable<object> Datas { get; }
        void SetItems(long[] itemIds);
    }
}