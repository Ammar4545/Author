using ApiApp.hepler;
using ApiApp.Models.RequestDTO;
using ApiApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ErrorHandeler error;

        public AuthenticationController(IUserRepository userRepository, ErrorHandeler error)
        {
            this.userRepository = userRepository;
            this.error = error;
        }

        [HttpPost("Registration")]
        public IActionResult Registration(UserAddTDO userAddTDO)
        {
            var AuthRes =userRepository.Registeration(userAddTDO);
            if (AuthRes.Success)
            {
                return Ok(AuthRes);
            }
            error.LoadError(AuthRes.ErrorCode);
            ModelState.AddModelError("UserName", error.ErrorMessage);
            return ValidationProblem();

        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginTDO userLoginTDO)
        {
            var AuthRes = userRepository.UserLogin(userLoginTDO);
            if (AuthRes.Success)
            {
                return Ok(AuthRes);
            }
            error.LoadError(AuthRes.ErrorCode);
            ModelState.AddModelError(error.ErrorProp, error.ErrorMessage);
            return ValidationProblem();

        }

    }
}
