using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Amazon.AspNetCore.Identity.Cognito;


namespace webAdvert.web.Models.Accounts
{
    public class SignUpModel
    {

        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(6, ErrorMessage ="Password should be at lease 6 characters")]
        [Display(Name ="Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password  and confirmation must natch")]
        public string ConfirmPassword{ get; set; }
    }
}
