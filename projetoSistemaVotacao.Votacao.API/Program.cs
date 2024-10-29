using Models;
using Persistence;
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

app.MapGet("/api/votacoes", async (EFCoreContext context) =>
    {
        return await context.Votacoes.ToListAsync();
    }
);

app.MapPost("/api/votacoes", async (EFCoreContext context, VotacaoModeloModel votacao) =>
{
    context.Votacoes.Add(votacao);
    await context.SaveChangesAsync();
    return Results.Created($"/api/votacoes/{votacao.VotacaoID}", votacao);
});

app.UseCors();

app.Run();
