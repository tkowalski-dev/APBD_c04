using WebApp_c04.Entities;
using WebApp_c04.Repositories;
using WebApp_c04.Services;

var builder = WebApplication.CreateBuilder(args);
        
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProduct_WarehouseRepository, Product_WarehouseRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();

var app = builder.Build();
        
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
        
app.Run();