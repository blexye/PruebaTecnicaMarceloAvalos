using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;

namespace PruebaTecnicaMarceloAvalos.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(AppDbContext context)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("El nombre es obligatorio");

            RuleFor(p => p.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("El email es obligatorio")
                .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
                .WithMessage("El email no tiene un formato válido")

                .MustAsync(async (email, cancellation) =>
                {
                    return !await context.User
                    .AnyAsync(c => c.Email == email, cancellation);
                })
                .WithMessage("El email ya existe");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("La contraseña es obligatoria")
                .MinimumLength(8)
                .WithMessage("La contraseña debe tener un mínimo de 8 caracteres");
        }

    }
}
