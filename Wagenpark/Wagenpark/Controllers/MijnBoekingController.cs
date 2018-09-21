using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ActionResult Boeken(string incheckdatum1, string uitcheckdatum1) {

            

            if (incheckdatum1 != null)
            {


                CultureInfo MyCultureInfo = new CultureInfo("nl-NL");

                DateTime incheckdatum = DateTime.ParseExact(incheckdatum1, "dd-MM-yyyy",MyCultureInfo);
                DateTime uitcheckdatum = DateTime.ParseExact(incheckdatum1, "dd-MM-yyyy", MyCultureInfo); 




                var lodges = from a in db.Lodges where (incheckdatum < a.Boekingen.FirstOrDefault().incheckdatum && uitcheckdatum < a.Boekingen.FirstOrDefault().incheckdatum) || (incheckdatum > a.Boekingen.FirstOrDefault().uitcheckdatum && uitcheckdatum > a.Boekingen.FirstOrDefault().uitcheckdatum) select a;


                List<LodgeTypes> lodgeTypes = new List<LodgeTypes>();

                foreach (var item in lodges)
                {


                    lodgeTypes.Add(item.LodgeTypes);
                }

                lodgeTypes.Distinct().ToList();

                BoekenModel boekmodle = new BoekenModel();
                boekmodle.lodgestypes = lodgeTypes;
               

                //lodgeTypes = (from i in db.Lodges where i.Bezettingsgraad == true select i.LodgeTypes).Distinct().ToList();

                return View(boekmodle);

            }
            else {

                return View(new BoekenModel());
            }
        }



        public ActionResult BoekingBevestigen() {

            Wagenpark.Models.BoekingBevestigen boek = new Wagenpark.Models.BoekingBevestigen();

            Boekingen boeken = new Boekingen();
            boeken.incheckdatum = DateTime.Now.AddDays(-1);
            boeken.uitcheckdatum = DateTime.Now.AddDays(3);
            boeken.lodgeID = 1;
            boek.boeking = boeken;

            /*boek.boeking = (from i in db.Boekingen select i).FirstOrDefault();*/
            boek.lodge = (from i in db.LodgeTypes select i).FirstOrDefault();

            return View(boek);
        }

        public ActionResult EmailBevestigen(DateTime incheckdatum, DateTime uitcheckdatum, int lodgeid) {

            //id is lodgetype
            var gebruiker = (from i in db.Gasten where i.Email == User.Identity.Name select i);

            if (gebruiker.Any()) {

                var lodges = from a in db.Lodges where (incheckdatum < a.Boekingen.FirstOrDefault().incheckdatum && uitcheckdatum < a.Boekingen.FirstOrDefault().incheckdatum) || (incheckdatum > a.Boekingen.FirstOrDefault().uitcheckdatum && uitcheckdatum > a.Boekingen.FirstOrDefault().uitcheckdatum) && a.LodgeTypeID == lodgeid select a;

                if (lodges.Any())
                {
                    Boekingen df = new Boekingen();
                    df.gastID = gebruiker.FirstOrDefault().GastenID;
                    df.incheckdatum = incheckdatum;
                    df.uitcheckdatum = uitcheckdatum;
                    df.lodgeID = lodges.FirstOrDefault().LodgeID;

                    db.Boekingen.Add(df);
                    db.SaveChanges();

                    return View();

                }
                else {
                    // error
                    return null;
                }
                

            }

            return null;

            
        }
    }
}