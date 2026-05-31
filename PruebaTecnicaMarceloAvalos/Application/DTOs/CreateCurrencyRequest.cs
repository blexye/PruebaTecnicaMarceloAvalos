namespace PruebaTecnicaMarceloAvalos.Application.DTOs
{
	public class CreateCurrencyRequest
	{
		public string Code { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public decimal RateToBase { get; set; }
	}
}
