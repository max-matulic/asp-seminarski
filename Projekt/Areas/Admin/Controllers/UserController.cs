using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.IO;
using System.Threading.Tasks;

namespace Projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserController(UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users;

            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);

            if (applicationUser != null)
            {
                return View(applicationUser);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = user.Username,
                    Email = user.Email
                };

                if (user.Avatar != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        user.Avatar.CopyTo(stream);
                        var file = stream.ToArray();

                        applicationUser.Avatar = file;
                    }
                }

                IdentityResult identityResult = await _userManager.CreateAsync(applicationUser, user.Password);

                if (identityResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Errors(identityResult);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);

            if (applicationUser != null)
            {
                return View(applicationUser);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, string email, string password, string username)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser != null)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    applicationUser.Email = email;
                }
                else
                {
                    ModelState.AddModelError("", "Email is empty!");
                }

                if (!string.IsNullOrEmpty(username))
                {
                    applicationUser.UserName = username;
                }
                else
                {
                    ModelState.AddModelError("", "Username is empty!");
                }

                if (!string.IsNullOrEmpty(password))
                {
                    applicationUser.PasswordHash = _passwordHasher.HashPassword(applicationUser, password);
                }
                else
                {
                    ModelState.AddModelError("", "Password is empty!");
                }

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(username))
                {
                    IdentityResult result = await _userManager.UpdateAsync(applicationUser);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Errors(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found!");
            }

            return View(applicationUser);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser != null)
            {
                var result = await _userManager.DeleteAsync(applicationUser);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Errors(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "User not found!");
            }

            return RedirectToAction(nameof(Index));
        }

        private void Errors(IdentityResult identityResult)
        {
            foreach (IdentityError error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
