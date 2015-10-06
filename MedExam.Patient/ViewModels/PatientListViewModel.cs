using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
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
        private readonly ReportService _reportService;

        public PatientListViewModel(OrganizationService organizationService, PatientService patientService, ReportService reportService)
        {
            _patientService = patientService;
            _reportService = reportService;

            var organizations = organizationService.LoadAllOrganizations();
            Organizations = new ListCollectionView(organizations);
            Patients = new ObservableCollection<PatientViewModel>();

            Organizations.CurrentChanged += (sender, args) =>
            {
                LoadPatients((OrganizationDto)Organizations.CurrentItem);
            };

            PrintReports = new DelegateCommand(OnPrintReports, CanPrintReports);
            Refresh = new DelegateCommand<OrganizationDto>(OnRefresh);
            SearchPatients = new DelegateCommand<string>(OnSearchPatients);
            FoundCountPatients = new ObservableObject<string>();
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

            ShowReportsRequest = new InteractionRequest<ReportListViewModel>();
        }

        public ObservableCollection<PatientViewModel> Patients { get; private set; }
        public ICollectionView Organizations { get; private set; }
        public InteractionRequest<ReportListViewModel> ShowReportsRequest { get; private set; }
        public DelegateCommand PrintReports { get; private set; }
        public DelegateCommand<OrganizationDto> Refresh { get; private set; }
        public DelegateCommand<string> SearchPatients { get; private set; }
        public ObservableObject<string> FoundCountPatients { get; private set; }

        private void OnSearchPatients(string searchText)
        {
            Patients.Clear();
            var patients = _patientService.LoadPatientsByToken(searchText);
            Patients.AddRange(patients.Select(PatientDtoMap));
            FoundCountPatients.Value = string.Format("Найдено: {0}", patients.Length);
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

            ShowReportsRequest.Raise(new ReportListViewModel(_reportService, patientIds) { Title = "Печать" });
        }

        private bool CanPrintReports()
        {
            return Patients.Any(c => c.IsSelected);
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
            FoundCountPatients.Value = "";
            var patients = _patientService.LoadPatientsByOrganizationId(organization.Id);
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
                Gender = patient.Gender.Text(),
                Organization = patient.Organization.FullName
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
