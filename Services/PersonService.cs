using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.CompilerServices;
using Entites;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;


namespace Services
{
    public class PersonService : IPersonService
    {
        private readonly List<Person> _person;
        private readonly ICountryService _countryService;
        public PersonService()
        {
            _person = new List<Person>();
            _countryService = new CountryService();
        }
        //converting from person type to PersonResponse type 
        //from the data store to data access
        private PersonResponse ConvertPersonToResponse(Person person)
        {
            PersonResponse personResponse = person.ToPersonResponse();
            //getting country name using a method from country service that return country by its id 
            personResponse.CountryName = _countryService.GetCountryById(person.CountryId)?.CountryName;
            return personResponse;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddrequest)
        {

            //checking if the request is not null
            if(personAddrequest == null)
                throw new ArgumentNullException(nameof(personAddrequest));
            //Model Validation
            ValidationHelper.ModelValidation(personAddrequest);

            Person person = personAddrequest.ToPerson();
            //assigning a Guid to person id 
            person.Id = Guid.NewGuid();
            _person.Add(person);
            //return an object of PersonResponse type
            return ConvertPersonToResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _person.Select(p=>p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonById(Guid? id)
        {
            if (id == null) return null;
            Person? person = _person.FirstOrDefault(p=>p.Id == id);
            if (person == null) return null;
            return person.ToPersonResponse();
        }

        public List<PersonResponse> GetFilteredPersons(string searchby, string searchbody)
        {
            List<PersonResponse> allpersons = GetAllPersons();
            List<PersonResponse> MatchingPerosns = allpersons;
            if (string.IsNullOrEmpty(searchby) && string.IsNullOrEmpty(searchbody))
                return MatchingPerosns;
            switch (searchby)
            {
                case nameof(Person.Name):
                    MatchingPerosns = allpersons.Where(temp => (!string.IsNullOrEmpty(temp.Name)) ? temp.Name.Contains(searchbody,StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Email):
                    MatchingPerosns = allpersons.Where(temp => (!string.IsNullOrEmpty(temp.Email)) ? temp.Email.Contains(searchbody, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.DateOfBirth):
                    MatchingPerosns = allpersons.Where(temp => (temp.DateOfBirth != null) ? temp.DateOfBirth.Value.ToString("dd MM yyyy").Contains(searchbody, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.Gender):
                    MatchingPerosns = allpersons.Where(temp => (!string.IsNullOrEmpty(temp.Gender)) ? temp.Gender.Contains(searchbody, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                case nameof(Person.CountryId):
                    MatchingPerosns = allpersons.Where(temp => (!string.IsNullOrEmpty(temp.CountryName)) ? temp.CountryName.Contains(searchbody, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;
                default:MatchingPerosns = allpersons; break;

            }
            return MatchingPerosns;
        }

        public List<PersonResponse> GetSortedPersons(List<PersonResponse> allpersons, string sortby, SortOrderOption sortorder)
        {
            if(string.IsNullOrEmpty(sortby)) return allpersons;
            List<PersonResponse> SortedPersons = (sortby, sortorder) switch
            {
                (nameof(PersonResponse.Name), SortOrderOption.ASC) => allpersons.OrderBy(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.Name), SortOrderOption.DESC) => allpersons.OrderByDescending(temp => temp.Name, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponse.DateOfBirth), SortOrderOption.ASC) => allpersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                (nameof(PersonResponse.DateOfBirth), SortOrderOption.DESC) => allpersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
                _ => allpersons
            } ;
            return SortedPersons;
        }
    }
}
