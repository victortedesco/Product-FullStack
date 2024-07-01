using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDataContext>(options => options.UseSqlite("DataSource=Products.db"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.MapHealthChecks("status");
app.MapControllers();

app.Run();
