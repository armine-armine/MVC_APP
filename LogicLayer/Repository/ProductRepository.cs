using LogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Repository
{
  public  class ProductRepository : IProduct
    {
        db_UsersProductContext _dbContext;
        public ProductRepository(db_UsersProductContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<TblProducts> GetAllProductsCategory()
        {
            var products = _dbContext.Tbl_Products.Include(pr => pr.ProductCategory);
            return products;
        }
        public IEnumerable<TblProducts> GetAllProducts()
        {
            var products = _dbContext.Tbl_Products;
            return products;
        }
        public IEnumerable<TblProductCategory> CreateProduct() 
        {

            var productCategories = new List<TblProductCategory>();
            foreach (var item in _dbContext.Tbl_ProductCategory)
            {
                productCategories.Add(new TblProductCategory()
                {
                    CategoryName = item.CategoryName,
                    CategoryId = item.CategoryId
                });
            }
            return productCategories;
            
        }
        public async Task<int> Create(TblProducts product) 
        {
             await _dbContext.Tbl_Products.AddAsync(product);
           await  _dbContext.SaveChangesAsync();
            return product.ProductId;
          
        }
        public async Task<TblProducts> GetEditProduct(int id)
        {
          return await _dbContext.Tbl_Products.Include(p => p.ProductCategory).Where(c => c.ProductId == id).FirstOrDefaultAsync();
            
        }
        public async Task<int> PutEditProduct(TblProducts product)
        {
            var getProduct =await _dbContext.Tbl_Products.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (product.ProductImage==null)
            {
                product.ProductImage = getProduct.ProductImage;
            }

           _dbContext.Entry(getProduct).CurrentValues.SetValues(product);
          await  _dbContext.SaveChangesAsync();
            return product.ProductId;
        }

        public async Task<TblProducts> GetProductDetails(int? id)
        {
           return await _dbContext.Tbl_Products.Include(p => p.ProductCategory).FirstOrDefaultAsync(p => p.ProductId == id);
       

        }

        public async Task Delete(int? id)
        {

                var getusersdelete = await _dbContext.Tbl_Products.FindAsync(id);
                _dbContext.Tbl_Products.Remove(getusersdelete);
                await _dbContext.SaveChangesAsync();


        }
    }
}
