using System.Text.Json.Serialization;
using PruebaTecnicaMarceloAvalos.Domain.Entities;

namespace PruebaTecnicaMarceloAvalos.Application.DTOs
{
	public class CreateAddressRequest
	{
		public string Street { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string Country { get; set; } = string.Empty;
		public string? ZipCode { get; set; }
	}
}
