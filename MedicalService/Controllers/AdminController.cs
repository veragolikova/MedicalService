using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MedicalService.Models;
using MedicalService.ViewModels.AdminViewModels;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalService.Controllers
{
    public class AdminController : Controller
    {
        UserManager<User> _userManager;

        public AdminController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin"))
                {
                    return View();
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> UserList(string userName, string email)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin"))
                {
                    var users = _userManager.Users;
                    if (!String.IsNullOrEmpty(userName))
                    {
                        users = users.Where(p => p.UserName.ToLower().Contains(userName.ToLower()));
                    }
                    if (!String.IsNullOrEmpty(email))
                    {
                        users = users.Where(p => p.Email.ToLower().Contains(email.ToLower()));
                    }

                    UserListViewModel viewModel = new UserListViewModel
                    {
                        Users = users.ToList(),
                        Email = email,
                        UserName = userName
                    };
                    return View(viewModel);
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> PatientList(string secondName, string firstName, DateTime dateOfBirth)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin"))
                {
                    using (var db = new ApplicationContext())
                    {
                        IEnumerable<Patient> patients = db.Patients;
                        if (!String.IsNullOrEmpty(secondName))
                        {
                            patients = patients.Where(p => p.SecondName.ToLower().Contains(secondName.ToLower()));
                        }
                        if (!String.IsNullOrEmpty(firstName))
                        {
                            patients = patients.Where(p => p.FirstName.ToLower().Contains(firstName.ToLower()));
                        }
                        if (!(dateOfBirth == new DateTime()))
                        {
                            patients = patients.Where(p => p.DateOfBirth.Equals(dateOfBirth));
                        }

                        PatientListViewModel viewModel = new PatientListViewModel
                        {
                            Patients = patients.ToList(),
                            SecondName = secondName,
                            FirstName = firstName,
                            DateOfBirth = dateOfBirth
                        };
                        return View(viewModel);
                    }
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();

        }
        public async Task<IActionResult> DoctorList(string secondName, int departmentNumber, string specialty)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin"))
                {
                    using (var db = new ApplicationContext())
                    {
                        IEnumerable<Doctor> doctors = db.Doctors;
                        if (!String.IsNullOrEmpty(secondName))
                        {
                            doctors = doctors.Where(p => p.SecondName.ToLower().Contains(secondName.ToLower()));
                        }
                        if (!String.IsNullOrEmpty(specialty))
                        {
                            doctors = doctors.Where(p => p.Specialty.ToLower().Contains(specialty.ToLower()));
                        }
                        if (!(departmentNumber <= 0))
                        {
                            doctors = doctors.Where(p => p.DepartmentNumber.Equals(departmentNumber));
                        }

                        DoctorListViewModel viewModel = new DoctorListViewModel
                        {
                            Doctors = doctors.ToList(),
                            SecondName = secondName,
                            Specialty = specialty,
                            DepartmentNumber = departmentNumber
                        };
                        return View(viewModel);
                    }
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
        }
    }
}

