using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wagenpark.Controllers
{
    public class BoekingenController : Controller
    {
        // GET: Boekingen
        public ActionResult Index()
        {
            // wijzig boekingen, toegankelijk voor de medewerkers van kunja
            return View();
        }
    }
}