using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HomeWork.Models
{
    public class WeatherDBContext : DbContext
    {
       // public DbSet<WeatherData> weatherData { get; set; }
        public DbSet<City> cities { get; set; }

        public WeatherDBContext()
            : base("Name=WeatherDBContext")
        {
            //Database.SetInitializer<WeatherDBContext>(new CreateDatabaseIfNotExists<WeatherDBContext>());
        }
    }
}