using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Core.Jwt.Example.Models;
using WebApi.Core.Jwt.Example.Repository.Entities;
using WebApi.Core.Jwt.Example.Services;
using WebApi.Core.Jwt.Example.Util;

namespace WebApi.Core.Jwt.Example.Controllers
{    
    [ApiController]
    [Route("api/v1/Login")]
    public class LoginController : BaseController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;            
        }

        [HttpPost ("SignIn")]
        [ProducesResponseType(typeof(LoginResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignIn(LoginModel model)
        {   
            if (ModelState.IsValid)
            {
                User user = _loginService.SingIn(model.Login, model.Password);
                if (user != null)
                {
                    List<Role> roles = _loginService.GetUserRole(user.IdUser);
                    
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Login.ToString()));                    
                    claims.Add(new Claim(ClaimTypes.GivenName, user.Name.ToString()));                    

                    foreach (var item in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Name));
                    }

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Constants._JWT_KEY));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = Constants._JWT_ISSUER,
                        Audience = Constants._JWT_AUD,
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddHours(Constants._JWT_EXP_HOUR),
                        SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    LoginResponseModel ret = new LoginResponseModel();
                    ret.login = model.Login;
                    ret.token = tokenString;
                    ret.idUser = user.IdUser;
                    ret.expiration = DateTime.UtcNow.AddHours(Constants._JWT_EXP_HOUR);
                    ret.name = user.Name;                                       

                    return Ok(ret);
                }
            }            
            return BadRequest(); //400           
        }

        [HttpPost("SignUp")]
        [ProducesResponseType(typeof(LoginResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
        public ActionResult SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                ResultModel ret = new ResultModel();
                var user = _loginService.SingUp(model);

                if (user == null)
                {
                    ret.Message = "Error: User not inserted";
                    return BadRequest(ret);
                }
                else
                {                    
                    ret.Message = "User inserted";
                    return Ok(ret);
                }                
                
            }
            return BadRequest(); //400           
        }
    }
}