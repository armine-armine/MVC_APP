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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UsersProducts.ViewModel;

namespace UsersProducts.Controllers
{
    
    public class ProductController : Controller
    {
         private readonly string API_URL = "http://localhost:5003";
        //private readonly  IConfiguration _configuration;
        //public ProductController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

      
        [Authorize(Roles = "Viewer, Admin, Approver")]
        public async Task<IActionResult> Index()
        {
           // var API_URL = _configuration["Position:Url"];
            List<ProductViewModel> productCategoryViewModel = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/GetProductList"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        productCategoryViewModel = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);

                    }
                }
            }

            return View(productCategoryViewModel);
        }

        //using (HttpResponseMessage response = await httpClient.PostAsync(API_URL, new StringContent(jsonModel, Encoding.UTF8, "application/json"))


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ProductCategoryEditViewModel tblProductCategories = new ProductCategoryEditViewModel();
            //var API_URL = _configuration["Position:Url"];
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/CreateProduct"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        tblProductCategories.Category = JsonConvert.DeserializeObject<List<CategoriesViewModel>>(result);
                    }
                }
            }

            return View(tblProductCategories);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product, IFormFile file)
        {
           // var API_URL = _configuration["Position:Url"];
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
                using (HttpResponseMessage response = await httpClient.PostAsync($"{API_URL}/api/Product/Create", new StringContent(productJson, Encoding.UTF8, "application/json")))
                {

                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                    else
                        return View(product);

                }
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
          //  var API_URL = _configuration["Position:Url"];
            //ProductViewModel product =new ProductViewModel();
            //List<CategoriesViewModel> category = new List<CategoriesViewModel>();
            ProductCategoryEditViewModel viewModel = new ProductCategoryEditViewModel();
            viewModel.Category = new List<CategoriesViewModel>();
            viewModel.Product = new ProductViewModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/EditProduct/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                       viewModel.Product = JsonConvert.DeserializeObject<ProductViewModel>(result);

                    }
                }

                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/GetCategoryList"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        viewModel.Category = JsonConvert.DeserializeObject<List<CategoriesViewModel>>(result);

                    }
                }

                //foreach (var item in category)
                //{
                //    viewModel.Category.Add(new ViewModel.CategoriesViewModel { CategoryId=item.CategoryId,CategoryName=item.CategoryName});
                //}
               
                //viewModel.Product.ProCatId = product.ProCatId;
                //viewModel.Product.ProductImage = product.ProductImage;
                //viewModel.Product.ProductPrice = product.ProductPrice;
                //viewModel.Product.ProductCategory.CategoryName = product.ProductCategory.CategoryName;
                //viewModel.Product.ProductName = product.ProductName;

            }

            return View(viewModel);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryEditViewModel Model, IFormFile file)
        {
          //  var API_URL = _configuration["Position:Url"];

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
           // var API_URL = _configuration["Position:Url"];
            ProductViewModel product = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/GetDetailsProduct/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<ProductViewModel>(result);

                    }
                }
            }
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
           // var API_URL = _configuration["Position:Url"];

            ProductViewModel product = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.GetAsync($"{API_URL}/api/Product/DeleteProduct/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {

                        var result = await response.Content.ReadAsStringAsync();
                        product = JsonConvert.DeserializeObject<ProductViewModel>(result);

                    }
                }
            }
            return View(product);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           // var API_URL = _configuration["Position:Url"];


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage response = await httpClient.DeleteAsync($"{API_URL}/api/Product/Delete/{id}")) { }
                
            }
            return RedirectToAction("Index");

        }

    }
}
