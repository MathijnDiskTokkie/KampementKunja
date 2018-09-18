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
        KunjaDBConnection db = new KunjaDBConnection();        
       

        // GET: Boeking
        public ActionResult Index()
        {
            MijnProfiel ass = new MijnProfiel();
           
            

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