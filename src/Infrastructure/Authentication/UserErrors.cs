using Domain.Common.Results;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public static class UserErrors
{
    public static Error RegistrationFailed(IEnumerable<IdentityError> errors) =>
        Error.Failure("User.RegistrationFailed", errors.ToString() ?? "Registration failed");
}
