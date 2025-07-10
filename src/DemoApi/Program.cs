using DemoApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using DemoApi.Models;

//Aquí va un comentario
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

async Task<List<Contacto>> GetContactosAsync(DataContext context) => await context.Contactos.ToListAsync();

//Comentario nuevo
app.MapGet("/contactos", async (DataContext context) =>
{
    var contactos = await GetContactosAsync(context);
    return contactos;
});

app.MapGet("/contactos/{id}", async (int id, DataContext context) =>
{
    var contacto = await context.Contactos.FindAsync(id);
    return contacto is not null ? Results.Ok(contacto) : Results.NotFound();
}); 

app.MapPost("Add/Contacto", async (DataContext context, Contacto contacto) =>
{
    if (contacto == null)
    {
        return Results.BadRequest("El contacto no puede estar vacío.");
    }

    context.Contactos.Add(contacto);
    await context.SaveChangesAsync();
    return Results.Created($"/contactos/{contacto.Id}", contacto);
});


app.MapPut("/contactos/{id}", async (int id, Contacto contacto, DataContext context) =>
{
    var existingContacto = await context.Contactos.FindAsync(id);
    if (existingContacto is null)
    {
        return Results.NotFound();
    }

    existingContacto.Name = contacto.Name;
    existingContacto.Lastname = contacto.Lastname;
    existingContacto.Birthday = contacto.Birthday;
    existingContacto.PhoneNumber = contacto.PhoneNumber;
    existingContacto.Email = contacto.Email;

    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/contactos/{id}", async (int id, DataContext context) =>
{
    var contacto = await context.Contactos.FindAsync(id);
    if (contacto is null)
    {
        return Results.NotFound();
    }

    context.Contactos.Remove(contacto);
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

