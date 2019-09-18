using ProjectQT.DataModel.Models;
using ProjectQT.ViewModel.DashboardModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.BAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Properties

        /// <summary>
        /// Declare datacontext
        /// </summary>
        protected QTShopDbContext DbContext;

        /// <summary>
        /// Declare Dbset EntityFrameworkCore
        /// </summary>
        protected DbSet<T> _dbSet;

        #endregion

        /// <summary>
        /// Generic repository constructor
        /// </summary>
        /// <param name="auctionDbContext"></param>
        public GenericRepository()
        {
            DbContext = new QTShopDbContext();
            _dbSet = DbContext.Set<T>();
        }

        /// <summary>
        /// </summary>
        public IQueryable<T> ObjectContext { get; set; }

        #region Implementation        
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            var listEntities = _dbSet.AsEnumerable();
            return listEntities;
        }

        /// <summary>
        /// Method get one Object with condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<T> Get(Expression<Func<T, bool>> where)
        {
            try
            {
                return _dbSet.Where(where);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get many T entity by condition
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMany(Func<T, bool> where)
        {
            var listEntities = _dbSet.AsEnumerable().Where(where);
            return listEntities;
        }

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(object id)
        {
            var entitiesByid = _dbSet.Find(id);
            return entitiesByid;
        }

        /// <summary>
        /// Create T entity
        /// </summary>
        /// <param name="entity"></param>
        public bool Create(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Update T entity
        /// </summary>
        /// <param name="entity"></param>
        public bool Update(T entity)
        {
            try
            {
                _dbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// Remove T entity by id
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(object id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                _dbSet.Remove(entity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Remove a list entities
        /// </summary>
        /// <param name="entities"></param>
        public bool DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEntity(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateRange(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T GetBy(Expression<Func<T, bool>> where)
        {
            try
            {
                return _dbSet.Where(where).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
