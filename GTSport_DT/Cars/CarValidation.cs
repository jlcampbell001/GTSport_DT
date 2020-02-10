using GTSport_DT.General;
using GTSport_DT.Manufacturers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    public class CarValidation : Validation<Car>
    {
        private CarsRepository carsRepository;
        private ManufacturersRepository manufacturersRepository;

        public CarValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            carsRepository = new CarsRepository(npgsqlConnection);
            manufacturersRepository = new ManufacturersRepository(npgsqlConnection);
        }

        public override void ValidateDelete(string primaryKey)
        {
            Car car = carsRepository.GetById(primaryKey);

            if (car == null)
            {
                throw new CarNotFoundExcpetion(CarNotFoundExcpetion.CarKeyNotFoundMsg, primaryKey);
            }

            if (false)
            {
                throw new CarInUseException(CarInUseException.CarInUseOwnedCarMsg);
            }
        }

        public override void ValidateSave(Car validateEntity)
        {
            if (String.IsNullOrWhiteSpace(validateEntity.Name))
            {
                throw new CarNameNotSetException(CarNameNotSetException.CarNameNotSetMsg);
            }

            if (String.IsNullOrWhiteSpace(validateEntity.ManufacturerKey))
            {
                throw new CarManufacturerKeyNotSetException(CarManufacturerKeyNotSetException.CarManufacturerKeyNotSetMsg);
            }

            ValidateDriveTrain(validateEntity.DriveTrain);
            ValidateAspiration(validateEntity.Aspiration);
            ValidateCategory(validateEntity.Category);

            Car car = carsRepository.GetByName(validateEntity.Name);
            if (car != null)
            {
                if (String.IsNullOrWhiteSpace(validateEntity.PrimaryKey)
                    || car.PrimaryKey != validateEntity.PrimaryKey)
                {
                    throw new CarNameAlreadyExistsException(CarNameAlreadyExistsException.CarNameAlreadyExistsMsg);
                }
            }

            Manufacturer manufacturer = manufacturersRepository.GetById(validateEntity.ManufacturerKey);
            if (manufacturer == null)
            {
                throw new ManufacturerNotFoundException(ManufacturerNotFoundException.ManufacturerKeyNotFoundMsg, validateEntity.ManufacturerKey);
            }
        }

        public void ValidateSearchCriteria(CarSearchCriteria carSearchCriteria)
        {
            if (carSearchCriteria.CategoryFrom != null)
            {
                ValidateCategory(carSearchCriteria.CategoryFrom);
            }

            if (carSearchCriteria.CategoryTo != null)
            {
                ValidateCategory(carSearchCriteria.CategoryTo);
            }

            if (carSearchCriteria.DriveTrain != null)
            {
                ValidateDriveTrain(carSearchCriteria.DriveTrain);
            }
        }

        public void ValidateDriveTrain(string driveTrain)
        {
            if (Array.BinarySearch(DriveTrain.DriveTrains, driveTrain) < 0)
            {
                throw new CarDriveTrainNotValidException(CarDriveTrainNotValidException.CarDriveTrainNotValidMsg, driveTrain);
            }
        }

        public void ValidateAspiration(string aspiration)
        {
            if (Array.BinarySearch(Aspiration.Aspirations, aspiration) < 0)
            {
                throw new CarAspirationNotValidException(CarAspirationNotValidException.CarAspirationNotValidMsg, aspiration);
            } 
        }

        public void ValidateCategory(CarCategory.Category category)
        {
            if (Array.BinarySearch(CarCategory.categories, category) < 0)
            {
                throw new CarCategoryNotValidException(CarCategoryNotValidException.CarCategoryNotValidMsg, category);
            }
        }
    }
}
