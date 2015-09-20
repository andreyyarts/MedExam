using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerCa125Report : DirectionReport
    {
        private const string Template = "Онкомаркер специфический CA-125";

        public SpecificTumorMarkerCa125Report(ReportService reportService)
            : base(reportService, Template)
        {
            Title = Template;
        }

        public override string Title { get; set; }
    }
}