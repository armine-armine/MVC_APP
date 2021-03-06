﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public partial class TblProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProCatId { get; set; }

        public virtual TblProductCategory ProductCategory { get; set; } 
        
    }
}
