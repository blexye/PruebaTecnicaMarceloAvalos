namespace PruebaTecnicaMarceloAvalos.Application.DTOs
{
	public class UpdateAddressRequest
	{
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string? ZipCode { get; set; }
	}
}
