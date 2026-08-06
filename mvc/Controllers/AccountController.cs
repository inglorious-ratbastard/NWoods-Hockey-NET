using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcSample.Models.ViewModels;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(
            "Index",
            "Home");
    }

  public IActionResult LoginRegister()
  {
      return View(new LoginRegisterViewModel());
  }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRegisterViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.LoginEmail);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid login.");
            return View("LoginRegister", model);
        }

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName,
            model.LoginPassword,
            model.RememberMe,
            false);

        if (result.Succeeded)
        {
            return RedirectToAction("Main", "Home");
        }

        ModelState.AddModelError("", "Invalid login.");

        return View("LoginRegister", model);
    }

  [HttpPost]
  public async Task<IActionResult> Register(LoginRegisterViewModel model)
  {
      var user = new IdentityUser
      {
          UserName = model.RegisterEmail,
          Email = model.RegisterEmail
      };

      var result = await _userManager.CreateAsync(
          user,
          model.RegisterPassword);

      if (result.Succeeded)
      {
          await _signInManager.SignInAsync(user, false);

          return RedirectToAction("Index", "Home");
      }

      foreach (var error in result.Errors)
          ModelState.AddModelError("", error.Description);

      return View("LoginRegister", model);
  }

}
