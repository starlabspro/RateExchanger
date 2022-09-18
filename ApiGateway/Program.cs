using BuildingBlocks.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("Yarp"));

var app = builder.Build();

app.UseCorrelationId();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Currently there is no authentication for this proxy.
    endpoints.MapControllers();
    endpoints.MapReverseProxy();
});

app.Run();