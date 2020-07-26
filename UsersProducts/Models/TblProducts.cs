using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsersProducts.Models
{
    public partial class TblProducts
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProCatId { get; set; }

        public virtual TblProductCategory ProductCategory { get; set; } 
        
    }
}
