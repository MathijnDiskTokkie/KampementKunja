using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Wagenpark.Models
{
    public class BoekingBevestigen
    {
        
        public LodgeTypes lodge { get; set; }
        public Boekingen boeking { get; set; }


        public int aantalnachten
        {
            get
            {
                DateTime start = boeking.incheckdatum;
                DateTime end = boeking.uitcheckdatum;

                return ((int)(end - start).TotalDays) - 1;

            }

          
        }

        public string Datumvanaf
        {

            get {
                CultureInfo MyCultureInfo = new CultureInfo("nl-NL");


                return boeking.incheckdatum.ToString("dd MMM yyyy", MyCultureInfo);
            }


        }

        public string Datumtot
        {

            get
            {
                CultureInfo MyCultureInfo = new CultureInfo("nl-NL");


                return boeking.uitcheckdatum.ToString("dd MMM yyyy", MyCultureInfo);
            }


        }


    }
}