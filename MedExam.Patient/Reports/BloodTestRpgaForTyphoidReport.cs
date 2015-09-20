using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class BloodTestRpgaForTyphoidReport : DirectionReport
    {
        public BloodTestRpgaForTyphoidReport(ReportService reportService)
            : base(reportService, "Анализ крови. РПГА на брюшной тиф")
        {
            Title = "Анализ крови: РПГА на брюшной тиф";
        }

        public override string Title { get; set; }
    }
}