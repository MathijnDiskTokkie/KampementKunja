using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wagenpark.Models
{
    public class BoekenModel
    {

        public List<LodgeTypes> lodgestypes { get; set;}
        public DateTime incheckdatum { get; set; }
        public DateTime uitcheckdatum { get; set; }
    }
}