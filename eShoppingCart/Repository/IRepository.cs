using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eShoppingCart.Repository
{
    public interface IRepository<tblEntity> where tblEntity : class
    {
        IEnumerable<tblEntity> GetAllRecords();
        IQueryable<tblEntity> GetAllRecordsIQueryable();
        int GetAllrecordCount();
        void Add(tblEntity entity);
        void Update(tblEntity entity);
        void UpdateByWhereClause(Expression<Func<tblEntity, bool>> wherePredict, Action<tblEntity> ForEachPredict);
        tblEntity GetFirstOrDefault(int recordId);
        void Remove(tblEntity entity);
        void RemoveByWhereClause(Expression<Func<tblEntity, bool>> wherePredict);
        void RemoveRangeByWhareClause(Expression<Func<tblEntity, bool>> wherePredict);
        void InActiveAndDeleteMarkByWhereClause(Expression<Func<tblEntity, bool>> wherePredict, Action<tblEntity> ForEachPredict);
        tblEntity GetFirstOrDeafultByParameter(Expression<Func<tblEntity, bool>> wherePredict);
        IEnumerable<tblEntity> GetListParameter(Expression<Func<tblEntity, bool>> wherePredict);
        IEnumerable<tblEntity> GetResultBySqlProcedure(string query, params object[] parameters);
        IEnumerable<tblEntity> GetRecordToShow(int PageNo, int PazeSize, int CurrentPage, Expression<Func<tblEntity, bool>> wherePredict, Expression<Func<tblEntity, bool>> OrderByPredict);
        IEnumerable<tblEntity> GetProduct();

    }
}
