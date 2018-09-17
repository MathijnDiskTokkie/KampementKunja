using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wagenpark.Models;

namespace Wagenpark.Controllers
{
    public class MijnBoekingController : Controller
    {

        
        ApplicationDbContext dbuser = new ApplicationDbContext();
       

        // GET: Boeking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Boeken() {
            return View();
        }

        public ActionResult BoekingBevestigen() {

            return View();
        }

        public ActionResult EmailBevestigen() {

            return View();
        }
    }
}