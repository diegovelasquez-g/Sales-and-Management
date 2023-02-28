using Microsoft.AspNetCore.Mvc;
using Sales_and_Management.Services;
using Sales_and_Management.Models;

namespace Sales_and_Management.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _iusersService;
        public UsersController(IUsersService iusersService)
        {
            _iusersService = iusersService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _iusersService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await _iusersService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> Post(Users user)
        {
            //Validate if the email / username is already used
            var userNameValidator = await _iusersService.GetValidateUserName(user.userName);
            var emailValidator = await _iusersService.GetValidateUserEmail(user.email);

            if (userNameValidator != null)
                return BadRequest("El usuario ya se encuentra en uso.");
            if (emailValidator != null)
                return BadRequest("El email ya se encuentra en uso.");
            if (userNameValidator != null && emailValidator != null)
                return BadRequest("Las credenciales ya se encuentran en uso.");
            await _iusersService.CreateNewUserAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Users userToUpdate)
        {
            var user = _iusersService.GetUserByIdAsync(userToUpdate.Id);
            if (user == null)
            {
                return NotFound();
            }
            await _iusersService.UpdateUserAsync(userToUpdate);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _iusersService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _iusersService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("SignIn")]
        public async Task<IActionResult> Get(string userName, string password)
        {
            var user = await _iusersService.GetUserLogin(userName, password);
            if (user == null)
            {
                return NotFound("Credenciales invalidas");
            }
            return Ok(user);
        }

    }
}
