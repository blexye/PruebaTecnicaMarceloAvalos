using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Application.Validators;
using PruebaTecnicaMarceloAvalos.Endpoints;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
//using PruebaTecnicaMarceloAvalos.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

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

//app.UseMiddleware<ApiKeyMiddleware>();

app.MapUserEndpoints();
app.MapAddressEndpoints();
app.MapCurrencyEndpoints();

app.Run();
