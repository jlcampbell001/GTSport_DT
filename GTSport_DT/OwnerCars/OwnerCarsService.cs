using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    public class OwnerCarsService : BackEndService<OwnerCar, OwnerCarsRepository, OwnerCarValidation, OwnerCarNotFoundException>
    {
        public OwnerCarsService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "OWC";
            keyNotFoundMessage = OwnerCarNotFoundException.OwnerCarKeyNotFoundMsg;
        }

        public OwnerCar GetByCarID(string carID)
        {
            OwnerCar ownerCar = repository.GetByCarID(carID);

            if (ownerCar == null)
            {
                throw new OwnerCarNotFoundException(OwnerCarNotFoundException.OwnerCarIDNotFoundMsg, carID);
            }

            return ownerCar;
        }

        public List<OwnerCar> GetListForCarKey(string carKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = repository.GetListForForCarKey(carKey, orderedList);

            return ownerCars;
        }

        public List<OwnerCar> GetListForOwnerKey(string ownerKey, Boolean orderedList = false)
        {
            List<OwnerCar> ownerCars = repository.GetListForOwnerKey(ownerKey, orderedList);

            return ownerCars;
        }
    }
}
