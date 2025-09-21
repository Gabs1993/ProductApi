using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Users
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O Email não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo PassWord é obrigatório.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string PassWord { get; set; } = string.Empty;

        public string Role { get; set; } = "user";
    }
}
