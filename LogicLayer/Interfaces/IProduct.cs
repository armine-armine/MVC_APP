using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interfaces
{
   public interface IProduct
    {
        public IEnumerable<TblProducts> GetAllProductsCategory();
        public IEnumerable<TblProducts> GetAllProducts();

        public IEnumerable<TblProductCategory> CreateProduct();

        public  Task<int> Create(TblProducts product);

        public Task<TblProducts> GetEditProduct(int id);
        public Task<int> PutEditProduct(TblProducts product);
        public Task<TblProducts> GetProductDetails(int? id);

        public Task Delete(int? id);

    }
}
