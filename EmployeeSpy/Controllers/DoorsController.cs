using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace EmployeeSpy.Controllers
{
    [Route("api/doors")]
    [ApiController]
    [Authorize]
    public class DoorsController : ControllerBase
    {
        private IRepository<Door> _repo;

        public DoorsController(IRepository<Door> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Door> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public Door Get(int id)
        {
            return _repo.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Door door)
        {
            _repo.Add(door);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Door value)
        {
            var exDoor = _repo.GetById(id);
            exDoor.KeepOpenSeconds = value.KeepOpenSeconds;
            _repo.Commit();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
