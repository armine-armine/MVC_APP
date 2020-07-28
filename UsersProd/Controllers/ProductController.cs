using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using Shared.ViewModels;

namespace UsersProd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly db_UsersProductContext _dbContext;
        #endregion Fields

        #region Constructor
        public ProductController(db_UsersProductContext _dbContext)
        {
            this._dbContext = _dbContext;
        }

        #endregion Constructor

        #region Methods


        [HttpGet, Route("allProducts")]
        public IActionResult GetAllProducts()
        {
            var allCategories = _dbContext.Tbl_ProductCategory.ToList();
            var products = _dbContext.Tbl_Products.Include(pr => pr.ProductCategory)
                .Select(pr => new ProductCategoryViewModel
                {
                    ProductId = pr.ProductId,
                    ProductName = pr.ProductName,
                    ProductImage = pr.ProductImage,
                    ProCatId = pr.ProCatId,
                    ProductPrice = pr.ProductPrice,
                    CategoryName = pr.ProductCategory.CategoryName
                    
                }) ;


            return Ok(products);
        }

        [HttpGet("allproductCategories")]
        public IActionResult GetProductCategories()
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

            return Ok(productCategories);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(TblProducts products)
        {

            if (ModelState.IsValid)
            {
                _dbContext.Add(products);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest();

        }

        [HttpGet] 
        [Route("EditProduct/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var product = await _dbContext.Tbl_Products.Include(p=>p.ProductCategory).Where(c=>c.ProductId==id).FirstOrDefaultAsync();
            List<TblProductCategory> categories =  _dbContext.Tbl_ProductCategory.ToList<TblProductCategory>();
            ProductEditViewModel productEditViewModel = new ProductEditViewModel {
            Product=product,
            Category=categories
            };
           

            return Ok(productEditViewModel);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(TblProducts inputProduct)
        {

            var product = _dbContext.Tbl_Products.FirstOrDefault(x => x.ProductId == inputProduct.ProductId);

            if (ModelState.IsValid)
            {
                if (inputProduct.ProductImage==null)
                {
                    product.ProductImage = product.ProductImage;
                }
                else
                {
                    product.ProductImage = inputProduct.ProductImage;
                }
                product.ProductName = inputProduct.ProductName;
                product.ProductPrice = inputProduct.ProductPrice;
                TblProductCategory category= _dbContext.Tbl_ProductCategory.FirstOrDefault(c => c.CategoryId == inputProduct.ProCatId);

                product.ProductCategory = category;
                
              //  _dbContext.Entry(inputProduct).State = EntityState.Modified;
              // _dbContext.Update(inputProduct);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("GetDetailsProduct/{id:int}")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var getusersdetail = await _dbContext.Tbl_Products.Include(p=>p.ProductCategory).FirstOrDefaultAsync(p=>p.ProductId==id);
            return Ok(getusersdetail);
        }


        [HttpGet("DeleteProduct/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var getdelete = await _dbContext.Tbl_Products.Include(p => p.ProductCategory).FirstOrDefaultAsync(p => p.ProductId == id);
            return Ok(getdelete);
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var getusersdelete = await _dbContext.Tbl_Products.FindAsync(id);
            _dbContext.Tbl_Products.Remove(getusersdelete);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        #endregion Methods
    }
}
