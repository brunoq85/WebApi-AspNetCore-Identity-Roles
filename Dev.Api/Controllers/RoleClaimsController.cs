using Dev.Api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dev.Api.Controllers
{
    [Route("api/associa-funcao-afirmacao", Name="Associa função/afirmação")]
    [ApiController]
    public class RoleClaimsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleClaimsController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> CreateRoleAsync(RoleClaimsViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claim = new Claim(model.ClaimType, model.ClaimValue);

            if (role == null) return BadRequest("A função informada não existe");            

            var result = await _roleManager.AddClaimAsync(role, claim);

            if (result.Succeeded)
                return Ok($"A função {role.Name} foi associada a afirmação {claim.Type}");

            foreach (var erro in result.Errors)
            {
                return BadRequest(erro.Description);
            }

            return BadRequest(model);
        }

        // TODO: Atualizar role claim
        //[HttpPost("atualizar/{id:guid}")]
        //public async Task<ActionResult> UpdateRoleAsync(Guid id, RoleClaimsViewModel model)
        //{
        //    if (id.ToString() == model.Id) return BadRequest("O id informado não é o mesmo que foi passado na query");

        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var role = await _roleManager.FindByIdAsync(model.RoleId);
        //    var claim = new Claim(model.ClaimType, model.ClaimValue);

        //    if (role == null) return BadRequest("A função informada não existe");

        //    role.Id = model.Id;

        //    var result = await _roleManager.AddClaimAsync(role, claim);

        //    if (result.Succeeded)
        //        return Ok($"A função {role.Name} foi associada a afirmação {claim.Type}");

        //    foreach (var erro in result.Errors)
        //    {
        //        return BadRequest(erro.Description);
        //    }

        //    return BadRequest(model);
        //}

        // TODO: Exluir role claim
        //[HttpPost("excluir/{id:guid}")]
        //public async Task<ActionResult> DeleteRoleAsync(Guid id)
        //{
        //    if (!ModelState.IsValid) return BadRequest(ModelState);

        //    var role = await _roleManager.FindByIdAsync(id.ToString());

        //    if (role == null) return BadRequest("A função informada não existe");

        //    var result = await _roleManager.RemoveClaimAsync(role, claim);

        //    if (result.Succeeded)
        //        return Ok($"A função {role.Name} foi associada a afirmação {claim.Type}");

        //    foreach (var erro in result.Errors)
        //    {
        //        return BadRequest(erro.Description);
        //    }

        //    return BadRequest(model);
        //}
    }
}
