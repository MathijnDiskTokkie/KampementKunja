using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wagenpark.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            // wijzig de managers dit is alleen toegankelijk voor de admin panel
            return View();
        }
    }
}