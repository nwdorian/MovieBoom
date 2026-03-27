using System.Text.Encodings.Web;
using Application.Abstractions.Emails;
using Application.Contracts.Emails;
using Infrastructure.Authorization;
using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Users;

namespace Web.Controllers;

public class UsersController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IEmailService emailService
) : Controller
{
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegister input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        ApplicationUser user = new() { UserName = input.Email, Email = input.Email };

        IdentityResult result = await userManager.CreateAsync(user, input.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(input);
        }

        await userManager.AddToRoleAsync(user, Roles.Member);

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLogin input, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
            input.Email,
            input.Password,
            input.RememberMe,
            false
        );

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View();
        }

        if (returnUrl is null)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        return LocalRedirect(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPassword input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        ApplicationUser? user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
        {
            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        string token = await userManager.GeneratePasswordResetTokenAsync(user);
        string? callback = Url.Action(
            nameof(ResetPassword),
            "Users",
            new { token, email = user.Email },
            Request.Scheme
        );
        EmailRequest emailRequest = new(
            user.Email!,
            "Reset password token",
            $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callback!)}'>clicking here</a>."
        );

        await emailService.SendEmail(emailRequest);

        return RedirectToAction(nameof(ForgotPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirmation()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword(string token, string email)
    {
        ResetPassword model = new() { Token = token, Email = email };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPassword input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        ApplicationUser? user = await userManager.FindByEmailAsync(input.Email);
        if (user is null)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }

        IdentityResult result = await userManager.ResetPasswordAsync(user, input.Token, input.Password);
        if (!result.Succeeded)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        return RedirectToAction(nameof(ResetPasswordConfirmation));
    }

    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }
}
