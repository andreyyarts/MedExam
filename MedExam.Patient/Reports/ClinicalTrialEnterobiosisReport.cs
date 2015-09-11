using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class ClinicalTrialEnterobiosisReport : DirectionReport
    {
        private const string Template = "����������� ������������ (����������)";

        public ClinicalTrialEnterobiosisReport(ReportService reportService)
            : base(reportService, Template)
        {
        }

        public override string Title
        {
            get { return Template; }
        }
    }
}