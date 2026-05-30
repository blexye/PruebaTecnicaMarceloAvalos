namespace PruebaTecnicaMarceloAvalos.Domain.Entities
{
	public class Users
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		public string PasswordHash { get; set; } = string.Empty;
		public ICollection<Addresses> Addresses { get; set; } = new List<Addresses>();
	}
}
