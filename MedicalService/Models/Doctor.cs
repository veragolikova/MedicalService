using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MedicalService.Models.SubModels;

namespace MedicalService.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid? PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int WorkExperience { get; set; }
        public int DepartmentNumber { get; set; }
        public string DepartmentName { get; set; }
        public string Specialty { get; set; }
        public string University { get; set; }
        //public List<Patient> Patients { get; set; }
        //public Doctor()
        //{
        //    Patients = new List<Patient>();
        //}

    }
}
