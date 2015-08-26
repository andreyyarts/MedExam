using MedExam.Common.Interfaces;

namespace MedExam.Patient.Reports.Views
{
    /// <summary>
    /// Interaction logic for SpecificTumorMarkerReportView.xaml
    /// </summary>
    public partial class SpecificTumorMarkerReportView : IReportView
    {
        public SpecificTumorMarkerReportView()
        {
            InitializeComponent();
        }

        public string Title { get; set; }
    }
}
