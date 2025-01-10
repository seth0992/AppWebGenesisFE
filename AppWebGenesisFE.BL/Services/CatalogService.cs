using AppWebGenesisFE.BL.Repositories;
using AppWebGenesisFE.Models.Entities.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.BL.Services
{
    public interface ICatalogService
    {
        Task<List<IdentificationTypeModel>> GetIdentificationTypes();
        Task<List<ProvinceModel>> GetProvinces();
        Task<List<CantonModel>> GetCantons();
        Task<List<DistrictModel>> GetDistricts();

        //Task<List<CustomerModel>> GetCustomers();
        Task<List<CantonModel>> GetCantonsOfProvinces(int idProvince);
        Task<List<DistrictModel>> GetCantonsOfDistricts(int idCanton);
    }
    public class CatalogService(ICatalogRepository catalogRepository) : ICatalogService
    {
        public Task<List<CantonModel>> GetCantons()
        {
            return catalogRepository.GetCantons();
        }

        public Task<List<DistrictModel>> GetCantonsOfDistricts(int idCanton)
        {
            return catalogRepository.GetCantonsOfDistricts(idCanton);
        }

        public Task<List<CantonModel>> GetCantonsOfProvinces(int idProvince)
        {
            return catalogRepository.GetCantonsOfProvinces(idProvince);
        }

        public Task<List<DistrictModel>> GetDistricts()
        {
            return catalogRepository.GetDistricts();
        }

        public Task<List<IdentificationTypeModel>> GetIdentificationTypes()
        {
            return catalogRepository.GetIdentificationTypes();
        }

        public Task<List<ProvinceModel>> GetProvinces()
        {
            return catalogRepository.GetProvinces();
        }
    }
}
