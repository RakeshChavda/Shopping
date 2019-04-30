using eShoppingCart.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace eShoppingCart.Repository
{
    public class GenericRepository<tblEntity> : IRepository<tblEntity> where tblEntity : class
    {
        DbSet<tblEntity> _dbSet;
        private eShoppingDBEntities _DBEntity;

        public GenericRepository(eShoppingDBEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<tblEntity>();
        }
        public void Add(tblEntity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }

        public int GetAllrecordCount()
        {
            return _dbSet.Count();
        }

        public IEnumerable<tblEntity> GetAllRecords()
        {
            return _dbSet.ToList();
        }

        public IQueryable<tblEntity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public tblEntity GetFirstOrDeafultByParameter(Expression<Func<tblEntity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public tblEntity GetFirstOrDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public IEnumerable<tblEntity> GetListParameter(Expression<Func<tblEntity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();  
        }

        public IEnumerable<tblEntity> GetResultBySqlProcedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _DBEntity.Database.SqlQuery<tblEntity>(query, parameters).ToList();
            }
            else
            {
                return _DBEntity.Database.SqlQuery<tblEntity>(query).ToList();
            }
        }

        public void InActiveAndDeleteMarkByWhereClause(Expression<Func<tblEntity, bool>> wherePredict, Action<tblEntity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public void Remove(tblEntity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public void RemoveByWhereClause(Expression<Func<tblEntity, bool>> wherePredict)
        {
            tblEntity entity = _dbSet.Where(wherePredict).FirstOrDefault();
            Remove(entity);
        }

        public void RemoveRangeByWhareClause(Expression<Func<tblEntity, bool>> wherePredict)
        {
            List<tblEntity> entity = _dbSet.Where(wherePredict).ToList();
            foreach (var e in entity)
            {
                Remove(e);
            }
        }

        public void Update(tblEntity entity)
        {
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<tblEntity, bool>> wherePredict, Action<tblEntity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }

        public IEnumerable<tblEntity> GetRecordToShow(int PageNo, int PazeSize, int CurrentPage, Expression<Func<tblEntity, bool>> wherePredict, Expression<Func<tblEntity, bool>> OrderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(OrderByPredict).Where(wherePredict).ToList();
            }
            else
            {
                return _dbSet.OrderBy(OrderByPredict).ToList();
            }
        }

        public IEnumerable<tblEntity> GetProduct()
        {
            return _dbSet.ToList();
        }
       
    }
}