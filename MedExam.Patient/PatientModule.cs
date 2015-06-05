using MedExam.Common;
using MedExam.Common.interfaces;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace MedExam.Patient
{
    public class PatientModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly ILoggerApp<PatientModule> _logger;

        public PatientModule(IUnityContainer container, IRegionManager regionManager, ILoggerApp<PatientModule> logger)
        {
            _container = container;
            _regionManager = regionManager;
            _logger = logger;
        }

        public void Initialize()
        {
            _logger.Log(LogLevel.Debug, "PatientModule Initialize");

            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, () => _container.Resolve<PatientListView>());
        }
    }
}
