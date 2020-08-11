using System;
using System.Collections.Generic;
using UsersProducts.Models;

namespace UsersProducts.Models
{
    public partial class TblUserRoles
    {
        public TblUserRoles()
        {
            TblUsers = new HashSet<TblUsers>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<TblUsers> TblUsers { get; set; }
    }
}
