using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;
using MedExam.Common.interfaces;
using MedExam.Patient.dto;
using MedExam.Patient.services;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Patient.ViewModels
{
    public class PatientListViewModel
    {
        private readonly PatientService _patientService;
        private readonly IPrintService _printService;
        private readonly PatientReportService _patientReportService;
        private ICommand _refresh;

        public PatientListViewModel(OrganizationService organizationService, PatientService patientService, IPrintService printService, PatientReportService patientReportService)
        {
            _patientService = patientService;
            _printService = printService;
            _patientReportService = patientReportService;

            var organizations = organizationService.GetAllOrganizations();
            Organizations = new ListCollectionView(organizations);
            Patients = new ObservableCollection<PatientViewModel>();
            
            Organizations.CurrentChanged += (sender, args) =>
            {
                LoadPatients((OrganizationDto)Organizations.CurrentItem);
            };

            SelectPatient = new DelegateCommand<PatientViewModel>(OnSelectPatient);
            PrintReports = new DelegateCommand(OnPrintReports, CanPrintReports);
            Patients.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        args.NewItems.OfType<PatientViewModel>().ForEach(p => p.PropertyChanged += PatientIsSelectedPropertyChanged);
                        break;
                    case NotifyCollectionChangedAction.Reset:
                        if (args.OldItems == null)
                            break;

                        args.OldItems.OfType<PatientViewModel>().ForEach(p => p.PropertyChanged -= PatientIsSelectedPropertyChanged);
                        PrintReports.RaiseCanExecuteChanged();
                        break;
                }
            };
            
            NotificationRequest = new InteractionRequest<ReportListViewModel>();
        }

        public ObservableCollection<PatientViewModel> Patients { get; private set; }
        public ICollectionView Organizations { get; private set; }
        public InteractionRequest<ReportListViewModel> NotificationRequest { get; private set; }
        public DelegateCommand PrintReports { get; private set; }
        public DelegateCommand<PatientViewModel> SelectPatient { get; private set; }

        public ICommand Refresh
        {
            get { return _refresh ?? (_refresh = new DelegateCommand<OrganizationDto>(OnRefresh)); }
        }

        private void OnSelectPatient(PatientViewModel patient)
        {
            if (patient == null)
                return;

            var selectPatient = Patients.Single(p => p.Id == patient.Id);
            selectPatient.IsSelected = !selectPatient.IsSelected;
        }

        private void PatientIsSelectedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsSelected")
            {
                PrintReports.RaiseCanExecuteChanged();
            }
        }

        private void OnPrintReports()
        {
            var patientIds = Patients.Where(p => p.IsSelected).Select(p => p.Id).ToArray();

            ShowReports(new ReportListViewModel(_printService, _patientReportService, patientIds) { Title = "Печать" });
        }

        private bool CanPrintReports()
        {
            return Patients.Any(c => c.IsSelected);
        }

        private void ShowReports(ReportListViewModel notification)
        {
            NotificationRequest.Raise(notification);
        }

        private void OnRefresh(OrganizationDto organization)
        {
            LoadPatients(organization);
        }

        private void LoadPatients(OrganizationDto organization)
        {
            if (organization == null)
                throw new NullReferenceException("organization cannot be null");

            Patients.Clear();
            var patients = _patientService.GetPatientsByOrganizationId(organization.Id);
            Patients.AddRange(patients.Select(PatientDtoMap));
        }

        private static PatientViewModel PatientDtoMap(PatientDto patient)
        {
            return new PatientViewModel
            {
                Id = patient.Id,
                Address = patient.Address,
                BirthDate = patient.BirthDate,
                PersonName = PersonNameMap(patient.PersonName),
                PolicyDto = patient.PolicyDto,
                Gender = patient.Gender.Text()
            };
        }

        private static PersonName PersonNameMap(PersonNameDto name)
        {
            return new PersonName
            {
                LastName = name.LastName,
                FirstName = name.FirstName,
                MiddleName = name.MiddleName
            };
        }
    }
}
