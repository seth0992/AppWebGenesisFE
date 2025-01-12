using AppWebGenesisFE.Models.Common;
using AppWebGenesisFE.Models.Entities.Customer;
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
    public class TenantModel : BaseEntity
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La identificación es requerida")]
        [MaxLength(50, ErrorMessage = "La identificación no puede exceder los 50 caracteres")]
        public string Identification { get; set; } = string.Empty;

        //[Column(TypeName = "datetime")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime CreatedAt { get; set; }
        //[Column(TypeName = "datetime")]
        //public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserModel>? Users { get; set; }
    }
}
