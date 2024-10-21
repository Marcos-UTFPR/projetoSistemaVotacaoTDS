using Aula03.Models;
using Aula03.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:5070") // URL do frontend Blazor
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EFCoreContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")
));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/eleitores", async (EFCoreContext context) =>
    {
        return await context.Eleitores.ToListAsync();
    }
);

app.MapPost("/api/eleitores", async (EFCoreContext context, EleitorModeloModel eleitor) =>
{
    context.Eleitores.Add(eleitor);
    await context.SaveChangesAsync();
    return Results.Created($"/api/eleitores/{eleitor.EleitorID}", eleitor);
});

app.UseCors();

app.Run();