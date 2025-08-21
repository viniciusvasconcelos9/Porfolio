using EcommerceAPI.Infrastructure.Persistence;
using EcommerceAPI.Infrastructure.Persistence.Repositories;
using EcommerceAPI.Core.Interface;
using EcommerceAPI.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Banco de Dados
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 🔹 Repositórios
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// 🔹 MediatR - registra todos os Handlers da camada Application
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(EcommerceAPI.Application.Commands.Create.CreateCustomerCommand.CreateCustomerCommand).Assembly)
);

// 🔹 Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔹 Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
