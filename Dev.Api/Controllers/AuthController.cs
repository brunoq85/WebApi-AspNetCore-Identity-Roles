using Dev.Api.Extensions;
using Dev.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Api.Controllers
{
    [Route("api/autenticacao", Name = "Autenticação")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
                 
        private readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager; 
            _appSettings = appSettings.Value;            
        }

        [HttpPost("registrar_usuario")]
        public async Task<ActionResult> RegisterAsync(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var user = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return Ok("Usuário criado com sucesso");

            foreach (var erro in result.Errors)
            {
                return BadRequest(result.Errors);
            }

            return BadRequest(model);
        }

        [HttpPost("acessar_usuario")]
        public async Task<ActionResult> LoginAsync(LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

            if (result.Succeeded) return Ok(GerarToken());

            if (result.IsLockedOut) return BadRequest("Usuário bloqueado por inúmeras tentativas inválidas");

            return BadRequest("Usuário ou senha inválidos");
        }          

        private string GerarToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }


    }
}
