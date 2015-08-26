using System;
using System.Linq;
using MedExam.Common.Interfaces;
using MedExam.Patient.Entities;

namespace MedExam.Patient.services
{
    public class SystemService
    {
        private readonly IEntitiesFactory<MedExamEntities> _entitiesFactory;

        public SystemService(IEntitiesFactory<MedExamEntities> entitiesFactory)
        {
            _entitiesFactory = entitiesFactory;
        }

        public DateTime Now()
        {
            using (var db = _entitiesFactory.GetDbContext())
            {
                return db.Database.SqlQuery<DateTime>("SELECT getdate()").First();
            }
        }

        public DateTime Today()
        {
            return Now().Date;
        }
    }
}