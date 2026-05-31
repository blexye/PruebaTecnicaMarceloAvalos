using FluentValidation;
using PruebaTecnicaMarceloAvalos.Application.DTOs;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaMarceloAvalos.Validators
{
	public class UpdateCurrencyValidator : AbstractValidator<UpdateCurrencyRequest>
	{
		public UpdateCurrencyValidator(AppDbContext context)
		{
			RuleFor(p => p.Id)
				.NotEmpty()
				.WithMessage("El ID del código es obligatorio")
				.GreaterThan(0)
				.WithMessage("El ID debe ser mayor a 0");

			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage("El código no puede estar vacío")

				.MustAsync(async (request, code, cancellation) =>
				{
					return !await context.Currency
					.AnyAsync(c => c.Code == code && c.Id != request.Id, cancellation);
				})
				.WithMessage("El código ya existe");

			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre no puede estar vacío");

			RuleFor(p => p.RateToBase)
				.NotEmpty()
				.WithMessage("El campo es obligatorio")
				.GreaterThan(0)
				.WithMessage("El campo debe ser mayor a 0");
		}
	}
}
