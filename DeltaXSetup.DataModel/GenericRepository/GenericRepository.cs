using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.GenericRepository
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private Member Variables
        internal DeltaXEntities Context;
        internal DbSet<TEntity> DbSet;
        #endregion

        #region Public Constructor
        public GenericRepository(DeltaXEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Public Member Method
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        public virtual TEntity Get(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        public virtual void Delete(Func<TEntity, bool> where)
        {
            IQueryable<TEntity> objects = DbSet.Where(where).AsQueryable();
            foreach (TEntity item in objects)
            {
                DbSet.Remove(item);
            }
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> GetAllWithInclude(params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query;
        }

        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        public bool IsExist(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        public TEntity GetSingle(Func<TEntity, bool> condition)
        {
            return DbSet.Single<TEntity>(condition);
        }

        public TEntity GetFirst(Func<TEntity, bool> condition)
        {
            return DbSet.First<TEntity>(condition);
        }
        #endregion

    }
}
