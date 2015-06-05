using System;

namespace MedExam.Common.interfaces
{
    public interface IEntitiesFactory<TDbContext> where TDbContext : class, IDisposable
    {
        TDbContext GetDbContext();
    }
}