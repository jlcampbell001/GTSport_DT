using GTSport_DT.General;
using GTSport_DT.Manufacturers;
using GTSport_DT.OwnerCars;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Cars
{
    /// <summary>The validation calls for a car.</summary>
    /// <seealso cref="GTSport_DT.General.Validation{GTSport_DT.Cars.Car}"/>
    public class CarValidation : Validation<Car>
    {
        private CarsRepository carsRepository;
        private ManufacturersRepository manufacturersRepository;
        private OwnerCarsRepository ownerCarsRepository;

        /// <summary>Initializes a new instance of the <see cref="CarValidation"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public CarValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            carsRepository = new CarsRepository(npgsqlConnection);
            manufacturersRepository = new ManufacturersRepository(npgsqlConnection);
            ownerCarsRepository = new OwnerCarsRepository(npgsqlConnection);
        }

        /// <summary>Validates the aspiration.</summary>
        /// <param name="aspiration">The aspiration.</param>
        /// <exception cref="GTSport_DT.Cars.CarAspirationNotValidException">
        /// If the aspiration is not a valid value.
        /// </exception>
        public void ValidateAspiration(string aspiration)
        {
            if (Array.BinarySearch(Aspiration.Aspirations, aspiration) < 0)
            {
                throw new CarAspirationNotValidException(CarAspirationNotValidException.CarAspirationNotValidMsg, aspiration);
            }
        }

        /// <summary>Validates the category.</summary>
        /// <param name="category">The category.</param>
        /// <exception cref="GTSport_DT.Cars.CarCategoryNotValidException">
        /// If the category is not a valid value.
        /// </exception>
        public void ValidateCategory(CarCategory.Category category)
        {
            if (Array.BinarySearch(CarCategory.categories, category) < 0)
            {
                throw new CarCategoryNotValidException(CarCategoryNotValidException.CarCategoryNotValidMsg, category);
            }
        }

        /// <summary>Validations done to an entity for the passed primary key before it is deleted.</summary>
        /// <param name="primaryKey">The primary key of the entity to delete.</param>
        /// <exception cref="GTSport_DT.Cars.CarNotFoundException">
        /// If the car key can not be found to delete.
        /// </exception>
        /// <exception cref="GTSport_DT.Cars.CarInUseException">
        /// If the car is in use for a owned car.
        /// </exception>
        public override void ValidateDelete(string primaryKey)
        {
            Car car = carsRepository.GetById(primaryKey);

            if (car == null)
            {
                throw new CarNotFoundException(CarNotFoundException.CarKeyNotFoundMsg, primaryKey);
            }

            List<OwnerCar> ownerCars = ownerCarsRepository.GetListForForCarKey(primaryKey);

            if (ownerCars.Count > 0)
            {
                throw new CarInUseException(CarInUseException.CarInUseOwnedCarMsg);
            }
        }

        /// <summary>Validates the drive train.</summary>
        /// <param name="driveTrain">The drive train.</param>
        /// <exception cref="GTSport_DT.Cars.CarDriveTrainNotValidException">
        /// If the drive train is not a valid value.
        /// </exception>
        public void ValidateDriveTrain(string driveTrain)
        {
            if (Array.BinarySearch(DriveTrain.DriveTrains, driveTrain) < 0)
            {
                throw new CarDriveTrainNotValidException(CarDriveTrainNotValidException.CarDriveTrainNotValidMsg, driveTrain);
            }
        }

        /// <summary>Validations done to an entity before it is saved.</summary>
        /// <param name="validateEntity">The entity to validate.</param>
        /// <exception cref="GTSport_DT.Cars.CarNameNotSetException">If the name is not filled.</exception>
        /// <exception cref="GTSport_DT.Cars.CarManufacturerKeyNotSetException">
        /// If the manufacturer key is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.Cars.CarNameAlreadyExistsException">
        /// If the name already exists in another car.
        /// </exception>
        /// <exception cref="ManufacturerNotFoundException">
        /// If the manufacturer key can not be found.
        /// </exception>
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

        /// <summary>Validates the search criteria.</summary>
        /// <param name="carSearchCriteria">The car search criteria.</param>
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
    }
}