using System;
using System.Linq;
using System.Linq.Expressions;
using MedExam.Common.Interfaces;
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
                },
                Organization = new OrganizationDto
                {
                    Id = patient.num_org,
                    ShortName = patient.organization.name_org2,
                    FullName = patient.organization.name_org
                }
            };
        }

        public PatientDto[] LoadPatientsByToken(string searchText)
        {
            using (var db = _entitiesFactory.GetDbContext())
            {
                var words = searchText.Split(' ').ToList();
                var length = words.Count;
                var searchForFamily = words.First();
                words.Remove(searchForFamily);

                var patients = db.pacient
                    .Where(p => p.fam_pac.Contains(searchForFamily) &&
                                (length == 1 || words.All(w => p.io_pac.Contains(w))))
                    //.Where(p => words.All(w => p.fam_pac.Contains(w) || p.io_pac.Contains(w)))
                    .OrderByDescending(p => p.num_pac)
                    .Select(PatientMap())
                    .ToArray();

                return patients;
            }
        }
    }
}
