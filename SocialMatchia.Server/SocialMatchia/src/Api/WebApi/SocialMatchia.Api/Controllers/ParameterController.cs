using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMatchia.Application.Features.Queries.Parameter;
using SocialMatchia.Common.Features.ResponseModel;

namespace SocialMatchia.Api.Controllers
{
    public class ParameterController : BaseController
    {
        private readonly IMediator _mediator;

        public ParameterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityResponse>>> CityAsync()
        {
            var response = await _mediator.Send(new CityQuery());
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<CountryResponse>>> CountryAsync()
        {
            var response = await _mediator.Send(new CountryQuery());
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<GenderResponse>>> GenderAsync()
        {
            var response = await _mediator.Send(new GenderQuery());
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<SocialMediaResponse>>> SocialMediaAsync()
        {
            var response = await _mediator.Send(new SocialMediaQuery());
            return this.ToActionResult(response);
        }
    }
}
