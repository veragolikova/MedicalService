using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalService.Models.SubModels;
using Microsoft.AspNetCore.Mvc;

namespace MedicalService.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? AddressId { get; set; }
        [ForeignKey("AddressId")]
        public Address Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Guid? GPDoctorId { get; set; }
        [ForeignKey("GPDoctorId")]
        public Doctor GPDoctor { get; set; }
    }
}
