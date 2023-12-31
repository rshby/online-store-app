using Microsoft.EntityFrameworkCore;
using online_store_app.Data;
using online_store_app.Repositories;
using online_store_app.Resolver;
using online_store_app.Services.Chart;
using online_store_app.Services.Product;
using online_store_app.Services.User;

var builder = WebApplication.CreateBuilder(args);

// register DB Context
string? connectionString = builder.Configuration.GetConnectionString("MySQLConnect");
builder.Services.AddDbContext<OnlineStoreContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)), ServiceLifetime.Transient);

// register layer Repository
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<ChartRepository>();
builder.Services.AddTransient<ProductRepository>();

// register service layer
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IChartService, ChartService>();
builder.Services.AddTransient<IProductService, ProductService>();


// add graphQL server
builder.Services.AddGraphQLServer().AddQueryType<UserQueryType>().AddMutationType<OnlineStoreMutationType>().AddMutationConventions();

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

// add graphQL
app.MapGraphQL();

app.Run();
