using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsersProducts.Models
{
    public partial class TblProductCategory
    {
        public TblProductCategory()
        {
            TblProducts = new HashSet<TblProducts>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<TblProducts> TblProducts { get; set; }
    }
}
