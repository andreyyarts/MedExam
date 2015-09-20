using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerPsaReport : DirectionReport
    {
        private const string Template = "Онкомаркер специфический PSA";

        public SpecificTumorMarkerPsaReport(ReportService reportService)
            : base(reportService, Template)
        {
            Title = Template;
        }

        public override string Title { get; set; }
    }
}