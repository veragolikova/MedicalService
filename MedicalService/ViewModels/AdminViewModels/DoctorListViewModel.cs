using MedicalService.Models;
using System.Collections.Generic;

namespace MedicalService.ViewModels.AdminViewModels
{
    public class DoctorListViewModel
    {
        public string SecondName { get; set; }
        public int DepartmentNumber { get; set; }
        public string Specialty { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
