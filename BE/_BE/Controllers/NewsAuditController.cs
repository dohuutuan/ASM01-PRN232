using _BE.Models.Responses;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAuditController : ControllerBase
    {
        private readonly INewsAuditService _auditService;

        public NewsAuditController(INewsAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpGet]
        [Authorize(Roles = "1,999")]
        public IActionResult Get()
        {
            var audit = _auditService.GetAuditInfo();
            return Ok(new APIResponse<IQueryable<NewsAuditDto>>(200, "Success", audit));
        }
    }

}
