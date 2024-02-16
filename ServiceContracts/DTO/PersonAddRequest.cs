using System;
using ServiceContracts.Enums;
using Entites;
using System.ComponentModel.DataAnnotations;
namespace ServiceContracts.DTO
{
    /// <summary>
    /// Acts as DTO to insert a new person 
    /// </summary>
    public class PersonAddRequest
    {
        [Required(ErrorMessage ="The Name field can't be blank")]   
        public string? Name { get; set; }
        [Required(ErrorMessage = "The Email field can't be blank")]
        [EmailAddress(ErrorMessage ="The Email should be in proper format")]
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public GenderOption? Gender { get; set; }
        public Guid? CountryId { get; set; }
        public string? Address { get; set; }
        public bool? ReceiveNewsLetter { get; set; }
        
        public Person ToPerson()
        {
            return new Person
            {
                Name = Name,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender.ToString(),
                CountryId = CountryId,
                Address = Address,
                ReceiveNewsLetter = ReceiveNewsLetter,
            };
        }
    }
}
