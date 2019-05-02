using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.ViewModels
{
    public class UserRegistration
    {
        public string loginID { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string password { get; set; }
        public string role { get; set; }
        
        public string userID { get; set; }
        [Display(Name = "Name")]
        public string name { get; set; }
        [Display(Name = "Address")]
        public string address { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Phone")]
        public string phone { get; set; }
    }
}