using System.Windows.Documents;
using MedExam.Common.interfaces;
using MedExam.Patient.Reports.ViewModels;
using MedExam.Patient.Reports.Views;

namespace MedExam.Patient.Reports
{
    public class DirectionInImmunologyLaboratoryReport : IReportFlow
    {
        private readonly DirectionInImmunologyLaboratoryReportViewModel _data;

        public DirectionInImmunologyLaboratoryReport(DirectionInImmunologyLaboratoryReportViewModel data)
        {
            _data = data;
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
                return view.ReportSection;
            }
        }

        public object Data
        {
            get { return _data; }
        }
    }
}