using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Regions
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The repository for the region table.
    /// </summary>
    // --------------------------------------------------------------------------------
    public class RegionsRepository : SQLRespository<Region>
    {
        public RegionsRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "REGIONS";
            idField = "regkey";
            getListOrderByField = "regdescription";
            updateCommand = "UPDATE regions SET regdescription = @desc WHERE regkey = @pk";
            insertCommand = "INSERT INTO regions(regkey, regdescription) VALUES (@pk, @desc)";
        }

        protected override void AddParameters(ref NpgsqlCommand cmd, Region entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("desc", entity.Description);
        }

        protected override Region RecordToEntity(NpgsqlDataReader dataReader)
        {
            Region region = new Region();
            region.PrimaryKey = dataReader.GetString(0);
            region.Description = dataReader.GetString(1);

            return region;
        }

        // ********************************************************************************
        /// <summary>
        /// Gets a region that matches the passed description.
        /// </summary>
        /// <param name="description">The description to look for.</param>
        /// <returns>A region that was found or null if it was not found.</returns>
        // ********************************************************************************
        public Region GetByDescription(string description)
        {
            Region region = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM regions WHERE regdescription = @desc";

            cmd.Parameters.AddWithValue("desc", description);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                region = RecordToEntity(dataReader);
            }

            dataReader.Close();

            cmd.Dispose();

            return region;
        }
    }
}
