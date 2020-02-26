using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The service class for the owner cars.</summary>
    /// <seealso cref="GTSport_DT.General.BackEndService{GTSport_DT.OwnerCars.OwnerCar, GTSport_DT.OwnerCars.OwnerCarsRepository, GTSport_DT.OwnerCars.OwnerCarValidation, GTSport_DT.OwnerCars.OwnerCarNotFoundException}"/>
    public class OwnerCarsService : BackEndService<OwnerCar, OwnerCarsRepository, OwnerCarValidation, OwnerCarNotFoundException>
    {
        /// <summary>Initializes a new instance of the <see cref="OwnerCarsService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public OwnerCarsService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "OWC";
            keyNotFoundMessage = OwnerCarNotFoundException.OwnerCarKeyNotFoundMsg;
        }

        /// <summary>Gets the owner car by car identifier.</summary>
        /// <param name="carID">The car identifier.</param>
        /// <returns>The owner car that is found.</returns>
        /// <exception cref="GTSport_DT.OwnerCars.OwnerCarNotFoundException">
        /// Thrown when the owner car can not be found by the car ID.
        /// </exception>
        public OwnerCar GetByCarID(string carID)
        {
            OwnerCar ownerCar = repository.GetByCarID(carID);

            if (ownerCar == null)
            {
                throw new OwnerCarNotFoundException(OwnerCarNotFoundException.OwnerCarIDNotFoundMsg, carID);
            }

            return ownerCar;
        }

        /// <summary>Gets the list of owner cars for car key.</summary>
        /// <param name="carKey">The car key.</param>
        /// <param name="orderedList">If set to <c>true</c> order list.</param>
        /// <returns>A list of owner cars found for the car key.</returns>
        public List<OwnerCar> GetListForCarKey(string carKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = repository.GetListForForCarKey(carKey, orderedList);

            return ownerCars;
        }

        /// <summary>Gets the list of owner cars for owner key.</summary>
        /// <param name="ownerKey">The owner key.</param>
        /// <param name="orderedList">If set to <c>true</c> order list.</param>
        /// <returns>The list of owner cars for the owner key.</returns>
        public List<OwnerCar> GetListForOwnerKey(string ownerKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = repository.GetListForOwnerKey(ownerKey, orderedList);

            return ownerCars;
        }
    }
}