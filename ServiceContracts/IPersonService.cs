using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;


namespace ServiceContracts
{
    public interface IPersonService
    {
        /// <summary>
        /// Adds person object to list of persons 
        /// </summary>
        /// <param name="personAddrequest">Person object to be added</param>
        /// <returns>return Person object after addding it </returns>
        PersonResponse AddPerson(PersonAddRequest? personAddrequest);
        /// <summary>
        /// Returns all existing person 
        /// </summary>
        /// <returns>returns list of CountryResponse</returns>
        List<PersonResponse> GetAllPersons();
        /// <summary>
        /// it returns Person based on ID
        /// </summary>
        /// <param name="id">Person GUID to search</param>
        /// <returns>Matching Person as PersonResponse type</returns>
        PersonResponse GetPersonById(Guid? id);
        /// <summary>
        /// returns every person object that match specific search field and search string 
        /// </summary>
        /// <param name="searchby">search field</param>
        /// <param name="searchbody">search string</param>
        /// <returns></returns>
        List<PersonResponse> GetFilteredPersons(string searchby, string searchbody);
        /// <summary>
        /// returns sorted list of PersonResponse which is sorted by specific property and order
        /// </summary>
        /// <param name="allpersons">the list that will be sorted</param>
        /// <param name="sortby">the sort property</param>
        /// <param name="sortorder">the sort order</param>
        /// <returns></returns>
        List<PersonResponse> GetSortedPersons(List<PersonResponse> allpersons, string sortby, SortOrderOption sortorder);

        PersonResponse UpdatePerson (PersonUpdateRequest? personUpdaterequest);
        bool DeletePerson(Guid? PersonId);
    }
}
