using Microsoft.AspNetCore.Identity;
using SocialMatchia.Infrastructure.Persistence.Context;
using SocialMatchia.Infrastructure.Persistence.Extensions;
using SocialMatchia.Application.Extensions;
using SocialMatchia.Api.Filters;
using FluentValidation.AspNetCore;
using SocialMatchia.Common.Exceptions;
using System.Reflection;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SocialMatchia.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(opt=> opt.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(opt =>
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

builder.Services.AddApplicationServices();
builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddIdentity<IdentityUser<Guid>,IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<SocialMatchiaDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomException();
app.UseAuthorization();

app.MapControllers();

app.Run();
