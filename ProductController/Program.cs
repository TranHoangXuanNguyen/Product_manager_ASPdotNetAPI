

using Microsoft.EntityFrameworkCore;
using ProductRepository;
using ProductRepository.Interfaces;
using ProductService.Interfaces;

//test fix 

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// close



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();




// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ProductDBContext>(options =>
            options.UseSqlServer("Server=DESKTOP-K0G7JLN\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddScoped<IProductService, ProductService.Services.ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository.Repository.ProductRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add services to the container.
/*builder.Services.AddDbContext<ProductDBContext>(options =>
            options.UseSqlServer("Server=DESKTOP-L7F9546\\SQLEXPRESS;Database=ProductDB;Trusted_Connection=True;TrustServerCertificate=True"));*/
// conecstring template :Database=PowerGLDB_ATECO;Database=SampleDB;Trusted_Connection=True;TrustServerCertificate=True"
//Conect data base

// Dependency Injection
//builder.Services.AddScoped<IProductRepository, ProductRepository.Repository.ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService.Services.ProductService>();
// tao tuong ung voi moi request, voi 1 request voi se tao moi

//builder.Services.AddSingleton<IProductRepository, ProductRepository.Repository.ProductRepository>();
//builder.Services.AddSingleton<IProductService, ProductService.Services.ProductService>();
// tao 1 doi tuong, su dung mai cho den khi tat chuong trinh


/*builder.Services.AddTransient<IProductRepository, ProductRepository.ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService.ProductService>();*/
// duoc goi la se tao moi => tao moi sau moi lan goi

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Áp dụng CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();






//test




//end