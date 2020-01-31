using GTSport_DT.General;
using Npgsql;

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
            updateCommand = "UPDATE regions SET regdescription = @desc WHERE regkey = @pk";
            insertCommand = "INSERT INTO regions(regkey, regdescription) VALUES (@pk, @desc)";
        }

        /// <summary>Gets a region that matches the passed description.</summary>
        /// <param name="description">The description to look for.</param>
        /// <returns>A region that was found or null if it was not found.</returns>
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

        /// <summary>Adds parameters to a SQL command object based on the passed entity.</summary>
        /// <param name="cmd">The SQL command object to update.</param>
        /// <param name="entity">The entity to get data from.</param>
        protected override void AddParameters(ref NpgsqlCommand cmd, Region entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("desc", entity.Description);
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override Region RecordToEntity(NpgsqlDataReader dataReader)
        {
            Region region = new Region();
            region.PrimaryKey = dataReader.GetString(0);
            region.Description = dataReader.GetString(1);

            return region;
        }
    }
}