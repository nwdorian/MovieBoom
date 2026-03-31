using Application.Abstractions.Users;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Users;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public Guid UserId => httpContextAccessor.HttpContext.User.GetUserId();
}
