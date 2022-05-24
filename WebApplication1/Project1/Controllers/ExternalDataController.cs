using App.Entities;
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
    [Route("external-data")]
    public class ExternalDataController : Controller
    {
        private ILogger<ExternalDataController> _logger;
        private IRestApiListnerService _service;
        private IExternalDataService _externalDataService;

        public ExternalDataController(ILogger<ExternalDataController> logger, IRestApiListnerService restService,
            IExternalDataService externalDataService)
        {
            _logger = logger;
            _service = restService;
            _externalDataService = externalDataService;
        }

        [HttpGet]
        [Route("refresh-api-external-data/{competentionId}")]
        public IActionResult UpsertDataFromApi(int competentionId)
        {
            if (_service.UpsertDataFromApi(competentionId))
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("roles")]
        public List<RoleApiDto> Roles()
        {
            var roles = _externalDataService.GetRoles();


            return roles;
        }


        [HttpGet]
        [Route("role")]
        public RoleApiDto Role()
        {
            return _externalDataService.GetRole(null);
        }


        [HttpGet]
        [Route("team")]
        public List<Team> Teams()
        {
            return _externalDataService.GetTeams();
        }

        [HttpGet]
        [Route("competentions")]
        public List<Competention> Competentions()
        {
            return _externalDataService.GetCompitetions();
        }

        [HttpGet]
        [Route("participants")]
        public List<ApplicationUser> Participants()
        {
            return _externalDataService.GetParticipants();
        }

        [HttpGet]
        [Route("new-team")]
        public Team Team()
        {
            return _externalDataService.GetTeam(null);
        }

        [HttpGet]
        [Route("new-competention")]
        public Competention Competention()
        {
            return _externalDataService.GetCompitetion(null);
        }

        [HttpGet]
        [Route("new-application-user")]
        public ApplicationUser ApplicationUser()
        {
            return _externalDataService.GetParticipant(null);
        }
    }
}
