using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Endpoints;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(
	builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

/*app.MapGet("/", () =>
	{
		return "Hello World!";
	}
);*/

app.MapUserEndpoints();
app.MapAddressEndpoints();

app.Run();
