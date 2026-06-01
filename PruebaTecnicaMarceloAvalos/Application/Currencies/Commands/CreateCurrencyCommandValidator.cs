using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Application.Currencies.Commands;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

namespace PruebaTecnicaMarceloAvalos.Application.Validators
{
	public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
	{
		public CreateCurrencyCommandValidator(AppDbContext context)
		{
			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage("El código no puede estar vacío")

				.MustAsync(async (code, cancellation) =>
				{
					return !await context.Currency
						.AnyAsync(c => c.Code == code, cancellation);
				})
				.WithMessage("El código ya existe");

			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre no puede estar vacío");

			RuleFor(p => p.RateToBase)
				.GreaterThanOrEqualTo(1)
				.WithMessage("El campo es obligatorio");
		}
	}
}
