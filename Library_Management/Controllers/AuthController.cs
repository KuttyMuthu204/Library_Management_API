using Library_Management.DBContext;
using Library_Management.Models;
using Library_Management.Services;
using Library_Management.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token if the credentials are valid.
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(Routes.UserLogin)]
        public async Task<ActionResult> Login([FromBody][Required] LoginModel loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(await _authService.GetJWTToken(loginModel));
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Unexpected error occrred while getting JWT Token for the user: {loginModel.Username}");
            }
        }
    }
}
