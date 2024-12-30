using Corporation.DAL.Models.Identity;
using Corporation.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Corporation.PL.Controllers
{
    public class AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        ) : Controller
    {

        #region Service

        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signInManager = signInManager;

        #endregion

        #region SignUp

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUp)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByNameAsync(signUp.UserName);

            if (user is not null)
            {
                ModelState.AddModelError(nameof(SignUpViewModel.UserName), errorMessage: "This username is already in user for another account.");
                return View(signUp);
            }
            user = new ApplicationUser()
            {
                UserName = signUp.UserName,
                Email = signUp.Email,
                IsAgree = signUp.IsAgree,
                FirstName = signUp.FirstName,
                LastName = signUp.LastName,
            };

            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (result.Succeeded)
                return RedirectToAction(nameof(SignIn));

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(user);
        }

        #endregion

        #region SignIn

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null)
            {
                var isPasswordValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (isPasswordValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account Is Not Confirmed Yet!!");
                        return View(model);
                    }

                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Your Account Is Locked!!");
                        return View(model);
                    }

                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToAction("Send2FACode", "Account");
                    }

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");

            return View(model);
        }

        #endregion

        #region SignOut

        public async new Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        #endregion
    }
}