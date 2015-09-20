using MedExam.Common.Interfaces;

namespace MedExam.Common
{
    public class ReportList
    {
        public ReportList(IReportFlow[] reports)
        {
            Reports = reports;
        }

        public IReportFlow[] Reports { get; private set; }
    }
}