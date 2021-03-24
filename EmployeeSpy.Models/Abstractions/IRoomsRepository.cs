using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeSpy.Core.Abstractions
{
    public interface IRoomsRepository : IRepository<Room>
    {
        Room GetByControlId(int gateKeeperId);
    }
}
