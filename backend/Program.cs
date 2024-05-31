using System.Reflection;
using API.API.Extensions;
using API.Common;
using API.Database;
using API.Extensions;
using API.Features;
using API.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(new ExceptionFilter());
    });

builder.Services.AddDistributedMemoryCache(); // Dodaj cache pamięciowy (in-memory)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Ustaw czas wygaśnięcia sesji
    options.Cookie.HttpOnly = true; // Ustawienia ciasteczek
    options.Cookie.IsEssential = true; // Ciasteczka sesji są niezbędne
});

builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddMessageBroker(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddCommon(builder.Configuration);
builder.Services.AddFeatures(builder.Configuration);

Globals.ApplicationUrl = builder.Configuration.GetValue<string>("ApplicationConfig:ApplicationUrl");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(c =>
{
    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Projekt PRZ API V1");
    c.RoutePrefix = "api/swagger";
});

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseSession();

app.UseAuthorization();

app.MapControllers();

app.Run();