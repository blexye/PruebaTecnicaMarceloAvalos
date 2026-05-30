using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using SQLitePCL;

namespace PruebaTecnicaMarceloAvalos.Validators
{
	public class CurrencyValidator : AbstractValidator<Currency>
	{
		private readonly AppDbContext _context;
		public CurrencyValidator(AppDbContext context)
		{			
			_context = context;

			RuleFor(p => p.Code)
				.NotEmpty()

				.MustAsync(async (code, cancellation) =>
				{
					return !await _context.Currency
						.AnyAsync(c => c.Code == code, cancellation);
				})
				.WithMessage("El código ya existe");

			RuleFor(p => p.Name)
				.NotEmpty()
				.WithMessage("El nombre no puede estar vacío");

			RuleFor(p => p.RateToBase)
				.NotEmpty();
		}
	}
}
