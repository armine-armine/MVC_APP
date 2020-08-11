using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Interfaces
{
   public interface ICategory
    {
        public IEnumerable<TblProductCategory> GetAllCategory();
    }
}
