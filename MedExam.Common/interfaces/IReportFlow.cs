using System.Collections.Generic;
using System.Windows.Documents;

namespace MedExam.Common.interfaces
{
    public interface IReportFlow
    {
        string Title { get; }
        int CountInWidth { get; }
        int CountInHeight { get; }
        Block Report { get; }
        IEnumerable<object> Datas { get; }
        void SetItems(long[] itemIds);
    }
}