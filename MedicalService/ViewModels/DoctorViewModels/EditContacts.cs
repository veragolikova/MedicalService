using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalService.ViewModels.DoctorViewModels
{
    public class EditContacts
    {
        public Guid Id { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
