using BusinessLayer.IBusinessLayer;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Mvc;

namespace RestFullAPIs_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusinessLayer _userBusinessLayer;

        public UserController(IUserBusinessLayer userBusinessLayer)
        {
            _userBusinessLayer = userBusinessLayer;
        }
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequestModel user) { 
            var response = _userBusinessLayer.CreateUser(user);
            return Ok(response);
        }
    }
}
