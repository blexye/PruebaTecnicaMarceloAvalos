using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.Currencies.Commands;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaMarceloAvalos.Application.Validators
{
	public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
	{
		public UpdateCurrencyCommandValidator(AppDbContext context)
		{
			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage("El código no puede estar vacío");

			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre no puede estar vacío");

			RuleFor(p => p.RateToBase)
				.GreaterThan(0)
				.WithMessage("El campo no puede estar vacío y/o debe ser mayor a 0");
		}
	}
}
