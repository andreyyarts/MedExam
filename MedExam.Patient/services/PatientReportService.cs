using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MedExam.Common.interfaces;
using MedExam.Patient.dto;
using MedExam.Patient.Entities;

namespace MedExam.Patient.services
{
    public class PatientReportService
    {
        private readonly IEntitiesFactory<MedExamEntities> _entitiesFactory;

        public PatientReportService(IEntitiesFactory<MedExamEntities> entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public PatientReportDto GetPatientById(int patientId)
        {
            using (var db = _entitiesFactory.GetDbContext())
            {
                var patient = db.pacient
                    .Where(p => p.num_pac == patientId)
                    .Include(p => p.organization)
                    .Select(PatientMap())
                    .FirstOrDefault();

                return patient;
            }
        }

        private static Expression<Func<pacient, PatientReportDto>> PatientMap()
        {
            return patient => new PatientReportDto
            {
                Id = patient.num_pac,
                Address = patient.address,
                BirthDate = patient.data_birth,
                Gender = patient.pol == "Ì" ? Gender.Male : Gender.Female,
                PersonName = new PersonNameDto
                {
                    LastName = patient.fam_pac,
                    FirstNameAndMiddleName = patient.io_pac
                },
                PolicyDto = new PolicyDto
                {
                    Series = patient.polic_ser,
                    Number = patient.polic_nom,
                    DateFrom = patient.polic
                },
                OrganizationName = patient.organization.name_org2
            };
        }
    }
}