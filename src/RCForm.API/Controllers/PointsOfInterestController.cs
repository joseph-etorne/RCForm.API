using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RCForm.API.Models;
using RCForm.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCForm.API.Controllers
{
    [Route("api/cities")]

    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private IRCFormRepository _repo;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, IRCFormRepository repo)
        {
            _logger = logger;
            _mailService = mailService;
            _repo = repo;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {

                if (!_repo.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found.");
                    return NotFound();
                }

                var poiForCity = _repo.GetPointsOfInterestForCity(cityId);

                var poiForCityResults = Mapper.Map<IEnumerable<PointOfInterestDTO>>(poiForCity);

                return Ok(poiForCityResults);
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }

        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name ="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if (!_repo.CityExists(cityId))
            {
                return NotFound();
            }

            var poi = _repo.GetPointOfInterestForCity(cityId, id);

            if (poi==null)
            {
                return NotFound();
            }

            var poiResult = Mapper.Map<PointOfInterestDTO>(poi);

            return Ok(poiResult);

        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDTO pointOfInterest)
        {
            if (pointOfInterest==null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repo.CityExists(cityId))
            {
                return NotFound();
            }


            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _repo.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdPOItoReturn = Mapper.Map<PointOfInterestDTO>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new
            { cityId = cityId, id = createdPOItoReturn.Id }, createdPOItoReturn);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDTO pointOfInterest)
        {
            if (pointOfInterest==null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repo.CityExists(cityId))
            {
                return NotFound();
            }

            var poiEntity = _repo.GetPointOfInterestForCity(cityId,id);

            if (poiEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, poiEntity);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }


        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDTO> patchDoc)
        {
            //Find entity first then map it to a DTO before applying patchdoc.

            if (patchDoc==null)
            {
                return BadRequest();
            }

            if (!_repo.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _repo.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDTO>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_repo.CityExists(cityId))
            {
                return NotFound();
            }

            var poi = _repo.GetPointOfInterestForCity(cityId, id);

            if (poi==null)
            {
                return NotFound();
            }

            _repo.DeletePointOfInterest(poi);

            if (!_repo.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            _mailService.Send("Point of Interest Deleted.", $"Point of interest {poi.Name} with id {poi.Id} was deleted.");

            _logger.LogCritical($"Point of interest with City ID no. {cityId} and ID no. {id} was deleted from database.");

            return NoContent();
        }
    }
}
