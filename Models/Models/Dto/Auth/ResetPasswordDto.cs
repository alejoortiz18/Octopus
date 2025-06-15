using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dto.Auth
{
    public class ResetPasswordDto
    {
        public bool IsGenerated { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
    }
}
