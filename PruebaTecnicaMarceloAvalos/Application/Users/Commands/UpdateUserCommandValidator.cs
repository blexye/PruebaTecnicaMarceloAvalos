using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.Users.Commands;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaMarceloAvalos.Application.Validators
{
	public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
	{
		public UpdateUserCommandValidator(AppDbContext context)
		{
			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre es obligatorio");

			RuleFor(p => p.Email)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("El email es obligatorio")
				.Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
				.WithMessage("El email no tiene un formato válido");
		}
	}
}
