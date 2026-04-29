using MediatR;
using FluentResults;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using studyRats.Service.Platform.Domain.Entities.Users;
using studyRats.Service.Platform.Domain.ValueObjects;
using studyRats.Service.Platform.Domain.Abstractions.DomainErrors;

namespace studyRats.Service.Platform.Application.Users.Commands.Create
{
    public class CreateUserCommand(string Name, string Email) : IRequest<Result<User>>
    {
        public string Name { get; } = Name;
        public string Email { get; } = Email;
    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<User?>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Por causa do EmailAttribute, o email já passou pela lógica de validação uma vez,
            // então não há a necessidade de verificar se o Result foi um Fail aqui
            // pois se fosse, na verdade ele já deve ter mostrado o erro de validação para o usuário, e não chegaria até aqui
            var emailResult = Email.Create(request.Email);

            var user = User.Create(request.Name, emailResult.Value);

            _userRepository.Add(user);

            var result = (await _unitOfWork.SaveChangesAsync(cancellationToken));
            if (result.IsFailed)
            {
                return Result.Fail(result.Error());
            }
            
            return Result.Ok(user);
        }
    }
}
