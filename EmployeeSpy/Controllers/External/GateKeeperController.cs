﻿using EmployeeSpy.Core.Abstractions;
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
        public bool Post([FromBody] PassAttemptDto model)
        {
            return (model.PersonType == PersonType.Visitor)
                ? _gateKeeperService.VerifyVisitorPassAttempt(model.PersonId, model.GateKeeperId)
                : _gateKeeperService.VerifyEmployeePassAttempt(model.PersonId, model.GateKeeperId);
        }
    }
}
