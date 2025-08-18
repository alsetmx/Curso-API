using DemoApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using DemoApi.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Demo API",
        Version = "v1",
        Description = "Esta es una API de ejemplo para mostrar la documentación de Swagger",
        Contact = new OpenApiContact
        {
            Name = "Marcela",
            Email = "mmquiroga@outlook.com"
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token JWT en el campo: Bearer {token}"
    });


});





var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.MapControllers();

app.Run();

