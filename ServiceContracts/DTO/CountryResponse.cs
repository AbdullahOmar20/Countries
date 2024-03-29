﻿using System;
using System.Collections.Generic;
using Entites;

namespace ServiceContracts.DTO
{
    /// <summary>
    /// DTO class is used as return type for ICountryService methods
    /// </summary>
    public class CountryResponse
    {
        public Guid CountryId { get; set; }
        public string? CountryName { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }
            if(obj.GetType() != typeof(CountryResponse))
            {
                return false;
            }
            CountryResponse countrycompare = (CountryResponse)obj;
            return this.CountryId == countrycompare.CountryId && this.CountryName == countrycompare.CountryName;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse
            {
                CountryId = country.Id,
                CountryName = country.Name,
            };
        }
    }
}
