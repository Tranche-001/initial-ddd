using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using studyRats.Service.Platform.Domain.Entities.Users;

namespace studyRats.Service.Platform.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>?>
    {
    }
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>?>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>?> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            if (users == null)
            {

            }
            return users;
        }
    }
}
