using System.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Common.Windows
{
    /// <summary>
    ///     Interaction logic for NotificationChildWindow.xaml
    /// </summary>
    public partial class DefaultNotificationWindow : Window
    {
        /// <summary>
        ///     Creates a new instance of <see cref="DefaultNotificationWindow" />
        /// </summary>
        public DefaultNotificationWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Sets or gets the <see cref="Notification" /> shown by this window./>
        /// </summary>
        public Notification Notification
        {
            get { return DataContext as Notification; }
            set { DataContext = value; }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}