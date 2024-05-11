using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.ParameterModel.Specification
{
    public class GetCitiesByCountryIdSpec : Specification<City>
    {
        public GetCitiesByCountryIdSpec(Guid countryId)
        {
            Query.Where(x => x.CountryId == countryId);
        }
    }
}
