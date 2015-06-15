using MedExam.Common;
using MedExam.Common.interfaces;
using MedExam.Patient.ViewModels;
using MedExam.Patient.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace MedExam.Patient
{
    public class PatientModule : IModule
    {
        private const int CurrentOrganizationPosition = 16;
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

            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion, GetViewWithModel);
        }

        private PatientListView GetViewWithModel()
        {
            var view = _container.Resolve<PatientListView>();
            var viewModel = (PatientListViewModel)view.DataContext;
            viewModel.Organizations.MoveCurrentToPosition(CurrentOrganizationPosition);
            return view;
        }
    }
}
