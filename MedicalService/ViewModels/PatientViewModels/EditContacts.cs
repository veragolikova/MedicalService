using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalService.ViewModels.PatientViewModels
{
    public class EditContacts
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}
