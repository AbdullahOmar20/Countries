using Entites;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class PersonUpdateRequest
    {
        /// <summary>
        /// Represents the DTO class that contains person details to be updated
        /// </summary>
        [Required(ErrorMessage ="Person ID can't be blank")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Name field can't be blank")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "The Email field can't be blank")]
        [EmailAddress(ErrorMessage = "The Email should be in proper format")]
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
                Id = Id,
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
