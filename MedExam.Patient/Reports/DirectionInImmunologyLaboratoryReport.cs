using System.Linq;
using System.Windows.Documents;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports.ViewModels;
using MedExam.Patient.Reports.Views;

namespace MedExam.Patient.Reports
{
    public class DirectionInImmunologyLaboratoryReport : IReportFlow
    {
        private readonly long[] _patientIds;

        public DirectionInImmunologyLaboratoryReport(long[] patientIds)
        {
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
                var datas = _patientIds.Select(pId => new DirectionInImmunologyLaboratoryReportViewModel
                {
                    CurrentOrganizationName = string.Concat("ГБУЗ ТО ", SpecialChars.Laquo, "ОКБ №1", SpecialChars.Raquo),
                    CurrentDepartmentName = "ОТДЕЛЕНИЕ ПРОФОСМОТРОВ",
                    PatientFullName = "patient " + pId
                }).Cast<object>().ToArray();
                return datas;
            }
        }
    }
}