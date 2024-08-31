using Microsoft.AspNetCore.Mvc;
using NorthwindBasedWebAPI.Repositories.IRepository;

namespace NorthwindBasedWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogRepository _logRepository;

        public LogsController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [HttpGet]
        public IActionResult GetLogs()
        {
            var logs = _logRepository.GetAll();

            return Ok(logs);
        }
    }
}
