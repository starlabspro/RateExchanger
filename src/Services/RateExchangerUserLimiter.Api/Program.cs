using BuildingBlocks.Caching.Contract;
using BuildingBlocks.Caching.Service;
using BuildingBlocks.EFCore;
using BuildingBlocks.Validation.Contracts;
using BuildingBlocks.Validation.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using RateExchangerUserLimiter.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.SetupSwaggerGen();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader("api-version"),
        new UrlSegmentApiVersionReader());
});

builder.Services.AddTransient<ICacheManager, CacheManager>();
builder.Services.AddSingleton<IValidatorService, ValidatorService>();
builder.Services.AddDbContext<RateExchangerDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))); //this should be done for every project that has to use this
// builder.Services.AddVersionedApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();