using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ReviewIT.DataAccess;

namespace ReviewIT
{
    [Authorize]
    [Route("")]
    public class AccountController : Controller  
    {
        private readonly DataService _dataService;

        public AccountController(DataContext dataContext)
        {
            // Instantiate an instance of the data service.
            _dataService = new DataService(dataContext);
        }

        [AllowAnonymous]
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            // if this value is true, return to register view without adding the user to the database.
            bool returnToView = false;

            // if the state is not valid, return to the view.
            returnToView = !ModelState.IsValid;

            // Nickname must be unique.
            User existingUser = _dataService.GetUser(registerViewModel.NickName);
            if (existingUser != null) 
            {
                // Set nickname already in use error message.
                ModelState.AddModelError("Error", "An account already exists with that nickname.");
                returnToView = true;
                existingUser = null;
            }

            existingUser = _dataService.GetUser(registerViewModel.UserId);
            if (existingUser != null) 
            {
                // Set UserId already in use error message.
                ModelState.AddModelError("Error", "An account already exists with that Id.");
                returnToView = true;
                existingUser = null;
            }

            existingUser = _dataService.GetUser(registerViewModel.EmailAddress);
            if (existingUser != null) 
            {
                // Set email address already in use error message.
                ModelState.AddModelError("Error", "An account already exists with that email address.");
                returnToView = true;
            }

            // if this value is true, return to register view without adding the user to the database.
            if (returnToView)
            {
                return View();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            User user = new User()
            {
                NickName = registerViewModel.NickName,
                UserId = registerViewModel.UserId,
                EmailAddress = registerViewModel.EmailAddress,
                PasswordHash = passwordHasher.HashPassword(null,registerViewModel.Password)
            };
        
            _dataService.AddUser(user);

            return RedirectToAction(nameof(Login));
        }


        [AllowAnonymous]
        [HttpGet("sign-in")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl)
        {          
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = _dataService.GetLoginUser(loginViewModel.EmailAddress);

            if (user == null) 
            {
                // Set email address not registered error message.
                ModelState.AddModelError("Error", "An account does not exist with that email address or id.");
            
                return View();
            }

            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            PasswordVerificationResult passwordVerificationResult 
                =  passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginViewModel.Password);
            
            if(passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                // Set invalid password error message.
                ModelState.AddModelError("Error", "Invalid password.");

                return View();
            }

            // Add the user's ID (NameIdentifier), first name and role
            // to the claims that will be put in the cookie.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Name, user.NickName),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties {};

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), 
                authProperties);

            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            else 
            {
                return Redirect(returnUrl);
            }
        }

        [Route("sign-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index), "Home");
        }

        [AllowAnonymous]
        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet("profile")]
        public IActionResult Profile()
        {
        // Get currently logged in user ID from the auth cookie.
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Get user.
        User u = _dataService.GetUser(userId);

        return View(u);
        }

        [HttpGet("edit-profile")]
        public IActionResult EditProfile()
        {
            // Get user id.
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Get user.
            User u = _dataService.GetUser(userId);

            // Populate view model.
           EditProfileViewModel vm = new EditProfileViewModel()
            {
            EmailAddress = u.EmailAddress,
            // UserId = u.UserId,
            NickName = u.NickName,
        };

            return View(vm);
        }


        [HttpPost("edit-profile")]
        public IActionResult EditProfile(EditProfileViewModel vm)
        {
        
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        // Get current user.
        User current = _dataService.GetUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        PasswordHasher<string> hasher = new PasswordHasher<string>();

        // Confirm password.
        if (hasher.VerifyHashedPassword(null, current.PasswordHash, vm.OldPassword) == PasswordVerificationResult.Failed)
        {
            ModelState.AddModelError("OldPassword", "Your password is incorrect.");

            return View(vm);
        }

        // Set user fields.
        // current.UserId = vm.UserId;
        current.NickName = vm.NickName;
        current.EmailAddress = vm.EmailAddress;

        // Check if we should be updating the password.
        if (!string.IsNullOrEmpty(vm.NewPassword))
        {
        // Hash password.
            current.PasswordHash = hasher.HashPassword(null, vm.NewPassword);
        }

        // Update.
        _dataService.UpdateUser(current);

        return RedirectToAction(nameof(Profile));
        }
    }
}