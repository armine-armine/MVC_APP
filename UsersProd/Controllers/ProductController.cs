using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace UsersProd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly db_UsersProductContext _dbContext;
        #endregion Fields
        private readonly IProduct _product;
        private readonly ICategory _category;

        #region Constructor
        public ProductController(db_UsersProductContext dbContext, IProduct product, ICategory category)
        {
            _dbContext = dbContext;
            _product = product;
            _category = category;
        }

        #endregion Constructor

        #region Methods


        [HttpGet("GetProductList")]
        public IActionResult GetProductList()
        {
            var products = _product.GetAllProductsCategory();
            return Ok(products);
        }

        [HttpGet, Route("GetCategoryList")]
        public IActionResult GetCategoryList()
        {
            var products = _category.GetAllCategory();
            return Ok(products);
        }

        [HttpGet("CreateProduct")]
        public IActionResult CreateProduct()
        {
            var productCategories = _product.CreateProduct();

            return Ok(productCategories);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(TblProducts products)
        {

            if (ModelState.IsValid)
            {

                await _product.Create(products);
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

            return Ok(await _product.GetEditProduct(id));
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(TblProducts inputProduct)
        {

            var product =await _product.PutEditProduct(inputProduct);
            return Ok();
        }

        [HttpGet]
        [Route("GetDetailsProduct/{id:int}")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(await _product.GetProductDetails(id));
        }


        [HttpGet("DeleteProduct/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            return Ok(await _product.GetProductDetails(id));
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _product.Delete(id);
            return Ok();
        }

        #endregion Methods
    }
}
