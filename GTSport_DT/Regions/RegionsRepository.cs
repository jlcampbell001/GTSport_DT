using GTSport_DT.General;
using Npgsql;
using System.Data;

namespace GTSport_DT.Regions
{
    /// <summary>The repository for the region table.</summary>
    public class RegionsRepository : SQLRespository<Region>
    {
        /// <summary>Initializes a new instance of the <see cref="RegionsRepository"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public RegionsRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "REGIONS";
            idField = "regkey";
            getListOrderByField = "regdescription";
        }

        /// <summary>Gets a region that matches the passed description.</summary>
        /// <param name="description">The description to look for.</param>
        /// <returns>A region that was found or null if it was not found.</returns>
        public Region GetByDescription(string description)
        {
            Region region = GetByFieldString(description, "regdescription");

            return region;
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Region RecordToEntity(NpgsqlDataReader dataReader)
        {
            Region region = new Region();
            region.PrimaryKey = dataReader.GetString(dataReader.GetOrdinal("regkey"));
            region.Description = dataReader.GetString(dataReader.GetOrdinal("regdescription"));

            return region;
        }

        protected override void UpdateRow(ref DataRow dataRow, Region entity)
        {
            dataRow["regkey"] = entity.PrimaryKey;
            dataRow["regdescription"] = entity.Description;
        }
    }
}