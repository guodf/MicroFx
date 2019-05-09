using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroFx.Identity.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroFx.Identity.Controllers
{
    /// <summary>
    /// 用户认证API
    /// </summary>
    [SwaggerTag("用户认证API")]
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;
        ILogger<TokenController> _logger;
        IConfiguration _configuration;
        /// <summary>
        /// 授权管理
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="userManager"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public TokenController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,ILogger<TokenController> logger,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 生成用户Token
        /// </summary>
        /// <returns>
        ///     {token:"token",Seconds:100}
        /// </returns>
        /// <param name="sign"></param>
        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(typeof(UserTokenDto),200)]
        [ProducesResponseType(typeof(BadRequestResult),400)]
        public async Task<ActionResult<UserTokenDto>> PostAsync([FromBody]SignInDto sign)
        {
            var result = await _signInManager.PasswordSignInAsync(sign.UName, sign.Pwd, false, lockoutOnFailure: true);
            var token = String.Empty;
            
            _logger.LogInformation($"login with UserName:[{sign.UName}]");
            _logger.LogDebug($"login with UserName:[{sign.UName}],Password:[{sign.Pwd}]");
            if (result.Succeeded)
            {
                var dateTime = DateTime.Now.AddDays(1);
                var user=await _userManager.FindByNameAsync(sign.UName);
                HttpContext.User.AddIdentity(new ClaimsIdentity(new List<Claim> {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id)
                }));
                token = GeneratorToken(dateTime);
                var seconds = (dateTime.Ticks - DateTime.UnixEpoch.Ticks) / TimeSpan.TicksPerSecond;
                _logger.LogInformation($"login success!");
                _logger.LogDebug($"token overtime {dateTime}");

                return new UserTokenDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Phone = user.PhoneNumber,
                    Email = user.Email,
                    Token = token,
                    Seconds = (long)TimeSpan.FromDays(1).TotalSeconds
                };
            }
            return BadRequest();            
        }

        /// <summary>
        /// 刷新Token
        /// </summary>
        /// <returns>{token:"token",Seconds:100}</returns>
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Refresh")]
        public ActionResult<UserTokenDto> Post()
        {
            _logger.LogInformation("refresh token!");
            _logger.LogDebug("refresh token!");
            var dateTime = DateTime.Now.AddDays(1);
            var token = GeneratorToken(dateTime);
            var result= new UserTokenDto { Token = token, Seconds = (long)TimeSpan.FromDays(1).TotalSeconds };
            _logger.LogDebug($"new token info:[{result}]");
            _logger.LogInformation("refresh token success!");
            return result;
        }

        private string GeneratorToken(DateTime dateTime)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"));

            var jwtToken = new JwtSecurityToken(
                    issuer: "jwt",
                    audience: "jwt",
                    expires: dateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    claims: HttpContext.User.Claims
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
