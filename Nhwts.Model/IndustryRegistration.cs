using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Nhwts.Model
{
   
    public class IndustryRegistration : StateViewModel
    {
       
        public string? Category { get; set; }
        public string? Stcode { get; set; }
        public string? Dtcode { get; set; }
       
        [Required]
        [Display(Name = "User Name")]
        public string? Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string? Password { get; set; }
        [Compare("Password")]
        public string? Confirmpassword { get; set; }
        [Required]
        [Display(Name = "Industry Name")]
        public string? IndustryName { get; set; }
        public string? IndustryAddress { get; set; }
        public string? ContactPersonName { get; set; }
        public string? MobileNumber { get; set; }
        public string? EmailId { get; set; }
        public string? AuthorizationNo { get; set; }
        public string? AuthissuanceDate { get; set; }
        public string? AuthValidity { get; set; }
        public string? ProductCommenceYear { get; set; }
        public string? FacilityShiftdd { get; set; }
        public string? FileName { get; set; }
        public string? Status { get; set; }
        public string? Exception { get; set; }
        
        [Required]
        public IFormFile? FormFile { get; set; }

    }
}
