using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Newtonsoft.Json;
using Shared.Models;
using Shared.ViewModels;

namespace UsersProducts.Controllers
{
    public class ProductController : Controller
    {
        private readonly db_UsersProductContext _dbContext;
        private readonly string API_URL = "http://localhost:5003";
        public ProductController(db_UsersProductContext _dbContext)
        {
            this._dbContext = _dbContext;
        }


        [Authorize(Roles = "Viewer, Admin, Approver")]
        public async Task<IActionResult> Index()
        {
            List<ProductCategoryViewModel> productCategoryViewModel = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/allProducts"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        productCategoryViewModel = JsonConvert.DeserializeObject<List<ProductCategoryViewModel>>(result);

                    }
                }
            }

            return View(productCategoryViewModel);
        }

        //using (HttpResponseMessage response = await httpClient.PostAsync(API_URL, new StringContent(jsonModel, Encoding.UTF8, "application/json"))


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ProductEditViewModel tblProductCategories = new ProductEditViewModel();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/allproductCategories"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        tblProductCategories.Category = JsonConvert.DeserializeObject<List<TblProductCategory>>(result);
                    }
                }
            }

            return View(tblProductCategories);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(TblProducts product, IFormFile file)
        {
            string filename = System.Guid.NewGuid().ToString() + ".jpg";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            product.ProductImage = filename;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                var productJson = JsonConvert.SerializeObject(product);
                using (HttpResponseMessage response = await httpClient.PostAsync($"{API_URL}/api/Product/create", new StringContent(productJson, Encoding.UTF8, "application/json")))
                {

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                    else
                        return View(product);

                }
            }

        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            ProductEditViewModel productEditViewModel = null;
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/EditProduct/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        productEditViewModel = JsonConvert.DeserializeObject<ProductEditViewModel>(result);

                    }
                }
            }

            return View(productEditViewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel Model, IFormFile file)
        {


            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                string filename = System.Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                Model.Product.ProductImage = filename;
            }


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                var productJson = JsonConvert.SerializeObject(Model.Product);
                using (HttpResponseMessage response = await httpClient.PutAsync($"{API_URL}/api/Product/Edit", new StringContent(productJson, Encoding.UTF8, "application/json")))
                {

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                    else
                        return View(Model);

                }
            }
        }


        [Authorize(Roles = "Admin, Approver")]
        public async Task<IActionResult> Details(int? id)
        {
            TblProducts product = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/GetDetailsProduct/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<TblProducts>(result);

                    }
                }
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {

            TblProducts product = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/DeleteProduct/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<TblProducts>(result);

                    }
                }
            }
            return View(product);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.DeleteAsync($"{API_URL}/api/Product/Delete/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                       

                    }
                }
            }
            return RedirectToAction("Index");

        }

    }
}
