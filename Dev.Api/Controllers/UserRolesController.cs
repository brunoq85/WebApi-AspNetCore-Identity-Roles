using Dev.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("associar-usuario-funcao")]
        public async Task<ActionResult> ConnectUserRoleAsync(UserRolesViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            var result = await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
                return Ok($"O usuário {user.Email} foi associado à funçao {role.Name}");

            foreach (var erro in result.Errors)
            {
                return BadRequest(erro.Description);
            }

            return BadRequest(model);
        }

        [HttpPost("desassociar-usuario-funcao")]
        public async Task<ActionResult> DisconnectUserRoleAsync(UserRolesViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(model.UserId);
            var role = await _roleManager.FindByIdAsync(model.RoleId);

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
                return Ok($"O usuário {user.Email} foi desassociado à funçao {role.Name}");

            foreach (var erro in result.Errors)
            {
                return BadRequest(erro.Description);
            }

            return BadRequest(model);
        }
    }
}
