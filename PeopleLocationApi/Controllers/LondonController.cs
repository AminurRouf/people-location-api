using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PeopleLocationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LondonController : ControllerBase
    {
        [SwaggerResponse(200, "Success")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new [] { "value1", "value2" };
        }

    }
}
