using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectArea.Entities;
using ProjectArea.Services;
using ProjectArea.ViewModels;
using System.Threading.Tasks;

namespace ProjectArea.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private IUserManagerData _userManagerData;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUserManagerData userManagerData)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userManagerData = userManagerData;
        }

        //Login VIEW
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Login POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync
                                (model.UserName, model.Password, 
                                model.RememberMe, false);
                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.RedirectUrl))
                    {
                        return Redirect(model.RedirectUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Username and Password don't match");
            return View(model);
        }

        //Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        //Register VIEW
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //Register POST
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.UserName };

                user.Email = model.Email;
                user.FirstName = model.Name;
                user.LastName = model.LastName;

                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }
    }
}
