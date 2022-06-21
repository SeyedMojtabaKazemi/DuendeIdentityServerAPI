using Contract.UserAggregate;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerAPI.Controllers
{
    /// <summary>
    /// this is summary for controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {

        private readonly IUserServiceCommand _userServiceCommand;

        public IdentityController(IUserServiceCommand userServiceCommand)
        {
            _userServiceCommand = userServiceCommand;
        }


        ///<summary>
        ///this is summary for action
        /// </summary>
        /// <remarks>
        /// this is remark for action
        /// </remarks>
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult("Hello World!");
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]UserDto user)
        {
            await _userServiceCommand.Add(user);
            return Ok(user);
        }

        [HttpGet("UpdateSecurityStamp")]
        public async Task<IActionResult> UpdateSecurityStamp()
        {
            await _userServiceCommand.UpdateSecurityStamp();
            return Ok();
        }
    }
}
