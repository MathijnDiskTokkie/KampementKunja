using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        public ActionResult Index(string updaten, int? boekingid, string voornaam, string achternaam, string postcode, string woonplaats)
        {

            if (updaten != null)
            {
                if (!updaten.Equals("True"))
                {
                    var boeking = from i in db.Boekingen where i.Boekingid == boekingid select i;
                    db.Boekingen.Remove(boeking.FirstOrDefault());
                    db.SaveChanges();

                }
                else
                {
                    // updaten
                    var gebruiker = from s in db.Gasten where s.Email == User.Identity.Name select s;
                    if (gebruiker.Any())
                    {
                        gebruiker.FirstOrDefault().Voornaam = voornaam;
                        gebruiker.FirstOrDefault().Achternaam = achternaam;
                        gebruiker.FirstOrDefault().Woonplaats = woonplaats;
                        gebruiker.FirstOrDefault().Postcode = postcode;
                        db.SaveChanges();
                    }
                }
            }
            var gebruikerid = from a in db.Gasten where a.Email == User.Identity.Name select a.GastenID;

            var boekingen = from i in db.Boekingen where i.gastID == gebruikerid.FirstOrDefault() select i;

            MijnProfiel profiel = new MijnProfiel();
            profiel.boekingen = boekingen.ToList();
            profiel.gast = (from d in db.Gasten where d.Email == User.Identity.Name select d).FirstOrDefault();
           
            
            return View(profiel);
        }
        public ActionResult Boeken() {
            return View(new BoekenModel());
        }

        [HttpPost]
        public ActionResult Boeken(BoekenModel model)
        {
            CultureInfo MyCultureInfo = new CultureInfo("nl-NL");

            DateTime incheckdatum = model.incheckdatum;
            DateTime uitcheckdatum = model.uitcheckdatum;


            List<LodgeTypes> lodgeTypes = new List<LodgeTypes>();


            /*var boekingen = from a in db.Boekingen select a;

            if (!boekingen.Any())
            {
                lodgeTypes = (from lod in db.LodgeTypes select lod).Distinct().ToList();
                

            }
            else
            {*/

                var lodges = (from dbLoges in db.Lodges
                              where (incheckdatum < dbLoges.Boekingen.FirstOrDefault().incheckdatum && uitcheckdatum < dbLoges.Boekingen.FirstOrDefault().incheckdatum) ||
                              (incheckdatum > dbLoges.Boekingen.FirstOrDefault().uitcheckdatum && uitcheckdatum > dbLoges.Boekingen.FirstOrDefault().uitcheckdatum) || (dbLoges.Boekingen.FirstOrDefault() == null) 
                              select dbLoges).ToList();

                foreach (var item in lodges)
                {
                    lodgeTypes.Add(item.LodgeTypes);
                }

            //}

            lodgeTypes.Distinct().ToList();


            BoekenModel boekmodle = new BoekenModel();
            boekmodle.lodgestypes = lodgeTypes;
            boekmodle.incheckdatum = incheckdatum;
            boekmodle.uitcheckdatum = uitcheckdatum;
            return View(boekmodle);
        }



        public ActionResult BoekingBevestigen(DateTime incheckdatum, DateTime uitcheckdatum, int lodgetype) {

            if (lodgetype != 0)
            {
                Wagenpark.Models.BoekingBevestigen boek = new Wagenpark.Models.BoekingBevestigen();
                Boekingen boeken = new Boekingen();
                boeken.incheckdatum = incheckdatum;
                boeken.uitcheckdatum = uitcheckdatum;
                boeken.lodgeID = lodgetype;



                boeken.uitcheckdatum =uitcheckdatum;
                boeken.lodgeID = lodgetype;
                boek.boeking = boeken;
                boek.lodge = (from i in db.LodgeTypes select i).FirstOrDefault();

                    return View(boek);
                }
                else {

                    return null;

                }
        }

        public ActionResult EmailBevestigen(DateTime incheckdatum, DateTime uitcheckdatum, int lodgeid) {

            //id is lodgetype
            var gebruiker = (from i in db.Gasten where i.Email == User.Identity.Name select i);

            if (gebruiker.Any()) {

                var lodges = (from dbLoges in db.Lodges
                              where (incheckdatum < dbLoges.Boekingen.FirstOrDefault().incheckdatum && uitcheckdatum < dbLoges.Boekingen.FirstOrDefault().incheckdatum) ||
                              (incheckdatum > dbLoges.Boekingen.FirstOrDefault().uitcheckdatum && uitcheckdatum > dbLoges.Boekingen.FirstOrDefault().uitcheckdatum)
                              select dbLoges).ToList();

                var boekingen = from i in db.Boekingen select i;

                if (lodges.Any())
                {
                    var lodgesbeschikbaar = from a in db.Lodges where a.LodgeTypeID == lodgeid select a;

                    Boekingen df = new Boekingen();
                    df.gastID = gebruiker.FirstOrDefault().GastenID;
                    df.incheckdatum = incheckdatum;
                    df.uitcheckdatum = uitcheckdatum;
                    df.lodgeID = lodgesbeschikbaar.FirstOrDefault().LodgeID;
                    db.Boekingen.Add(df);
                    db.SaveChanges();

                    EmailVerzenden(df);

                    return View();

                }
                else {

                    if (!boekingen.Any()) {

                        var lodgesbeschikbaar = from a in db.Lodges where a.LodgeTypeID == lodgeid select a;

                        Boekingen df = new Boekingen();
                        df.gastID = gebruiker.FirstOrDefault().GastenID;
                        df.incheckdatum = incheckdatum;
                        df.uitcheckdatum = uitcheckdatum;
                        df.lodgeID = lodgesbeschikbaar.FirstOrDefault().LodgeID;
                        db.Boekingen.Add(df);
                        db.SaveChanges();

                        EmailVerzenden(df);

                        return View();

                    }
                    // error
                    return null;
                }
                

            }

            return null;

            
        }


        public void EmailVerzenden(Boekingen boekingen)
        {

            // verzend een email
            var gebruikerd = from i in db.Boekingen where i.Boekingid == boekingen.Boekingid select i;

            try
            {
                string gebruiker = User.Identity.Name;



                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;


                //If you need to authenticate
                client.Credentials = new NetworkCredential("kampementkunja@gmail.com", "Kampementkunja123");
                MailMessage mailMessage = new MailMessage();
                MailAddress mailAddress = new MailAddress("noreply@kampementkunja.nl");
                mailMessage.From = mailAddress;
                mailMessage.To.Add(gebruiker);
                mailMessage.Subject = "Bevestiging boeking " + boekingen.Boekingid;
                mailMessage.Body = "Beste "+gebruikerd.FirstOrDefault().Gasten.Voornaam+",\n\n" +
                    "Hartelijk dank voor uw boeking bij kampement kunja. Wij willen doormiddel van deze mail u een bevestiging sturen.\n" +
                    "Hieronder hebben we uw boekingsdetails:\n\n" +
                    "" +
                    "Incheckdatum: " + boekingen.incheckdatum.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("nl-NL")) + "\n" +
                    "Uitcheckdatum: " + boekingen.uitcheckdatum.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("nl-NL")) + "\n" +
                    "\n" +
                    "\n" +
                    "Wij hopen hiermee u voldoende geinformeerd te hebben.\n\n\n" +
                    "" +
                    "" +
                    "Met vriendelijke groet,\n\n" +
                    "" +
                    "" +
                    "Kampement Kunja";
                    

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {


            }


        }

        

        public ActionResult GetLodgeTypes()
        {
            return PartialView("LodgeTypePartial");
        }
    }
}