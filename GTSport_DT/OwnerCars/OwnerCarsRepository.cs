using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    public class OwnerCarsRepository : SQLRespository<OwnerCar>
    {
        public OwnerCarsRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "OWNERCARS";
            idField = "owckey";
            getListOrderByField = "owccarid";
        }

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

        public OwnerCar GetByCarID(string carID)
        {
            OwnerCar ownerCar = GetByFieldString(carID, "owccarid");

            return ownerCar;
        }

        public List<OwnerCar> GetListForOwnerKey(string ownerKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = GetListForFieldString(ownerKey, "owcownkey", orderedList: orderedList);

            return ownerCars;
        }

        public List<OwnerCar> GetListForForCarKey(string carKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = GetListForFieldString(carKey, "owccarkey", orderedList: orderedList);

            return ownerCars;
        }
    }
}
