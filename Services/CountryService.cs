
using Entites;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly CountriesDbContext _context;
        public CountryService(CountriesDbContext context)
        {
            _context = context;
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
            if(_context.Countries.Where(c => c.Name ==  countryAddrequest.CountryName).Count()>0)
            {
                throw new ArgumentException("the given name is already exists");
            }

            Country country = countryAddrequest.ToCountry();
            country.Id = Guid.NewGuid();
            _context.Countries.Add(country);
            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            var countries = _context.Countries.ToList().Select(o=>o.ToCountryResponse());
            return countries.ToList();
           
        }

        public CountryResponse GetCountryById(Guid? id)
        {
            if (id == null) return null;
            Country? response =  _context.Countries.FirstOrDefault(c=>c.Id == id);
            if (response == null)
                return null;
            return response.ToCountryResponse();
        }
    }
}
