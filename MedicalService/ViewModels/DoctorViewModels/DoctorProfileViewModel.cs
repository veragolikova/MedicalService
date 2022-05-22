using MedicalService.Models;
using System;
using System.Collections.Generic;

namespace MedicalService.ViewModels.DoctorViewModels
{
    public class DoctorProfileViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
