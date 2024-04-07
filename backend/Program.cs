using System.Reflection;
using API.Database;
using API.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(new ExceptionFilter());
    });

builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDatabase(builder.Configuration);

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

app.UseAuthorization();

app.MapControllers();

app.Run();