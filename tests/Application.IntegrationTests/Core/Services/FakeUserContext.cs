using Application.Abstractions.Users;
using Infrastructure.Users;

namespace Application.IntegrationTests.Core.Services;

public class FakeUserContext : IUserContext
{
    public Guid UserId => UserFaker.UserId;
}
