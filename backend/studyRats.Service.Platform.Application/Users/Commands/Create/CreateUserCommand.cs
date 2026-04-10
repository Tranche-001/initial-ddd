using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Entities.Users;

namespace studyRats.Service.Platform.Application.Users.Commands.Create
{
    public class CreateUserCommand(string Name, string Email) : IRequest
    {
        public string Name { get; } = Name;
        public string Email { get; } = Email;
    }

    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(Guid.NewGuid(),
                request.Name,
                request.Email);

            _userRepository.Add(user);

            return _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
