using AppWebGenesisFE.Models.Entities.Tenant;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.ServiceDefaults
{
    // ITenantService.cs
    public interface ITenantService
    {
        long GetCurrentTenantId();
        Task<TenantModel> GetCurrentTenant();
        void SetCurrentTenant(int tenantId);
    }

    public class TenantService : ITenantService
    {
        private int? _currentTenantId;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<TenantModel> GetCurrentTenant()
        {
            throw new NotImplementedException();
        }

        public long GetCurrentTenantId()
        {
            // Para el login y registro, permitimos que no haya tenant
            if (_httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated != true)
            {
                return 0; // O retornar null si prefieres manejar ese caso
            }

            var tenantClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("TenantId");
            if (tenantClaim == null)
                throw new UnauthorizedAccessException("No tenant specified");

            return long.Parse(tenantClaim.Value);
        }
        public void SetCurrentTenant(int tenantId)
        {
            throw new NotImplementedException();
        }
    }
}
