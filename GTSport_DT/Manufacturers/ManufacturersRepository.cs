using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Manufacturers
{
    /// <summary>The repository for the manufacturers table.</summary>
    /// <seealso cref="GTSport_DT.General.SQLRespository{GTSport_DT.Manufacturers.Manufacturer}"/>
    public class ManufacturersRepository : SQLRespository<Manufacturer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturersRepository"/> class.
        /// </summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public ManufacturersRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "MANUFACTURERS";
            idField = "mankey";
            getListOrderByField = "manname";
            updateCommand = "UPDATE manufacturers SET manname = @name, mancoukey = @coukey WHERE mankey = @pk";
            insertCommand = "INSERT INTO manufacturers(mankey, manname, mancoukey) VALUES (@pk, @name, @coukey)";
        }

        /// <summary>Gets the manufacturer with the passed name.</summary>
        /// <param name="name">The name. to look up.</param>
        /// <returns>The manufacture found or null if one is not found.</returns>
        public Manufacturer GetByName(string name)
        {
            Manufacturer manufacturer = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM manufacturers WHERE manname = @name";

            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                manufacturer = RecordToEntity(dataReader);
            }

            dataReader.Close();

            cmd.Dispose();

            return manufacturer;
        }

        /// <summary>Gets the list of manufacturers for country key.</summary>
        /// <param name="countryKey">The country key.</param>
        /// <param name="orderedList">
        /// If set to <c>true</c> get the list ordered by manufacture name.
        /// </param>
        /// <returns>The list of manufacturers for the country key.</returns>
        public List<Manufacturer> GetListForCountryKey(string countryKey, Boolean orderedList = false)
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM manufacturers WHERE mancoukey = @coukey";

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(getListOrderByField))
                {
                    getListOrderByField = idField;
                }

                cmd.CommandText = cmd.CommandText + " ORDER BY " + getListOrderByField;
            }

            cmd.Parameters.AddWithValue("coukey", countryKey);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                Manufacturer manufacturer = RecordToEntity(dataReader);

                manufacturers.Add(manufacturer);
            }

            dataReader.Close();

            cmd.Dispose();

            return manufacturers;
        }

        /// <summary>Adds parameters to a SQL command object based on the passed entity.</summary>
        /// <param name="cmd">The SQL command object to update.</param>
        /// <param name="entity">The entity to get data from.</param>
        protected override void AddParameters(ref NpgsqlCommand cmd, Manufacturer entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("coukey", entity.CountryKey);
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Manufacturer RecordToEntity(NpgsqlDataReader dataReader)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.PrimaryKey = dataReader.GetString(0);
            manufacturer.Name = dataReader.GetString(2);
            manufacturer.CountryKey = dataReader.GetString(1);

            return manufacturer;
        }
    }
}