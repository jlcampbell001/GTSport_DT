using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    public class CountriesRepository : SQLRespository<Country>
    {
        public CountriesRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "COUNTRIES";
            idField = "coukey";
            getListOrderByField = "coudescription";
            updateCommand = "UPDATE countries SET coudescription = @desc, couregkey = @regkey WHERE coukey = @pk";
            insertCommand = "INSERT INTO countries(coukey, coudescription, couregkey) VALUES (@pk, @desc, @regkey)";
        }

        protected override void AddParameters(ref NpgsqlCommand cmd, Country entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("desc", entity.Description);
            cmd.Parameters.AddWithValue("regkey", entity.RegionKey);
        }

        protected override Country RecordToEntity(NpgsqlDataReader dataReader)
        {
            Country country = new Country();
            country.PrimaryKey = dataReader.GetString(0);
            country.Description = dataReader.GetString(1);
            country.RegionKey = dataReader.GetString(2);

            return country;
        }

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
    }
}
