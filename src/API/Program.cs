using System.Text;
using API;
using Infrastructure;
using Application.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddWebServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

public class LowerCaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // get the paths
        var paths = swaggerDoc.Paths.ToDictionary(
            path => path.Key.ToLowerInvariant(),
            path => swaggerDoc.Paths[path.Key]);

        // add the paths in swagger
        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var pathItem in paths)
        {
            swaggerDoc.Paths.Add(pathItem.Key, pathItem.Value);
        }
    }
}