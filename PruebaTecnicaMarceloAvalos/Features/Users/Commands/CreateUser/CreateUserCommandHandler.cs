using MediatR;
using PruebaTecnicaMarceloAvalos.Infrastructure.Persistence;
using PruebaTecnicaMarceloAvalos.Domain.Entities;
using FluentValidation;

namespace PruebaTecnicaMarceloAvalos.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IResult>
    {
        private readonly AppDbContext _db;
        private readonly IValidator<CreateUserCommand> _validator;

        public CreateUserCommandHandler(AppDbContext db, IValidator<CreateUserCommand> validator)
        {
            _db = db;
            _validator = validator;
        }

        public async Task<IResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult =
                await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _db.User.Add(user);

            await _db.SaveChangesAsync(cancellationToken);

            return Results.Created($"/users/{user.Id}", user);
        }
    }
}
