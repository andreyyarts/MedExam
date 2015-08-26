using System;
using System.Linq;
using System.Linq.Expressions;
using MedExam.Common.Interfaces;
using MedExam.Patient.dto;
using MedExam.Patient.Entities;

namespace MedExam.Patient.services
{
    public class OrganizationService
    {
        private readonly IEntitiesFactory<MedExamEntities> _entitiesFactory;

        public OrganizationService(IEntitiesFactory<MedExamEntities> entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public OrganizationDto[] LoadAllOrganizations()
        {
            using (var db = _entitiesFactory.GetDbContext())
            {
                
                var organizations = db.organization
                    .OrderBy(o => o.name_org)
                    .Select(OrganizationMap())
                    .ToArray();

                return organizations;
            }
        }

        private static Expression<Func<organization, OrganizationDto>> OrganizationMap()
        {
            return o => new OrganizationDto
            {
                FullName = o.name_org,
                Id = o.num_org,
                ShortName = o.name_org2
            };
        }
    }
}