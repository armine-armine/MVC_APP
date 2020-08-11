using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersProducts.ViewModel
{
    public class ProductCategoryEditViewModel
    {
        public ProductViewModel Product { get; set; }
        public List<CategoriesViewModel> Category { get; set; }
    }
}
