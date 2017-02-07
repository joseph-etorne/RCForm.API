using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RCForm.API.Models;
using RCForm.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private IRCFormRepository _repo;

        public CitiesController(IRCFormRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _repo.GetCities();
            var results = Mapper.Map<IEnumerable<CityWithoutPOIDTO>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePOI=false)
        {

            var city = _repo.GetCity(id, includePOI);

            if (city==null)
            {
                return NotFound();
            }

            if (includePOI)
            {
                var cityResult = Mapper.Map<CityDTO>(city);

                return Ok(cityResult);
            }

            var cityWithoutPOIResult = Mapper.Map<CityWithoutPOIDTO>(city);

            return Ok(cityWithoutPOIResult);

        }

    }
}
