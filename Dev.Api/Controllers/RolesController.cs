using Dev.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.Api.Controllers
{
    
    [Route("api/funcao", Name = "Função")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("listar-todos")]
        public ActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles;

            if (roles == null) return BadRequest("Não existem funções registradas");

            return Ok(roles.Select(r => new { Id = r.Id, Name = r.Name }));
        }

        [HttpGet("listar-por_id/{id:guid}")]
        public async Task<ActionResult> GetByIdRole(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null) return BadRequest($"Não existe função registrado com o id {id}");

            return Ok(new { Id = role.Id, Name = role.Name});
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> CreateRoleAsync(RoleViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var role = new IdentityRole
            {
                Name = model.Name
            };

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return Ok("Função criada com sucesso");

            foreach (var erro in result.Errors)
            {
                return BadRequest(erro.Description);
            }

            return BadRequest(model);
        }

        [HttpPost("atualizar/{id:guid}")]
        public async Task<ActionResult> UpdateRoleAsync(Guid id, RoleViewModel model)
        {
            if (id.ToString() != model.Id) return BadRequest("O id informado não é o mesmo que foi passado na query");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var roleUpdated = await _roleManager.FindByNameAsync(model.Name);

            roleUpdated.Name = model.Name;

            var result = await _roleManager.UpdateAsync(roleUpdated);

            if (result.Succeeded)
                return Ok("Função atualizada com sucesso");

            foreach (var erro in result.Errors)
            {
                return BadRequest(erro.Description);
            }

            return BadRequest(model);
        }

        [HttpPost("excluir/{id:guid}")]
        public async Task<ActionResult> DeleteRoleAsync(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return Ok("Função excluída com sucesso");

            foreach (var erro in result.Errors)
            {
                return BadRequest(erro.Description);
            }

            return BadRequest(role);
        }
    }
}
