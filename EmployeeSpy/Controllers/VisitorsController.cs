using EmployeeSpy.Abstractions;
using EmployeeSpy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeSpy.Controllers
{
    [Route("api/visitors")]
    [ApiController]
    [Authorize]
    public class VisitorsController : ControllerBase
    {
        private IRepository<Visitor> _repo;

        public VisitorsController(IRepository<Visitor> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IEnumerable<Visitor> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public Visitor Get(int id)
        {
            return _repo.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Visitor value)
        {
            _repo.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Visitor value)
        {
            var visitor = _repo.GetById(id);
            visitor.FirstName = value.FirstName;
            visitor.LastName = value.LastName;
            _repo.Commit();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
