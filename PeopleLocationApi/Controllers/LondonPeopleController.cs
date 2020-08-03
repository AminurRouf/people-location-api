using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleLocationApi.Constants;
using PeopleLocationApi.Models;
using PeopleLocationApi.Tasks;
using Swashbuckle.AspNetCore.Annotations;

namespace PeopleLocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LondonPeopleController : ControllerBase
    {
        private readonly IPeopleLocationTask _peopleLocationTasks;

        public LondonPeopleController(IPeopleLocationTask peopleLocationTasks)
        {
            _peopleLocationTasks = peopleLocationTasks;
        }

        /// <summary>
        /// Get people who are listed as living in London.
        /// </summary>
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not Found")]
        [Produces(typeof(List<Person>))]
        [HttpGet("/city/london/people")]
        public async Task<IActionResult> GetPeopleLivingInLondon()
        {
            var people = await _peopleLocationTasks.GetPeopleLivingIn(LondonCityConstants.Name);
            if (people == null)
                return NotFound();
            return Ok(people);
        }

        /// <summary>
        /// Get people whose current coordinates are within 50 miles of London.
        /// </summary>
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not Found")]
        [Produces(typeof(List<Person>))]
        [HttpGet("/city/london/coordinates-within-fifty-miles/people")]
        public async Task<IActionResult> GetPeopleCoordinatesWithinFiFtyMilesOfLondon()
        {
            var people = await _peopleLocationTasks.GetPeopleCoordinatesWithIn(50,
                new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude));

            if (people == null)
                return NotFound();
            return Ok(people);
        }

        /// <summary>
        /// Get people who are listed as either living in London, or whose current coordinates are within 50 miles of London.
        /// </summary>
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not Found")]
        [Produces(typeof(List<Person>))]
        [HttpGet("/city/london/living-in-or-coordinates-within-fifty-miles/people")]
        public async Task<IActionResult> GetPeopleLivingInOrCoordinatesWithInFiftyMilesOfLondon()
        {
            var people =
                await _peopleLocationTasks.GetPeopleLivingInOrCoordinatesWithInFiftyMiles(LondonCityConstants.Name, 50,
                    new GeoCoordinate(LondonCityConstants.Latitude, LondonCityConstants.Longitude));

            if (people == null)
                return NotFound();
            return Ok(people);
        }
    }
}