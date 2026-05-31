using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Infrastructure;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class AddressEndpoints
	{
		public static void MapAddressEndpoints(this WebApplication app)
		{
			// Crear dirección para un usuario
			app.MapPost("/users/{userId}/addresses", async (int userId, IValidator<Address> validator, Address address, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(address);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var userExists = await db.User
					.AnyAsync(u => u.Id == userId);

				if (!userExists)
					return Results.NotFound($"El usuario {userId} no existe.");

				address.UserId = userId;

				db.Address.Add(address);
				await db.SaveChangesAsync();

				return Results.Created($"/users/{userId}/addresses/{address.Id}", address);
			})
			.WithTags("Addresses");

			// Listar las direcciones de un usuario
			app.MapGet("/users/{userId}/addresses", async (int UserId, AppDbContext db) =>
			{
				var addresses = await db.Address
					.Where(a => a.UserId == UserId)
					.ToListAsync();

				return addresses is null
					? Results.NotFound()
					: Results.Ok(addresses);
			})
			.WithTags("Addresses");

			// Modificar dirección
			app.MapPut("/addresses/{addressId}", async (int AddressId, Address updateAddress, IValidator<Address> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(updateAddress);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var address = await db.Address.FindAsync(AddressId);

				if (address is null)
					return Results.NotFound();

				address.Street = updateAddress.Street;
				address.City = updateAddress.City;
				address.Country = updateAddress.Country;
				address.ZipCode = updateAddress.ZipCode;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Addresses");

			// Eliminar dirección
			app.MapDelete("/addresses/{addressId}", async (int AddressId, AppDbContext db) =>
			{
				var address = await db.Address.FindAsync(AddressId);

				if (address is null)
					return Results.NotFound();

				db.Address.Remove(address);
				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Addresses");
		}
	}
}
