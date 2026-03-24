namespace Infrastructure.Users;

public static class UserFaker
{
    public static readonly Guid UserId = Guid.Parse("f1e2d3c4-b5a6-4789-0123-456789abcdef");
    public static readonly string Email = "demo@movieboom.com";

    public static ApplicationUser Create()
    {
        ApplicationUser user = new()
        {
            Id = UserId,
            UserName = Email,
            Email = Email,
            EmailConfirmed = true,
        };
        return user;
    }
}
