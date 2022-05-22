using MedicalService.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MedicalService.ViewModels.AdminViewModels
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public string Email  { get; set; }
        public string UserName { get; set; }

    }
}
