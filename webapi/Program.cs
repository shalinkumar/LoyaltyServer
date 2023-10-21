using Application.Category;
using Application.Products;
using FluentValidation.AspNetCore;
using Infrastructure.Database.DataAccess;
using MediatR;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.OpenApi.Models;
using Serilog;
using webapi.EndPoints;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200", "https://shalinloyaltyclient.azurewebsites.net");
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Loyalty It Project", Version = "v1" });
});

builder.Services.AddAuthentication(
      CertificateAuthenticationDefaults.AuthenticationScheme)
      .AddCertificate();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "Loyalty It Project";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal Api.");
    swaggerUIOptions.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseCors("CORSPolicy");
app.MapProductEndPoint();
app.MapCategoryEndPoint();

try
{
    Log.Information("Starting host");
    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}


