using System.ComponentModel.DataAnnotations;

namespace Dev.Api.ViewModels
{
    public class UserRolesViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string RoleId { get; set; }
    }
}
