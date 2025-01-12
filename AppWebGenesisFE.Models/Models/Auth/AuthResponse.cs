using AppWebGenesisFE.Models.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Models.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public long TokenExpired { get; set; }
        //public DateTime Expiration { get; set; }
        //public UserModel? User { get; set; }
        //public TenantModel? Tenant { get; set; }
    }
}
