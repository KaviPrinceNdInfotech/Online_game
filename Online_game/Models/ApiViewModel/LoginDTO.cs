using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Online_game.Models.ApiViewModel
{
    public class LoginDTO
    {

        public string Mobile_no { get; set; }

        public string Password { get; set; }
    }
    public class RegistrationDTO
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Mobile_no { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string City { get; set; }
    }
    public class ForgetPasswordDTO
    {

        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}