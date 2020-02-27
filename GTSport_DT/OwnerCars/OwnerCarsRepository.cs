using GTSport_DT.Cars;
using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The repository for the owner cars table.</summary>
    /// <seealso cref="GTSport_DT.General.SQLRespository{GTSport_DT.OwnerCars.OwnerCar}"/>
    public class OwnerCarsRepository : SQLRespository<OwnerCar>
    {
        /// <summary>Initializes a new instance of the <see cref="OwnerCarsRepository"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public OwnerCarsRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "OWNERCARS";
            idField = "owckey";
            getListOrderByField = "owccarid";
        }

        /// <summary>Gets the owner car by car identifier.</summary>
        /// <param name="carID">The car identifier to look for.</param>
        /// <returns>The owner car found or null if not found.</returns>
        public OwnerCar GetByCarID(string carID)
        {
            OwnerCar ownerCar = GetByFieldString(carID, "owccarid");

            return ownerCar;
        }

        /// <summary>Gets the list of owner cars for car key.</summary>
        /// <param name="carKey">The car key to filter on</param>
        /// <param name="orderedList">If set true get the list ordered.</param>
        /// <returns>The list of owner cars for the car key.</returns>
        public List<OwnerCar> GetListForForCarKey(string carKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = GetListForFieldString(carKey, "owccarkey", orderedList: orderedList);

            return ownerCars;
        }

        /// <summary>Gets the list of owner cars for owner key.</summary>
        /// <param name="ownerKey">The owner key.</param>
        /// <param name="orderedList">If set to <c>true</c> order list.</param>
        /// <returns>The owner cars list for the owner key.</returns>
        public List<OwnerCar> GetListForOwnerKey(string ownerKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = GetListForFieldString(ownerKey, "owcownkey", orderedList: orderedList);

            return ownerCars;
        }

        /// <summary>
        /// <para>Gets the owner car statistics.</para>
        /// <para><font color="#333333">Totals are every car owned even duplicates.</font></para>
        /// </summary>
        /// <param name="ownerKey">The owner key to filter on.</param>
        /// <returns>The statistics found.</returns>
        public List<OwnerCarsStatistic> GetOwnerCarStatistics(string ownerKey)
        {
            List<OwnerCarsStatistic> ownerCarsStatistics = new List<OwnerCarsStatistic>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT carcategory, count(*) FROM " + tableName + " "
                + "INNER JOIN CARS on carkey = owccarkey "
                + "WHERE owcownkey = @ownerkey "
                + "GROUP BY carcategory  ORDER BY carcategory ";

            cmd.Parameters.AddWithValue("ownerkey", ownerKey);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                OwnerCarsStatistic ownerCarStatistic = RecordToOwnerCarStatistic(dataReader);

                ownerCarsStatistics.Add(ownerCarStatistic);
            }

            dataReader.Close();

            cmd.Dispose();

            return ownerCarsStatistics;
        }

        /// <summary>Gets the unique owner car statistics.</summary>
        /// <param name="ownerKey">The owner key.</param>
        /// <returns>A list of statistics for the unique cars owned.</returns>
        public List<OwnerCarsStatistic> GetUniqueOwnerCarStatistics(string ownerKey)
        {
            List<OwnerCarsStatistic> ownerCarsStatistics = new List<OwnerCarsStatistic>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT carcategory, count(DISTINCT owccarkey) FROM " + tableName + " "
                + "INNER JOIN CARS on carkey = owccarkey "
                + "WHERE owcownkey = @ownerkey "
                + "GROUP BY carcategory  ORDER BY carcategory ";

            cmd.Parameters.AddWithValue("ownerkey", ownerKey);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                OwnerCarsStatistic ownerCarStatistic = RecordToUniqueOwnerCarStatistic(dataReader);

                ownerCarsStatistics.Add(ownerCarStatistic);
            }

            dataReader.Close();

            cmd.Dispose();

            return ownerCarsStatistics;
        }

        /// <summary>Convert a record retrieved from the database to an entity object.</summary>
        /// <param name="dataReader">The database reader with the results from a database request.</param>
        /// <returns>A new entity with the data.</returns>
        protected override OwnerCar RecordToEntity(NpgsqlDataReader dataReader)
        {
            OwnerCar ownerCar = new OwnerCar();
            ownerCar.PrimaryKey = dataReader.GetString(dataReader.GetOrdinal("owckey"));
            ownerCar.OwnerKey = dataReader.GetString(dataReader.GetOrdinal("owcownkey"));
            ownerCar.CarKey = dataReader.GetString(dataReader.GetOrdinal("owccarkey"));
            ownerCar.CarID = dataReader.GetString(dataReader.GetOrdinal("owccarid"));
            ownerCar.PaintJob = dataReader.GetString(dataReader.GetOrdinal("owccolour"));
            ownerCar.MaxPower = dataReader.GetInt32(dataReader.GetOrdinal("owcmaxpower"));
            ownerCar.PowerLevel = dataReader.GetInt32(dataReader.GetOrdinal("owcpowerlevel"));
            ownerCar.WeightReductionLevel = dataReader.GetInt32(dataReader.GetOrdinal("owcweightreductionlevel"));
            ownerCar.AcquiredDate = dataReader.GetDateTime(dataReader.GetOrdinal("owcdateaquired"));

            return ownerCar;
        }

        /// <summary>Updates the passed data row.</summary>
        /// <param name="dataRow">The data row to update.</param>
        /// <param name="entity">The entity to update from.</param>
        protected override void UpdateRow(ref DataRow dataRow, OwnerCar entity)
        {
            dataRow["owckey"] = entity.PrimaryKey;
            dataRow["owcownkey"] = entity.OwnerKey;
            dataRow["owccarkey"] = entity.CarKey;
            dataRow["owccarid"] = entity.CarID;
            dataRow["owccolour"] = entity.PaintJob;
            dataRow["owcmaxpower"] = entity.MaxPower;
            dataRow["owcpowerlevel"] = entity.PowerLevel;
            dataRow["owcweightreductionlevel"] = entity.WeightReductionLevel;
            dataRow["owcdateaquired"] = entity.AcquiredDate;
        }

        private OwnerCarsStatistic RecordToOwnerCarStatistic(NpgsqlDataReader dataReader)
        {
            OwnerCarsStatistic ownerCarsStatistic = new OwnerCarsStatistic();
            ownerCarsStatistic.Category = CarCategory.GetCategoryByDBValue(dataReader.GetString(0));
            ownerCarsStatistic.carsOwned = dataReader.GetInt32(1);

            return ownerCarsStatistic;
        }

        private OwnerCarsStatistic RecordToUniqueOwnerCarStatistic(NpgsqlDataReader dataReader)
        {
            OwnerCarsStatistic ownerCarsStatistic = new OwnerCarsStatistic();
            ownerCarsStatistic.Category = CarCategory.GetCategoryByDBValue(dataReader.GetString(0));
            ownerCarsStatistic.uniqueCarsOwned = dataReader.GetInt32(1);

            return ownerCarsStatistic;
        }
    }
}