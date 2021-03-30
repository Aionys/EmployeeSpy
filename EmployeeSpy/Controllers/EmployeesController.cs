using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeSpy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private IRepository<Employee> _repo;

        public EmployeesController(IRepository<Employee> repository)
        {
            _repo = repository;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return _repo.GetAll();
        }

        [HttpGet("{id}")]
        public Employee Get(int id)
        {
            return _repo.GetById(id);
        }

        [HttpPost]
        public void Post([FromBody] Employee employee)
        {
            _repo.Add(employee);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee employee)
        {
            var existingEmp = _repo.GetById(id);
            existingEmp.FirstName = employee.FirstName;
            existingEmp.LastName = employee.LastName;
            _repo.Commit();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _repo.Delete(id);
        }
    }
}
