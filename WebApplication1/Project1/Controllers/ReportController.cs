using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReportApp.Logic.Services;
using ReportApp.Logic.Services.Interfacies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;

namespace ReportApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        IJsonService _jsonService;

        private readonly ILogger<ReportController> _logger;

        public ReportController(ILogger<ReportController> logger, IJsonService jsonService)
        {
            _logger = logger;
            _jsonService = jsonService;
        }
      
        [HttpGet]
        public void UpdateReportValues()
        {
            _jsonService.UpdateReportValues(new DateTime(2020, 1, 1));
        }
    }
}
