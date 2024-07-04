using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class GetCitiesByCountryIdSpec : Specification<City>
    {
        public GetCitiesByCountryIdSpec(Guid countryId)
        {
            Query.Where(x => x.CountryId == countryId);
        }
    }
}
