namespace PruebaTecnicaMarceloAvalos.Domain.Entities
{
	public class Currencies
	{
		public int Id { get; set; }
		public string Code { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public decimal RateToBase { get; set; }
	}
}