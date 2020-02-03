using GTSport_DT.General;
using Npgsql;
using System;
using System.Collections.Generic;

namespace GTSport_DT.Manufacturers
{
    /// <summary>The service for working the manufacturers.</summary>
    /// <seealso cref="GTSport_DT.General.BackEndService{GTSport_DT.Manufacturers.Manufacturer, GTSport_DT.Manufacturers.ManufacturersRepository, GTSport_DT.Manufacturers.ManufacturerValidation, GTSport_DT.Manufacturers.ManufacturerNotFoundException}"/>
    public class ManufacturersService : BackEndService<Manufacturer, ManufacturersRepository, ManufacturerValidation, ManufacturerNotFoundException>
    {
        /// <summary>Initializes a new instance of the <see cref="ManufacturersService"/> class.</summary>
        /// <param name="npgsqlConnection">The NPGSQL connection.</param>
        public ManufacturersService(NpgsqlConnection npgsqlConnection) : base(npgsqlConnection)
        {
            primaryKeyPrefix = "MAN";
            keyNotFoundMessage = ManufacturerNotFoundException.ManufacturerKeyNotFoundMsg;
        }

        /// <summary>Gets the manufacture with the passed name.</summary>
        /// <param name="name">The name. to look up.</param>
        /// <returns>The manufacturer found.</returns>
        /// <exception cref="GTSport_DT.Manufacturers.ManufacturerNotFoundException">
        /// When the manufacturer can not be found by the passed name.
        /// </exception>
        public Manufacturer GetByName(string name)
        {
            Manufacturer manufacturer = repository.GetByName(name);

            if (manufacturer == null)
            {
                throw new ManufacturerNotFoundException(ManufacturerNotFoundException.ManufacturerNameNotFoundMsg, name);
            }

            return manufacturer;
        }

        /// <summary>Gets the list of manufacturers for country key.</summary>
        /// <param name="countryKey">The country key.</param>
        /// <param name="orderedList">
        /// If set to <c>true</c> the list is ordered by the manufacturer name.
        /// </param>
        /// <returns>The list of manufacturers for the country key.</returns>
        public List<Manufacturer> GetListForCountryKey(string countryKey, Boolean orderedList = false)
        {
            List<Manufacturer> manufacturers = repository.GetListForCountryKey(countryKey, orderedList);

            return manufacturers;
        }
    }
}