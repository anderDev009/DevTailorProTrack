using Microsoft.EntityFrameworkCore;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.Ioc.Dependencies;

var builder = WebApplication.CreateBuilder(args);


//dependencias
builder.Services.AddDbContext<TailorProTrackContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TailorProTrackContext")));

//dependencias de productos
builder.Services.AddProductDependencies();
//dependencias de los tipos de productos
builder.Services.AddTypeProductDependencies();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
