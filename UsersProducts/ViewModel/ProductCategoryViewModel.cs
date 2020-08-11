using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsersProducts.ViewModel
{
    public class ProductCategoryViewModel
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProCatId { get; set; }
        public string CategoryName { get; set; }
        //public List<CategoryViewModel> Category { get; set; }
        //public ProductCategoryViewModel()
        //{
        //    Category = new List<CategoryViewModel>();
        //}


} 
//    public class CategoryViewModel
//    {
//        public int id { get; set; }
//        public string Name { get; set; }
//    }

}
