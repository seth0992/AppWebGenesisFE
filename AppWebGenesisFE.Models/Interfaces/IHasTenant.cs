using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Interfaces
{
    public interface IHasTenant
    {
        long TenantId { get; set; }
    }
}
