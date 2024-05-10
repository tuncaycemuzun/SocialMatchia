using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialMatchia.Api.Controllers
{
    public class TestController : BaseController
    {
        [Authorize]
        [HttpGet]
        public IActionResult Auth()
        {
            return Ok();
        }


        [HttpGet]
        public IActionResult NonAuth()
        {
            return Ok();
        }
    }
}
