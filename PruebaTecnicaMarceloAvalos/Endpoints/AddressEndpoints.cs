using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Infrastructure;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Application.DTOs;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class AddressEndpoints
	{
		public static void MapAddressEndpoints(this WebApplication app)
		{
			// Crear dirección para un usuario
			app.MapPost("/users/{userId}/addresses", async (int userId, CreateAddressRequest request, IValidator<CreateAddressRequest> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(request);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var userExists = await db.User
					.AnyAsync(u => u.Id == userId);

				if (!userExists)
					return Results.NotFound($"El usuario {userId} no existe.");

				var address = new Address
				{
					UserId = userId,
					Street = request.Street,
					City = request.City,
					Country = request.Country,
					ZipCode = request.ZipCode
				};

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
			app.MapPut("/addresses/{addressId}", async (int addressId, UpdateAddressRequest request, IValidator<UpdateAddressRequest> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(request);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var address = await db.Address.FindAsync(addressId);

				if (address is null)
					return Results.NotFound();

				address.Street = request.Street;
				address.City = request.City;
				address.Country = request.Country;
				address.ZipCode = request.ZipCode;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Addresses");

			// Eliminar dirección
			app.MapDelete("/addresses/{addressId}", async (int addressId, AppDbContext db) =>
			{
				var address = await db.Address.FindAsync(addressId);

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
