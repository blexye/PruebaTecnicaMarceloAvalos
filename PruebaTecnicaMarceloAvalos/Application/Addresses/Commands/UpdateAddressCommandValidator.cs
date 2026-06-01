using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.Addresses.Commands;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

namespace PruebaTecnicaMarceloAvalos.Application.Validators
{
	public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
	{
		public UpdateAddressCommandValidator(AppDbContext db)
		{
			RuleFor(p => p.Street)
				.NotEmpty()
				.WithMessage("El nombre de la calle es obligatorio");

			RuleFor(p => p.City)
				.NotEmpty()
				.WithMessage("El nombre de la ciudad es obligatorio");

			RuleFor(p => p.Country)
				.NotEmpty()
				.WithMessage("El país es obligatorio");

			RuleFor(p => p.ZipCode)
				.MaximumLength(20)
				.WithMessage("El código postal no puede tener más de 20 caracteres");
		}
	}
}
