using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

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

            FillDataSet();
        }

        /// <summary>Gets the manufacturer with the passed name.</summary>
        /// <param name="name">The name. to look up.</param>
        /// <returns>The manufacture found or null if one is not found.</returns>
        public Manufacturer GetByName(string name)
        {
            Manufacturer manufacturer = GetByFieldString(name, "manname");

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
            List<Manufacturer> manufacturers = GetListForFieldString(countryKey, "mancoukey", orderedList: orderedList);

            return manufacturers;
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Manufacturer RecordToEntity(NpgsqlDataReader dataReader)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.PrimaryKey = dataReader.GetString(dataReader.GetOrdinal("mankey"));
            manufacturer.Name = dataReader.GetString(dataReader.GetOrdinal("manname"));
            manufacturer.CountryKey = dataReader.GetString(dataReader.GetOrdinal("mancoukey"));

            return manufacturer;
        }

        /// <summary>Updates the passed data row.</summary>
        /// <param name="dataRow">The data row to update.</param>
        /// <param name="entity">The entity to update from.</param>
        protected override void UpdateRow(ref DataRow dataRow, Manufacturer entity)
        {
            dataRow["mankey"] = entity.PrimaryKey;
            dataRow["manname"] = entity.Name;
            dataRow["mancoukey"] = entity.CountryKey;
        }
    }
}