﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQT.BAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Get all entities of type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get entity of type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> where);
        
        /// <summary>
        /// Get entity of type T
        /// </summary>
        /// <returns></returns>
        T GetBy(Expression<Func<T, bool>> where);

        /// <summary>
        /// Gets entities using delegate
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Func<T, bool> where);

        /// <summary>
        /// Get an entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);

        /// <summary>
        /// Marks an entity as new
        /// </summary>
        /// <param name="entity"></param>
        bool Create(T entity);
        
        /// <summary>
        /// Marks an entity as new
        /// </summary>
        /// <param name="entity"></param>
        bool CreateRange(IEnumerable<T> entities);

        /// <summary>
        /// Marks an entity as modified
        /// </summary>
        /// <param name="entity"></param>
        bool Update(T entity);

        /// <summary>
        /// Marks an enity to be remove by id
        /// </summary>
        /// <param name="id"></param>
        bool Delete(object id);
        
        /// <summary>
        /// Marks an enity to be remove entity
        /// </summary>
        /// <param name="id"></param>
        bool DeleteEntity(T entity);

        /// <summary>
        ///  Marks an enity to be remove range
        /// </summary>
        /// <param name="entities"></param>
        bool DeleteRange(IEnumerable<T> entities);

    }
}
