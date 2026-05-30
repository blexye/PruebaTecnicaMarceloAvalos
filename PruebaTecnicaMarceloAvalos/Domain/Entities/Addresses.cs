namespace PruebaTecnicaMarceloAvalos.Domain.Entities
{
	public class Addresses
	{
		public int Id { get; set; }
		//FK
		public int UserId { get; set; }
		//Propiedad de nagevación
		public Users Users { get; set; } = null!;
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string? ZipCode { get; set; }
	}
}
