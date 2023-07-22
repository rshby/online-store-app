using Microsoft.EntityFrameworkCore;
using online_store_app.Data;
using online_store_app.Repositories;

var builder = WebApplication.CreateBuilder(args);

// register DB Context
string? connectionString = builder.Configuration.GetConnectionString("MySQLConnect");
builder.Services.AddDbContext<OnlineStoreContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);

// register layer Repository
builder.Services.AddScoped<UserRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
