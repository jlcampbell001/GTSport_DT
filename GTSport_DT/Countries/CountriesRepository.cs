using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

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
            updateCommand = "UPDATE countries SET coudescription = @desc, couregkey = @regkey WHERE coukey = @pk";
            insertCommand = "INSERT INTO countries(coukey, coudescription, couregkey) VALUES (@pk, @desc, @regkey)";
        }

        /// <summary>Gets a country entity that matches the passed description.</summary>
        /// <param name="description">The description to look for.</param>
        /// <returns>The found country entity or null if one is not found.</returns>
        public Country GetByDescription(string description)
        {
            Country country = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM countries WHERE coudescription = @desc";

            cmd.Parameters.AddWithValue("desc", description);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                country = RecordToEntity(dataReader);
            }

            dataReader.Close();

            cmd.Dispose();

            return country;
        }

        /// <summary>Gets the list of countries that are linked to the passed region key.</summary>
        /// <param name="regionKey">The region key to match.</param>
        /// <param name="orderedList">If set to true it will order the list by the description.</param>
        /// <returns>The list of countries found for the region key.</returns>
        public List<Country> GetListForRegionKey(string regionKey, Boolean orderedList = false)
        {
            List<Country> countries = new List<Country>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM countries WHERE couregkey = @regkey";

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(getListOrderByField))
                {
                    getListOrderByField = idField;
                }

                cmd.CommandText = cmd.CommandText + " ORDER BY " + getListOrderByField;
            }

            cmd.Parameters.AddWithValue("regkey", regionKey);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                Country country = RecordToEntity(dataReader);

                countries.Add(country);
            }

            dataReader.Close();

            cmd.Dispose();

            return countries;
        }

        /// <summary>Adds parameters to a SQL command object based on the passed entity.</summary>
        /// <param name="cmd">The SQL command object to update.</param>
        /// <param name="entity">The entity to get data from.</param>
        protected override void AddParameters(ref NpgsqlCommand cmd, Country entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("desc", entity.Description);
            cmd.Parameters.AddWithValue("regkey", entity.RegionKey);
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Country RecordToEntity(NpgsqlDataReader dataReader)
        {
            Country country = new Country();
            country.PrimaryKey = dataReader.GetString(0);
            country.Description = dataReader.GetString(1);
            country.RegionKey = dataReader.GetString(2);

            return country;
        }
    }
}