using MedicalService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MedicalService.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<ActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin"))
                    return RedirectToAction("Index", "Admin");
                else if (userRoles.Contains("doctor"))
                    return RedirectToAction("Index", "Doctor");
                else if (userRoles.Contains("patient"))
                    return RedirectToAction("Index", "Patient");
                else
                     return RedirectToAction("Create", "Patient");
            }

            return NotFound();
        }
    }
}
