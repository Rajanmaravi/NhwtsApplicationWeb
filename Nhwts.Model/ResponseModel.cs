using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Model
{
    public class ResponseModel
    {
        public string? Message { get; set; }
        public bool? IsSuccess { get; set; }
        public bool? IsResponse { get; set; }
    }
}
