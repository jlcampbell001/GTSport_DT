using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Manufacturers
{
    public class ManufacturersRepository : SQLRespository<Manufacturer>
    {
        public ManufacturersRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "MANUFACTURERS";
            idField = "mankey";
            getListOrderByField = "manname";
            updateCommand = "UPDATE manufacturers SET manname = @name, mancoukey = @coukey WHERE mankey = @pk";
            insertCommand = "INSERT INTO manufacturers(mankey, manname, mancoukey) VALUES (@pk, @name, @coukey)";

        }

        protected override void AddParameters(ref NpgsqlCommand cmd, Manufacturer entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("coukey", entity.CountryKey);
        }

        protected override Manufacturer RecordToEntity(NpgsqlDataReader dataReader)
        {
            Manufacturer manufacturer = new Manufacturer();
            manufacturer.PrimaryKey = dataReader.GetString(0);
            manufacturer.Name = dataReader.GetString(2);
            manufacturer.CountryKey = dataReader.GetString(1);

            return manufacturer;
        }

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

    }
}