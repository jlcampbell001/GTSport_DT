using GTSport_DT.Cars;
using GTSport_DT.General;
using GTSport_DT.Owners;
using Npgsql;
using System;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The owner car validation.</summary>
    /// <seealso cref="GTSport_DT.General.Validation{GTSport_DT.OwnerCars.OwnerCar}"/>
    public class OwnerCarValidation : Validation<OwnerCar>
    {
        private CarsRepository carsRepository;
        private OwnerCarsRepository ownerCarsRepository;
        private OwnersRepository ownersRepository;

        /// <summary>Initializes a new instance of the <see cref="OwnerCarValidation"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public OwnerCarValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            ownersRepository = new OwnersRepository(npgsqlConnection);
            carsRepository = new CarsRepository(npgsqlConnection);
            ownerCarsRepository = new OwnerCarsRepository(npgsqlConnection);
        }

        /// <summary>Validations done to an entity for the passed primary key before it is deleted.</summary>
        /// <param name="primaryKey">The primary key of the entity to delete.</param>
        /// <exception cref="GTSport_DT.OwnerCars.OwnerCarNotFoundException">
        /// Thrown when the owner car can not be found by the primary key.
        /// </exception>
        public override void ValidateDelete(string primaryKey)
        {
            OwnerCar ownerCar = ownerCarsRepository.GetById(primaryKey);

            if (ownerCar == null)
            {
                throw new OwnerCarNotFoundException(OwnerCarNotFoundException.OwnerCarKeyNotFoundMsg, primaryKey);
            }
        }

        /// <summary>Validations done to an entity before it is saved.</summary>
        /// <param name="validateEntity">The entity to validate.</param>
        /// <exception cref="GTSport_DT.OwnerCars.OwnerCarIDNotSetException">
        /// Thrown when the car ID is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.OwnerCars.OwnerCarOwnerKeyNotSetException">
        /// Thrown when the owner key is not filled.
        /// </exception>
        /// <exception cref="GTSport_DT.OwnerCars.OwnerCarCarKeyNotSetException">
        /// Thrown when the car key is not filled.
        /// </exception>
        /// <exception cref="OwnerNotFoundException">
        /// Thrown when the owner key can not be found in the owners table.
        /// </exception>
        /// <exception cref="CarNotFoundException">
        /// Thrown when the car key can not be found in the cars table.
        /// </exception>
        public override void ValidateSave(OwnerCar validateEntity)
        {
            if (String.IsNullOrWhiteSpace(validateEntity.CarID))
            {
                throw new OwnerCarIDNotSetException(OwnerCarIDNotSetException.OwnerCarIDNotSetMsg);
            }

            if (String.IsNullOrWhiteSpace(validateEntity.OwnerKey))
            {
                throw new OwnerCarOwnerKeyNotSetException(OwnerCarOwnerKeyNotSetException.OwnerCarOwnerKeyNotSetMsg);
            }

            if (String.IsNullOrWhiteSpace(validateEntity.CarKey))
            {
                throw new OwnerCarCarKeyNotSetException(OwnerCarCarKeyNotSetException.OwnerCarCarKeyNotSetMsg);
            }

            Owner owner = ownersRepository.GetById(validateEntity.OwnerKey);

            if (owner == null)
            {
                throw new OwnerNotFoundException(OwnerNotFoundException.OwnerKeyNotFoundMsg, validateEntity.OwnerKey);
            }

            Car car = carsRepository.GetById(validateEntity.CarKey);

            if (car == null)
            {
                throw new CarNotFoundException(CarNotFoundException.CarKeyNotFoundMsg, validateEntity.CarKey);
            }
        }
    }
}