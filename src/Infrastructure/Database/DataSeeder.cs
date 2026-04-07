using Domain.Genres;
using Domain.Movies;
using Infrastructure.Authorization;
using Infrastructure.Genres;
using Infrastructure.Movies;
using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database;

public class DataSeeder(
    ApplicationDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager,
    ILogger<DataSeeder> logger
)
{
    public async Task SeedAsync()
    {
        await SeedRolesAsync();
        await SeedUserAsync();
        await SeedGenresAsync();
        await SeedMoviesAsync();
    }

    private async Task SeedRolesAsync()
    {
        if (await roleManager.RoleExistsAsync(Roles.Member))
        {
            logger.LogInformation("{Role} role already exists", Roles.Member);
            return;
        }

        await roleManager.CreateAsync(new ApplicationRole() { Name = Roles.Member });

        logger.LogInformation("{Role} role created", Roles.Member);
    }

    private async Task SeedUserAsync()
    {
        if (await userManager.FindByEmailAsync(UserFaker.Email) is not null)
        {
            logger.LogInformation("Demo user already exists");
            return;
        }

        ApplicationUser user = UserFaker.Create();

        IdentityResult result = await userManager.CreateAsync(user, "Test123!");

        if (!result.Succeeded)
        {
            logger.LogError(
                "Failed to create demo user: {Errors}",
                string.Join(", ", result.Errors.Select(e => e.Description))
            );
            return;
        }

        await userManager.AddToRoleAsync(user, Roles.Member);

        logger.LogInformation("Demo user created.");
    }

    private async Task SeedGenresAsync()
    {
        if (await dbContext.Genres.AnyAsync())
        {
            logger.LogInformation("Genres already exist");
            return;
        }

        List<Genre> genres = GenreFaker.Create();
        dbContext.Genres.AddRange(genres);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("{Count} genres seeded", genres.Count);
    }

    private async Task SeedMoviesAsync()
    {
        if (await dbContext.Movies.AnyAsync())
        {
            logger.LogInformation("Movies already exist");
            return;
        }

        List<Movie> movies = MovieFaker.Create();
        dbContext.Movies.AddRange(movies);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("{Count} movies seeded", movies.Count);
    }
}
