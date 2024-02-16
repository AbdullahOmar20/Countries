using Entites;
using ServiceContracts.Enums;
using System;


namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class is used as return type for IPersonService methods
    /// </summary>
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set;}
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public double? Age { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? CountryName {  get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetter { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != typeof(PersonResponse))
            {
                return false;
            }
            PersonResponse personcompare = (PersonResponse)obj;
            return this.Id == personcompare.Id && this.Name == personcompare.Name;
        }
        public PersonUpdateRequest ToPersonUpdateRequest()
        {
            return new PersonUpdateRequest()
            {
                Id = Id,
                Name = Name,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Address = Address,
                ReceiveNewsLetter = ReceiveNewsLetter,
                CountryId = CountryId,
                Gender = (GenderOption)Enum.Parse(typeof(GenderOption),Gender, true)
            };
        }
    }

    public static class PersonExtensions
    {
        public static PersonResponse ToPersonResponse(this Person person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name,
                Email = person.Email,
                DateOfBirth = person.DateOfBirth,
                Age = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25): null,
                Gender = person.Gender,
                CountryId = person.CountryId,
                Address = person.Address,
                ReceiveNewsLetter = person.ReceiveNewsLetter
            };
        }
    }
}
