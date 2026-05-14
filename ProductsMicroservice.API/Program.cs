using BusinessLogicLayer;
using DataAccessLayer;
using FluentValidation.AspNetCore;
using ProductsMicroservice.API.APIEndpoints;
using ProductsMicroservice.API.Middleware;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

// Add controllers
builder.Services.AddControllers();

// Fluent Validation
builder.Services.AddFluentValidationAutoValidation();

// Add model binder to read values from JSON to enum
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.Converters.Add(new 
        JsonStringEnumConverter());
});

//Add swagger services for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//Enable CORS
app.UseCors();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapProductAPIEndpoints();

app.Run();
