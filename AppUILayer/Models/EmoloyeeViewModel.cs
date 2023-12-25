using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace AppUILayer.Models
{
    public class EmoloyeeViewModel
    {
        [Key]
        [Display(Name = "ID")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfJoining { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [Display(Name = "Confirm Password")]
        public string cnfrmPassword { get; set; }

    }
}