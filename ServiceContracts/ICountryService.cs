using Entites;
using ServiceContracts.DTO;
namespace ServiceContracts
{
    /// <summary>
    /// represents buisness logic for manipulating country entity 
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Adds country object to list of countries 
        /// </summary>
        /// <param name="countryAddrequest">Country object to be added</param>
        /// <returns>return country object after addding it </returns>
        CountryResponse Addcountry(CountryAddRequest? countryAddrequest);
        /// <summary>
        /// It returns all countries
        /// </summary>
        /// <returns>returns all countries from the list as a list of CountryResponse </returns>
        List<CountryResponse> GetAllCountries();
        /// <summary>
        /// it returns country based on ID
        /// </summary>
        /// <param name="id">Coutnry GUID to search</param>
        /// <returns>Matching country as CountryResponse type</returns>
        CountryResponse? GetCountryById(Guid? id);
    }
}
