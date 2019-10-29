using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public static class Constants
    {
        public const string HEADER_APIKEY = "X-Api-Key";
        public const string API_KEY = "fa4f5d1686a225bb235adcafcb45d523";
        public const string HEADER_CONTENT = "application/json";
        public const string API_URL = @"https://api.openweathermap.org/data/2.5/weather";
        public const string API_PARAM = "q=";
        public const float KELV2CEL = 273f;
    }
}