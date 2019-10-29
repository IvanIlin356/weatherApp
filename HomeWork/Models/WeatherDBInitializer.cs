using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HomeWork.Models
{
    public class WeatherDBInitializer : DropCreateDatabaseAlways<WeatherDBContext>
    {
        protected override void Seed(WeatherDBContext context)
        {
            context.cities.Add(new City { Name = "Moscow", LastUse = DateTime.Now });      
            context.cities.Add(new City { Name = "Tokyo", LastUse = DateTime.Now });

            base.Seed(context);
        }
    }
}