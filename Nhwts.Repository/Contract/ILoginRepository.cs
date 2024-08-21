using Nhwts.Model;
using Nhwts.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Repository.Contract
{
    public interface ILoginRepository
    {
       Task<NonOcmmsUserModel> GetIndustry(LoginDto login);

        Task<IndustryRegistration> SignupIndustry(IndustryRegistration details);
    }
}
