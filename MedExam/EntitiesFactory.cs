using System.Data.Entity;
using MedExam.Common;
using MedExam.Common.Interfaces;
using Microsoft.Practices.Unity;

namespace MedExam
{
    public class EntitiesFactory<TDbContext> : IEntitiesFactory<TDbContext> where TDbContext : DbContext
    {
        private readonly IUnityContainer _container;
        private readonly ILoggerApp<DbContext> _logger;

        public EntitiesFactory(IUnityContainer container, ILoggerApp<DbContext> logger)
        {
            _container = container;
            _logger = logger;
        }

        public TDbContext GetDbContext()
        {
            var dbContext = _container.Resolve<TDbContext>();
            dbContext.Database.Log = s => { _logger.Log(LogLevel.Debug, s); };
            dbContext.Configuration.LazyLoadingEnabled = true;

            return dbContext;
        }
    }
}