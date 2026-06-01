namespace PruebaTecnicaMarceloAvalos.Application.Currencies.Commands
{
	public record UpdateCurrencyCommand
	(
		string Code,
		string Name,
		decimal RateToBase
	);
}
