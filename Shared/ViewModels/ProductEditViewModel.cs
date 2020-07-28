using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.ViewModels
{
   public class ProductEditViewModel
    {
        public TblProducts Product { get; set; }
        public List<TblProductCategory> Category { get; set; }

    }
}
