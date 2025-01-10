namespace AppWebGenesisFE.Models.Models
{
    public class CustomerDTO
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

    }
    
}
