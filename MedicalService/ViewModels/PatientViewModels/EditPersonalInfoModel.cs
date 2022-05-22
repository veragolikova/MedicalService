using System;
using System.ComponentModel.DataAnnotations;
using MedicalService.Models;


namespace MedicalService.ViewModels.PatientViewModels
{
    public class EditPersonalInfoModel
    {
        public Guid Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
