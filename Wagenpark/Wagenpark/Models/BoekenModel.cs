using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wagenpark.Models
{
    public class BoekenModel
    {

        public List<LodgeTypes> lodgestypes { get; set;}
        public string incheckdatum { get; set; }
        public string uitcheckdatum { get; set; }
    }
}