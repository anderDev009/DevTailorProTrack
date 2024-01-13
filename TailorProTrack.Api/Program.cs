using Microsoft.EntityFrameworkCore;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.Ioc.Dependencies;

var builder = WebApplication.CreateBuilder(args);


//dependencias
//cadena de conexion
builder.Services.AddDbContext<TailorProTrackContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TailorProTrackContext")));
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
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
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

app.UseCors("AllowSpecificOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
