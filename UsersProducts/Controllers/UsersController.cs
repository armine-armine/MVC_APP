using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace UsersProducts.Controllers
{
    public class UsersController : Controller
    { public readonly db_UsersProductContext _constring;
        public UsersController(db_UsersProductContext constring)
        {
            _constring = constring;
        }
        public IActionResult Index()
        {
            var result = _constring.Tbl_Users.ToList();
            return View(result);
        }
    }
}
