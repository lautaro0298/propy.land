using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImprovedWebApp.DTOs
{
    public class ViewPortDto
    {
        public NortheastDto Northeast { get; set; }
        public SouthwestDto Southwest { get; set; }
    }

    public class NortheastDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class SouthwestDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
