using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.DTOs;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class CurrencyEndpoints
	{
		public static async Task MapCurrencyEndpoints(this WebApplication app)
		{
			// Crear moneda
			app.MapPost("/currency", async (CreateCurrencyRequest request, IValidator<CreateCurrencyRequest> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(request);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var currency = new Currency
				{
					Code = request.Code,
					Name = request.Name,
					RateToBase = request.RateToBase
				};

				db.Currency.Add(currency);
				await db.SaveChangesAsync();

				return Results.Created($"/currency{currency.Id}", currency);
			})
			.WithTags("Currencies");

			// Listar monedas
			app.MapGet("/currency", async (AppDbContext db) =>
			{
				return await db.Currency.ToListAsync();
			})
			.WithTags("Currencies");

			// Obtener moneda por ID
			app.MapGet("/currency/{id}", async (int Id, AppDbContext db) =>
			{
				var currency = await db.Currency.FindAsync(Id);

				return currency is null
					? Results.NotFound()
					: Results.Ok(currency);
			})
			.WithTags("Currencies");

			// Modificar moneda
			app.MapPut("/currency/{id}", async (int id, UpdateCurrencyRequest request, IValidator<UpdateCurrencyRequest> validator, AppDbContext db) =>
			{
				request.Id = id;
				var result = await validator.ValidateAsync(request);
				
				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var currency = await db.Currency.FindAsync(id);

				if (currency is null)
					return Results.NotFound();

				currency.Code = request.Code;
				currency.Name = request.Name;
				currency.RateToBase = request.RateToBase;

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
			app.MapPost("/currency/convert", async (CurrencyConversionRequest request, AppDbContext db) =>
			{
				// Moneda origen
				var fromCurrency = await db.Currency
					.FirstOrDefaultAsync(c =>
						c.Code == request.From);

				// Moneda destino
				var toCurrency = await db.Currency
					.FirstOrDefaultAsync(c =>
						c.Code == request.To);

				if (fromCurrency is null)
					return Results.NotFound($"La moneda {fromCurrency} no existe.");

				if (toCurrency is null)
					return Results.NotFound($"La moneda {toCurrency} no existe");

				var amountInBase = request.Amount / fromCurrency.RateToBase;

				var convertedAmount = amountInBase * toCurrency.RateToBase;

				return Results.Ok(new
				{
					request.From,
					request.To,
					request.Amount,
					ConvertedAmount = convertedAmount
				});
			})
			.WithTags("Currencies");
		}
	}
}
