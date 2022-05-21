using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthService.Entities;
using AuthService.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    /*[ApiController]
    [Route("manufactures")]*/
    public class ManufacturesController : ControllerBase
    {

        private readonly ILogger<ManufacturesController> _logger;
        private readonly IManufactureService _manufactureService;

        public ManufacturesController(ILogger<ManufacturesController> logger, IManufactureService manufactureService)
        {
            _logger = logger;
            _manufactureService = manufactureService;
        }


        [HttpPost, Route("all")]
        public List<Manufacture> GetManufactures()
        {
            return _manufactureService.GetFactyries();
        }
    }
}
