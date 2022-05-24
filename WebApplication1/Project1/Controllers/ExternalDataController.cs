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
        public List<RoleDto> Roles()
        {
            var roles = _externalDataService.GetRoles();


            return roles;
        }

        [HttpGet]
        [Route("team")]
        public List<TeamDto> Teams()
        {
            return _externalDataService.GetTeams();
        }

        [HttpGet]
        [Route("competentions")]
        public List<CompetentionDto> Competentions()
        {
            return _externalDataService.GetCompitetions();
        }

        [HttpGet]
        [Route("participants")]
        public List<ParticipantDto> Participants()
        {
            return _externalDataService.GetParticipants();
        }

        [HttpGet]
        [Route("new-role")]
        public RoleDto Role()
        {
            return _externalDataService.GetRole(null);
        }

        [HttpGet]
        [Route("new-team")]
        public TeamDto Team()
        {
            return _externalDataService.GetTeam(null);
        }

        [HttpGet]
        [Route("new-competention")]
        public CompetentionDto Competention()
        {
            return _externalDataService.GetCompitetion(null);
        }

        [HttpGet]
        [Route("new-participant")]
        public ParticipantDto Participant()
        {
            return _externalDataService.GetParticipant(null);
        }
    }
}
