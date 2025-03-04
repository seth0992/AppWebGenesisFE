﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Models.Auth
{
    public class UserSession
    {
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public long TenantId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
