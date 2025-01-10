using AppWebGenesisFE.Models.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Entities.Customer
{
    public class CustomerModel
    {
        public long ID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CommercialName { get; set; } = string.Empty;
        public string Identification { get; set; } = string.Empty;
        public string IdentificationTypeId { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Neighborhood { get; set; } = string.Empty;
        public int DistrictID { get; set; }
        public long TenantId { get; set; }



        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IdentificationTypeModel? IdentificationType { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DistrictModel? District { get; set; }

    }
}
