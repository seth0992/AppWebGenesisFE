using AppWebGenesisFE.Models.Entities.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Entities.Tenant
{
    public class TenantModel
    {
        public long ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;  
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<UserModel>? Users { get; set; }
    }
}
