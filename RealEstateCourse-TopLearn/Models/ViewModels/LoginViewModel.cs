using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RealEstateCourse_TopLearn.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }

        [TempData]
        public string? ErrorMessage { get; set; }
    }
}
