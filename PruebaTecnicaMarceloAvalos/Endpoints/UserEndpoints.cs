using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Application.Users.Commands;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Application.Users.DTOs;
using PruebaTecnicaMarceloAvalos.Application.Users.Queries;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class UserEndpoints
	{
		public static void MapUserEndpoints(this WebApplication app)
		{
			// Crear usuario
			app.MapPost("/users", async (CreateUserCommand command, IValidator<CreateUserCommand> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(command);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var user = new User
				{
					Name = command.Name,
					Email = command.Email,
					PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password)
				};

				db.User.Add(user);
				await db.SaveChangesAsync();

				var response = new UserResponse
				(
					user.Name,
					user.Email
				);

				return Results.Created($"/users/{user.Id}", response);
			})
			.WithTags("Users");

			// Listar usuarios
			app.MapGet("/users", async(AppDbContext db) =>
			{
				var query = new GetAllUsersQuery();

				var users = await db.User
					.Select(u => new
					{
						u.Id,
						u.Name,
						u.Email
					})
					.ToListAsync();

				return Results.Ok(users);
			})
			.WithTags("Users");

			// Obtener usuario por ID
			app.MapGet("/users/{id}", async (int id, AppDbContext db) =>
			{
				var query = new GetUserByIdQuery(id);

				var user = await db.User
					.Where(u => u.Id == query.Id)
					.Select(u => new
					{
						u.Id,
						u.Name,
						u.Email,
						u.Address
					})
					.FirstOrDefaultAsync();

				return user is null
					? Results.NotFound()
					: Results.Ok(user);
			})
			.WithTags("Users");

			// Modificar usuario
			app.MapPut("/users/{id}", async (int id, UpdateUserCommand command, IValidator<UpdateUserCommand> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(command);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var user = await db.User.FindAsync(id);

				if (user is null)
					return Results.NotFound();

				var emailExists = await db.User
					.AnyAsync(u => u.Email == command.Email && u.Id != id);

				if (emailExists)
					return Results.BadRequest("El email ya existe");

				user.Name = command.Name;
				user.Email = command.Email;
				user.IsActive = command.IsActive;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Users");

			// Eliminar usuario
			app.MapDelete("/users/{id}", async (int id, AppDbContext db) =>
			{
				var user = await db.User.FindAsync(id);

				if (user is null)
					return Results.NotFound();

				db.User.Remove(user);
				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Users");
		}
	}
}
