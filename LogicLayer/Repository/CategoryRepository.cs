using LogicLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Repository
{
   public class CategoryRepository:ICategory
    {
        db_UsersProductContext _dbContext;
        public CategoryRepository(db_UsersProductContext dbContext)
        {
            _dbContext = dbContext;
    }
        public IEnumerable<TblProductCategory> GetAllCategory()
        {
            var category = _dbContext.Tbl_ProductCategory;
            return category;
        }
    }
}
