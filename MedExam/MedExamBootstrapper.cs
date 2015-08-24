using System.Windows;
using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using PrintingService;

namespace MedExam
{
    class MedExamBootstrapper : UnityBootstrapper
    {
        private readonly ILoggerFacade _logger = new NLogLoggerAdapter();

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            RegisterTypeIfMissing(typeof(ILoggerApp<>), typeof(Logger<>), false);
            RegisterTypeIfMissing(typeof(IEntitiesFactory<>), typeof(EntitiesFactory<>), true);
            RegisterTypeIfMissing(typeof(IPrintService), typeof(PrintService), true);
            Container.RegisterInstance(SettingsService.Load<LocalSettings>());
        }

        protected override ILoggerFacade CreateLogger()
        {
            return _logger;
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            var logger = Container.Resolve<Logger<App>>();
            logger.Log(LogLevel.Debug, "app started");

            var mainWindow = (Window)Shell;
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();

            mainWindow.Closed += (sender, args) =>
            {
                logger.Log(LogLevel.Debug, "app closed");
            };
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog) ModuleCatalog;
            moduleCatalog.AddModule(typeof (PatientModule));
        }
    }
}
