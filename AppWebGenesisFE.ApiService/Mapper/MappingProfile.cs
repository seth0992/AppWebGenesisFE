using AppWebGenesisFE.Models.Entities.Customer;
using AppWebGenesisFE.Models.Models;
using AutoMapper;

namespace AppWebGenesisFE.ApiService.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDTO, CustomerModel>()
                .ForMember(dest => dest.IdentificationType, opt => opt.Ignore())
                .ForMember(dest => dest.District, opt => opt.Ignore());

            CreateMap<CustomerModel, CustomerDTO>();
        }
    }
    
  
}
