using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Application.Addresses.Commands;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Application.Addresses.Queries;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class AddressEndpoints
	{
		public static void MapAddressEndpoints(this WebApplication app)
		{
			// Crear dirección para un usuario
			app.MapPost("/users/{userId}/addresses", async (int userId, CreateAddressCommand command, IValidator<CreateAddressCommand> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(command);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var userExists = await db.User
					.AnyAsync(u => u.Id == userId);

				if (!userExists)
					return Results.NotFound($"El usuario {userId} no existe.");

				var address = new Address
				{
					UserId = userId,
					Street = command.Street,
					City = command.City,
					Country = command.Country,
					ZipCode = command.ZipCode
				};

				db.Address.Add(address);
				await db.SaveChangesAsync();

				return Results.Created($"/users/{userId}/addresses/{address.Id}", address);
			})
			.WithTags("Addresses")
			.WithSummary("Crear dirección para un usuario");

			// Listar las direcciones de un usuario
			app.MapGet("/users/{userId}/addresses", async (int UserId, AppDbContext db) =>
			{
				var query = new GetAddressByIdQuery(UserId);
				
				var addresses = await db.Address
					.Where(u => u.UserId == query.Id)
					.ToListAsync();

				return addresses is null
					? Results.NotFound()
					: Results.Ok(addresses);
			})
			.WithTags("Addresses")
			.WithSummary("Listar las direcciones para cada usuario");

			// Modificar dirección
			app.MapPut("/addresses/{addressId}", async (int addressId, UpdateAddressCommand command, IValidator<UpdateAddressCommand> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(command);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var address = await db.Address.FindAsync(addressId);

				if (address is null)
					return Results.NotFound();

				address.Street = command.Street;
				address.City = command.City;
				address.Country = command.Country;
				address.ZipCode = command.ZipCode;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Addresses")
			.WithSummary("Modificar una dirección");

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
			.WithTags("Addresses")
			.WithSummary("Eliminar una dirección");
		}
	}
}
