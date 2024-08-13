using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Repository.Contract
{
    public interface ICaptchaRepository
    {
        Task<string> GenerateCaptchaImageAsync();
    }
}
