using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using FluentResults;
using studyRats.Service.Platform.Domain.Abstractions;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using studyRats.Service.Platform.Domain.Entities.Users;
using studyRats.Service.Platform.Domain.ValueObjects;

namespace studyRats.Service.Platform.Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<User>?>>
    {
    }
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<User>?>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Implemetation based on 
        // https://enterprisecraftsmanship.com/posts/advanced-error-handling-techniques/
        public async Task<Result<IEnumerable<User>?>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            users = null; // Simulate not found

            if (users == null)
            {
                return Result.Fail(Errors.General.NotFound("User", "All"));
            }

            return Result.Ok(users);
        }
    }
}
