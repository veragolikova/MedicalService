using System;

namespace MedicalService.ViewModels.DoctorViewModels
{
    public class EditPersonalInfoModel
    {
        public Guid Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DepartmentNumber { get; set; }
        public string DepartmentName { get; set; }
        public string Specialty { get; set; }
    }
}
