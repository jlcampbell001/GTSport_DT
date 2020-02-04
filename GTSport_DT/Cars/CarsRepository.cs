using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
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
            updateCommand = "UPDATE cars SET carname = @name, carmankey = @mankey, caryear = @year, carcategory = @category, "
                + "carprice = @price, cardisplacementcc = @displacement, carmaxpower = @maxpower, carpowerrpm = @powerrpm, "
                + "cartorqueftlb = @torqueftlb, cartorquerpm = @torquerpm, cardrivetrain = @drivetrain, caraspiration = @aspiration, "
                + "carlength = @length, carwidth = @width, carheight = @height, carweight = @weight, carmaxspeed = @maxspeed, "
                + "caracceleration = @acceleration, carbraking = @braking, carcornering = @cornering, carstability = @stability "
                + "WHERE carkey = @pk";
            insertCommand = "INSERT INTO cars(carkey, carname, carmankey, caryear, carcategory, carprice, cardisplacementcc, "
                +"carmaxpower, carpowerrpm, cartorqueftlb, cartorquerpm, cardrivetrain, caraspiration, carlength, carwidth, "
                + "carheight, carweight, carmaxspeed, caracceleration, carbraking, carcornering, carstability) "
                + "VALUES (@pk, @name, @mankey, @year, @category, @price, @displacement, @maxpower, @powerrpm, @torqueftlb, " 
                + "@torquerpm, @drivetrain, @aspiration, @length, @width, @height, @weight, @maxspeed, @acceleration, @braking, "
                + "@cornering, @stability)";
        }

        protected override void AddParameters(ref NpgsqlCommand cmd, Car entity)
        {
            cmd.Parameters.AddWithValue("pk", entity.PrimaryKey);
            cmd.Parameters.AddWithValue("name", entity.Name);
            cmd.Parameters.AddWithValue("mankey", entity.ManufacturerKey);
            cmd.Parameters.AddWithValue("year", entity.Year);
            cmd.Parameters.AddWithValue("category", entity.Category.DBValue);
            cmd.Parameters.AddWithValue("price", entity.Price);
            cmd.Parameters.AddWithValue("displacement", entity.DisplacementCC);
            cmd.Parameters.AddWithValue("maxpower", entity.MaxPower);
            cmd.Parameters.AddWithValue("powerrpm", entity.PowerRPM);
            cmd.Parameters.AddWithValue("torqueftlb", entity.TorqueFTLB);
            cmd.Parameters.AddWithValue("torquerpm", entity.TorqueRPM);
            cmd.Parameters.AddWithValue("drivetrain", entity.DriveTrain);
            cmd.Parameters.AddWithValue("aspiration", entity.Aspiration);
            cmd.Parameters.AddWithValue("length", entity.Length);
            cmd.Parameters.AddWithValue("width", entity.Width);
            cmd.Parameters.AddWithValue("height", entity.Height);
            cmd.Parameters.AddWithValue("weight", entity.Weight);
            cmd.Parameters.AddWithValue("maxspeed", entity.MaxSpeed);
            cmd.Parameters.AddWithValue("acceleration", entity.Acceleration);
            cmd.Parameters.AddWithValue("braking", entity.Braking);
            cmd.Parameters.AddWithValue("cornering", entity.Cornering);
            cmd.Parameters.AddWithValue("stability", entity.Stability);
        }

        protected override Car RecordToEntity(NpgsqlDataReader dataReader)
        {
            Car car = new Car();
            car.PrimaryKey = dataReader.GetString(0);
            car.Acceleration = dataReader.GetDouble(1);
            car.Aspiration = dataReader.GetString(2);
            car.Braking = dataReader.GetDouble(3);
            car.Category = GetCategoryByDBValue(dataReader.GetString(4));
            car.Cornering = dataReader.GetDouble(5);
            car.DisplacementCC = dataReader.GetString(6);
            car.DriveTrain = dataReader.GetString(7);
            car.Height = dataReader.GetDouble(8);
            car.Length = dataReader.GetDouble(9);
            car.ManufacturerKey = dataReader.GetString(10);
            car.MaxPower = dataReader.GetInt32(11);
            car.MaxSpeed = dataReader.GetDouble(12);
            car.Name = dataReader.GetString(13);
            car.PowerRPM = dataReader.GetString(14);
            car.Price = dataReader.GetDouble(15);
            car.Stability = dataReader.GetDouble(16);
            car.TorqueFTLB = dataReader.GetDouble(17);
            car.TorqueRPM = dataReader.GetString(18);
            car.Weight = dataReader.GetDouble(19);
            car.Width = dataReader.GetDouble(20);
            car.Year = dataReader.GetInt32(21);

            return car;
        }

        public Car GetByName(string name)
        {
            Car car = null;

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM cars WHERE carname = @name";

            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();

            NpgsqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                car = RecordToEntity(dataReader);
            }

            dataReader.Close();

            cmd.Dispose();

            return car;
        }

        public List<Car> GetListForManufacturerKey(string manufacturerKey, Boolean orderedList = false)
        {
            List<Car> cars = new List<Car>();

            var cmd = new NpgsqlCommand();

            cmd.Connection = npgsqlConnection;
            cmd.CommandText = "SELECT * FROM cars WHERE carmankey = @mankey";

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(getListOrderByField))
                {
                    getListOrderByField = idField;
                }

                cmd.CommandText = cmd.CommandText + " ORDER BY " + getListOrderByField;
            }

            cmd.Parameters.AddWithValue("mankey", manufacturerKey);
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

            if (carSearchCriteria.CategoryFrom != null || carSearchCriteria.CategoryTo != null)
            {
                whereClause = whereClause + " carcategory BETWEEN @mincategory AND @maxcategory ";

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

            if (!String.IsNullOrWhiteSpace(whereClause))
            {
                cmd.CommandText = cmd.CommandText + " WHERE " + whereClause;
            }

            if (orderedList)
            {
                if (String.IsNullOrWhiteSpace(getListOrderByField))
                {
                    getListOrderByField = idField;
                }

                cmd.CommandText = cmd.CommandText + " ORDER BY " + getListOrderByField;
            }

//            cmd.Parameters.AddWithValue("mankey", manufacturerKey);
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
    }
}
