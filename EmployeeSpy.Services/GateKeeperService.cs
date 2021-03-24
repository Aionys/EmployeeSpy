using EmployeeSpy.Core.Abstractions;
using System;
using System.Threading.Tasks;

namespace EmployeeSpy.Services
{
    public class GateKeeperService : IGateKeeperService
    {
        public Task<bool> VerifyPassAttempt(int personId, int gateKeeperId)
        {
            throw new NotImplementedException();
        }
    }
}
