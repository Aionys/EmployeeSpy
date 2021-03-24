using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSpy.Core.Abstractions
{
    public interface IGateKeeperService
    {
        Task<bool> VerifyPassAttempt(int personId, int gateKeeperId);
    }
}
