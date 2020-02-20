using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Cars
{
    /// <summary>The service for cars.</summary>
    /// <seealso cref="GTSport_DT.General.BackEndService{GTSport_DT.Cars.Car, GTSport_DT.Cars.CarsRepository, GTSport_DT.Cars.CarValidation, GTSport_DT.Cars.CarNotFoundException}"/>
    public class CarsService : BackEndService<Car, CarsRepository, CarValidation, CarNotFoundException>
    {
        /// <summary>Initializes a new instance of the <see cref="CarsService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public CarsService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "CAR";
            keyNotFoundMessage = CarNotFoundException.CarKeyNotFoundMsg;
        }

        /// <summary>Gets the car by the passed name.</summary>
        /// <param name="name">The name.</param>
        /// <returns>The car found.</returns>
        /// <exception cref="GTSport_DT.Cars.CarNotFoundException">
        /// If a car can not be found with the name.
        /// </exception>
        public Car GetByName(string name)
        {
            Car car = repository.GetByName(name);

            if (car == null)
            {
                throw new CarNotFoundException(CarNotFoundException.CarNameNotFoundMsg, name);
            }

            return car;
        }

        /// <summary>Gets the list of cars for manufacturer key.</summary>
        /// <param name="manufacturerKey">The manufacturer key.</param>
        /// <param name="orderedList">If set to <c>true</c> the list will be ordered.</param>
        /// <returns>The list of cars found.</returns>
        public List<Car> GetListForManufacturerKey(string manufacturerKey, Boolean orderedList = false)
        {
            List<Car> cars = repository.GetListForManufacturerKey(manufacturerKey, orderedList);

            return cars;
        }

        /// <summary>Gets the list of cars for search criteria.</summary>
        /// <param name="carSearchCriteria">The car search criteria.</param>
        /// <param name="orderedList">If set to <c>true</c> the list will be ordered.</param>
        /// <returns>The list of cars found.</returns>
        public List<Car> GetListForSearchCriteria(CarSearchCriteria carSearchCriteria, Boolean orderedList = false)
        {
            validation.ValidateSearchCriteria(carSearchCriteria);

            List<Car> cars = repository.GetListByCriteria(carSearchCriteria, orderedList);

            return cars;
        }
    }
}