using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_game.Models.ViewModel
{
    public class ForgetPassword
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [CompareAttribute("NewPassword", ErrorMessage = "NewPassword and ConfirmPassword doesn't match.")]
        public string ConfirmPassword { get; set; }

    }
}