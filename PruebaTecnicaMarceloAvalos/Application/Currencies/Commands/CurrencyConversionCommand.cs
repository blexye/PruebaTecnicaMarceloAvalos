namespace PruebaTecnicaMarceloAvalos.Application.Currencies.Commands
{
	public record CurrencyConversionCommand
	(
		string From,
		string To,
		decimal Amount
	);
}
