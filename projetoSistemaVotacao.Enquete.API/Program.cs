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

app.MapGet("/api/enquetes", async (EFCoreContext context) =>
    {
        return await context.Enquetes.ToListAsync();
    }
);

app.MapPost("/api/enquetes", async (EFCoreContext context, EnqueteModeloModel enquete) =>
{
    context.Enquetes.Add(enquete);
    await context.SaveChangesAsync();
    return Results.Created($"/api/enquetes/{enquete.EnqueteID}", enquete);
});

app.UseCors();

app.Run();
