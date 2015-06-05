using System.Windows.Controls;
using MedExam.Patient.viewModel;

namespace MedExam.Patient
{
    /// <summary>
    /// Interaction logic for PatientListView.xaml
    /// </summary>
    public partial class PatientListView : UserControl
    {
        public PatientListView(PatientListViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
