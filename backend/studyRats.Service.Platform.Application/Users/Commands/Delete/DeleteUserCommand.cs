using MediatR;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace studyRats.Service.Platform.Application.Users.Commands.Delete
    {
        public class DeleteUserCommand : IRequest
        {
            public Guid Id { get; set; }

            public DeleteUserCommand(Guid id)
            {
                Id = id; 
            }
        }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUnitOfWork _unitOfWork;

            public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
            {
                _userRepository = userRepository;
                _unitOfWork = unitOfWork;
            }

            public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                }

                _userRepository.Remove(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
       
        }
    }
