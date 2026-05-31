using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Application.DTOs;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

namespace PruebaTecnicaMarceloAvalos.Application.Validators
{
	public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyRequest>
	{
		public CreateCurrencyValidator (AppDbContext context)
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
				.NotEmpty()
				.WithMessage("El campo es obligatorio");
		}
	}
}
