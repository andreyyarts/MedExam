using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerReportPsa : SpecificTumorMarkerReport
    {
        public SpecificTumorMarkerReportPsa(ReportService reportService)
            : base(reportService)
        {
        }

        public override string Title
        {
            get { return "Онкомаркер специфический PSA"; }
        }
    }
}