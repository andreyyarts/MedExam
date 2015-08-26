using System;

namespace MedExam.Common.Interfaces
{
    public interface IEntitiesFactory<TDbContext> where TDbContext : class, IDisposable
    {
        TDbContext GetDbContext();
    }
}