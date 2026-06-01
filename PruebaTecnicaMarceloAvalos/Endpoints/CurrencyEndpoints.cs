using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.Currencies.Commands;
using PruebaTecnicaMarceloAvalos.Application.Currencies.Queries;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class CurrencyEndpoints
	{
		public static void MapCurrencyEndpoints(this WebApplication app)
		{
			// Crear moneda
			app.MapPost("/currency", async (CreateCurrencyCommand command, IValidator<CreateCurrencyCommand> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(command);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var currency = new Currency
				{
					Code = command.Code,
					Name = command.Name,
					RateToBase = command.RateToBase
				};

				db.Currency.Add(currency);
				await db.SaveChangesAsync();

				var currencies = await db.Currency
					.Select(u => new
					{
						u.Code,
						u.Name,
						u.RateToBase
					})
					.ToListAsync();

				return Results.Created();
			})
			.WithTags("Currencies");

			// Listar monedas
			app.MapGet("/currency", async (AppDbContext db) =>
			{
				return await db.Currency.ToListAsync();
			})
			.WithTags("Currencies");

			// Obtener moneda por ID
			app.MapGet("/currency/{id}", async (int id, AppDbContext db) =>
			{
				var query = new GetCurrencyByIdQuery(id);

				var currency = await db.Currency
					.Where(u => u.Id == query.Id)
					.Select(u => new
					{
						u.Name,
						u.Code,
						u.RateToBase
					})
					.FirstOrDefaultAsync();

				return currency is null
					? Results.NotFound()
					: Results.Ok(currency);
			})
			.WithTags("Currencies");

			// Modificar moneda
			app.MapPut("/currency/{id}", async (int id, UpdateCurrencyCommand command, IValidator<UpdateCurrencyCommand> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(command);
				
				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var currency = await db.Currency.FindAsync(id);

				if (currency is null)
					return Results.NotFound();

				var codeExists = await db.Currency
					.AnyAsync(u => u.Code == command.Code && u.Id != id);

				if (codeExists)
					return Results.BadRequest("El código ya existe");

				currency.Code = command.Code;
				currency.Name = command.Name;
				currency.RateToBase = command.RateToBase;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Currencies");

			//Eliminar moneda
			app.MapDelete("/currency/{id}", async (int Id, AppDbContext db) =>
			{
				var currency = await db.Currency.FindAsync(Id);

				if (currency is null)
					return Results.NotFound();

				db.Currency.Remove(currency);
				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Currencies");

			// Conversión de divisas
			app.MapPost("/currency/convert", async (CurrencyConversionCommand command, IValidator<CurrencyConversionCommand> validator, AppDbContext db) =>
			{
				// Moneda origen
				var fromCurrency = await db.Currency
					.FirstOrDefaultAsync(c =>
						c.Code == command.From);

				// Moneda destino
				var toCurrency = await db.Currency
					.FirstOrDefaultAsync(c =>
						c.Code == command.To);

				if (fromCurrency is null)
					return Results.NotFound($"La moneda {fromCurrency} no existe.");

				if (toCurrency is null)
					return Results.NotFound($"La moneda {toCurrency} no existe");

				var amountInBase = command.Amount / fromCurrency.RateToBase;

				var convertedAmount = Math.Round(amountInBase * toCurrency.RateToBase, 2);

				return Results.Ok(new
				{
					command.From,
					command.To,
					command.Amount,
					ConvertedAmount = convertedAmount
				});
			})
			.WithTags("Currencies");
		}
	}
}
