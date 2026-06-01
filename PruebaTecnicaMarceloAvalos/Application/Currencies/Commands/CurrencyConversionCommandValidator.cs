using FluentValidation;

namespace PruebaTecnicaMarceloAvalos.Application.Currencies.Commands
{
	public class CurrencyConversionCommandValidator : AbstractValidator<CurrencyConversionCommand>
	{
		public CurrencyConversionCommandValidator()
		{
			RuleFor(c => c.Amount)
				.GreaterThanOrEqualTo(0)
				.WithMessage("Ingrese un monto válido");
		}
	}
}
