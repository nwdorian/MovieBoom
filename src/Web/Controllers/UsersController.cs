using Application.Abstractions;
using Domain.Common.Results;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Users;

namespace Web.Controllers;

public class UsersController(IUserService userService) : Controller
{
    public async Task<IActionResult> Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserCreate userCreate, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        Result createUser = await userService.Register(userCreate.Email, userCreate.Password);
        if (createUser.IsFailure)
        {
            ModelState.AddModelError(string.Empty, createUser.Error.Description);
            return View();
        }

        returnUrl ??= Url.Content("~/");
        return LocalRedirect(returnUrl);
    }
}
