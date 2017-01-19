using RCForm.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDTO> Cities { get; set; }

        public CitiesDataStore()
        {
            //init dummy data
            Cities = new List<CityDTO>()
            {
                new CityDTO()
                {
                    Id=1,
                    Name="New York City",
                    Description="The one with that big park.",
                    PointsOfInterest= new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=1,
                            Name="Central Park",
                            Description="The most visited urban park in the United States."
                        },
                        new PointOfInterestDTO()
                        {
                            Id=2,
                            Name="Empire State Building",
                            Description="A 102-storey skyscraper located in Midtown Manhattan."
                        },
                    }
                },
                new CityDTO()
                {
                    Id=2,
                    Name="Antwerp",
                    Description="The one with the cathedral that was never really finished.",
                    PointsOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=3,
                            Name="Cathedral of Our Lady",
                            Description="A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans." 
                        },
                        new PointOfInterestDTO()
                        {
                            Id=4,
                            Name="Antwerp Central Station",
                            Description="The finest exampleof railway architecture in Belgium."
                        },
                    }
                },
                new CityDTO()
                {
                    Id=3,
                    Name="Paris",
                    Description="The one with the big tower.",
                    PointsOfInterest = new List<PointOfInterestDTO>()
                    {
                        new PointOfInterestDTO()
                        {
                            Id=5,
                            Name="Eiffel Tower",
                            Description="A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel."
                        },
                        new PointOfInterestDTO()
                        {
                            Id=6,
                            Name="The Louvre",
                            Description="The world's largest museum."
                        },
                    }
                },
            };
        }
    }
}
