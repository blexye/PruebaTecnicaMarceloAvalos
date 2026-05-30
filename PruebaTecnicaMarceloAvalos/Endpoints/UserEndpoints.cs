using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Infrastructure;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class UserEndpoints
	{
		public static void MapUserEndpoints(this WebApplication app)
		{
			// Crear usuario
			app.MapPost("/users", async (User user, AppDbContext db) =>
			{
				db.User.Add(user);
				await db.SaveChangesAsync();

				return Results.Created($"/users/{user.Id}", user);
			});

			// Listar usuarios
			app.MapGet("/users", async(AppDbContext db) =>
			{
				return await db.User.ToListAsync();
			});

			// Obtener usuario por ID
			app.MapGet("/users/{id}", async (int Id, AppDbContext db) =>
			{
				var user = await db.User.FindAsync(Id);

				return user is null
					? Results.NotFound()
					: Results.Ok(user);
			});

			// Modificar usuario
			app.MapPut("/users/{id}", async (int Id, User updateUser, AppDbContext db) =>
			{
				var user = await db.User.FindAsync(Id);

				if (user is null)
					return Results.NotFound();

				user.Name = updateUser.Name;
				user.Email = updateUser.Email;
				user.IsActive = updateUser.IsActive;

				await db.SaveChangesAsync();

				return Results.NoContent();
			});

			// Eliminar usuario
			app.MapDelete("/users/{id}", async (int Id, AppDbContext db) =>
			{
				var user = await db.User.FindAsync();

				if (user is null)
					return Results.NotFound();

				db.User.Remove(user);
				await db.SaveChangesAsync();

				return Results.NoContent();
			});
		}
	}
}
