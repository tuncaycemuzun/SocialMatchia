using Ardalis.Specification;

namespace SocialMatchia.Domain.Models.Specifications
{
    public class CitiesByCountryIdSpec : Specification<City>
    {
        public CitiesByCountryIdSpec(Guid countryId)
        {
            Query.Where(x => x.CountryId == countryId);
        }
    }
}
