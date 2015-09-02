using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerPsaReport : DirectionInImmunologyLaboratoryReport
    {
        private const string Template = "Онкомаркер специфический PSA";

        public SpecificTumorMarkerPsaReport(ReportService reportService)
            : base(reportService, Template)
        {
        }

        public override string Title
        {
            get { return Template; }
        }
    }
}