using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.DTOs;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaMarceloAvalos.Application.Validators
{
	public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
	{
		public UpdateUserValidator(AppDbContext context)
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre es obligatorio");

			RuleFor(p => p.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("El email es obligatorio")
				.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
				.WithMessage("El email no tiene un formato válido")

				.MustAsync(async (request, email, cancellation) =>
				{
					return !await context.User
					.AnyAsync(c => c.Email == email && c.Id != request.Id, cancellation);
				})
				.WithMessage("El email ya existe");
		}
	}
}
