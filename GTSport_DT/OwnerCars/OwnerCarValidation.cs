using GTSport_DT.Cars;
using GTSport_DT.General;
using GTSport_DT.Owners;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    public class OwnerCarValidation : Validation<OwnerCar>
    {
        private OwnersRepository ownersRepository;
        private CarsRepository carsRepository;
        private OwnerCarsRepository ownerCarsRepository;

        public OwnerCarValidation(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            ownersRepository = new OwnersRepository(npgsqlConnection);
            carsRepository = new CarsRepository(npgsqlConnection);
            ownerCarsRepository = new OwnerCarsRepository(npgsqlConnection);
        }

        public override void ValidateDelete(string primaryKey)
        {
            OwnerCar ownerCar = ownerCarsRepository.GetById(primaryKey);

            if (ownerCar == null)
            {
                throw new OwnerCarNotFoundException(OwnerCarNotFoundException.OwnerCarKeyNotFoundMsg, primaryKey);
            }
        }

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
