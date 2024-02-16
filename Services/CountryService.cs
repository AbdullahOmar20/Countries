
using Entites;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly List<Country> _countries;
        public CountryService()
        {
            _countries = new List<Country>();
        }
        public CountryResponse Addcountry(CountryAddRequest? countryAddrequest)
        {
            //validation if the countryrequest can't be null 
            if (countryAddrequest == null)
                throw new ArgumentNullException(nameof(countryAddrequest));
            //validation if the country name is null 
            if(countryAddrequest.CountryName == null)
                throw new ArgumentException(nameof(countryAddrequest.CountryName));
            //validation if country name is duplicated 
            if(_countries.Where(c => c.Name ==  countryAddrequest.CountryName).Count()>0)
            {
                throw new ArgumentException("the given name is already exists");
            }

            Country country = countryAddrequest.ToCountry();
            country.Id = Guid.NewGuid();
            _countries.Add(country);
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public CountryResponse GetCountryById(Guid? id)
        {
            if (id == null) return null;
            Country? response =  _countries.FirstOrDefault(c=>c.Id == id);
            if (response == null)
                return null;
            return response.ToCountryResponse();
        }
    }
}
