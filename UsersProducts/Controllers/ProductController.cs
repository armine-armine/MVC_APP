using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using UsersProducts.Models;

namespace UsersProducts.Controllers
{
    public class ProductController : Controller
    {
        private readonly db_UsersProductContext _dbContext;
       
        public ProductController(db_UsersProductContext _dbContext)
        {
            this._dbContext = _dbContext;
        }


        [Authorize(Roles = "Viewer, Admin, Approver")]
        public IActionResult Index()
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


            return View(products);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var a = new List<TblProductCategory>();
            foreach (var item in _dbContext.Tbl_ProductCategory)
            {
                a.Add(new TblProductCategory()
                {
                    CategoryName = item.CategoryName,
                    CategoryId = item.CategoryId
                });
            }
            ViewBag.Category = a;
            return View();
           
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(TblProducts nec, IFormFile file)
        {
           
                string filename = System.Guid.NewGuid().ToString() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                nec.ProductImage = filename;

               
                if (ModelState.IsValid)
                {

                    _dbContext.Add(nec);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                return View(nec);   
            
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
           
                if (id == 0)
                {
                    return RedirectToAction("Index");
                }
                var getusersdetail = await _dbContext.Tbl_Products.FindAsync(id);
            var a = new List<TblProductCategory>();
            foreach (var item in _dbContext.Tbl_ProductCategory)
            {
                a.Add(new TblProductCategory()
                {
                    CategoryName = item.CategoryName,
                    CategoryId = item.CategoryId
                });
            }
            ViewBag.Category = a;
            return View(getusersdetail);
              
        }


        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Edit(TblProducts inputProduct, IFormFile file)
        {
          
                string filename = System.Guid.NewGuid().ToString() + ".jpg";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                inputProduct.ProductImage = filename;

                if (ModelState.IsValid)
                {
                    _dbContext.Update(inputProduct);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");

                }
                return View(inputProduct);
        }


         [Authorize(Roles = "Admin, Approver")]
        public async Task<IActionResult> Details(int? id)
        {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                var getusersdetail = await _dbContext.Tbl_Products.FindAsync(id);
                return View(getusersdetail);           
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
         
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                var getusersdetail = await _dbContext.Tbl_Products.FindAsync(id);
                return View(getusersdetail);
            
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
                var getusersdelete = await _dbContext.Tbl_Products.FindAsync(id);
                _dbContext.Tbl_Products.Remove(getusersdelete);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
         
        }

    }
}
