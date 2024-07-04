using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMatchia.Application.Features.Commands;
using SocialMatchia.Application.Features.Queries;
using SocialMatchia.Common.Features.ResponseModel;

namespace SocialMatchia.Api.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<UserInformationResponse>> InformationAsync()
        {
            var response = await _mediator.Send(new UserInformationQuery());
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> InformationAsync([FromBody] UpsertUserInformationCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> PhotoAsync()
        {
            var response = await _mediator.Send(new UserPhotoQuery());
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PhotoAsync([FromBody] CreateUserPhotoCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> PhotoAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteUserPhotoCommand { Id = id });
            return this.ToActionResult(response);
        }


        [HttpGet]
        public async Task<ActionResult<UserSettingResponse>> SettingAsync()
        {
            var response = await _mediator.Send(new UserSettingQuery());
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> SettingAsync([FromBody] UpsertUserSettingCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserSocialMediaResponse>>> SocialMediaAsync()
        {
            var response = await _mediator.Send(new UserSocialMediaQuery());
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> SocialMediaAsync([FromBody] UpsertUserSocialMediaCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        [Route("{page}")]
        public async Task<ActionResult<List<UserSearchModel>>?> Search([FromRoute] int page)
        {
            var response = await _mediator.Send(new UserSearchQuery { Page = page });
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> LikeAsync([FromBody] CreateLikeCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<bool>> LikeAsync([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new UndoLikeCommand
            {
                 TargetUserId = id
            });
            return this.ToActionResult(response);
        }
    }
}
