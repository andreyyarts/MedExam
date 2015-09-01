using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class SpecificTumorMarkerCa125Report : DirectionInImmunologyLaboratoryReport
    {
        public SpecificTumorMarkerCa125Report(ReportService reportService)
            : base(reportService)
        {
        }

        public override string Title
        {
            get { return "Онкомаркер специфический CA-125"; }
        }
    }
}