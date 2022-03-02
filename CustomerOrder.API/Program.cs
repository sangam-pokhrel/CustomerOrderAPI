using CustomerOrder.API.EntertainmentHubPublicAPI.Middleware;
using CustomerOrder.Application;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CustomerOrder.API.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "CustomerOrder.DTO.xml"));
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddCors(options =>
    options.AddPolicy(name: "allowAll",
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyMethod()
                .AllowAnyOrigin();
        }));

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
});

builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ErrorResponseMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Order"));
app.UseSwagger();
app.UseRouting();
app.UseCors("allowAll");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();