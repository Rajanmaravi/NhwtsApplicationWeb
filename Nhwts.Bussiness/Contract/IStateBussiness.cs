using Nhwts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhwts.Bussiness.Contract
{
    public interface IStateBussiness
    {
        Task<List<States>> GetStates();
    }
}
