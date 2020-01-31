using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    public class CountriesService : BackEndService<Country, CountriesRepository, CountryValidation, CountryNotFoundException>
    {
        public CountriesService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "COU";
            keyNotFoundMessage = CountryNotFoundException.CountryKeyNotFoundMsg;
        }

        public Country GetByDescription(string description)
        {
            Country country = repository.GetByDescription(description);

            if (country == null)
            {
                throw new CountryNotFoundException(CountryNotFoundException.CountryDescriptionNotFoundMsg, description);
            }

            return country; 
        }

        public List<Country> GetListForRegionKey(string regionKey, Boolean orderedList = false)
        {
            List<Country> countries = repository.GetListForRegionKey(regionKey, orderedList);

            return countries;
        }
    }
}
