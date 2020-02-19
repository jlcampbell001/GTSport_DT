using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    public class CarsService : BackEndService<Car, CarsRepository, CarValidation, CarNotFoundExcpetion>
    {
        public CarsService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "CAR";
            keyNotFoundMessage = CarNotFoundExcpetion.CarKeyNotFoundMsg;
        }

        public Car GetByName(string name)
        {
            Car car = repository.GetByName(name);

            if (car == null)
            {
                throw new CarNotFoundExcpetion(CarNotFoundExcpetion.CarNameNotFoundMsg, name);
            }

            return car;
        }

        public List<Car> GetListForManufacturerKey(string manufacturerKey, Boolean orderedList = false)
        {
            List<Car> cars = repository.GetListForManufacturerKey(manufacturerKey, orderedList);

            return cars;
        }

        public List<Car> GetListForSearchCriteria(CarSearchCriteria carSearchCriteria, Boolean orderedList = false)
        {
            validation.ValidateSearchCriteria(carSearchCriteria);

            List<Car> cars = repository.GetListByCriteria(carSearchCriteria, orderedList);

            return cars;
        }
    }
}
