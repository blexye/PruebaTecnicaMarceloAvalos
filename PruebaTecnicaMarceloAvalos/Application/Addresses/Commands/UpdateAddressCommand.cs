namespace PruebaTecnicaMarceloAvalos.Application.Addresses.Commands
{
	public record UpdateAddressCommand
	(
		string Street,
		string City,
		string Country,
		string ZipCode
	);
}