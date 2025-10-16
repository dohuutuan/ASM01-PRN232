using _BE.Models;
using _BE.Models.Responses;
using _BE.Repositories.Interface;
using _BE.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace _BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "999")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [EnableQuery]
        public IQueryable<SystemAccount> Get()
        {
            return _accountService.GetAccounts();
        }

        [HttpPost]
        public IActionResult CreateAccount(SystemAccount account)
        {
            try
            {
                var created = _accountService.CreateAccount(account);
                return Ok(new APIResponse<SystemAccount>
                {
                    StatusCode = 201,
                    Message = "Account created successfully",
                    Data = created
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccount(short id, SystemAccount account, [FromQuery] string? currentPassword)
        {
            try
            {
                var updated = _accountService.UpdateAccount(id, account, currentPassword);
                return Ok(new APIResponse<SystemAccount>
                {
                    StatusCode = 200,
                    Message = "Account updated successfully",
                    Data = updated
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(short id)
        {
            try
            {
                _accountService.DeleteAccount(id);
                return Ok(new APIResponse<string>
                {
                    StatusCode = 200,
                    Message = "Account deleted successfully",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<string>
                {
                    StatusCode = 400,
                    Message = ex.Message,
                    Data = null
                });
            }
        }



    }
}
