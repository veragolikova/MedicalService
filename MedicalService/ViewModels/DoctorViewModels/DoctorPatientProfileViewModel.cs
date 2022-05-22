using MedicalService.Models;
using System;

namespace MedicalService.ViewModels.DoctorViewModels
{
    public class DoctorPatientProfileViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public PatientProfile PatientProfile { get; set; }
    }
}
