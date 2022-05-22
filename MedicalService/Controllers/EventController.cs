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
    public class EventController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public EventController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> CalendarAsync(string patientId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                using (var db = new ApplicationContext())
                {
                    if (patientId != null)
                    {
                        var patient = db.Patients.FirstOrDefault(x => x.Id == new Guid(patientId));
                        var events = db.Events.Where(a => a.PatientId == new Guid(patientId)).ToList();
                        if (userRoles.Contains("admin") || userRoles.Contains("doctor"))
                        {
                            return View("Calendar", patient);
                        }
                        else if (userRoles.Contains("patient"))
                        {
                            return View("PatientCalendar", patient);
                        }
                        else 
                            return View("Error", new string[] { "В доступе отказано" });

                    }
                    else
                    {
                        var patient = db.Patients.FirstOrDefault(x => x.Id == user.PatientId);
                        if (userRoles.Contains("patient"))
                        {
                            return View("PatientCalendar", patient);
                        }
                        else if (userRoles.Contains("doctor") || !userRoles.Contains("patient"))
                        {
                            return RedirectToAction("Create", "Patient");
                        }
                        else 
                            return View("Error", new string[] { "В доступе отказано" });
                    }
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> PatientCalendar() => View();


        [HttpGet]
        public Microsoft.AspNetCore.Mvc.JsonResult GetEvents(string patientId)
        {
            using (var db = new ApplicationContext()) 
            {
                var events = db.Events.Where(a => a.PatientId == new Guid(patientId)).ToList();
                events.Add(new Event { Title = "Vitamins A+D+E", Description = "Test text", StartDate = new DateTime(2022, 05, 10), EndDate = new DateTime(2022, 05, 12), TimesADame =2 });
                events.Add(new Event { Title = "Doctor Mom", Description = "Test text, test text :)", StartDate = new DateTime(2022, 05, 11), EndDate = new DateTime(2022, 05, 17), TimesADame =3 });
           
                return new JsonResult(events); 
            }
        }
        public IActionResult SaveEvent(string patientId,string id, string title, DateTime startDate, DateTime endDate, string description, int timesADay)
        {
            using (var db = new ApplicationContext()) 
            {
                
                var patient = db.Patients.FirstOrDefault(x => x.Id == new Guid(patientId));
                if (id != null)
                {
                    var _event = db.Events.Where(a => a.Id == new Guid(id)).FirstOrDefault();
                    if (_event != null)
                    {
                        _event.Title = title;
                        _event.StartDate = startDate;
                        _event.EndDate = endDate.AddDays(1); ;
                        _event.Description = description;
                        _event.TimesADame = timesADay;
                        _event.PatientId = new Guid(patientId);
                    }
                    db.SaveChanges();
                }
                else
                {
                    db.Events.Add(new Event {Title = title, StartDate = startDate, EndDate = endDate, Description = description, TimesADame = timesADay, PatientId = new Guid(patientId), Patient = patient });
                    db.SaveChanges();
                }
                return RedirectToAction("ListOfPatients", "Doctor");
                //return View("Calendar", patient);
            }
        }
        public IActionResult DeleteEvent(string patientId, string id)
        {
            using (var db = new ApplicationContext())
            {
                var events = db.Events;
                var patient = db.Patients.FirstOrDefault(x => x.Id == new Guid(patientId));
                if (id != null)
                {
                    var _event = db.Events.Where(a => a.Id == new Guid(id)).FirstOrDefault();
                    if (_event != null)
                    {
                        db.Events.Remove(_event);
                        db.SaveChanges();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
                return RedirectToAction("ListOfPatients", "Doctor");
                //return View("Calendar", patient);
            }
        }

    }
}
