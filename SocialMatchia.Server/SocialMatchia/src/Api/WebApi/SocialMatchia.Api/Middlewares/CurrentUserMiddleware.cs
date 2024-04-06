using SocialMatchia.Common;

namespace SocialMatchia.Api.Middlewares
{
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate _next;

        public CurrentUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentUser = context.RequestServices.GetRequiredService<CurrentUser>();

            currentUser.Id = new Guid("ab750336-f152-46cb-b2d2-1e520711d361");

            await _next(context);
        }
    }
}
