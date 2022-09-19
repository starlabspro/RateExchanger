using BuildingBlocks.Caching;
using BuildingBlocks.EFCore;
using BuildingBlocks.FixerClient;
using BuildingBlocks.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using RateExchanger;
using RateExchanger.Data;
using RateExchanger.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(typeof(RootInjectionClass).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(RootInjectionClass).Assembly);
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationHandler<,>));
builder.Services.AddDbContext<RateExchangerContext>(builder.Configuration);
builder.Services.AddFixerClient(builder.Configuration);
builder.Services.AddInMemoryCache(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupSwaggerGen();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();