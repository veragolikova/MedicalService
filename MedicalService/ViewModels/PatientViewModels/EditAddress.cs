using System;
using System.ComponentModel.DataAnnotations;
using MedicalService.Models.SubModels;

namespace MedicalService.ViewModels.PatientViewModels
{
    public class EditAddress
    {
        public Guid Id { get; set; }
        public Address Address { get; set; }
    }
}
