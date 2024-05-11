using Ardalis.Result.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SocialMatchia.Application.Features.Commands.Like;
using SocialMatchia.Application.Features.Commands.UserInformation;
using SocialMatchia.Application.Features.Commands.UserPhoto;
using SocialMatchia.Application.Features.Commands.UserSetting;
using SocialMatchia.Application.Features.Commands.UserSocialMedia;
using SocialMatchia.Application.Features.Queries.UserInformation;
using SocialMatchia.Application.Features.Queries.UserPhoto;
using SocialMatchia.Application.Features.Queries.UserSetting;
using SocialMatchia.Application.Features.Queries.UserSocialMedia;
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
        public async Task<ActionResult<UserInformationResponse>> InformationAsync([FromBody] UserInformationQuery query)
        {
            var response = await _mediator.Send(query);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> UpsertInformationAsync([FromBody] UpsertUserInformationCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreatePhotoAsync([FromBody] CreateUserPhotoCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeletePhotoAsync([FromBody] DeleteUserPhotoCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> PhotosAsync([FromBody] UserPhotoQuery query)
        {
            var response = await _mediator.Send(query);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<UserSettingResponse>> SettingAsync([FromBody] UserSettingQuery query)
        {
            var response = await _mediator.Send(query);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> UpsertSettingAsync([FromBody] UpsertUserSettingCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }


        [HttpPost]
        public async Task<ActionResult<bool>> UpsertSocialMediaAsync([FromBody] UpsertUserSocialMediaCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<UserSocialMediaResponse>> SocialMediaAsync([FromBody] UserSocialMediaQuery query)
        {
            var response = await _mediator.Send(query);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> LikeAsync([FromBody] CreateLikeCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> UndoLikeAsync([FromBody] UndoLikeCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }
    }
}
