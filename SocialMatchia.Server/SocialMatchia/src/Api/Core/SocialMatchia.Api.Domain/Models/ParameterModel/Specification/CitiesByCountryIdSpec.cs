using Ardalis.Specification;
using SocialMatchia.Domain.Models.ParameterModel;

namespace SocialMatchia.Domain.Models.ParameterModel.Specification
{
    public class CitiesByCountryIdSpec : Specification<City>
    {
        public CitiesByCountryIdSpec(Guid countryId)
        {
            Query.Where(x => x.CountryId == countryId);
        }
    }
}
