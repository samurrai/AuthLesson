using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookieAuthLesson.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Registration([FromForm]AuthViewModel authViewModel)
        {

        }
    }
}