using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace UsersProd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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

        //[Authorize(Roles = "Viewer, Admin, Approver")]
        [HttpGet, Route("allProducts")]
        public IActionResult GetAllProducts()
        {

            var products = _dbContext.Tbl_Products.Include(pr => pr.ProductCategory)
                .Select(pr => new ProductCategoryViewModel
                {
                    ProductId = pr.ProductId,
                    ProductName = pr.ProductName,
                    ProductImage = pr.ProductImage,
                    ProCatId = pr.ProCatId,
                    ProductPrice = pr.ProductPrice,
                    CategoryName = pr.ProductCategory.CategoryName
                });


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

        //[HttpPost("create")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Create(TblProducts nec, IFormFile file)
        //{

        //    string filename = System.Guid.NewGuid().ToString() + ".jpg";
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    nec.ProductImage = filename;


        //    if (ModelState.IsValid)
        //    {

        //        _dbContext.Add(nec);
        //        await _dbContext.SaveChangesAsync();
        //        return RedirectToAction("Index");

        //    }
        //    return Ok(nec);

        //}

        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(int id)
        //{

        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var product = await _dbContext.Tbl_Products.FindAsync(id);

        //    var productCategories = new List<TblProductCategory>();
        //    foreach (var item in _dbContext.Tbl_ProductCategory)
        //    {
        //        productCategories.Add(new TblProductCategory()
        //        {
        //            CategoryName = item.CategoryName,
        //            CategoryId = item.CategoryId
        //        });
        //    }
        //    //stex a inch vor dzev avelacnel getusersdetail mej vor xndir chunenas
        //    return Ok(product);

        //}

        //[HttpPost]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Edit(TblProducts inputProduct, IFormFile file)
        //{

        //    string filename = System.Guid.NewGuid().ToString() + ".jpg";
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    inputProduct.ProductImage = filename;

        //    if (ModelState.IsValid)
        //    {
        //        _dbContext.Update(inputProduct);
        //        await _dbContext.SaveChangesAsync();
        //        return BadRequest();

        //    }
        //    return Ok(inputProduct);
        //}



        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var getusersdetail = await _dbContext.Tbl_Products.FindAsync(id);
        //    return Ok(getusersdetail);
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{

        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var getusersdetail = await _dbContext.Tbl_Products.FindAsync(id);
        //    return Ok(getusersdetail);

        //}



        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var getusersdelete = await _dbContext.Tbl_Products.FindAsync(id);
        //    _dbContext.Tbl_Products.Remove(getusersdelete);
        //    await _dbContext.SaveChangesAsync();
        //    return Ok();

        //}

        #endregion Methods
    }
}
