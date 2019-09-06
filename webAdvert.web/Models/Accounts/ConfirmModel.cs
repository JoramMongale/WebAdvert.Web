using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webAdvert.web.Models.Accounts
{
    public class ConfirmModel
    {
        [Required]
        [Display(Name="Email")]
        [EmailAddress]
        public string Email  { get; set; }
        [Required]
        public string Code { get; set; }


    }
}
