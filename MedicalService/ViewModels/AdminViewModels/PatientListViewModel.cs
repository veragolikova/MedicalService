using MedicalService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;

namespace MedicalService.ViewModels.AdminViewModels
{
    public class PatientListViewModel
    {
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Patient> Patients { get; set; }
    }
}
