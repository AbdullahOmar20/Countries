// using System;
// using Entites;
// using ServiceContracts.DTO;
// using ServiceContracts;
// using Services;
// using ServiceContracts.Enums;

// namespace ProjectTest
// {
//     public class PersonServiceTest
//     {
//         private readonly IPersonService _personService;
//         private readonly ICountryService _countryService;
//         public PersonServiceTest()
//         {
//             _personService = new PersonService();
//             _countryService = new CountryService();
//         }
//         #region AddPerson
//         [Fact]
//         public void AddPerson_NullPerson()
//         {
//             //Arrange
//             PersonAddRequest? request = null;
//             //Assert
//             Assert.Throws<ArgumentNullException>(() =>
//             {
//                 //Act
//                 _personService.AddPerson(request);
//             });
//         }
//         [Fact]
//         public void AddPerson_PersonNameIsNull()
//         {
//             //Arrange
//             PersonAddRequest request = new PersonAddRequest() { Name = null };
//             Assert.Throws<ArgumentException>(() =>
//             {
//                 _personService.AddPerson(request);
//             });
//         }
//         public void AddPerson_ProperPerson()
//         {
//             //Arrange
//             PersonAddRequest personRequest = new PersonAddRequest()
//             {
//                 Name = "abdullah",
//                 Email = "abdullahamer323",
//                 Address = "tatai",
//                 CountryId = Guid.NewGuid(),
//                 Gender = GenderOption.Male
//             };
//             //Act
//             PersonResponse response = _personService.AddPerson(personRequest);
//             List<PersonResponse> list_of_person = _personService.GetAllPersons();
//             //Assert
//             Assert.True(response.Id != Guid.Empty);
//             Assert.Contains(response, list_of_person);
//         }
//         #endregion
//         #region GetAllPersons
//         [Fact]
//         public void GetAllPersons_EmptyList()
//         {
//             //Act
//             List<PersonResponse> personResponses = _personService.GetAllPersons();
//             //Assert
//             Assert.Empty(personResponses);
//         }
//         public void GetAllPersons_ProperList()
//         {
//             //Arrange
//             CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Gaza" };
//             CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Palestine" };
//             CountryResponse countryResponse1 = _countryService.Addcountry(countryAddRequest1);
//             CountryResponse countryResponse2 = _countryService.Addcountry(countryAddRequest2);
//             PersonAddRequest person1 = new PersonAddRequest() { Name = "abdu", Email = "abdu@gmail.com", CountryId = countryResponse1.CountryId };
//             PersonAddRequest person2 = new PersonAddRequest() { Name = "abd", Email = "abd@gmail.com", CountryId = countryResponse2.CountryId };
//             List<PersonAddRequest> PersonAddList = new List<PersonAddRequest>() { person1, person2 };
//             List<PersonResponse> personResponses = _personService.GetAllPersons();
//             foreach(PersonAddRequest person in PersonAddList)
//             {
//                 PersonResponse personResponse = _personService.AddPerson(person1);
//                 personResponses.Add(personResponse);
//             }
//             //Act
//             List<PersonResponse> PersonListFromGet = _personService.GetAllPersons();
//             //Assert
//             foreach (PersonResponse person in personResponses)
//             {
//                 Assert.Contains(person, PersonListFromGet);
//             }
//         }
//         #endregion

//         #region getPersonById
//         [Fact]
//         public void GetPersonById_NullPerson()
//         {
//             //Arrange
//             Guid? personid = null;
//             //Act
//             PersonResponse? personResponse = _personService.GetPersonById(personid);
//             //Assert
//             Assert.Null(personResponse);
//         }
//         [Fact]
//         public void GetPersonById_ProperPerson()
//         {
//             //Arrange
//             CountryAddRequest countryAddRequest = new CountryAddRequest() { CountryName = "Gaza" };
//             CountryResponse countryresponse = _countryService.Addcountry(countryAddRequest);
//             //Act
//             PersonAddRequest personrequest = new PersonAddRequest() { Name = "abdu", Email = "abdu@gmail.com", CountryId = countryresponse.CountryId };
//             PersonResponse personresponse = _personService.AddPerson(personrequest);
//             PersonResponse? response_ById = _personService.GetPersonById(personresponse.Id);
//             //Assert
//             Assert.Equal(personresponse, response_ById);

//         }
//         #endregion

//         #region GetFilteredPersons
//         [Fact]
//         //if search text is empty and it searched with PersonName it should return all persons
//         public void GetFilteredPersons_EmptySearchText()
//         {
//             //Arrange
//             CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Gaza" };
//             CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Palestine" };
//             CountryResponse countryResponse1 = _countryService.Addcountry(countryAddRequest1);
//             CountryResponse countryResponse2 = _countryService.Addcountry(countryAddRequest2);
//             PersonAddRequest person1 = new PersonAddRequest() { Name = "abdu", Email = "abdu@gmail.com", CountryId = countryResponse1.CountryId };
//             PersonAddRequest person2 = new PersonAddRequest() { Name = "abd", Email = "abd@gmail.com", CountryId = countryResponse2.CountryId };
//             List<PersonAddRequest> PersonAddList = new List<PersonAddRequest>() { person1, person2 };
//             List<PersonResponse> personResponses = _personService.GetAllPersons();
//             foreach (PersonAddRequest person in PersonAddList)
//             {
//                 PersonResponse personResponse = _personService.AddPerson(person1);
//                 personResponses.Add(personResponse);
//             }
//             //Act
//             List<PersonResponse> PersonListFromSearch = _personService.GetFilteredPersons(nameof(Person.Name),"");
//             //Assert
//             foreach (PersonResponse person in personResponses)
//             {
//                 Assert.Contains(person, PersonListFromSearch);
//             }
//         }
//         [Fact]
//         //Add few persons and search based on person name with search string it should return person object with specific name
//         public void GetFilteredPersons_SearchByPersonName()
//         {
//             //Arrange
//             CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Gaza" };
//             CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Palestine" };
//             CountryResponse countryResponse1 = _countryService.Addcountry(countryAddRequest1);
//             CountryResponse countryResponse2 = _countryService.Addcountry(countryAddRequest2);
//             PersonAddRequest person1 = new PersonAddRequest() { Name = "abdu", Email = "abdu@gmail.com", CountryId = countryResponse1.CountryId };
//             PersonAddRequest person2 = new PersonAddRequest() { Name = "abd", Email = "abd@gmail.com", CountryId = countryResponse2.CountryId };
//             List<PersonAddRequest> PersonAddList = new List<PersonAddRequest>() { person1, person2 };
//             List<PersonResponse> personResponses = _personService.GetAllPersons();
//             foreach (PersonAddRequest person in PersonAddList)
//             {
//                 PersonResponse personResponse = _personService.AddPerson(person1);
//                 personResponses.Add(personResponse);
//             }
//             //Act
//             List<PersonResponse> PersonListFromGet = _personService.GetFilteredPersons(nameof(Person.Name),"ma");
//             //Assert
//             foreach (PersonResponse person in personResponses)
//             {
//                 if(person.Name != null) 
//                 {
//                 if(person.Name.Contains("ma",StringComparison.OrdinalIgnoreCase))
//                 {
//                     Assert.Contains(person, PersonListFromGet); 

//                 }

//                 }
//             }
//         }
//         #endregion

//         #region GetSortedPersons
//         [Fact]
//         //when we sort based on the name ascendingly it should return list of persons sorted by name asc
//         public void GetSortedResponse()
//         {
//             //Arrange
//             CountryAddRequest countryAddRequest1 = new CountryAddRequest() { CountryName = "Gaza" };
//             CountryAddRequest countryAddRequest2 = new CountryAddRequest() { CountryName = "Palestine" };
//             CountryResponse countryResponse1 = _countryService.Addcountry(countryAddRequest1);
//             CountryResponse countryResponse2 = _countryService.Addcountry(countryAddRequest2);
//             PersonAddRequest person1 = new PersonAddRequest() { Name = "abdu", Email = "abdu@gmail.com", CountryId = countryResponse1.CountryId };
//             PersonAddRequest person2 = new PersonAddRequest() { Name = "abd", Email = "abd@gmail.com", CountryId = countryResponse2.CountryId };
//             List<PersonAddRequest> PersonAddList = new List<PersonAddRequest>() { person1, person2 };
//             List<PersonResponse> personResponses = _personService.GetAllPersons();
//             foreach (PersonAddRequest person in PersonAddList)
//             {
//                 PersonResponse personResponse = _personService.AddPerson(person1);
//                 personResponses.Add(personResponse);
//             }
//             List<PersonResponse> allpersons = _personService.GetAllPersons();
//             //Act

//             List<PersonResponse> PersonListFromGet = _personService.GetSortedPersons(allpersons ,nameof(Person.Name), SortOrderOption.ASC);
//             personResponses = personResponses.OrderBy(temp=>temp.Name).ToList();
//             //Assert
//             for(int i = 0; i< personResponses.Count; i++)
//             {
//                 Assert.Equal(allpersons[i], personResponses[i]);
//             }
//         }
//         #endregion
//     }
// }
     
