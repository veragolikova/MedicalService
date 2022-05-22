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
using MedicalService.ViewModels;
using MedicalService.Models.SubModels;
using Microsoft.EntityFrameworkCore;
using MedicalService.ViewModels.DoctorViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalService.Controllers
{
    public class DoctorController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public DoctorController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("doctor") || userRoles.Contains("admin"))
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
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(DoctorProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var user = await _userManager.FindByEmailAsync(model.Doctor.Email);
                    if (user != null)
                    {
                        Doctor doctor = new Doctor
                        {
                            FirstName = model.Doctor.FirstName,
                            MiddleName = model.Doctor.MiddleName,
                            SecondName = model.Doctor.SecondName,
                            CreatedOn = DateTime.Now,
                            PhoneNumber = model.Doctor.PhoneNumber,
                            Email = model.Doctor.Email,
                            DateOfBirth = model.Doctor.DateOfBirth,
                            WorkExperience = model.Doctor.WorkExperience,
                            DepartmentNumber = model.Doctor.DepartmentNumber,
                            DepartmentName = model.Doctor.DepartmentName,
                            Specialty = model.Doctor.Specialty,
                            University = model.Doctor.University
                        };

                        db.Doctors.Add(doctor);
                        db.SaveChanges();

                        user.Doctor = doctor;
                        user.DoctorId = doctor.Id;
                        await _userManager.AddToRoleAsync(user, "doctor");
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            DoctorProfileViewModel viewModel = new DoctorProfileViewModel { Id = doctor.Id, Doctor = doctor };
                            return View("DoctorProfile", viewModel);
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        return View("Error", new string[] { "You need to create a User with Email: " + model.Doctor.Email + " before the creation of the doctor profile."});
                    }

                }
                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> DoctorProfile(Guid doctorId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!userRoles.Contains("admin") || doctorId == Guid.Empty)
                {
                    doctorId = (Guid)user.DoctorId;
                }
                if(userRoles.Contains("admin") || userRoles.Contains("doctor"))
                {    
                    Doctor doctor = null;
                    using (var db = new ApplicationContext())
                    {
                        doctor = db.Doctors.FirstOrDefault(x => x.Id == doctorId);
                    }
                    DoctorProfileViewModel model = new DoctorProfileViewModel { Id = doctorId, UserName = user.UserName, Email = user.Email, Doctor = doctor };
                    return View(model);
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
            
        }

        public async Task<IActionResult> EditPersonalInfo(Guid doctorId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!(userRoles.Contains("admin") && doctorId != Guid.Empty))
                {
                    doctorId = (Guid)user.DoctorId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("doctor"))
                {
                    Doctor doctor = null;
                    using (var db = new ApplicationContext())
                    {
                        doctor = db.Doctors.FirstOrDefault(x => x.Id == doctorId);
                    }

                    EditPersonalInfoModel model = new EditPersonalInfoModel { Id = doctorId, FirstName = doctor.FirstName, SecondName = doctor.SecondName, MiddleName = doctor.MiddleName, DateOfBirth = doctor.DateOfBirth, DepartmentName = doctor.DepartmentName, DepartmentNumber = doctor.DepartmentNumber, Specialty = doctor.Specialty };
                    return View(model);
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonalInfo(EditPersonalInfoModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationContext())
                {
                    var doctor = db.Doctors.FirstOrDefault(x => x.Id == model.Id);
                    doctor.FirstName = model.FirstName;
                    doctor.MiddleName = model.MiddleName;
                    doctor.SecondName = model.SecondName;
                    doctor.DateOfBirth = model.DateOfBirth;
                    doctor.DepartmentName = model.DepartmentName;
                    doctor.DepartmentNumber = model.DepartmentNumber;
                    doctor.Specialty = model.Specialty;
                    db.Doctors.Update(doctor);
                    var result = await db.SaveChangesAsync();
                    if (result == 1)
                    {
                        DoctorProfileViewModel viewModel = new DoctorProfileViewModel { Id = model.Id, Doctor = doctor };
                        return View("DoctorProfile", viewModel);
                    }
                    else
                    {
                        return View("Error", new string[] { "Что-то пошло не так, попробуйте снова..." });
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditEducationAndWorkExperience(Guid doctorId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!(userRoles.Contains("admin") && doctorId != Guid.Empty))
                {
                    doctorId = (Guid)user.DoctorId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("doctor"))
                {
                    Doctor doctor = null;
                    using (var db = new ApplicationContext())
                    {
                        doctor = db.Doctors.FirstOrDefault(x => x.Id == doctorId);
                    }
                    EditEducationAndWorkExperience model = new EditEducationAndWorkExperience { Id = doctorId, University = doctor.University, WorkExperience = doctor.WorkExperience };
                    return View(model);
                }
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditEducationAndWorkExperience(EditEducationAndWorkExperience model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationContext())
                {
                    var doctor = db.Doctors.FirstOrDefault(x => x.Id == model.Id);
                    doctor.University = model.University;
                    doctor.WorkExperience = model.WorkExperience;
                    db.Doctors.Update(doctor);
                    var result = await db.SaveChangesAsync();
                    if (result == 1)
                    {
                        DoctorProfileViewModel viewModel = new DoctorProfileViewModel { Id = model.Id, Doctor = doctor };
                        return View("DoctorProfile", viewModel);
                    }
                    else
                    {
                        return View("Error", new string[] { "Что-то пошло не так, попробуйте снова..." });
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> EditContacts(Guid doctorId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!(userRoles.Contains("admin") && doctorId != Guid.Empty))
                {
                    doctorId = (Guid)user.DoctorId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("doctor"))
                {
                    Doctor doctor = null;
                    using (var db = new ApplicationContext())
                    {
                        doctor = db.Doctors.FirstOrDefault(x => x.Id == doctorId);
                    }
                    EditContacts model = new EditContacts { Id = doctorId, PhoneNumber = doctor.PhoneNumber, Email = doctor.Email };
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditContacts(EditContacts model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationContext())
                {
                    var doctor = db.Doctors.FirstOrDefault(x => x.Id == model.Id);
                    var user = await _userManager.FindByEmailAsync(doctor.Email);
                    doctor.PhoneNumber = model.PhoneNumber;
                    doctor.Email = model.Email;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    db.Doctors.Update(doctor);
                    _userManager.UpdateAsync(user);
                    var result = await db.SaveChangesAsync();
                    if (result == 1)
                    {
                        DoctorProfileViewModel viewModel = new DoctorProfileViewModel { Id = model.Id, Doctor = doctor };
                        return View("DoctorProfile", viewModel);
                    }
                    else
                    {
                        return View("Error", new string[] { "Что-то пошло не так, попробуйте снова..." });
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ListOfPatients(Guid doctorId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if ((userRoles.Contains("doctor") && doctorId == Guid.Empty))
                {
                    doctorId = (Guid)user.DoctorId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("doctor"))
                {
                    
                    IEnumerable<Patient>  patients = null;
                    using (var db = new ApplicationContext())
                    {
                        patients = db.Patients.Where(x => x.GPDoctorId == doctorId).ToList();
                    }
                    return View(patients);
                }
            }
            else
            {
                return View("Error", new string[] { "В доступе отказано" });
            }
            return NotFound();
        }

    }
}
