using System.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Common.Windows
{
    /// <summary>
    ///     Interaction logic for ConfirmationChildWindow.xaml
    /// </summary>
    public partial class DefaultConfirmationWindow : Window
    {
        /// <summary>
        ///     Creates a new instance of ConfirmationChildWindow.
        /// </summary>
        public DefaultConfirmationWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets or gets the <see cref="Confirmation" /> shown by this window./>
        /// </summary>
        public Confirmation Confirmation
        {
            get { return DataContext as Confirmation; }
            set { DataContext = value; }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Confirmation.Confirmed = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Confirmation.Confirmed = false;
            Close();
        }
    }
}