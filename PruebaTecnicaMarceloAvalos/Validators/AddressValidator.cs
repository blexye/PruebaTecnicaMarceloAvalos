using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using SQLitePCL;

namespace PruebaTecnicaMarceloAvalos.Validators
{
	public class AddressValidator : AbstractValidator<Address>
	{
		public AddressValidator(AppDbContext context)
		{
			RuleFor(p => p.UserId)
				.NotEmpty()
				.GreaterThan(0)
				.WithMessage("El ID del usuario es obligatorio");

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
