using DotNetEnv;
using NewsApi.Service.PGSQL;
using NewsApi.Service.RAWG;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGame,Game>();
builder.Services.AddScoped<IPGSQL,PGSQL>();
DotNetEnv.Env.Load("C:\\Users\\val\\RiderProjects\\NewsApi\\.env");
builder.Configuration.AddEnvironmentVariables();
new Constant(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();