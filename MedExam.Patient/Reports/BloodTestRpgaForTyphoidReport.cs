using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class BloodTestRpgaForTyphoidReport : DirectionInImmunologyLaboratoryReport
    {
        public BloodTestRpgaForTyphoidReport(ReportService reportService)
            : base(reportService)
        {
        }

        public override string Title
        {
            get { return "Анализ крови: РПГА на брюшной тиф"; }
        }
    }
}