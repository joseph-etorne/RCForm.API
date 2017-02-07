using RCForm.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API
{
    public static class RCFormContextExtensions
    {
        public static void EnsureSeedDataForContext(this RCFormContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            //init seed data
            var cities = new List<City>()
            {
                new City()
                {
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Central Park",
                            Description = "The most visited urban park in the US."
                        },
                        new PointOfInterest()
                        {
                            Name="Empire State Building",
                            Description="A 102-storey skyscraper located in Midtown Manhattan."
                        }
                    }
                },
                new City()
                {
                    Name="Naga City",
                    Description="An maogmang lugar.",
                    PointsOfInterest= new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Naga Garden Restaurant",
                            Description="An old Chinese family restaurant that made the famous toasted siopao."
                        },
                        new PointOfInterest()
                        {
                            Name="CWC",
                            Description="A famous wakeboarding spot."
                        }
                    }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
