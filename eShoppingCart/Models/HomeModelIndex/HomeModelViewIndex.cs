using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eShoppingCart.DAL;
using eShoppingCart.Repository;
using System.Data.SqlClient;
using PagedList;
using PagedList.Mvc;

namespace eShoppingCart.Models.HomeModelIndex
{
    public class HomeModelViewIndex
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        eShoppingDBEntities SqlEntity = new eShoppingDBEntities();

        public IPagedList<ProductM> ListOfProduct { get; set; }
        public  HomeModelViewIndex CreateModel(string search,int pageSize, int? page)

        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@Search",search??(object)DBNull.Value) 
            };

            IPagedList<ProductM> data = SqlEntity.Database.SqlQuery<ProductM>("e_SearchByName @Search", param).ToList().ToPagedList(page ??1, pageSize);
            return new HomeModelViewIndex()
            {
                ListOfProduct = data
            };
        }
    }
}