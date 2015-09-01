using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerPsaReport : DirectionInImmunologyLaboratoryReport
    {
        public SpecificTumorMarkerPsaReport(ReportService reportService)
            : base(reportService)
        {
        }

        public override string Title
        {
            get { return "Онкомаркер специфический PSA"; }
        }
    }
}