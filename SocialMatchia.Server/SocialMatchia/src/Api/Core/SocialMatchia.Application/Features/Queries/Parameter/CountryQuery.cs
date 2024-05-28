﻿using Ardalis.Result;
using MediatR;
using SocialMatchia.Common.Features.ResponseModel;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Application.Features.Queries.Parameter
{
    public class CountryQuery : IRequest<Result<List<CountryResponse>>>
    {

    }

    public class CountryHandler : IRequestHandler<CountryQuery, Result<List<CountryResponse>>>
    {
        private readonly IReadRepository<Country> _repository;

        public CountryHandler(IReadRepository<Country> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<CountryResponse>>> Handle(CountryQuery request, CancellationToken cancellationToken)
        {
            var countries = await _repository.ListAsync();
            var response = countries.Select(c => new CountryResponse
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return Result.Success(response);
        }
    }
}