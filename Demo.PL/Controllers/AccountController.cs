using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteKeeperPro.Application.Common.Services.EmailSettings;
using NoteKeeperPro.Infrastructure.Identity;
using NoteKeeperPro.Web.ViewModels.Identity;
using NuGet.Common;

namespace NoteKeeperPro.Web.Controllers
{
    // Dont Forget To Add [Authorize] To Rest Of Controllers like Collaborator
    public class AccountController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings _emailSettings;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSettings emailSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser
                {
                    UserName = registerViewModel.Email.Split('@')[0],
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FName,
                    LastName = registerViewModel.LName,
                    IsAgree = registerViewModel.IsAgree,


                };
                var Result = await _userManager.CreateAsync(User, registerViewModel.Password);
                if (Result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(registerViewModel); // ModelState is not valid or result not succeded
        }

        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email); // Check existence of user with Email

                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (flag) // Email and Password correct
                    {
                        // A Token Generated [As Encrypted String]
                        var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else // Email correct but Password incorrect
                    {
                        ModelState.AddModelError(string.Empty, "Password is incorrect");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email not found");
                }
            }

            return View(loginViewModel);
        }

        #endregion

        #region SignOut
        [HttpGet]
        public new async Task<IActionResult> SignOut() // (new) is cuz controller has built in SignOut , so new to use my own. 
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region SendRestPasswordUrl
        [HttpGet]
        public async Task<IActionResult> SendRestPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
                if (user is not null)
                {
                    // توليد التوكن لإعادة تعيين كلمة المرور
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // إنشاء رابط إعادة تعيين كلمة المرور
                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        subject = "Reset your password",
                        body = url,
                    };

                    // إرسال البريد (مثال placeholder, تحتاج تنفذها بنفسك)
                    // await _emailService.SendAsync(email);

                    _emailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");

                }

                ModelState.AddModelError(string.Empty, "Email is not found.");
            }

            return View(forgetPasswordViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CheckYourInbox()
        {
            //if (ModelState.IsValid)
            //{
            //    var user = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);
            //    if (user is not null)
            //    {
            //        // توليد التوكن لإعادة تعيين كلمة المرور
            //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //        // إنشاء رابط إعادة تعيين كلمة المرور
            //        var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, token = token }, Request.Scheme);

            //        var email = new Email()
            //        {
            //            To = forgetPasswordViewModel.Email,
            //            subject = "Reset your password",
            //            body = url,
            //        };

            //        // إرسال البريد (مثال placeholder, تحتاج تنفذها بنفسك)
            //        // await _emailService.SendAsync(email);

            //        _emailSettings.SendEmail(email);
            //        return RedirectToAction("CheckYourInbox");

            //    }

            //    ModelState.AddModelError(string.Empty, "Email is not found.");
            //}

            return View();
        }
        #endregion

        #region ResePassword
        [HttpGet]
        public IActionResult ResePassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResePassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user = await _userManager.FindByEmailAsync(email);

                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, resetPasswordViewModel.Password);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Login));
                    }
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid Operation plz Try Again");
            return View(resetPasswordViewModel);
        }
        #endregion

    }
}