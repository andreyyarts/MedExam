using System.Windows.Controls;
using MedExam.Patient.ViewModels;

namespace MedExam.Patient.Views
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
