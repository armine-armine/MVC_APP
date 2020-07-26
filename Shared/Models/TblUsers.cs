using System;
using System.Collections.Generic;

namespace Shared.Models
{
    public  class TblUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }

        public virtual TblUserRoles UserRole { get; set; }

    }
}
