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
using MedicalService.ViewModels.PatientViewModels;

namespace MedicalService.Controllers
{
    public class PatientController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public PatientController(UserManager<User> userManager, SignInManager<User> signInManager)
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
                if (userRoles.Contains("patient") || userRoles.Contains("doctor") || userRoles.Contains("admin"))
                {
                    return View();
                }
            }

            return RedirectToAction("Create", "Patient");
        }
        public async Task<IActionResult> PatientProfileEditable(Guid patientId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("patient"))
                {
                    patientId = (Guid)user.PatientId;
                }
                if (userRoles.Contains("patient") || userRoles.Contains("admin"))
                {
                    Doctor doctor = null;
                    Patient patient = null;
                    Address address = null;
                    using (var db = new ApplicationContext())
                    {
                        patient = db.Patients.FirstOrDefault(x => x.Id == patientId);
                        address = db.Addresses.FirstOrDefault(x => x.Id == patient.AddressId);
                        doctor = db.Doctors.FirstOrDefault(x => x.Id == patient.GPDoctorId);
                        patient.Address = address;
                        patient.GPDoctor = doctor;

                    }
                    var patientUser = await _userManager.FindByEmailAsync(patient.Email);
                    PatientProfile model = new PatientProfile { Id = patientId.ToString(), UserName = patientUser.UserName, Email = patientUser.Email, Patient = patient };
                    return View(model);
                }
            }

            return RedirectToAction("Create", "Patient");
        }
        public async Task<IActionResult> OpenPatientProfile(Guid patientId)
        {
            if (patientId == Guid.Empty)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Contains("admin"))
                {
                    return RedirectToAction("PatientProfileEditable", "Patient", new { patientId = patientId });

                }
                else if (userRoles.Contains("doctor"))
                {
                    Doctor doctor = null;
                    Patient patient = null;
                    Address address = null;
                    using (var db = new ApplicationContext())
                    {

                        patient = db.Patients.FirstOrDefault(x => x.Id == patientId);
                        address = db.Addresses.FirstOrDefault(x => x.Id == patient.AddressId);
                        doctor = db.Doctors.FirstOrDefault(x => x.Id == patient.GPDoctorId);
                        patient.Address = address;
                        patient.GPDoctor = doctor;

                    }
                    var patientUser = await _userManager.FindByEmailAsync(patient.Email);
                    PatientProfile model = new PatientProfile { Id = patientId.ToString(), UserName = patientUser.UserName, Email = patient.Email, Patient = patient };
                    return View("PatientProfile", model);
                }
                //else if (userRoles.Contains("doctor") || !userRoles.Contains("patient"))
                //{
                //    return RedirectToAction("Create", "Patient");
                //}
                else
                {
                    return View("Error", new string[] { "В доступе отказано" });
                }
            }
            return NotFound();
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(PatientProfile model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var user = await _userManager.GetUserAsync(User);
                    Patient patient = new Patient
                    {
                        FirstName = model.Patient.FirstName,
                        MiddleName = model.Patient.MiddleName,
                        SecondName = model.Patient.SecondName,
                        CreatedOn = DateTime.Now,
                        PhoneNumber = model.Patient.PhoneNumber,
                        Email = user.Email,
                        DateOfBirth = model.Patient.DateOfBirth,
                        Address = new Address
                        {
                            Country = model.Patient.Address.Country,
                            Region = model.Patient.Address.Region,
                            City = model.Patient.Address.City,
                            StreetName = model.Patient.Address.StreetName,
                            HouseNumber = model.Patient.Address.HouseNumber,
                            FlatNumber = model.Patient.Address.FlatNumber,
                            //PostalCode = model.Patient.Address.PostalCode
                        }//,
                        //GPDoctor = model.Patient.GPDoctor,

                    };
                    db.Patients.Add(patient);
                    db.SaveChanges();

                    if (user != null)
                    {
                        user.Patient = patient;
                        user.PatientId = patient.Id;
                        await _userManager.AddToRoleAsync(user, "patient");
                        var result = await _userManager.UpdateAsync(user);
                        if (result.Succeeded)
                        {
                            return RedirectToAction("PatientProfileEditable");
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }

                }


            }
            return View(model);
        }
        public async Task<IActionResult> EditPersonalInfo(Guid patientId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!(userRoles.Contains("admin") && patientId != Guid.Empty))
                {
                    patientId = (Guid)user.PatientId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("patient"))
                {
                    Patient patient = null;
                    using (var db = new ApplicationContext())
                    {
                        patient = db.Patients.FirstOrDefault(x => x.Id == patientId);
                    }

                    EditPersonalInfoModel model = new EditPersonalInfoModel { Id = patientId, FirstName = patient.FirstName, SecondName = patient.SecondName, MiddleName = patient.MiddleName, DateOfBirth = patient.DateOfBirth };
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
                    var patient = db.Patients.FirstOrDefault(x => x.Id == model.Id);
                    patient.FirstName = model.FirstName;
                    patient.MiddleName = model.MiddleName;
                    patient.SecondName = model.SecondName;
                    patient.DateOfBirth = model.DateOfBirth;
                    db.Patients.Update(patient);
                    var result = await db.SaveChangesAsync();
                    if (result == 1)
                    {
                        PatientProfile viewModel = new PatientProfile { Id = model.Id.ToString(), Patient = patient };
                        return View("PatientProfileEditable", viewModel);
                    }
                    else
                    {
                        return View("Error", new string[] { "Что-то пошло не так, попробуйте снова..." });
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditAddress(Guid patientId)
        {
            //при обновлении адреса -> обновлять участок и врача!

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!(userRoles.Contains("admin") && patientId != Guid.Empty))
                {
                    patientId = (Guid)user.PatientId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("patient"))
                {
                    Patient patient = null;
                    Address address = null;
                    using (var db = new ApplicationContext())
                    {
                        patient = db.Patients.FirstOrDefault(x => x.Id == patientId);
                        address = db.Addresses.FirstOrDefault(x => x.Id == patient.AddressId);
                    }
                    EditAddress model = new EditAddress { Id = patientId, Address = address };
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
        public async Task<IActionResult> EditAddress(EditAddress model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationContext())
                {
                    var patient = db.Patients.FirstOrDefault(x => x.Id == model.Id);
                    var address = db.Addresses.FirstOrDefault(x => x.Id == patient.AddressId);
                    address.Country = model.Address.Country;
                    address.City = model.Address.City;
                    address.Region = model.Address.Region;
                    address.StreetName = model.Address.StreetName;
                    address.HouseNumber = model.Address.HouseNumber;
                    address.FlatNumber = model.Address.FlatNumber;
                    db.Addresses.Update(address);
                    var result = await db.SaveChangesAsync();
                    if (result == 1)
                    {
                        PatientProfile viewModel = new PatientProfile { Id = model.Id.ToString(), Patient = patient };
                        return View("PatientProfileEditable", viewModel);
                    }
                    else
                    {
                        return View("Error", new string[] { "Что-то пошло не так, попробуйте снова..." });
                    }
                }
            }
            return View(model);
        }
        public async Task<IActionResult> EditContacts(Guid patientId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (!(userRoles.Contains("admin") && patientId != Guid.Empty))
                {
                    patientId = (Guid)user.PatientId;
                }
                if (userRoles.Contains("admin") || userRoles.Contains("patient"))
                {
                    Patient patient = null;
                    using (var db = new ApplicationContext())
                    {
                        patient = db.Patients.FirstOrDefault(x => x.Id == patientId);
                    }
                    EditContacts model = new EditContacts { Id = patientId, PhoneNumber = patient.PhoneNumber, Email = patient.Email };
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
        public async Task<IActionResult> EditContacts(EditContacts model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationContext())
                {
                    var patient = db.Patients.FirstOrDefault(x => x.Id == model.Id);
                    var user = await _userManager.FindByEmailAsync(patient.Email);
                    var userRoles = await _userManager.GetRolesAsync(user);
                    if (userRoles.Contains("doctor"))
                    {
                        patient.PhoneNumber = model.PhoneNumber;
                        patient.Email = model.Email;
                        db.Patients.Update(patient);
                    }
                    else
                    {
                        patient.PhoneNumber = model.PhoneNumber;
                        patient.Email = model.Email;
                        user.Email = model.Email;
                        user.UserName = model.Email;
                        db.Patients.Update(patient);
                        _userManager.UpdateAsync(user);
                    }
                    var result = await db.SaveChangesAsync();
                    if (result == 1)
                    {
                        PatientProfile viewModel = new PatientProfile { Id = model.Id.ToString(), Patient = patient, UserName = user.UserName };
                        return View("PatientProfileEditable", viewModel);
                    }
                    else
                    {
                        return View("Error", new string[] { "Что-то пошло не так, попробуйте снова..." });
                    }
                }
            }
            return View(model);
        }
    }
}
