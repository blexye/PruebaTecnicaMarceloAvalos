namespace PruebaTecnicaMarceloAvalos.Application.Addresses.Commands
{
	public record CreateAddressCommand
	(
		string Street,
		string City,
		string Country,
		string ZipCode
	);
}
