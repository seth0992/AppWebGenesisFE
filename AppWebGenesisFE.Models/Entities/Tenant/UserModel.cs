using AppWebGenesisFE.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Entities.Tenant
{
    public class UserModel : BaseEntity
    {
        [Required(ErrorMessage = "El ID del tenant es requerido")]
        public long TenantId { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [MaxLength(255, ErrorMessage = "El correo electrónico no puede exceder los 255 caracteres")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es requerido")]
        [MaxLength(100, ErrorMessage = "El rol no puede exceder los 100 caracteres")]
        public string Rol { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido")]
        [MaxLength(100, ErrorMessage = "El apellido no puede exceder los 100 caracteres")]
        public string LastName { get; set; } = string.Empty;

        public virtual ICollection<UserRoleModel> UserRoles { get; set; } = new List<UserRoleModel>();

        public UserModel()
        {
            UserRoles = new List<UserRoleModel>();
        }

        //[Column(TypeName = "datetime")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime CreatedAt { get; set; }
        //[Column(TypeName = "datetime")]
        //public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public virtual TenantModel? Tenant { get; set; }
    }
}
