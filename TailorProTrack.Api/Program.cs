using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.Ioc.Dependencies;
using TailorProTrack.Application;
var builder = WebApplication.CreateBuilder(args);


//dependencias
//cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("TailorProTrackContext");
builder.Services.AddDbContext<TailorProTrackContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 7, 1))));
//dependencias de productos
builder.Services.AddProductDependencies();
//dependencias de los tipos de productos
builder.Services.AddTypeProductDependencies();
//dependencias de inventario
builder.Services.AddInventoryDependencies();
//dependencias de size
builder.Services.AddSizeDependencies();
//dependencias de color
builder.Services.AddColorDependencies();
//dependencias de cliente
builder.Services.AddClientDependencies();
//dependencias de phone
builder.Services.AddPhoneDependencies();
//dependencias de order
builder.Services.AddOrderDependencies();
//dependencias de pagos
builder.Services.AddPaymentDependencies();
//dependencias de ventas
builder.Services.AddSaleDependencies();
//dependencias de usuairo
builder.Services.AddUserDependencies();
//dependencias de categorias
builder.Services.AddCategoryDependencies();
//dependencias de pedido / preorder
builder.Services.AddPreOrderDependencies();
//servicios de la capa de aplicacion
builder.Services.AddServicesApplicationLayer();
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders(["x-pagination"])
                ;
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TailorProTrackContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
