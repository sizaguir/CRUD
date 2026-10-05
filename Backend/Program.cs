using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Base de Datos
builder.Services.AddDbContext<PersonaDb>(opt => opt.UseSqlite("Data Source=agenda.db"));

builder.Services.AddCors(options => {
    options.AddPolicy("AllowReact", policy => 
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<PersonaDb>();
    db.Database.EnsureCreated();
}
app.UseCors("AllowReact");

// CRUD
// Leer (Listar)
app.MapGet("/personas", async (PersonaDb db) => await db.Personas.ToListAsync());

// Crear (Agregar)
app.MapPost("/personas", async (Persona persona, PersonaDb db) => {
    db.Personas.Add(persona);
    await db.SaveChangesAsync();
    return Results.Created($"/personas/{persona.Id}", persona);
});

// Actualizar (Editar)
app.MapPut("/personas/{id}", async (int id, Persona modificada, PersonaDb db) => {
    var persona = await db.Personas.FindAsync(id);
    if (persona is null) return Results.NotFound();
    
    persona.Nombre = modificada.Nombre;
    persona.Apellido = modificada.Apellido;
    await db.SaveChangesAsync();
    
    return Results.NoContent();
});

// eliminar (Borrar)
app.MapDelete("/personas/{id}", async (int id, PersonaDb db) => {
    var persona = await db.Personas.FindAsync(id);
    if (persona is null) return Results.NotFound();
    
    db.Personas.Remove(persona);
    await db.SaveChangesAsync();    
    return Results.NoContent();
});

app.Run();


// MODELOS
public class Persona {
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;
}

public class PersonaDb : DbContext {
    public PersonaDb(DbContextOptions options) : base(options) { }
    public DbSet<Persona> Personas => Set<Persona>();
}