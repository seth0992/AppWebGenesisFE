using AppWebGenesisFE.Models.Entities.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Entities
{
    public class RefreshTokenModel
    {
        public int ID { get; set; }
        public long UserID { get; set; }
        public string RefreshToken { get; set; }
        public virtual UserModel User { get; set; }

    }
}
