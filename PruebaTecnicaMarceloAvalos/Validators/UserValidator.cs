using FluentValidation;
using PruebaTecnicaMarceloAvalos.Domain.Entities;

namespace PruebaTecnicaMarceloAvalos.Validators
{
	public class UserValidator : AbstractValidator<User>
	{
		public UserValidator()
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

			RuleFor(p => p.PasswordHash)
				.NotEmpty()
				.WithMessage("La contraseña es obligatoria")
				.MinimumLength(8)
				.WithMessage("La contraseña debe tener un mínimo de 8 caracteres");
		}
	}
}
