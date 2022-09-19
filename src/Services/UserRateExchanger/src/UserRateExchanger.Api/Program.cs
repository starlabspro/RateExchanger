using BuildingBlocks.Caching;
using BuildingBlocks.Contracts;
using BuildingBlocks.Middleware;
using BuildingBlocks.Swagger;
using BuildingBlocks.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using UserRateExchanger;
using UserRateExchanger.Features;
using UserRateExchanger.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(RootInjectionClass).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(RootInjectionClass).Assembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationHandler<,>));
builder.Services.AddRateExchangerService(builder.Configuration);
builder.Services.AddInMemoryCache(builder.Configuration);
builder.Services.AddAutoMapper(typeof(UserRateExchangerProfile).Assembly);
builder.Services.AddTransient<ICacheManager<List<DateTime>>, RequestLimitCacheManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupSwaggerGen(builder.Configuration);
// builder.Services.AddVersionedApiExplorer();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var loggerFactory = app.Services.GetService<ILoggerFactory>();
app.UseExceptionHandler(loggerFactory!);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();