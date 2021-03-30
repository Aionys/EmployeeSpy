using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeSpy.Controllers
{
    [Route("api/Rooms")]
    [ApiController]
    [Authorize]
    public class RoomsController : ControllerBase
    {
        private IRepository<Room> _repo;

        public RoomsController(IRepository<Room> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public Room Get(int id)
        {
            return _repo.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Room value)
        {
            _repo.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Room value)
        {
            var room = _repo.GetById(id);
            room.Name = value.Name;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
