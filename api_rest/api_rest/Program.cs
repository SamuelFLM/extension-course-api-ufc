using api_rest.Domain.Models;
using api_rest.Domain.Repositories;
using api_rest.Domain.Services;
using api_rest.Mapping;
using api_rest.Persistence.Contexts;
using api_rest.Persistence.Repositories;
using api_rest.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

AddRepository(builder);
AddService(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Somente para banco de dados in-memory
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
};

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(option => { option.AllowAnyHeader(); option.AllowAnyOrigin(); option.AllowAnyMethod(); });
app.MapControllers();

app.Run();

static void AddRepository(WebApplicationBuilder builder)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseInMemoryDatabase("supermarket-api-in-memory");
    });

    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
}

static void AddService(WebApplicationBuilder builder)
{
    builder.Services.AddAutoMapper(typeof(ModelToResourceProfile));
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IUserService, UserService>();
}