using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Countries
{
    /// <summary>The service that works with the countries table.</summary>
    /// <seealso cref="GTSport_DT.General.BackEndService{GTSport_DT.Countries.Country, GTSport_DT.Countries.CountriesRepository, GTSport_DT.Countries.CountryValidation, GTSport_DT.Countries.CountryNotFoundException}"/>
    public class CountriesService : BackEndService<Country, CountriesRepository, CountryValidation, CountryNotFoundException>
    {
        /// <summary>Initializes a new instance of the <see cref="CountriesService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public CountriesService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "COU";
            keyNotFoundMessage = CountryNotFoundException.CountryKeyNotFoundMsg;
        }

        /// <summary>Gets country the by description.</summary>
        /// <param name="description">The description to look for.</param>
        /// <returns>The found country entity that matches the description.</returns>
        /// <exception cref="CountryNotFoundException">If there is no country with the description.</exception>
        public Country GetByDescription(string description)
        {
            Country country = repository.GetByDescription(description);

            if (country == null)
            {
                throw new CountryNotFoundException(CountryNotFoundException.CountryDescriptionNotFoundMsg, description);
            }

            return country;
        }

        /// <summary>Gets the list of countries for region key.</summary>
        /// <param name="regionKey">The region key to match against.</param>
        /// <param name="orderedList">If set to <c>true it orders the list by the description</c>.</param>
        /// <returns>The list of countries found for the region key.</returns>
        public List<Country> GetListForRegionKey(string regionKey, Boolean orderedList = false)
        {
            List<Country> countries = repository.GetListForRegionKey(regionKey, orderedList);

            return countries;
        }
    }
}