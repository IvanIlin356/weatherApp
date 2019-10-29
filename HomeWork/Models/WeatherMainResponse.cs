using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class WeatherMainResponse
    {
        public float temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }

        public string name { get; set; }
    }
}