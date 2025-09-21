using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.UserDTO
{
    public class UpdateUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
    }
}
