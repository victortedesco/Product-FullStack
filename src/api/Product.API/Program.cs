using Microsoft.EntityFrameworkCore;
using Product.Domain.Interfaces.Repository;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDataContext>(options => options.UseSqlite("DataSource=Products.db"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.MapHealthChecks("status");
app.MapControllers();

app.Run();
