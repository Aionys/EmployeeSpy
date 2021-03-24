using EmployeeSpy.Core.Abstractions;
using EmployeeSpy.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeSpy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GateKeeperController : ControllerBase
    {
        private readonly IGateKeeperService _gateKeeperService;

        public GateKeeperController(IGateKeeperService service)
        {
            _gateKeeperService = service;
        }

        [HttpPost("")]
        public async Task<object> Post([FromBody] PassAttemptDto model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            return await _gateKeeperService.VerifyPassAttempt(model.PersonId, model.GateKeeperId);
        }

        public async Task<object> Get()
        {
            return Ok();
        }
    }
}
