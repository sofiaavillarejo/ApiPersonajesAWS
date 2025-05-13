using ApiPersonajesAWS.Data;
using ApiPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//a�adimos el cors para poder obtener datos del scalar en la app
builder.Services.AddCors(p => p.AddPolicy("corsenabled", options =>
{
    options.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

string connectionString = builder.Configuration.GetConnectionString("Mysql");
builder.Services.AddTransient<RepositoryPersonajes>();
builder.Services.AddDbContext<PersonajesContext>(options => options.UseMySQL(connectionString));
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}
app.MapScalarApiReference(opt =>
{
    opt.Title = "Scalar Personajes";

});
app.UseCors("corsenabled");
app.MapOpenApi();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
