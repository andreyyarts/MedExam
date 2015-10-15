using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MedExam.Common;
using MedExam.Common.Interfaces;
using MedExam.Common.LocalSettings;
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
            Container.RegisterInstance(LocalSettingsService.LoadSetting<LocalSettings>());
            //LocalSettingsService.LoadSettings<ReportSetting>();

            LoadReports();
        }

        private void LoadReports()
        {
            var loadedReportSettings = LocalSettingsService.LoadSettings<ReportSetting>();
            var reportTypes = ReportTypes.GetAll().ToArray();

            var reports = loadedReportSettings.Select(s => SettingsToReportFlow(s, reportTypes))
                                              .OrderBy(r => r.Title)
                                              .ToArray();

            Container.RegisterInstance(new ReportList(reports));
        }

        private IReportFlow SettingsToReportFlow(ReportSetting s, IEnumerable<Type> reportTypes)
        {
            var reportType = reportTypes.Single(t => t.Name == s.Type);
            var report = (IReportFlow) Container.Resolve(reportType);
            report.Name = s.Name;
            report.Title = s.Title;
            report.Template = s.Template;
            return report;
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

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(PatientModule));
        }
    }
}
