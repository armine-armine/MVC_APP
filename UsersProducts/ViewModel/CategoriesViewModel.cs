using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersProducts.ViewModel
{
    public class CategoriesViewModel
    {
        public CategoriesViewModel()
        {
            TblProducts = new HashSet<ProductViewModel>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<ProductViewModel> TblProducts { get; set; }
    }
}
