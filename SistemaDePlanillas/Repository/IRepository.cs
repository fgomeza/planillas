using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaDePlanillas.Repository
{
    public interface IRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>();
        IRepository GetById<TEntity>(int Id);
        void Insert<TEntity> (TEntity entity);
        void Delete<TEntity>(int Id);
        void Update<TEntity>(TEntity entity);
        void Save();
    }
}