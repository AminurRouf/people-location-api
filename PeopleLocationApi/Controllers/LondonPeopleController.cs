using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PeopleLocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LondonPeopleController : ControllerBase
    {
        /// <summary>
        /// Fetches all people who are listed as living either in London, 
        /// or whose current coordinates are within 50 miles of London
        /// </summary>
        [SwaggerResponse(200, "Success")]
        [SwaggerResponse(404, "Not Found")]
        [HttpGet("/london/people/")]
        public IEnumerable<string> Get()
        {
            return new [] { "value1", "value2" };
        }

    }
}
