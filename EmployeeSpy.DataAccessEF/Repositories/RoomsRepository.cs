using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmployeeSpy.DataAccessEF.Repositories
{
    public class RoomsRepository : RepositoryBase<Room>, IRoomsRepository
    {
        public RoomsRepository(EmployeeSpyContext context) : base (context) { }

        public Room GetByControlId(int gateKeeperId)
        {
            var result = _context.Rooms
                .Where(r => r.Entrance.EntranceControl.Id == gateKeeperId)
                .Include(r => r.Entrance)
                .ThenInclude(d => d.EntranceControl).FirstOrDefault();
            
            if (result != null) return result;

            var exit = _context.Rooms
               .Where(r => r.Entrance.ExitControl.Id == gateKeeperId)
               .Include(r => r.Entrance)
               .ThenInclude(d => d.ExitControl).FirstOrDefault();

            return exit;
        }
    }
}
