using eShoppingCart.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eShoppingCart.Repository
{
    public class GenericUnitOfWork : IDisposable    // inherit manually 
    {
        private eShoppingDBEntities DBEntity = new eShoppingDBEntities();
        public IRepository<tblEntityType> GetRepositoryInstance<tblEntityType>() where tblEntityType : class
        {
            return new GenericRepository<tblEntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                DBEntity.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
    }
}