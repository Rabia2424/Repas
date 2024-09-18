using Core.Entities;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity>
        where TEntity : BaseEntity, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity t)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(t);
                addedEntity.State = EntityState.Added;
                t.CreatedAt = DateTime.UtcNow;
                t.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity t)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(t);
                deletedEntity.State = EntityState.Deleted;
                t.DeletedAt = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                var query = context.Set<TEntity>().Where(e => e.DeletedAt == null);
                return filter == null? query.SingleOrDefault(): query.SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                var query = context.Set<TEntity>().Where(e => e.DeletedAt == null);
                return filter == null ? query.ToList() : query.Where(filter).ToList();
            }
        }

        public void Update(TEntity t)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(t);
                updatedEntity.State = EntityState.Modified;
                t.UpdatedAt = DateTime.UtcNow;
                context.SaveChanges();
            }
        }

    }
}
