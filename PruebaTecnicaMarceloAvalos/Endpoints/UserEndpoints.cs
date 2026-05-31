using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using BCrypt;
using PruebaTecnicaMarceloAvalos.Application.DTOs;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Infrastructure;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace PruebaTecnicaMarceloAvalos.Endpoints
{
	public static class UserEndpoints
	{
		public static void MapUserEndpoints(this WebApplication app)
		{
			// Crear usuario
			app.MapPost("/users", async (CreateUserRequest request, IValidator<CreateUserRequest> validator, AppDbContext db) =>
			{
				var result = await validator.ValidateAsync(request);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var user = new User
				{
					Name = request.Name,
					Email = request.Email,
					PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
				};

				db.User.Add(user);
				await db.SaveChangesAsync();

				return Results.Created($"/users/{user.Id}", user);
			})
			.WithTags("Users");

			// Listar usuarios
			app.MapGet("/users", async(AppDbContext db) =>
			{
				return await db.User.ToListAsync();
			})
			.WithTags("Users");

			// Obtener usuario por ID
			app.MapGet("/users/{id}", async (int Id, AppDbContext db) =>
			{
				var user = await db.User.FindAsync(Id);

				return user is null
					? Results.NotFound()
					: Results.Ok(user);
			})
			.WithTags("Users");

			// Modificar usuario
			app.MapPut("/users/{id}", async (int id, UpdateUserRequest request, IValidator<UpdateUserRequest> validator, AppDbContext db) =>
			{
				request.Id = id;
				var result = await validator.ValidateAsync(request);

				if (!result.IsValid)
					return Results.BadRequest(result.Errors);

				var user = await db.User.FindAsync(id);

				if (user is null)
					return Results.NotFound();

				user.Name = request.Name;
				user.Email = request.Email;
				user.IsActive = request.IsActive;

				await db.SaveChangesAsync();

				return Results.NoContent();
			})
			.WithTags("Users");

			// Eliminar usuario
			app.MapDelete("/users/{id}", async (int Id, AppDbContext db) =>
			{
				var user = await db.User.FindAsync(Id);

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
