using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data.UserData;
using ToDoList.Services;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _services;
        public UserController(IUserServices services)
        {
            _services = services;
        }


        [HttpPost("login")]
        public IActionResult login(UserLoginDTO user)
        {
            var result = _services.login(user);

            if (result == null)
            {
                return Unauthorized("Invalid user");
            }
            return Ok(result);
        }


        [HttpPost]
        [Route("signup")]
        public IActionResult signup(UserSignupDTO user)
        {
            try
            {
                var u = _services.createUser(user);
                return Ok("User created successfully");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("getUser")]
        public ActionResult<UserDTO> get(int id)
        {

          
            return _services.get(id);


        }


    }
}
