using HumanResource.Application.Models.VMs.AddressVM;

namespace HumanResource.Application.Services.AddressService
{
    public interface IAddressService
    {
        Task<List<CountryVM>> GetCountries();
        Task<List<CityVM>> GetCities();
        Task<List<CityVM>> GetCities(int countryId);
        Task<List<DistrictVM>> GetDistricts(int cityId);
        Task<List<DistrictVM>> GetDistricts();
    }
}
