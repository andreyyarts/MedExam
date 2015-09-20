using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class ClinicalTrialEnterobiosisReport : DirectionReport
    {
        private const string Template = "Клиническое исследование (энтеробиоз)";

        public ClinicalTrialEnterobiosisReport(ReportService reportService)
            : base(reportService, Template)
        {
            Title = Template;
        }

        public override string Title { get; set; }
    }
}