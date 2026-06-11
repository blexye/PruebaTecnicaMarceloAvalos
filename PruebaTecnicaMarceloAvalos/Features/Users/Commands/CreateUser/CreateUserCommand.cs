using MediatR;
namespace PruebaTecnicaMarceloAvalos.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand
    (
        string Name,
        string Email,
        string Password
    ) : IRequest<IResult>;
}
