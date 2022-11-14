using AutoMapper;
using Coders.WebAPI.Models.DataContexts;
using Coders.WebAPI.Models.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;
services.AddEndpointsApiExplorer();

services.AddDbContext<CoderDbContext>(cfg =>
{
    cfg.UseSqlServer(conf.GetConnectionString("cString"));
});

services.AddAutoMapper(typeof(Program));

services.AddCors(cfg =>
{
    cfg.AddPolicy("_allowAnyOrigins", p =>
    {
        p.AllowAnyHeader()
         .AllowAnyMethod()
         .AllowAnyOrigin();
    });
});

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API For Fetch",
        Description = "Fruits' API For Fetch JavaScript",
        //TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Zakir Rahimli",
            Email = "zakirer@code.edu.az",
            Url = new Uri("https://www.facebook.com/zakir.rahimli"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;
if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API For Fetch V1");
});

app.UseRouting();

app.UseCors("_allowAnyOrigins");

await app.Initialize();

app.MapGet("/coders", async (CoderDbContext db) => Results.Ok(await db.Coders.ToListAsync()));
app.MapGet("/coder/{id}", async (CoderDbContext db, int id) => await db.Coders.FindAsync(id) is not Coder coder ? Results.NotFound() : Results.Ok(coder));
app.MapPost("/addcoder", async (CoderDbContext db, Coder coder) =>
{
    await db.Coders.AddAsync(coder);
    await db.SaveChangesAsync();

    return Results.Created($"/coder/${coder.Id}", coder);
});
app.MapPut("/putcoder/{id}", async (CoderDbContext db, Coder coder, int? id, IMapper mapper) =>
{
    if (id == null || id <= 0)
        return Results.Problem();

    Coder? entity = await db.Coders.FirstOrDefaultAsync(c => c.Id.Equals(id));

    if (entity is null)
        return Results.NotFound();

    mapper.Map(coder, entity);
    await db.SaveChangesAsync();

    return Results.Accepted($"/coder/${coder.Id}", coder);
});
app.MapDelete("/deletecoder/{id}", async (CoderDbContext db, int? id) =>
{
    if (id == null || id <= 0)
        return Results.Problem();

    Coder? entity = await db.Coders.FirstOrDefaultAsync(c => c.Id == id);

    if (entity is null)
        return Results.NotFound();

    db.Coders.Remove(entity);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
