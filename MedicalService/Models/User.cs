using Microsoft.AspNetCore.Identity;
using MedicalService.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalService.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
        public Guid? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        public Guid? DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
    }
}
