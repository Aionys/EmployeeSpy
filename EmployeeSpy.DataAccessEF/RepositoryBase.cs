using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeSpy.Models;
using EmployeeSpy.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeSpy.DataAccessEF
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly EmployeeSpyContext _context;

        public RepositoryBase(EmployeeSpyContext context)
        {
            _context = context;
        }

        protected IQueryable<TEntity> RepositoryQuery => _context.Set<TEntity>().AsQueryable();

        public TEntity GetById (int Id)
        {
            return RepositoryQuery.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return RepositoryQuery.ToArray();
        }

        public int Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                RejectChanges();
                throw;
            }
        }

        public void RejectChanges()
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        {
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        }

                    case EntityState.Deleted:
                        {
                            entry.State = EntityState.Unchanged;
                            break;
                        }

                    case EntityState.Added:
                        {
                            entry.State = EntityState.Detached;
                            break;
                        }
                }
            }
        }

        public void Delete(int Id)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(e => e.Id == Id);
            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (_context.Set<TEntity>().Local.All(e => e.Id != entity.Id))
            {
                _context.Set<TEntity>().Attach(entity);
            }

            _context.Set<TEntity>().Remove(entity);
        }

        public  void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Id > 0)
            {
                if (_context.Set<TEntity>().Local.All(e => e.Id != entity.Id))
                {
                    _context.Set<TEntity>().Attach(entity);
                }

                _context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                _context.Set<TEntity>().Add(entity);
            }
        }
    }
}