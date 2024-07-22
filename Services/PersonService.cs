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
        private readonly CountriesDbContext _context;
        private readonly ICountryService _countryService;
        public PersonService(CountriesDbContext context, ICountryService countryService)
        {
            _context = context;
            _countryService = countryService;
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
        private bool saving()
        {
            try{
                _context.SaveChanges();
            }
            catch (Exception ex)
            {return false;}

            return true;
        }
        public PersonResponse AddPerson(PersonAddRequest? personAddrequest)
        {

            //checking if the request is not null
            if(personAddrequest == null)
                throw new ArgumentNullException(nameof(personAddrequest));
            //Model Validation
            //ValidationHelper.ModelValidation(personAddrequest);

            Person person = personAddrequest.ToPerson();
            //assigning a Guid to person id 
            person.Id = Guid.NewGuid();
            _context.Persons.Add(person);
            _context.SaveChangesAsync();
            //saving();
            //return an object of PersonResponse type
            return ConvertPersonToResponse(person);
        }

        public List<PersonResponse> GetAllPersons()
        {
            return _context.Persons.ToList().Select(p=>p.ToPersonResponse()).ToList();
        }

        public PersonResponse? GetPersonById(Guid? id)
        {
            if (id == null) return null;
            Person? person = _context.Persons.FirstOrDefault(p=>p.Id == id);
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

        public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
        {
             if (personUpdateRequest == null)
            throw new ArgumentNullException(nameof(Person));

            //validation
            ValidationHelper.ModelValidation(personUpdateRequest);

            //get matching person object to update
            Person? matchingPerson = _context.Persons.FirstOrDefault(temp => temp.Id == personUpdateRequest.Id);
            if (matchingPerson == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            matchingPerson.Name = personUpdateRequest.Name;
            matchingPerson.Email = personUpdateRequest.Email;
            matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
            matchingPerson.Gender = personUpdateRequest.Gender.ToString();
            matchingPerson.CountryId = personUpdateRequest.CountryId;
            matchingPerson.Address = personUpdateRequest.Address;
            matchingPerson.ReceiveNewsLetter = personUpdateRequest.ReceiveNewsLetter;
            _context.Persons.Update(matchingPerson);
            saving();
            return matchingPerson.ToPersonResponse();
        }

        public bool DeletePerson(Guid? PersonId)
        {
            if (PersonId == null)
            {
                throw new ArgumentNullException(nameof(PersonId));
            }

            Person? person = _context.Persons.FirstOrDefault(temp => temp.Id == PersonId);
            if (person == null)
                return false;

            _context.Persons.Remove(person);
            saving();

            return true;
        }
    }
}
