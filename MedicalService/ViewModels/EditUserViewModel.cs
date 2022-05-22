using System.ComponentModel.DataAnnotations;

namespace MedicalService.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
