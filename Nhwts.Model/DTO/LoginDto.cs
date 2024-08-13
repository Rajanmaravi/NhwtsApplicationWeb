using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Model.DTO
{
    public class LoginDto
    {
        public string? username { get; set; }
        public string? state { get; set; }
        public string? category { get; set; }
        public string? usertype { get; set; }
        public string? captcha { get; set; }
        public string? mobileNumber {  get; set; }
        public string? loginRadio { get; set; }
        public string? password { get;set; }

    }
}
