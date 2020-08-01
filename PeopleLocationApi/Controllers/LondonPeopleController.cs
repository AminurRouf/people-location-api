using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleLocationApi.Models;
using PeopleLocationApi.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace PeopleLocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LondonPeopleController : ControllerBase
    {
        private readonly IBpdtsTestAppService _bpdtsTestAppService;

        public LondonPeopleController(IBpdtsTestAppService bpdtsTestAppService)
        {
            _bpdtsTestAppService = bpdtsTestAppService;
        }

        /// <summary>
        /// Fetches all people who are listed as living either in London, 
        /// or whose current coordinates are within 50 miles of London
        /// </summary>
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not Found")]
        [HttpGet("/london/people/")]
        public async Task<IActionResult> GetPeopleLivingInLondon()
        {
            var people = await _bpdtsTestAppService.GetPeopleLivingInLondon();
            if (people == null)
                return NotFound();
            return  Ok(people);
        }

    }
}
