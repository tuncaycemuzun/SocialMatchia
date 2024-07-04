using Microsoft.AspNetCore.Identity;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Common.Interfaces;
using SocialMatchia.Domain.Models;
using SocialMatchia.Domain.Models.Specification;
using System.Security.Claims;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CurrentUserMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<User>>();
                var userInformation = scope.ServiceProvider.GetRequiredService<IReadRepository<UserInformation>>();

                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = context.RequestServices.GetRequiredService<CurrentUser>();

                if (Guid.TryParse(userId, out Guid currentUserId))
                {
                    currentUser.Id = new Guid(userId);
                }

                var hasUserInfo = context.User.FindFirstValue("HasUserInfo");

                if (hasUserInfo is null)
                {
                    var hasUserInfoInTable = await userInformation.AnyAsync(new UserInformationByUserIdSpec(currentUser.Id));

                    if (hasUserInfoInTable == false)
                    {
                        var hasUserInformationUpdateRoute = context.Request.Path.Value?.Contains("information", StringComparison.OrdinalIgnoreCase);
                        var method = context.Request.Method;

                        if (hasUserInformationUpdateRoute != true && method != "PUT")
                        {
                            throw new NotFoundException("Hesap bilgilerinizi düzenlemeniz gerekmektedir");
                        }
                    }
                    else
                    {
                        var user = await userManager.FindByIdAsync(currentUserId.ToString());
                        if (user is not null)
                        {
                            await userManager.AddClaimAsync(user, new Claim("HasUserInfo", "true"));
                            var identity = (ClaimsIdentity)context.User.Identity!;
                            identity.AddClaim(new Claim("HasUserInfo", "true"));
                        }
                    }
                }
            }
        }

        await _next(context);
    }
}
