using System;
using System.Windows;
using NLog;

namespace MedExam
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string ErrorMessage = "Возникла непредвиденная ошибка. Приложение будет закрыто";
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            RunAsRelease();
        }

        private static void RunAsRelease()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                var bootstrapper = new MedExamBootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            _logger.Error(ex);
            MessageBox.Show(ErrorMessage);
            _logger.Debug("app closed after exception");
            Environment.Exit(1);
        }
    }
}
