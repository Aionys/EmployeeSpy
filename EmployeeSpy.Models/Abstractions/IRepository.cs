using System.Collections.Generic;
using EmployeeSpy.Models;

namespace EmployeeSpy.Core.Abstractions
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(int Id);

        IEnumerable<TEntity> GetAll();

        int Add(TEntity entity);

        void Commit();

        void RejectChanges();

        void Delete(TEntity entity);

        void Delete(int Id);

        void Save(TEntity entity);
    }
}
