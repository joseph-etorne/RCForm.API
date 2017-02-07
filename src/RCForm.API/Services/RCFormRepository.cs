using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCForm.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace RCForm.API.Services
{
    public class RCFormRepository : IRCFormRepository
    {
        private RCFormContext _ctx;

        public RCFormRepository(RCFormContext ctx)
        {
            _ctx = ctx;
        }

        public bool CityExists(int cityId)
        {
            return _ctx.Cities.Any(c => c.Id == cityId);
        }

        public IEnumerable<City> GetCities()
        {
            return _ctx.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(int cityId, bool includePOI)
        {
            if (includePOI)
            {
                return _ctx.Cities.Include(c => c.PointsOfInterest).Where(c => c.Id == cityId).FirstOrDefault();
            }

            return _ctx.Cities.Where(c => c.Id == cityId).FirstOrDefault();
        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int poiId)
        {
            return _ctx.PointsOfInterest.Where(p => p.CityId == cityId && p.Id == poiId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
        {
            return _ctx.PointsOfInterest.Where(p => p.CityId == cityId).ToList();
        }
    }
}
