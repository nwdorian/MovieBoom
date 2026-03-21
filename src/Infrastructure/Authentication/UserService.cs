using Application.Abstractions;
using Domain.Common.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public class UserService(UserManager<IdentityUser<Guid>> userManager) : IUserService
{
    public async Task<Result> Register(string email, string password)
    {
        IdentityUser<Guid> user = new() { UserName = email, Email = email };

        IdentityResult result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return UserErrors.RegistrationFailed(result.Errors);
        }

        return Result.Success();
    }
}
