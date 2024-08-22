using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialMatchia.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class BaseController : ControllerBase
    {
        
    }
}
