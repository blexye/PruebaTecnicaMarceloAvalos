using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Infrastructure;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Domain.Entities;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class AddressEndpoints
	{
		public static void MapAddressEndpoints(this WebApplication app)
		{
			// Crear dirección para un usuario
			app.MapPost("/users/{userId}/addresses", async (int userId, Address address, AppDbContext db) =>
			{
				var userExists = await db.User
					.AnyAsync(u => u.Id == userId);

				if (!userExists)
					return Results.NotFound($"El usuario {userId} no existe.");

				address.UserId = userId;

				db.Address.Add(address);
				await db.SaveChangesAsync();

				return Results.Created($"/users/{userId}/addresses/{address.Id}", address);
			});
		}
	}
}
