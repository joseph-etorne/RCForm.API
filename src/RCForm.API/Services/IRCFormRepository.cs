using RCForm.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Services
{
    public interface IRCFormRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePOI);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int poiId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest poi);
        void DeletePointOfInterest(PointOfInterest poi);
        bool Save();
    }
}
