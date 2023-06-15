using HumanResource.Application.Models.VMs.AddressVM;
using HumanResource.Domain.Repositories;
using HumanResource.Domain.Repositries;

namespace HumanResource.Application.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IDistrictRepository _districtRepository;

        public AddressService(ICityRepository cityRepository, IDistrictRepository districtRepository, ICountryRepository countryRepository)
        {
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
            _countryRepository = countryRepository;
        }

        public async Task<List<CityVM>> GetCities()
        {
            List<CityVM> Cities = await _cityRepository.GetFilteredList(
                select: x=> new CityVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: null,
                orderby: x=>x.OrderBy(x=>x.Name)
                );
            return Cities;
        }

        public async Task<List<CityVM>> GetCities(int countryId)
        {
            List<CityVM> Cities = await _cityRepository.GetFilteredList(
               select: x => new CityVM
               {
                   Id = x.Id,
                   Name = x.Name
               },
               where: x => x.CountryId == countryId,
               orderby: x => x.OrderBy(x => x.Name)
               );
            return Cities;
        }

        public async Task<List<CountryVM>> GetCountries()
        {
            List<CountryVM> countries = await _countryRepository.GetFilteredList(
               select: x => new CountryVM
               {
                   Id = x.Id,
                   Name = x.Name
               },
               where: null,
               orderby: x => x.OrderBy(x => x.Name)
               );
            return countries;
        }

        public async Task<List<DistrictVM>> GetDistricts()
        {
            List<DistrictVM> districts = await _districtRepository.GetFilteredList(
                    select: x => new DistrictVM
                    {
                        Id = x.Id,
                        Name = x.Name

                    },
                    where: null,
                    orderby: x => x.OrderBy(x => x.Name)
                    );

            return districts.ToList();
        }

        public async Task<List<DistrictVM>> GetDistricts(int cityId)
        {
            List<DistrictVM> districts = await _districtRepository.GetFilteredList(
                    select: x => new DistrictVM
                    {
                        Id = x.Id,
                        Name = x.Name
                        
                    },
                    where: x=>x.CityId == cityId,
                    orderby: x => x.OrderBy(x => x.Name),
                    include: null
                    );

            return districts.ToList();
        }
    }
}
