using MedExam.Common;
using MedExam.Patient.services;
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

        public PatientModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.MainContentRegion,
                                                    () => _container.Resolve<PatientListView>());

            _container.Resolve<ReportService>();
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
