using HomeWork.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace HomeWork.Controllers
{
    public class HomeController : Controller
    {
        WeatherDBContext db = new WeatherDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult WeatherData(string cityName)
        {
            WeatherMainResponse data = getWeatherMainResponse(weather_get(cityName));
                       
            if (data == null)
            {
                return WrongCity();
            }

            saveCityToHistory(cityName);
            return PartialView(data);       
        }

        public ActionResult CityHistory()
        {
            var cities = db.cities.OrderByDescending(c => c.LastUse).Take(10).ToList();
            return PartialView(cities);
        }

        public ActionResult WrongCity()
        {
            return PartialView("WrongCity");
        }

        public static string weather_get(string cityName)
        {
            string result = null;
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add(Constants.HEADER_APIKEY, Constants.API_KEY);
                httpClient.DefaultRequestHeaders
                      .Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.HEADER_CONTENT));
                try
                {
                    result = httpClient.GetStringAsync($"{Constants.API_URL}?{Constants.API_PARAM}{cityName}").Result;
                }
                catch
                {
                    result = null;
                }

                //var d = JObject.Parse(result)["main"].ToString();
            }

                return result;
        }

        public static WeatherMainResponse getWeatherMainResponse(string response)
        {
            if (response == null || response.Length == 0) return null;

            string resp = JObject.Parse(response)["main"].ToString();
            WeatherMainResponse result = JsonConvert.DeserializeObject<WeatherMainResponse>(resp);
            result.name = JObject.Parse(response)["name"].ToString();
            result.temp = (float)Math.Round(result.temp - Constants.KELV2CEL, 2);
            result.temp_max = (float)Math.Round(result.temp_max - Constants.KELV2CEL, 2);
            result.temp_min = (float)Math.Round(result.temp_min - Constants.KELV2CEL, 2);

            return result;            
        }

        public void saveCityToHistory(string cityName)
        {
            var c = db.cities.Where(ci => ci.Name == cityName).FirstOrDefault();
            if (c == null)
            {
                db.cities.Add(new City { Name = cityName , LastUse = DateTime.Now});
            }
            else
            {
                c.LastUse = DateTime.Now;
                db.Entry(c).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
        }

    }
}