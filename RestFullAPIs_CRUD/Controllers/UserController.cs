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

        [Route("CreateUsers")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequestModel user) { 
            var response = _userBusinessLayer.CreateUser(user);
            return Ok(response);
        }

        [Route("GetUsers")]
        [HttpGet]
        public IActionResult GetUser()
        {
            var response = _userBusinessLayer.GetUsers();
            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(string id)
        {
            var response = _userBusinessLayer.getusersById(id);
            return Ok(response);
        }

        [HttpDelete("DeleteUsers/{id}")]
        public IActionResult DeleteUsers(string id)
        {
            var response = _userBusinessLayer.DeleteUsers(id);
            return Ok(response);
        }




    }
}
