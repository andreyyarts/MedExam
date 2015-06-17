using System.Collections.Generic;
using MedExam.Common;

namespace MedExam.Patient.ViewModels
{
    public class ReportListViewModel : ItemsNotification<long>
    {
        public ReportListViewModel(long[] items) : base(items)
        {
            Reports = new List<ReportViewModel>(new[]
            {
                new ReportViewModel("Patient")
            }).ToArray();
        }

        public ReportListViewModel() : base(new long[0]) { }

        public ReportViewModel[] Reports { get; set; }
    }
}