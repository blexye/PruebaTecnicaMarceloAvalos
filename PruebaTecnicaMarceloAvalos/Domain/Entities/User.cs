using System.Text.Json.Serialization;

namespace PruebaTecnicaMarceloAvalos.Domain.Entities
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public string PasswordHash { get; set; } = string.Empty;
		// Un usuario -> muchas direcciones
		[JsonIgnore]
		public ICollection<Address> Address { get; set; } = new List<Address>();
	}
}
