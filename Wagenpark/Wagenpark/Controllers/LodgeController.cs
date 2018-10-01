using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    public class LodgeController : Controller
    {

         
        // GET: Lodge
        public ActionResult Index()
        {
            return View();
        }
    }
}