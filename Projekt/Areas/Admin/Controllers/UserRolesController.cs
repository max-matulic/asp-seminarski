using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UserRolesViewModel>();

            foreach (ApplicationUser user in users)
            {
                var userWithRoles = new UserRolesViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    Roles = new List<string>(await _userManager.GetRolesAsync(user))
                };

                usersWithRoles.Add(userWithRoles);
            }

            return View(usersWithRoles);
        }

        public async Task<IActionResult> AddRolesToUser(string id)
        {
            ViewBag.UserId = id;
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.Error = $"User with id - {id} cannot be found!";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var model = new List<AddRemoveUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var addRemoveUserRole = new AddRemoveUserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    addRemoveUserRole.Selected = true;
                }
                else
                {
                    addRemoveUserRole.Selected = false;
                }
                model.Add(addRemoveUserRole);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRolesToUser(List<AddRemoveUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.Error = $"User with id - {userId} cannot be found!";
                return View("NotFound");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove roles from existing user");

                return View(model);
            }

            result = await _userManager.AddToRolesAsync
                (user, model.Where(role => role.Selected == true)
                .Select(role => role.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
