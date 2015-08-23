using System;
using System.Linq;
using System.Linq.Expressions;
using MedExam.Common.interfaces;
using MedExam.Patient.dto;
using MedExam.Patient.Entities;

namespace MedExam.Patient.services
{
    public class PatientService
    {
        private readonly IEntitiesFactory<MedExamEntities> _entitiesFactory;

        public PatientService(IEntitiesFactory<MedExamEntities> entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public PatientDto[] LoadAllPatients()
        {
            using (var db = _entitiesFactory.GetDbContext())
            {
                var patients = db.pacient
                    .OrderByDescending(p => p.num_pac)
                    .Select(PatientMap())
                    .ToArray();

                return patients;
            }
        }

        public PatientDto[] LoadPatientsByOrganizationId(int organizationId)
        {
            using (var db = _entitiesFactory.GetDbContext())
            {
                var patients = db.pacient
                    .Where(p => p.num_org == organizationId)
                    .OrderByDescending(p => p.num_pac)
                    .Select(PatientMap())
                    .ToArray();

                return patients;
            }
        }

        private static Expression<Func<pacient, PatientDto>> PatientMap()
        {
            return patient => new PatientDto
            {
                Id = patient.num_pac,
                Address = patient.address,
                BirthDate = patient.data_birth,
                Gender = patient.pol == "М" ? Gender.Male : Gender.Female,
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
                }
            };
        }
    }
}
