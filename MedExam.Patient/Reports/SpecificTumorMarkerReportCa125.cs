using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerReportCa125 : SpecificTumorMarkerReport
    {
        public SpecificTumorMarkerReportCa125(ReportService reportService)
            : base(reportService)
        {
        }

        public override string Title
        {
            get { return "Онкомаркер специфический CA-125"; }
        }
    }
}