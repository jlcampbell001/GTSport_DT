using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GTSport_DT.Cars.CarCategory;

namespace GTSport_DT.Cars
{
    public class CarsRepository : SQLRespository<Car>
    {
        public CarsRepository(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            tableName = "CARS";
            idField = "carkey";
            getListOrderByField = "carname";
        }

        protected override Car RecordToEntity(NpgsqlDataReader dataReader)
        {
            Car car = new Car();
            car.PrimaryKey = dataReader.GetString(dataReader.GetOrdinal("carkey"));
            car.Acceleration = dataReader.GetDouble(dataReader.GetOrdinal("caracceleration"));
            car.Aspiration = dataReader.GetString(dataReader.GetOrdinal("caraspiration"));
            car.Braking = dataReader.GetDouble(dataReader.GetOrdinal("carbraking"));
            car.Category = GetCategoryByDBValue(dataReader.GetString(dataReader.GetOrdinal("carcategory")));
            car.Cornering = dataReader.GetDouble(dataReader.GetOrdinal("carcornering"));
            car.DisplacementCC = dataReader.GetString(dataReader.GetOrdinal("cardisplacementcc"));
            car.DriveTrain = dataReader.GetString(dataReader.GetOrdinal("cardrivetrain"));
            car.Height = dataReader.GetDouble(dataReader.GetOrdinal("carheight"));
            car.Length = dataReader.GetDouble(dataReader.GetOrdinal("carlength"));
            car.ManufacturerKey = dataReader.GetString(dataReader.GetOrdinal("carmankey"));
            car.MaxPower = dataReader.GetInt32(dataReader.GetOrdinal("carmaxpower"));
            car.MaxSpeed = dataReader.GetDouble(dataReader.GetOrdinal("carmaxspeed"));
            car.Name = dataReader.GetString(dataReader.GetOrdinal("carname"));
            car.PowerRPM = dataReader.GetString(dataReader.GetOrdinal("carpowerrpm"));
            car.Price = dataReader.GetDouble(dataReader.GetOrdinal("carprice"));
            car.Stability = dataReader.GetDouble(dataReader.GetOrdinal("carstability"));
            car.TorqueFTLB = dataReader.GetDouble(dataReader.GetOrdinal("cartorqueftlb"));
            car.TorqueRPM = dataReader.GetString(dataReader.GetOrdinal("cartorquerpm"));
            car.Weight = dataReader.GetDouble(dataReader.GetOrdinal("carweight"));
            car.Width = dataReader.GetDouble(dataReader.GetOrdinal("carwidth"));
            car.Year = dataReader.GetInt32(dataReader.GetOrdinal("caryear"));

            return car;
        }

        protected override void UpdateRow(ref DataRow dataRow, Car entity)
        {
            dataRow["carkey"] = entity.PrimaryKey;
            dataRow["caracceleration"] = entity.Acceleration;
            dataRow["caraspiration"] = entity.Aspiration;
            dataRow["carbraking"] = entity.Braking;
            dataRow["carcategory"] = entity.Category.DBValue;
            dataRow["carcornering"] = entity.Cornering;
            dataRow["cardisplacementcc"] = entity.DisplacementCC;
            dataRow["cardrivetrain"] = entity.DriveTrain;
            dataRow["carheight"] = entity.Height;
            dataRow["carlength"] = entity.Length;
            dataRow["carmankey"] = entity.ManufacturerKey;
            dataRow["carmaxpower"] = entity.MaxPower;
            dataRow["carmaxspeed"] = entity.MaxSpeed;
            dataRow["carname"] = entity.Name;
            dataRow["carpowerrpm"] = entity.PowerRPM;
            dataRow["carprice"] = entity.Price;
            dataRow["carstability"] = entity.Stability;
            dataRow["cartorqueftlb"] = entity.TorqueFTLB;
            dataRow["cartorquerpm"] = entity.TorqueRPM;
            dataRow["carweight"] = entity.Weight;
            dataRow["carwidth"] = entity.Width;
            dataRow["caryear"] = entity.Year;
        }

        public Car GetByName(string name)
        {
            Car car = GetByFieldString(name, "carname");

            return car;
        }

        public List<Car> GetListForManufacturerKey(string manufacturerKey, Boolean orderedList = false)
        {
            List<Car> cars = GetListForFieldString(manufacturerKey, "carmankey", orderedList: orderedList);

            return cars;
        }

        public List<CarStatistic> GetCarStatistics()
        {
            List<CarStatistic> carStatistics = new List<CarStatistic>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT carcategory, count(*), avg(carmaxpower), "
                + "avg(carmaxspeed), avg(caracceleration), avg(carbraking), avg(carcornering), avg(carstability), avg(carprice) FROM cars "
                + "GROUP BY carcategory ORDER BY carcategory";

            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                CarStatistic carStatistic = RecordToStatistic(dataReader);

                carStatistics.Add(carStatistic);
            }

            dataReader.Close();

            cmd.Dispose();

            return carStatistics;
        }

        private CarStatistic RecordToStatistic(NpgsqlDataReader dataReader)
        {
            CarStatistic carStatistic = new CarStatistic();
            carStatistic.Category = CarCategory.GetCategoryByDBValue(dataReader.GetString(0));
            carStatistic.NumberOfCars = dataReader.GetInt32(1);
            carStatistic.AvgMaxPower = dataReader.GetDouble(2);
            carStatistic.AvgMaxSpeed = dataReader.GetDouble(3);
            carStatistic.AvgAcceleration = dataReader.GetDouble(4);
            carStatistic.AvgBraking = dataReader.GetDouble(5);
            carStatistic.AvgCornering = dataReader.GetDouble(6);
            carStatistic.AvgStability = dataReader.GetDouble(7);
            carStatistic.AvgPrice = dataReader.GetDouble(8);

            return carStatistic;
        }

        public List<Car> GetListByCriteria(CarSearchCriteria carSearchCriteria, Boolean orderedList = false)
        {
            List<Car> cars = new List<Car>();

            string whereClause = "";

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM cars ";

            cmd.CommandText += AddJoins(carSearchCriteria);

            whereClause += SetCategoryRange(ref cmd, carSearchCriteria);

            whereClause += AddWhereAnd(whereClause.Length, SetYearRange(ref cmd, carSearchCriteria));

            whereClause += AddWhereAnd(whereClause.Length, SetMaxPowerRange(ref cmd, carSearchCriteria));

            whereClause += AddWhereAnd(whereClause.Length, SetDriveTrainCondition(ref cmd, carSearchCriteria));

            whereClause += AddWhereAnd(whereClause.Length, SetManufacturerCondition(ref cmd, carSearchCriteria));

            whereClause += AddWhereAnd(whereClause.Length, SetCountryCondition(ref cmd, carSearchCriteria));

            whereClause += AddWhereAnd(whereClause.Length, SetRegionCondition(ref cmd, carSearchCriteria));

            if (!String.IsNullOrWhiteSpace(whereClause))
            {
                cmd.CommandText += " WHERE " + whereClause;
            }

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(getListOrderByField))
                {
                    getListOrderByField = idField;
                }

                cmd.CommandText +=  " ORDER BY " + getListOrderByField;
            }
            else
            {
                cmd.CommandText +=  " ORDER BY " + idField;
            }

            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                Car car = RecordToEntity(dataReader);

                cars.Add(car);
            }

            dataReader.Close();

            cmd.Dispose();

            return cars;
        }

        private string AddJoins(CarSearchCriteria carSearchCriteria)
        {
            string joinClause = "";

            if (carSearchCriteria.ManufacturerName != null 
                || carSearchCriteria.CountryDescription != null
                || carSearchCriteria.RegionDescription != null)
            {
                joinClause += " INNER JOIN manufacturers on cars.carmankey = manufacturers.mankey ";
            }

            if (carSearchCriteria.CountryDescription != null
                || carSearchCriteria.RegionDescription != null)
            {
                joinClause += " INNER JOIN countries on manufacturers.mancoukey = countries.coukey ";
            }

            if (carSearchCriteria.RegionDescription != null)
            {
                joinClause += " INNER JOIN regions on countries.couregkey = regions.regkey ";
            }

            return joinClause;
        }

        private string AddWhereAnd(int whereLength, string additionalWhere)
        {
            string whereClause = additionalWhere;

            if (whereLength > 0 && additionalWhere.Length > 0)
            {
                whereClause = " AND " + additionalWhere;
            }

            return whereClause;
        }

        private string SetCategoryRange(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.CategoryFrom != null || carSearchCriteria.CategoryTo != null)
            {
                whereClause = " carcategory BETWEEN @mincategory AND @maxcategory ";

                string minCategory = CarCategory.Empty.DBValue;
                string maxCategory = CarCategory.Max.DBValue;

                if (carSearchCriteria.CategoryFrom != null)
                {
                    minCategory = carSearchCriteria.CategoryFrom.DBValue;
                }

                if (carSearchCriteria.CategoryTo != null)
                {
                    maxCategory = carSearchCriteria.CategoryTo.DBValue;
                }

                cmd.Parameters.AddWithValue("mincategory", minCategory);
                cmd.Parameters.AddWithValue("maxcategory", maxCategory);
            }

            return whereClause;
        }

        private string SetYearRange(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.YearFrom >= 0 || carSearchCriteria.YearTo >= 0)
            {
                whereClause = " caryear BETWEEN @minyear AND @maxyear ";

                int minYear = 0;
                int maxYear = int.MaxValue;

                if (carSearchCriteria.YearFrom >= 0)
                {
                    minYear = carSearchCriteria.YearFrom;
                }

                if (carSearchCriteria.YearTo >= 0)
                {
                    maxYear = carSearchCriteria.YearTo;
                }

                cmd.Parameters.AddWithValue("minyear", minYear);
                cmd.Parameters.AddWithValue("maxyear", maxYear);
            }

            return whereClause;
        }

        private string SetMaxPowerRange(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.MaxPowerFrom >= 0 || carSearchCriteria.MaxPowerTo >= 0)
            {
                whereClause = " carmaxpower BETWEEN @minmaxpower AND @maxmaxpower ";

                int minMaxPower = 0;
                int maxMaxPower = int.MaxValue;

                if (carSearchCriteria.MaxPowerFrom >= 0)
                {
                    minMaxPower = carSearchCriteria.MaxPowerFrom;
                }

                if (carSearchCriteria.MaxPowerTo >= 0)
                {
                    maxMaxPower = carSearchCriteria.MaxPowerTo;
                }

                cmd.Parameters.AddWithValue("minmaxpower", minMaxPower);
                cmd.Parameters.AddWithValue("maxmaxpower", maxMaxPower);
            }

            return whereClause;
        }

        private string SetDriveTrainCondition(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.DriveTrain != null)
            {
                whereClause = " cardrivetrain = @drivetrain ";

                cmd.Parameters.AddWithValue("drivetrain", carSearchCriteria.DriveTrain);
            }

            return whereClause;
        }

        private string SetManufacturerCondition(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.ManufacturerName != null)
            {
                whereClause = " manname = @manufacturername ";

                cmd.Parameters.AddWithValue("manufacturername", carSearchCriteria.ManufacturerName);
            }

            return whereClause;
        }

        private string SetCountryCondition(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.CountryDescription != null)
            {
                whereClause = " coudescription = @countrydescription ";

                cmd.Parameters.AddWithValue("countrydescription", carSearchCriteria.CountryDescription);
            }

            return whereClause;
        }

        private string SetRegionCondition(ref NpgsqlCommand cmd, CarSearchCriteria carSearchCriteria)
        {
            string whereClause = "";

            if (carSearchCriteria.RegionDescription != null)
            {
                whereClause = " regdescription = @regiondescription ";

                cmd.Parameters.AddWithValue("regiondescription", carSearchCriteria.RegionDescription);
            }

            return whereClause;
        }
    }
}
