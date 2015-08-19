using System.Linq;
using System.Windows.Documents;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports.ViewModels;
using MedExam.Patient.Reports.Views;
using MedExam.Patient.services;

namespace MedExam.Patient.Reports
{
    public class DirectionInImmunologyLaboratoryReport : IReportFlow
    {
        private readonly PatientReportService _patientReportService;
        private readonly long[] _patientIds;

        public DirectionInImmunologyLaboratoryReport(PatientReportService patientReportService, long[] patientIds)
        {
            _patientReportService = patientReportService;
            _patientIds = patientIds;
        }

        public string Title
        {
            get { return "Направление в иммунологическую лабораторию"; }
        }

        public int CountInWidth
        {
            get { return 2; }
        }

        public int CountInHeight
        {
            get { return 2; }
        }

        public Block Report
        {
            get
            {
                var view = new DirectionInImmunologyLaboratoryReportView();
                return view.ReportBlock;
            }
        }

        public object[] Datas
        {
            get
            {
                var settings = new
                {
                    OrganizationName = string.Concat("ГБУЗ ТО ", SpecialChars.Laquo, "ОКБ №1", SpecialChars.Raquo),
                    DepartmentName = "ОТДЕЛЕНИЕ ПРОФОСМОТРОВ",
                    MedExamDoctorName = "Куликова Л.В."
                };

                var datas = _patientIds.Select(pId =>
                {
                    var patient = _patientReportService.GetPatientById((int) pId);
                    return new DirectionInImmunologyLaboratoryReportViewModel
                    {
                        CurrentOrganizationName = settings.OrganizationName,
                        CurrentDepartmentName = settings.DepartmentName,
                        DoctorNameWithInitials = settings.MedExamDoctorName,
                        PatientFullName = patient.PersonName.FullName,
                        PatientAge = patient.BirthDate.Value.ToShortDateString(),
                        PatientOrganizationName = patient.OrganizationName
                    };
                }).Cast<object>().ToArray();
                return datas;
            }
        }
    }
}