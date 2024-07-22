using Countries.Models;
using Entites;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Diagnostics;

namespace Countries.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService person;
        private readonly ICountryService countryService;
        private readonly CountriesDbContext context;

        public HomeController(IPersonService _person, ICountryService _countryService, CountriesDbContext _context) 
        {
            person = _person;
            countryService = _countryService;
            context = _context;
        }
        [HttpGet]
        [Route("/")]
    public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(PersonResponse.Name), SortOrderOption sortOrder = SortOrderOption.ASC)
        {
        
        ViewBag.SearchFields = new Dictionary<string, string>()
        {
            { nameof(PersonResponse.Name), "Person Name" },
            { nameof(PersonResponse.Email), "Email" },
            { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
            { nameof(PersonResponse.Gender), "Gender" },
            { nameof(PersonResponse.Id), "Country" },
            { nameof(PersonResponse.Address), "Address" }
        };
        List<PersonResponse> persons = person.GetFilteredPersons(searchBy, searchString);
        ViewBag.CurrentSearchBy = searchBy;
        ViewBag.CurrentSearchString = searchString;

    
        List<PersonResponse> sortedPersons =  person.GetSortedPersons(persons, sortBy, sortOrder);
        ViewBag.CurrentSortBy = sortBy;
        ViewBag.CurrentSortOrder = sortOrder.ToString();

        return View(sortedPersons); 
        }
        [HttpGet]
        [Route("person/add")]
        public IActionResult Add()
        {
            var countries = countryService.GetAllCountries();
            ViewBag.Countries = countries;
            return View();
        }
        [HttpPost]
        [Route("person/add")]
        public IActionResult Add(PersonAddRequest model)
        {
            //if(ModelState.IsValid)
           // {
             //   var countries = countryService.GetAllCountries();
              //  ViewBag.Countries = countries;
             //   ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
             //   return View();
          //  }
            var per = person.AddPerson(model);
            return RedirectToAction("Index","Home");
        }

        
    }
}
