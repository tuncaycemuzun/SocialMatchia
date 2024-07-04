﻿using Ardalis.Result.AspNetCore;
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
        public async Task<ActionResult<UserInformationResponse>> InformationAsync([FromBody] UserInformationQuery query)
        {
            var response = await _mediator.Send(query);
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> InformationAsync([FromBody] UpsertUserInformationCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PhotoAsync([FromBody] CreateUserPhotoCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> PhotoAsync([FromBody] DeleteUserPhotoCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> PhotoAsync([FromBody] UserPhotoQuery query)
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

        [HttpPut]
        public async Task<ActionResult<bool>> SettingAsync([FromBody] UpsertUserSettingCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> SocialMediaAsync([FromBody] UpsertUserSocialMediaCommand command)
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

        [HttpDelete]
        public async Task<ActionResult<bool>> LikeAsync([FromBody] UndoLikeCommand command)
        {
            var response = await _mediator.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet]
        [Route("{page}")]
        public async Task<ActionResult<List<UserSearchModel>>?> Search([FromRoute] int pageNumber)
        {
            var response = await _mediator.Send(new UserSearchQuery { Page = pageNumber });
            return this.ToActionResult(response);
        }
    }
}
