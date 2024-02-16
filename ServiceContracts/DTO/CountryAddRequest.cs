using Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
    public class CountryAddRequest
    {
        [Required(ErrorMessage ="Name can't be null")]
        
        public string? CountryName{ get; set; }
        public Country ToCountry()
        {
            return new Country()
            {
                Name = CountryName
            };
        }
    }
}
