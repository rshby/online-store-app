using gateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient(WellKnownSchemaNames.OnlineStoreApp, x => x.BaseAddress = new Uri("http://localhost:5000/graphql"));

// register graphql server
builder.Services.AddGraphQLServer().AddRemoteSchema(WellKnownSchemaNames.OnlineStoreApp);

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

app.MapGraphQL();

app.Run();
