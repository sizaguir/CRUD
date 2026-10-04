using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Base de Datos en Memoria
builder.Services.AddDbContext<PersonaDb>(opt => opt.UseInMemoryDatabase("PersonasList"));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowReact", policy => 
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowReact");


// CRUD

app.MapGet("/personas", async (PersonaDb db) => await db.Personas.ToListAsync());

app.MapPost("/personas", async (Persona persona, PersonaDb db) => {
    db.Personas.Add(persona);
    await db.SaveChangesAsync();
    return Results.Created($"/personas/{persona.Id}", persona);
});

app.MapPut("/personas/{id}", async (int id, Persona input, PersonaDb db) => {
    var persona = await db.Personas.FindAsync(id);
    if (persona is null) return Results.NotFound();
    
    persona.Nombre = input.Nombre;
    persona.Apellido = input.Apellido;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/personas/{id}", async (int id, PersonaDb db) => {
    var persona = await db.Personas.FindAsync(id);
    if (persona is null) return Results.NotFound();
    
    db.Personas.Remove(persona);
    await db.SaveChangesAsync();
    return Results.Ok(persona);
});

app.Run();


// MODELOS
public class Persona {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
}

public class PersonaDb : DbContext {
    // Aquí están los otros tipos que faltaban entre < >
    public PersonaDb(DbContextOptions options) : base(options) { }
    public DbSet<Persona> Personas => Set<Persona>();
}