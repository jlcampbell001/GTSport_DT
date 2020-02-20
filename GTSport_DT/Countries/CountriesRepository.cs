using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace GTSport_DT.Countries
{
    /// <summary>This is the class repository for the countries table.</summary>
    /// <seealso cref="GTSport_DT.General.SQLRespository{GTSport_DT.Countries.Country}"/>
    public class CountriesRepository : SQLRespository<Country>
    {
        /// <summary>Initializes a new instance of the <see cref="CountriesRepository"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public CountriesRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "COUNTRIES";
            idField = "coukey";
            getListOrderByField = "coudescription";
        }

        /// <summary>Gets a country entity that matches the passed description.</summary>
        /// <param name="description">The description to look for.</param>
        /// <returns>The found country entity or null if one is not found.</returns>
        public Country GetByDescription(string description)
        {
            Country country = GetByFieldString(description, "coudescription");

            return country;
        }

        /// <summary>Gets the list of countries that are linked to the passed region key.</summary>
        /// <param name="regionKey">The region key to match.</param>
        /// <param name="orderedList">If set to true it will order the list by the description.</param>
        /// <returns>The list of countries found for the region key.</returns>
        public List<Country> GetListForRegionKey(string regionKey, Boolean orderedList = false)
        {
            List<Country> countries = GetListForFieldString(regionKey, "couregkey", orderedList: orderedList);

            return countries;
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Country RecordToEntity(NpgsqlDataReader dataReader)
        {
            Country country = new Country();
            country.PrimaryKey = dataReader.GetString(dataReader.GetOrdinal("coukey"));
            country.Description = dataReader.GetString(dataReader.GetOrdinal("coudescription"));
            country.RegionKey = dataReader.GetString(dataReader.GetOrdinal("couregkey"));

            return country;
        }

        /// <summary>Updates the passed data row.</summary>
        /// <param name="dataRow">The data row to update.</param>
        /// <param name="entity">The entity.</param>
        protected override void UpdateRow(ref DataRow dataRow, Country entity)
        {
            dataRow["coukey"] = entity.PrimaryKey;
            dataRow["coudescription"] = entity.Description;
            dataRow["couregkey"] = entity.RegionKey;
        }
    }
}