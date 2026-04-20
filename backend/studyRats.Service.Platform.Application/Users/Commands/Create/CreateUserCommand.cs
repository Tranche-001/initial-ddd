using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using studyRats.Service.Platform.Domain.Entities.Users;

namespace studyRats.Service.Platform.Application.Users.Commands.Create
{
    public class CreateUserCommand(string Name, string Email) : IRequest<User>
    {
        public string Name { get; } = Name;
        public string Email { get; } = Email;
    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<User?> Handle(CreateUserCommand request, CancellationToken cancellationToken)

        // If I have a create user request, I must first validate the reqwest info and then execute the code that I want
        // Validations use the Result Pattern, which is a way to return either a success or an error from a method, without throwing exceptions.
        // This allows for better error handling and more readable code.
        {
            var user = User.Create(request.Name, request.Name);

            _userRepository.Add(user);

            _unitOfWork.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
