using FluentValidation;
using PruebaTecnicaMarceloAvalos.Domain.Entities;

namespace PruebaTecnicaMarceloAvalos.Validators
{
	public class UsersValidator : AbstractValidator<Users>
	{
		public UsersValidator()
		{
			RuleFor(p => p.Id)
				.NotEmpty()
				.WithMessage("El ID es obligatorio");

			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre es obligatorio");

			RuleFor(p => p.Email)
				.NotEmpty()
				.WithMessage("El email es obligatorio")
				;
		}
	}
}
