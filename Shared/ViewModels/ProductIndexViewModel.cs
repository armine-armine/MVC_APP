using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersProd.ViewModels
{
    public class ProductEditViewMode
    {
      public  TblProducts Product { get; set; }
     public   List<TblProductCategory> Category { get; set; }

    }
}
