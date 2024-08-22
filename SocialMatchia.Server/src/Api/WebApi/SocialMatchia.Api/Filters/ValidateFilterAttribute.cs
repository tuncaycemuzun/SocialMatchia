using Microsoft.AspNetCore.Mvc.Filters;
using SocialMatchia.Common.Exceptions;

namespace SocialMatchia.Api.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToArray();

                var failureJoinedMessages = string.Join("##", errors);

                throw new PropertyValidationException(failureJoinedMessages);
            }
        }
    }
}
