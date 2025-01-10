using AppWebGenesisFE.Models.Entities.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Entities.Tenant
{
    public class TenantModel
    {
        public long ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;  
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }


        [JsonIgnore]
        public virtual ICollection<UserModel>? Users { get; set; }
    }
}
