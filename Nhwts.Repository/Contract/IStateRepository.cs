using Nhwts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Repository.Contract
{
    public interface IStateRepository
    {
        Task<List<States>> GetStates();

        Task<List<States>> GetDistricts(States stcode);
    }
}
