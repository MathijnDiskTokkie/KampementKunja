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
            var gebruikerid = from a in db.Gasten where a.Email == User.Identity.Name select a.GastenID;

            var boekingen = from i in db.Boekingen where i.gastID == gebruikerid.FirstOrDefault() select i;

            MijnProfiel profiel = new MijnProfiel();
            profiel.boekingen = boekingen.ToList();
            profiel.gast = (from d in db.Gasten where d.Email == User.Identity.Name select d).FirstOrDefault();
           
            
            return View(profiel);
        }

        public ActionResult Boeken() {

            List<LodgeTypes> lodgeTypes = new List<LodgeTypes>();

            lodgeTypes = (from i in db.Lodges where i.Bezettingsgraad == true select i.LodgeTypes).Distinct().ToList();

            return View(lodgeTypes);
        }

        public ActionResult BoekingBevestigen() {

            Wagenpark.Models.BoekingBevestigen boek = new Wagenpark.Models.BoekingBevestigen();
            boek.boeking = (from i in db.Boekingen select i).FirstOrDefault();
            boek.lodge = (from i in db.LodgeTypes select i).FirstOrDefault();

            return View(boek);
        }

        public ActionResult EmailBevestigen() {

            return View();
        }
    }
}