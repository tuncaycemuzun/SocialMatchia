using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialMatchia.Api.Filters;
using SocialMatchia.Api.Middlewares;
using SocialMatchia.Application.Extensions;
using SocialMatchia.Common;
using SocialMatchia.Common.Exceptions;
using SocialMatchia.Domain.Models.UserModel;
using SocialMatchia.Infrastructure.Persistence.Context;
using SocialMatchia.Infrastructure.Persistence.Extensions;
using System.Reflection;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialMatchia Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers(opt => opt.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(PropertyValidationException)));
}).AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<SocialMatchiaDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.ConfigureAll<BearerTokenOptions>(option =>
{
    option.BearerTokenExpiration = TimeSpan.FromDays(1);
    option.RefreshTokenExpiration = TimeSpan.FromDays(10);
});

builder.Services.ConfigureAll<TokenValidationParameters>(options =>
{
    options.RoleClaimType = "roles";
    options.NameClaimType = "name";
    options.ValidateIssuer = false;
    options.ValidateAudience = false;
    options.ValidateLifetime = true;
    options.ClockSkew = TimeSpan.Zero;
    options.RequireExpirationTime = true;
    options.RequireSignedTokens = true;
    options.ValidateIssuerSigningKey = true;
    options.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));

});

builder.Services.AddSingleton<CurrentUser>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapGroup("/identity").MapIdentityApi<User>();

app.UseCustomException();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddleware>();

app.MapControllers();

app.Run();

