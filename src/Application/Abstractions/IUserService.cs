using Domain.Common.Results;

namespace Application.Abstractions;

public interface IUserService
{
    Task<Result> Register(string email, string password);
}
