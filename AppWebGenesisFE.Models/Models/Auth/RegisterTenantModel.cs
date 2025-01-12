using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Models.Auth
{
    public class RegisterTenantModel
    {
        // Datos de la empresa
        [Required(ErrorMessage = "El nombre de la empresa es requerido")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "La identificación es requerida")]
        public string Identification { get; set; } = string.Empty;

        //[Required(ErrorMessage = "El tipo de identificación es requerido")]
        public bool isActive { get; set; } = true;

        // Datos del usuario administrador
        [Required(ErrorMessage = "El nombre es requerido")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; } 
    }
}
