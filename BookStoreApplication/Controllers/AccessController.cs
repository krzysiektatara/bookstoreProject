
using Microsoft.AspNetCore.Mvc;
using BookStoreApplicationAPI.Data.Entities;
using BookStoreApplicationAPI.Data.Models;
using BookStoreApplicationAPI.Data.Dto;
using BookStoreApplicationAPI.DAL.Services;
using BADataAccessLibrary.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Principal;

namespace BookStoreApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccessController (IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Login(VMLogin modelLogin)
        {
            var user = await _userService.GetUserByLoginAsync(modelLogin.Login);
            if (modelLogin.Password == user.Password)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleEnum.ToString())
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
            

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = modelLogin.KeepLoggedIn
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity), properties);
                return Ok(modelLogin);
            }

            return ValidationProblem(detail: "user or password not correct");
        }

    }
}
