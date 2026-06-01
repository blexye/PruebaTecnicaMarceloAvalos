namespace PruebaTecnicaMarceloAvalos.Application.Users.Commands
{
	public record UpdateUserCommand
	(
		string Name,
		string Email,
		bool IsActive
	);
}
