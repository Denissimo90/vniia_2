using App.Entities.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportApp.Logic.Services.Interfacies;
using System.Collections.Generic;

namespace ReportApp.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("restApiListner")]
    public class RestApiListnerController : Controller
    {
        private ILogger<RestApiListnerController> _logger;
        private IRestApiListnerService _service;
        private IJsonService _jsonService;

        public RestApiListnerController(ILogger<RestApiListnerController> logger, IRestApiListnerService service, IJsonService jsonService)
        {
            _logger = logger;
            _service = service;
            _jsonService = jsonService;
        }

        [HttpGet]
        [Route("getRolesFromApi")]
        public List<RoleDto> GetRoles()
        {
            return _service.GetApiRoles();
        }

        [HttpGet]
        [Route("upsertDataFromApi/{competentionId}")]
        public IActionResult UpsertDataFromApi(int competentionId)
        {
            if (_service.UpsertDataFromApi(competentionId))
                return Ok();
            else
                return BadRequest();
        }
    }
}
