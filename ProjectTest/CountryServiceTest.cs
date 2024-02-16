using Entites;
using ServiceContracts.DTO;
using ServiceContracts;
using Services;
namespace ProjectTest
{
    public class CountryServiceTest
    {
        private readonly ICountryService _countryservice;
        public CountryServiceTest()
        {
            _countryservice=new CountryService();
        }
        #region AddCountry
        //when CountryAddRequest is null, it should return ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? Request = null;
            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countryservice.Addcountry(Request);
            });
        }

        //when country name is null, it should return ArgumentExcxeption
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arrange
            CountryAddRequest? Request = new CountryAddRequest() { CountryName=null};
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countryservice.Addcountry(Request);
            });
        }

        //when country name is dupicated, it should return ArgumentException
        [Fact]
        public void AddCountry_CountryNameDuplicatedl()
        {
            //Arrange
            CountryAddRequest? Request1= new CountryAddRequest() { CountryName = "Gaza" };
            CountryAddRequest? Request2 = new CountryAddRequest() { CountryName = "Gaza" };
            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countryservice.Addcountry(Request1);
                _countryservice.Addcountry(Request2);
            });
        }
        //when you supply proper country name, it should insert it to the exisiting list of countries
        [Fact]
        public void AddCountry_ProperCountry()
        {
            //Arrange
            CountryAddRequest? Request = new CountryAddRequest() { CountryName = "Gaza" };

            //Act
            CountryResponse Response = _countryservice.Addcountry(Request);
            List<CountryResponse> getallcountries = _countryservice.GetAllCountries();

            //Assert
            Assert.True(Response.CountryId != Guid.Empty);
            Assert.Contains(Response, getallcountries);

        }
        #endregion
        #region GetAllCountries

        [Fact]
        public void GetAllCountries_EmptyList()
        {
            //Act
            List<CountryResponse> actual = _countryservice.GetAllCountries();
            //Assert
            Assert.Empty(actual);
        }
        [Fact]
        public void GetAllCountry_AddFewCountries() 
        {
            //Arrange
            List<CountryAddRequest> request_list = new List<CountryAddRequest>()
            {
                new CountryAddRequest(){CountryName="Gaza"},
                new CountryAddRequest(){CountryName="Palestine"}
            };
            //Act
            List<CountryResponse> list_from_addrequest = new List<CountryResponse>();
            foreach (var request in request_list)
            {
               list_from_addrequest.Add( _countryservice.Addcountry(request));
            }
            List<CountryResponse> actual =  _countryservice.GetAllCountries();
            foreach(CountryResponse expected in list_from_addrequest)
            {
                Assert.Contains(expected,actual);
            }
        }
        #endregion
        #region GetCountryByID
        [Fact]
        public void GetCountryById_NullId()
        {
            //Arrange
            Guid? id = null;
            //Act
            CountryResponse? respone = _countryservice.GetCountryById(id);
            //Assert
            Assert.Null(respone);
        }
        [Fact]
        public void GetCountryById_ValidCountryId()
        {
            //Arrange
            CountryAddRequest? request= new CountryAddRequest() { CountryName="Gaza"};
            CountryResponse? response = _countryservice.Addcountry(request);
            //Act
            CountryResponse? response_fromGet =_countryservice.GetCountryById(response.CountryId);
            //Assert
            Assert.Equal(response, response_fromGet);

        }
        #endregion

    }
}