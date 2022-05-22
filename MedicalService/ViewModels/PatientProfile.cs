using System;
using System.ComponentModel.DataAnnotations;
using MedicalService.Models;

namespace MedicalService.ViewModels
{
    public class PatientProfile
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public Patient Patient { get; set; }
    }
}
