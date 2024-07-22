using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entites
{
    public class CountriesDbContext : DbContext
    {
        public CountriesDbContext(DbContextOptions<CountriesDbContext> options) : base(options)
        {
        }
        public DbSet<Country> Countries { get; set;}
        public DbSet<Person> Persons {get; set;}
        
    }
}