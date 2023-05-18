using MarketProducts.Data.DbContexts;
using MarketProducts.Data.IRepositories;
using MarketProducts.Data.Repositories;
using MarketProducts.Service.Helpers;
using MarketProducts.Service.Interfaces;
using MarketProducts.Service.Mappers;
using MarketProducts.Service.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("MarketDb")));

builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
//builder.Services.AddScoped<IAttachmentService, AttachmentService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();

EnvironmentHelper.WebRootPath = app.Services.GetRequiredService<IWebHostEnvironment>()?.WebRootPath;

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

